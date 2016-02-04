using System;
using VVVV.Utils.OSC;
using System.Collections.Generic;

namespace MadMapper
{
	public class MadMapperResult
	{
		public string Id { get; private set; }

		public List<OSCPacket> Packets { get; private set; }

		public int ResultCount {
			get {
				return resultCounter;
			}
			set {
				resultCounter = value;
			}
		}

		public bool Finished {
			get {
				return resultCounter == Packets.Count;
			}
		}

		volatile int resultCounter = int.MaxValue;

		public MadMapperResult (string id)
		{
			Id = id;
			Packets = new List<OSCPacket> ();
		}
	}
}

