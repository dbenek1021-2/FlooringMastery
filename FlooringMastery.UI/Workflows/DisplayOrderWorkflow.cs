using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
	public class DisplayOrderWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Lookup an order");
			Console.WriteLine(ConsoleIO.LineBar);
			Console.Write("Enter the order date: (MMDDYYYY): ");
			string orderDate = Console.ReadLine();

			DisplayOrderResponse response = manager.DisplayOrders(orderDate);
			Console.Clear();

			if (response.Success)
			{
				var orders = response.Orders;
				foreach(var order in orders)
				{
					ConsoleIO.DisplayOrderDetails(order);
				}
			}
			else
			{
				Console.WriteLine("An error occurred: ");
				Console.WriteLine(response.Message);
			}

			Console.WriteLine("Press any key to return to the main menu...");
			Console.ReadKey();
		}
	}
}
