using NUnit.Framework;
using Opten.Common.Ip;

namespace Opten.Common.Test.Ip
{

	[TestFixture]
	public class IpAddressTests
	{

		[Test]
		public void Is_Ip_Address()
		{
			Assert.That(IpAddress.IsIpAddress("192.168.167.10"), Is.True);
			Assert.That(IpAddress.IsIpAddress("NO_IP"), Is.False);
		}

		[Test]
		public void Is_Ip_Address_In_Range()
		{
			Assert.That(IpAddress.IsIpAddressInRange("192.168.167.0", "192.168.167.255", "192.168.167.100"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRange("192.168.167.0", "192.168.167.100", "192.168.167.100"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRange("192.168.167.0", "192.168.167.255", "192.10.10.100"), Is.False);
			Assert.That(IpAddress.IsIpAddressInRange("192.168.167.0", "192.168.167.100", "10.10.10.10"), Is.False);
		}

		[Test]
		public void Is_Ip_Address_In_Range_From_Subnet()
		{
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.167.10", "192.168.167.0", "255.255.255.0"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.167.240", "192.168.167.0", "255.255.255.0"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.168.10", "192.168.167.0", "255.255.255.0"), Is.False);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.168.10", "192.168.167.0", "255.255.255.0"), Is.False);

			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.167.10", "192.168.167.0/24"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.167.240", "192.168.167.0/24"), Is.True);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.168.10", "192.168.167.0/24"), Is.False);
			Assert.That(IpAddress.IsIpAddressInRangeFromSubnet("192.168.168.10", "192.168.167.0/24"), Is.False);
		}

	}
}
