using System;
using VVVV.Utils.OSC;
using System.Threading.Tasks;

namespace MMApiExamples
{
	public class MadMapperCommunicator
	{
		/// <summary>
		/// Occurs when osc packet from MadMapper received.
		/// </summary>
		public event EventHandler<OSCPacket> PacketReceived;

		/// <summary>
		/// Gets a value indicating whether this <see cref="MMApiExamples.MadMapperCommunicator"/> is connected.
		/// </summary>
		/// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
		public bool Connected { get { return connected; } }

		private OSCReceiver server;

		private OSCTransmitter client;

		private volatile bool connected;

		/// <summary>
		/// Initializes a new instance of the <see cref="MMApiExamples.MadMapperCommunicator"/> class.
		/// </summary>
		/// <param name="remoteHost">Remote host address.</param>
		/// <param name="portNumber">Port number of remote host.</param>
		public MadMapperCommunicator (string remoteHost, int portNumber)
		{
			client = new OSCTransmitter (remoteHost, portNumber);
		}

		/// <summary>
		/// Connect the client and server.
		/// </summary>
		/// <param name="portNumber">Port number to listen on.</param>
		public void Connect (int portNumber)
		{
			connected = true;
			client.Connect ();

			server = new OSCReceiver (portNumber);
			server.Connect ();

			//server loop
			Task.Run (() => {
				while (connected) {
					var packet = server.Receive ();

					if (PacketReceived != null)
						PacketReceived (this, packet);
				}
				server.Close ();
			});
		}

		/// <summary>
		/// Closes the connection.
		/// </summary>
		public void Close ()
		{
			connected = false;
			client.Close ();
		}

		/// <summary>
		/// Sends a package to MadMapper.
		/// </summary>
		/// <param name="packet">Packet to send.</param>
		public void Send (OSCPacket packet)
		{
			client.Send (packet);
		}
	}
}

