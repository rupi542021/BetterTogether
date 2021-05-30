using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;
using BetterTogetherProj.Models.DAL;

namespace BetterTogetherProj.Models
{
    public class Program
    {
		public Program()
		{
			// timer to call MyMethod() every minutes 
			System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
			timer.AutoReset = true;
			timer.Elapsed += new System.Timers.ElapsedEventHandler(MyMethodCall);
			timer.Start();
		}

		public static void MyMethodCall(object sender, ElapsedEventArgs e)
		{
			Console.WriteLine("I will be call every minutes !!!");
		}


	}
}





