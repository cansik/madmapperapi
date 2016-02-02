using System;
using VVVV.Utils.OSC;
using System.Threading.Tasks;

namespace MMApiExamples
{
	class MainClass
	{
		const string REMOTE_ADDRESS = "127.0.0.1";
		const int REMOTE_PORT = 8000;
		const int LOCAL_PORT = 9000;

		static MadMapperCommunicator communicator;

		public static void Main (string[] args)
		{
			Console.WriteLine ("OSC Communicator Example");

			// create new communicator
			communicator = new MadMapperCommunicator (REMOTE_ADDRESS, REMOTE_PORT);

			// add event handler for receiving packages
			communicator.PacketReceived += Communicator_PacketReceived;

			// connect to madmapper
			communicator.Connect (LOCAL_PORT);

			// run example 1
			ReceiveSurfaces ();

			// run example 2
			SetValueOfSurface ();

			// run example 3
			ReceiveValueFromSurface ();

			// closes the connection to madmapper
			communicator.Close ();
		}

		static void Communicator_PacketReceived (object sender, OSCPacket e)
		{
			var bundle = e as OSCBundle;
		
			if (bundle != null) {
				foreach (OSCMessage m in bundle.Values) {
					Console.Write (m.Address + " ");
					Console.WriteLine (String.Join (", ", m.Values.ToArray ()));
				}	
			}
		}

		/// <summary>
		/// Example 1
		/// Receives the surfaces.
		/// </summary>
		static void ReceiveSurfaces ()
		{
			communicator.Send (new OSCMessage ("/getControls?root=/surfaces&recursive=0"));
		}

		/// <summary>
		/// Example 2
		/// Set value of surface
		/// </summary>
		static void SetValueOfSurface ()
		{
			var msg = new OSCMessage ("/surfaces/Quad 1/visible");
			msg.Append (false);
			communicator.Send (msg);
		}

		/// <summary>
		/// Example 3
		/// Receive value from surface
		/// </summary>
		static void ReceiveValueFromSurface ()
		{
			communicator.Send (new OSCMessage ("/getControlValues?url=/surfaces/Quad 1/visible&normalized=0"));
		}
	}
}
