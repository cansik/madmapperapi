using System;
using System.Linq;

namespace MadMapper
{
	public class OSCAddress
	{
		static char ADDRESS_SEPARATOR = '/';

		public string Address { get; private set; }

		String[] parts;

		public OSCAddress (string address)
		{
			Address = address;
			parts = address.Split (new char[] { ADDRESS_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
		}

		public string this [int i] {
			get {
				return parts [i];
			}
		}

		public string Id {
			get {
				return parts [0];
			}
		}

		public string Root {
			get {
				return parts [1];
			}
		}

		public string BaseAddress {
			get {
				return String.Concat (ADDRESS_SEPARATOR, String.Join (ADDRESS_SEPARATOR.ToString (), parts.Skip (1)));
			}
		}
	}
}

