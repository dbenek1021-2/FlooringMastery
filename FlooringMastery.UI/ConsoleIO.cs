using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI
{
	public class ConsoleIO
	{
		public const string LineBar = "***************************************************";

		public static void DisplayOrderDetails(Order order)
		{
			TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
			string format = "{0,-15} {1,15}";

			Console.WriteLine(LineBar);
			Console.WriteLine($"Order #: {order.OrderNumber} | Order Date: {order.ConvertToDate(order.OrderDate).ToShortDateString()}");
			Console.WriteLine(format, "Customer name: ", $"{myTI.ToTitleCase(order.CustomerName)}");
			Console.WriteLine(format, "Location: ", $"{order.State.ToUpper()}");
			Console.WriteLine(format, "Product: ", $"{myTI.ToTitleCase(order.ProductType)}");
			Console.WriteLine(format, "Materials: ", $"{order.MaterialCost:c}");
			Console.WriteLine(format, "Labor: ", $"{order.LaborCost:c}");
			Console.WriteLine(format, "Tax: ", $"{order.Tax:c}");
			Console.WriteLine(format, "Total: ", $"{order.Total:c}");
			Console.WriteLine($"{LineBar}\n");
		}
	}
}
