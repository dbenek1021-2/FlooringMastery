using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data
{
	public class OrderRepository : IOrderRepository
	{
		private string _orderPath = ConfigurationManager.AppSettings["OrderFile"].ToString();

		public List<Order> ReadOrders(string date)
		{
			List<Order> orders = new List<Order>();
			string orderFilename = "Orders_" + date + ".txt";
			var correctFileToRead = _orderPath + "\\" + orderFilename;

			if (File.Exists(correctFileToRead))
			{
				string[] allLines = File.ReadAllLines(correctFileToRead);

				for (int i = 1; i < allLines.Length; i++)
				{
					Order order = new Order();

					string[] filelines = allLines[i].Split(',');

					order.OrderDate = date;
					order.OrderNumber = int.Parse(filelines[0]);
					order.CustomerName = filelines[1];
					order.State = filelines[2];
					order.TaxRate = decimal.Parse(filelines[3]);
					order.ProductType = filelines[4];
					order.Area = decimal.Parse(filelines[5]);
					order.CostPerSquareFoot = decimal.Parse(filelines[6]);
					order.LaborCostPerSqareFoot = decimal.Parse(filelines[7]);
					order.MaterialCost = decimal.Parse(filelines[8]);
					order.LaborCost = decimal.Parse(filelines[9]);
					order.Tax = decimal.Parse(filelines[10]);
					order.Total = decimal.Parse(filelines[11]);

					orders.Add(order);
				}
			}
			return orders;
		}

		public void OverwriteFile(List<Order> order, string date)
		{
			ExportNewOrderToFile(order, date);
		}

		private void ExportNewOrderToFile(List<Order> order, string date)
		{
			string orderFilename = "Orders_" + date + ".txt";
			var correctFileToSave = _orderPath + "\\" + orderFilename;

			if (File.Exists(correctFileToSave))
			{
				File.Delete(correctFileToSave);
			}

			using (StreamWriter sr = new StreamWriter(correctFileToSave))
			{
				sr.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
				foreach (var newOrder in order)
				{
					sr.WriteLine(LineFormatForOrderFiles(newOrder));
				}
			}
		}

		private string LineFormatForOrderFiles(Order order)
		{
			TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

			return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber,
				myTI.ToTitleCase(order.CustomerName),
				order.State.ToUpper(),
				order.TaxRate,
				myTI.ToTitleCase(order.ProductType),
				Math.Round(order.Area, 2),
				Math.Round(order.CostPerSquareFoot, 2),
				Math.Round(order.LaborCostPerSqareFoot, 2),
				Math.Round(order.MaterialCost, 2),
				Math.Round(order.LaborCost, 2),
				Math.Round(order.Tax, 2),
				Math.Round(order.Total, 2));
		}
	}
}
