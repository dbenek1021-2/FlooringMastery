using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
	public class RemoveOrderWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Remove an order");
			Console.WriteLine(ConsoleIO.LineBar);
			Console.WriteLine();
			Console.WriteLine("Press any key to remove an order.");
			Console.ReadKey();
			Console.Clear();

			Console.Write("Enter the order date of the order you want to remove: (MMDDYYYY): ");
			string orderDate = Console.ReadLine();
			Console.Clear();
			Console.Write("Enter the order number: ");
			int orderNumber = int.Parse(Console.ReadLine());

			RemoveOrderResponse response = manager.GetOrderToRemove(orderDate, orderNumber);
			Console.Clear();

			if (response.Success)
			{
				var order = response.Order;
				bool keepLooping = true;
				ConsoleIO.DisplayOrderDetails(order);
				Console.WriteLine();
				do
				{
					Console.WriteLine("Would you like to delete this order? Y for Yes or N for No: ");
					string input = Console.ReadLine();

					if (input.ToUpper() == "Y" || input.ToUpper() == "Yes")
					{
						manager.RemoveOrder(order, orderDate, orderNumber);
						Console.WriteLine();
						Console.WriteLine("Your order has been removed.");
						keepLooping = false;

					}
					else if (input.ToUpper() == "N" || input.ToUpper() == "No")
					{
						Console.WriteLine();
						Console.WriteLine("Your request has been cancelled.");
						keepLooping = false;
					}
					else
					{
						Console.WriteLine();
						Console.WriteLine("You pressed an incorrect key. Press enter to try again...");
						Console.ReadKey();
						keepLooping = true;
					}
				} while (keepLooping == true);
			}
			else
			{
				Console.WriteLine("An error occurred: ");
				Console.WriteLine(response.Message);
			}
			Console.WriteLine();
			Console.WriteLine("Press any key to return to the main menu...");
			Console.ReadKey();
		}
	}
}