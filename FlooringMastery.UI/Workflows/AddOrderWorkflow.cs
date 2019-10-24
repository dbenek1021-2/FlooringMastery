using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using FlooringMastery.UI.Validate;

namespace FlooringMastery.UI.Workflows
{
	public class AddOrderWorkflow
	{
		public void Execute()
		{
			OrderManager orderManager = OrderManagerFactory.Create();
			ProductManager productManager = ProductManagerFactory.Create();
			TaxManager taxManager = TaxManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Place an order");
			Console.WriteLine(ConsoleIO.LineBar);
			Console.WriteLine();
			Console.WriteLine("Press any key to start your order.");
			Console.ReadKey();
			Console.Clear();

			Order newOrder = new Order();

			newOrder.OrderDate = AddOrderCheck.GetDate("Enter a future order date (MMDDYYYY): ");
			newOrder.CustomerName = AddOrderCheck.GetName("Enter customer name: ");
			newOrder.State = AddOrderCheck.GetState("Enter your State location (ex. OH for Ohio): ");
			newOrder.ProductType = AddOrderCheck.GetProduct("Enter product you would like to order: ");
			newOrder.Area = decimal.Parse(AddOrderCheck.GetArea("Enter area amount you would like to order (minimum of 100sq ft): "));

			var productLookup = productManager.ReturnProduct(newOrder.ProductType);
			newOrder.CostPerSquareFoot = productLookup.CostPerSquareFoot;
			newOrder.LaborCostPerSqareFoot = productLookup.LaborCostPerSqareFoot;

			var taxesLookup = taxManager.LoadTax(newOrder.State);
			newOrder.TaxRate = taxesLookup.TaxRate;

			newOrder.MaterialCost = newOrder.MaterialCostCalc(newOrder.Area, newOrder.CostPerSquareFoot);
			newOrder.LaborCost = newOrder.LaborCostCalc(newOrder.Area, newOrder.LaborCostPerSqareFoot);
			newOrder.Tax = newOrder.TaxCalc(newOrder.MaterialCost, newOrder.LaborCost, newOrder.TaxRate);
			newOrder.Total = newOrder.TotalCalc(newOrder.MaterialCost, newOrder.LaborCost, newOrder.Tax);

			AddOrderResponse response = orderManager.AddOrder(newOrder, newOrder.OrderDate);
			Console.Clear();

			if (response.Success)
			{
				ConsoleIO.DisplayOrderDetails(response.NewOrder);
				bool keepLooping = true;

				do
				{
					Console.WriteLine();
					Console.WriteLine("Would you like to save your new order? Y for Yes or N for No: ");
					string input = Console.ReadLine();
					if (input.ToUpper() == "Y" || input.ToUpper() == "Yes")
					{
						Console.WriteLine();
						Console.WriteLine("Your order has been placed.");
						keepLooping = false;
					}
					else if (input.ToUpper() == "N" || input.ToUpper() == "No")
					{
						orderManager.RemoveOrder(newOrder, newOrder.OrderDate, newOrder.OrderNumber);
						Console.WriteLine();
						Console.WriteLine("Your order has been cancelled.");
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
				Console.WriteLine();
				Console.WriteLine("An error occurred: ");
				Console.WriteLine(response.Message);
			}
			Console.WriteLine();
			Console.WriteLine("Press any key to return to the main menu...");
			Console.ReadKey();
		}
	}
}
