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
	public class EditOrderWorkflow
	{
		public void Execute()
		{
			OrderManager orderManager = OrderManagerFactory.Create();
			ProductManager productManager = ProductManagerFactory.Create();
			TaxManager taxManager = TaxManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Edit an order");
			Console.WriteLine(ConsoleIO.LineBar);
			Console.WriteLine();
			Console.WriteLine("Press any key to edit an order.");
			Console.ReadKey();
			Console.Clear();

			Order editOrder = new Order();

			editOrder.OrderDate = EditOrderCheck.GetDate("Enter the order date (MMDDYYYY): ");
			editOrder.OrderNumber = int.Parse(EditOrderCheck.GetOrderNumber("Enter the order number you would like to edit: "));

			EditOrderResponse response = orderManager.EditOrder(editOrder.OrderDate, editOrder.OrderNumber);
			Console.Clear();

			if (response.Success && response.Order != null)
			{
				editOrder.CustomerName = EditOrderCheck.GetName($"Enter customer name ({response.Order.CustomerName}): ", response.Order.CustomerName);
				editOrder.State = EditOrderCheck.GetState($"Enter your state location ({response.Order.State}): ", response.Order.State);
				editOrder.ProductType = EditOrderCheck.GetProduct($"Enter product you would like to order ({response.Order.ProductType}): ", response.Order.ProductType);
				editOrder.Area = decimal.Parse(EditOrderCheck.GetArea($"Enter area amount you would like to order (minimum of 100sq ft) ({response.Order.Area}sq ft): ", response.Order.Area));

				var productLookup = productManager.ReturnProduct(editOrder.ProductType);
				editOrder.CostPerSquareFoot = productLookup.CostPerSquareFoot;
				editOrder.LaborCostPerSqareFoot = productLookup.LaborCostPerSqareFoot;

				var taxesLookup = taxManager.LoadTax(editOrder.State);
				editOrder.TaxRate = taxesLookup.TaxRate;

				editOrder.MaterialCost = editOrder.MaterialCostCalc(editOrder.Area, editOrder.CostPerSquareFoot);
				editOrder.LaborCost = editOrder.LaborCostCalc(editOrder.Area, editOrder.LaborCostPerSqareFoot);
				editOrder.Tax = editOrder.TaxCalc(editOrder.MaterialCost, editOrder.LaborCost, editOrder.TaxRate);
				editOrder.Total = editOrder.TotalCalc(editOrder.MaterialCost, editOrder.LaborCost, editOrder.Tax);
			}

			if (response.Order == null || response.Orders.Count() == 0)
			{
				Console.WriteLine("An error occurred: ");
				Console.WriteLine($"Order date {editOrder.OrderDate} and / or Order # {editOrder.OrderNumber} does not exist.");
				Console.WriteLine();
				Console.WriteLine("Press any key to return to the main menu...");
				Console.ReadKey();
				Console.Clear();
				return;
			}

			Console.Clear();
			ConsoleIO.DisplayOrderDetails(editOrder);
			bool keepLooping = true;

			do
			{
				Console.WriteLine();
				Console.WriteLine("Would you like to save your edited order? Y for Yes or N for No: ");
				string input = Console.ReadLine();
				if (input.ToUpper() == "Y" || input.ToUpper() == "Yes")
				{
					orderManager.ExportEditOrder(response.Orders, editOrder, editOrder.OrderDate, editOrder.OrderNumber);

					Console.WriteLine();
					Console.WriteLine("Your order has been edited.");
					keepLooping = false;
				}
				else if (input.ToUpper() == "N" || input.ToUpper() == "No")
				{
					Console.WriteLine();
					Console.WriteLine("No edits were made to your order.");
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

			Console.WriteLine();
			Console.WriteLine("Press any key to return to the main menu...");
			Console.ReadKey();
		}
	}
}