using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace task_debugging
{
	class Program
	{
		static void Main(string[] args)
		{
			string key = Generate();
			Console.WriteLine(key);
		}

		static string Generate()
		{
			var networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
			byte[] byteArray = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());
			if (networkInterface == null)
			{
				throw new Exception("Something happened");
			}
			int[] addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes().Select((x, y) => x ^ byteArray[y])
					  .Select(a =>
					  {
						  if (a > 999)
						  {
							  return a;
						  }
						  return a * 10;
					  }).ToArray();

			return string.Join("-", addressBytes);
		}
	}
}
