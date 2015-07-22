using System;
using System.ServiceModel;

namespace TrollDetector
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (ServiceHost serviceHost = new ServiceHost(typeof(TrollDetectorService)))
			{
				try
				{
					serviceHost.Open();

					Console.WriteLine("Service is running.");
					Console.WriteLine("Press ENTER to stop service.");
					Console.ReadLine();

					serviceHost.Close();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.ReadLine();
				}
			}
		}
	}
}
