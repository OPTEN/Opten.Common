using System;
using System.Linq;
using System.Net;
using System.Web;

namespace Opten.Common.Ip
{
	/// <summary>
	/// The IP Address Helper.
	/// </summary>
	public static class IpAddress
	{

		/// <summary>
		/// Gets the IP Address.
		/// </summary>
		/// <returns></returns>
		public static string GetIpAddress()
		{
			HttpContext httpContext = HttpContext.Current;

			if (httpContext != null)
			{
				string ipAddress = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

				if (string.IsNullOrEmpty(ipAddress) == false)
				{
					string[] addresses = ipAddress.Split(',');
					if (addresses.Length != 0)
					{
						return addresses[0];
					}
				}

				return httpContext.Request.ServerVariables["REMOTE_ADDR"];
			}

			return "IP Address not known";
		}

		/// <summary>
		/// Gets the server IP Address.
		/// </summary>
		/// <returns></returns>
		public static string[] GetServerIpAddress()
		{
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			//IPAddress ipAddress = ipHostInfo.AddressList[0];

			return ipHostInfo.AddressList.Select(o => o.ToString()).ToArray();
		}

		/// <summary>
		/// Determines whether [is IP Address] [the specified IP Address].
		/// </summary>
		/// <param name="ipAddress">The IP Address.</param>
		/// <returns></returns>
		public static bool IsIpAddress(string ipAddress)
		{
			IPAddress address;
			return IPAddress.TryParse(ipAddress, out address) && address != null;
		}

		/// <summary>
		/// Determines whether [is IP Address in range from subnet] [the specified IP Address].
		/// </summary>
		/// <param name="ipAddress">The IP Address.</param>
		/// <param name="ipSubnetStartAddress">The ip subnet start address.</param>
		/// <param name="subnetMask">The subnet mask.</param>
		/// <returns></returns>
		public static bool IsIpAddressInRangeFromSubnet(string ipAddress, string ipSubnetStartAddress, string subnetMask)
		{
			if (string.IsNullOrEmpty(ipAddress)) return false;

			IPAddress ip = IPAddress.Parse(ipAddress);
			IPAddress subnetAddress = IPAddress.Parse(ipSubnetStartAddress);
			IPAddress subnet = IPAddress.Parse(subnetMask);

			IPAddress network1 = subnetAddress.GetNetworkAddress(subnet);
			IPAddress network2 = ip.GetNetworkAddress(subnet);

			if (network1 == null || network1 == null) return false;

			return network1.Equals(network2);
		}

		/// <summary>
		/// Determines whether [is IP Address in range from subnet] [the specified IP Address].
		/// </summary>
		/// <param name="ipAddress">The IP Address.</param>
		/// <param name="ipAddressWithNetmask">The IP Address with netmask.</param>
		/// <returns>
		///   <c>true</c> if [is IP Address in range from subnet] [the specified IP Address]; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="InvalidOperationException">The IP Address have to contains the net part. E.g. 192.168.167.0/24</exception>
		public static bool IsIpAddressInRangeFromSubnet(string ipAddress, string ipAddressWithNetmask)
		{
			if (ipAddressWithNetmask.Contains("/") == false)
			{
				throw new InvalidOperationException("The IP Address have to contains the Netmask. E.g. 192.168.167.0/24");
			}

			IPAddress ip = IPAddress.Parse(ipAddress);
			IPAddress subnetAddress = IPAddress.Parse(ipAddressWithNetmask.Split('/')[0]);
			IPAddress subnet = SubnetMask.CreateByNetmask(int.Parse(ipAddressWithNetmask.Split('/')[1]));

			//TODO: Way w/o converting ToString()? --> Overload with actual IPAddress class
			return IsIpAddressInRangeFromSubnet(ip.ToString(), subnetAddress.ToString(), subnet.ToString());
		}

		/// <summary>
		/// Determines whether [is IP Address in range from subnet] [the specified IP Address].
		/// </summary>
		/// <param name="ipAddress">The IP Address.</param>
		/// <param name="ipAddresses">The IP Addresses.</param>
		/// <param name="subnetMasks">The subnet masks.</param>
		/// <returns></returns>
		public static bool IsIpAddressInRangeFromSubnet(string ipAddress, string[] ipAddresses, string[] subnetMasks)
		{
			if (string.IsNullOrEmpty(ipAddress)) return false;

			int ipAddressesCount = ipAddresses.Count(),
				subnetMasksCount = subnetMasks.Count();

			if (ipAddressesCount == 0 || subnetMasksCount == 0) return false;

			for (int i = 0; i < ipAddressesCount; i++)
			{
				// there is no subnetmask found for this ip
				if (subnetMasksCount < (i + 1)) continue;

				if (IsIpAddressInRangeFromSubnet(ipAddress, ipAddresses[i], subnetMasks[i]))
					return true;
			}

			return false;
		}
		
		/// <summary>
		/// Determines whether [is IP Address in range] [the specified lower and upper IP Address].
		/// </summary>
		/// <param name="ipAddressLower">The IP Address lower.</param>
		/// <param name="ipAddressUpper">The IP Address upper.</param>
		/// <param name="ipAddress">The IP Address.</param>
		/// <returns></returns>
		public static bool IsIpAddressInRange(string ipAddressLower, string ipAddressUpper, string ipAddress)
		{
			IPAddress lower = IPAddress.Parse(ipAddressLower),
					  upper = IPAddress.Parse(ipAddressUpper),
					  ip = IPAddress.Parse(ipAddress);

			if (lower == null || upper == null || ip == null) return false;

			if (lower.AddressFamily != ip.AddressFamily) return false;

			byte[] ipBytes = ip.GetAddressBytes(),
				   lowerBytes = lower.GetAddressBytes(),
				   upperBytes = upper.GetAddressBytes();

			bool lowerBoundary = true,
				 upperBoundary = true;

			for (int i = 0; i < lowerBytes.Length && (lowerBoundary || upperBoundary); i++)
			{
				if ((lowerBoundary && ipBytes[i] < lowerBytes[i])
					||
					(upperBoundary && ipBytes[i] > upperBytes[i])) return false;

				lowerBoundary &= (ipBytes[i] == lowerBytes[i]);
				upperBoundary &= (ipBytes[i] == upperBytes[i]);
			}

			return true;
		}

		private static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
		{
			byte[] ipAddressBytes = address.GetAddressBytes();
			byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

			if (ipAddressBytes.Length != subnetMaskBytes.Length)
				return null;

			byte[] broadcastAddress = new byte[ipAddressBytes.Length];
			for (int i = 0; i < broadcastAddress.Length; i++)
			{
				broadcastAddress[i] = (byte)(ipAddressBytes[i] & (subnetMaskBytes[i]));
			}
			return new IPAddress(broadcastAddress);
		}
	}
}