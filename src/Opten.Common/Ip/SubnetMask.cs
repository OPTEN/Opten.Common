using System;
using System.Net;

namespace Opten.Common.Ip
{
	/// <summary>
	/// The Subnet Mask Helper (e.g. 192.168.167.0/24 --> 255.255.255.0).
	/// </summary>
	public static class SubnetMask
	{

		/// <summary>
		/// Creates the Subnet Mask by Hostmask.
		/// </summary>
		/// <param name="hostPartLength">Length of the host part.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Number of hosts is to large for IPv4</exception>
		public static IPAddress CreateByHostmask(int hostPartLength)
		{
			int netPartLength = 32 - hostPartLength;

			if (netPartLength < 2)
				throw new ArgumentException("Number of hosts is to large for IPv4");

			Byte[] binaryMask = new byte[4];

			for (int i = 0; i < 4; i++)
			{
				if (i * 8 + 8 <= netPartLength)
				{
					binaryMask[i] = (byte)255;
				}
				else if (i * 8 > netPartLength)
				{
					binaryMask[i] = (byte)0;
				}
				else
				{
					int oneLength = netPartLength - i * 8;
					string binaryDigit = String.Empty.PadLeft(oneLength, '1').PadRight(8, '0');
					binaryMask[i] = Convert.ToByte(binaryDigit, 2);
				}
			}

			return new IPAddress(binaryMask);
		}

		/// <summary>
		/// Creates the Subnet Mask by Netmask.
		/// </summary>
		/// <param name="netPartLength">Length of the net part.</param>
		/// <returns></returns>
		public static IPAddress CreateByNetmask(int netPartLength)
		{
			int hostpartLength = 32 - netPartLength;

			return CreateByHostmask(hostpartLength);
		}

	}
}