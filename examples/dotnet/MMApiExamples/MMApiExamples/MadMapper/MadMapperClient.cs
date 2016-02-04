using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using VVVV.Utils.OSC;
using System.Collections.Concurrent;
using System.Threading;

namespace MadMapper
{
	public class MadMapperClient : MadMapperCommunicator
	{
		static int safeMessageCount = 0;

		public string Name { get; set; }

		ManualResetEvent newMessageArrived = new ManualResetEvent (false);

		ConcurrentDictionary<string, MadMapperResult> results;

		public MadMapperClient (string name, string remoteHost, int portNumber) : base (remoteHost, portNumber)
		{
			Name = name;
			results = new ConcurrentDictionary<string, MadMapperResult> ();
			PacketReceived += MadMapperClient_PacketReceived;
		}

		void MadMapperClient_PacketReceived (object sender, OSCPacket e)
		{
			var msg = e as OSCMessage;

			if (msg != null) {

				var address = new OSCAddress (e.Address);

				// get identifier
				var id = address.Id;

				// return if message is not relevant
				if (!results.ContainsKey (id))
					return;

				// check if this is the replayMessageCount address
				if (address.Root.Equals ("replyMessageCount")) {
					// set result count
					results [id].ResultCount = (int)msg.Values [0];
				} else {
					// add msg to result
					msg.Address = address.BaseAddress;
					results [id].Packets.Add (msg);
				}

				// fire lock to let waiters check
				newMessageArrived.Set ();
			}
		}

		public async Task<List<OSCPacket>> Call (OSCPacket p)
		{
			// generate unique id
			var currentMessageId = Interlocked.Increment (ref safeMessageCount);
			var id = String.Concat (Name, "_", currentMessageId);

			// add result object
			var result = new MadMapperResult (id);
			while (!results.TryAdd (id, result)) {
			}

			// add id to message as parameter
			p.Address += "&id=" + id;

			// send message
			Send (p);

			// wait for all messages
			await Task.Run (() => {
				while (!result.Finished) {
					newMessageArrived.WaitOne ();
					newMessageArrived.Reset ();
				}
			});

			// remove result object
			MadMapperResult removedObject;

			while (!results.TryRemove (id, out removedObject)) {
			}

			return result.Packets;
		}
	}
}

