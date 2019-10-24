using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Data.Repositories;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using NUnit.Framework;
using System.Configuration;
using System.IO;

namespace FlooringMastery.Tests
{
	[TestFixture]
	public class TestModeTests
	{
		[TestCase("06062019")]
		public void CanDisplayOrder(string orderDate)
		{
			OrderManager manager = OrderManagerFactory.Create();

			DisplayOrderResponse response = manager.DisplayOrders(orderDate);

			Assert.IsNotNull(response.Order);
			Assert.IsTrue(response.Success);
			Assert.AreEqual(orderDate, response.Order.OrderNumber);
		}

		[TestCase("06062019", 1, "Acme", "OH", "6.25", "Wood", "100", "5.15", "4.75", "515.00", "475.00", "61.88", "1051.88", true)]
		public void CanAddOrder(string orderDate, int orderNumber, string customerName, string state, decimal taxRate, string product,
								decimal area, decimal costPerFt, decimal laborPerFt, decimal matCost, decimal laborCost, decimal tax,
								decimal total, bool expectedResult)

		{
			OrderManager manager = OrderManagerFactory.Create();
			Order newOrder = new Order();
			newOrder.OrderDate = orderDate;
			newOrder.OrderNumber = orderNumber;
			newOrder.CustomerName = customerName;
			newOrder.State = state;
			newOrder.TaxRate = taxRate;
			newOrder.ProductType = product;
			newOrder.Area = area;
			newOrder.CostPerSquareFoot = costPerFt;
			newOrder.LaborCostPerSqareFoot = laborPerFt;
			newOrder.MaterialCost = matCost;
			newOrder.LaborCost = laborCost;
			newOrder.Tax = tax;
			newOrder.Total = total;

			AddOrderResponse response = manager.AddOrder(newOrder, orderDate);

			Assert.IsNotNull(response.NewOrder);
			Assert.IsTrue(response.Success);
			Assert.AreEqual(orderNumber, expectedResult);
		}

		[TestCase("06062019", 1, "Acme", "OH", 6.25, "Wood", 100, 5.15, 4.75, 515.00, 475.00, 61.88, 1051.88, true)]
		public void CanEditOrder(string orderDate, int orderNumber, string customerName, string state, decimal taxRate, string product,
								decimal area, decimal costPerFt, decimal laborPerFt, decimal matCost, decimal laborCost, decimal tax,
								decimal total, bool expectedResult)

		{
			OrderManager manager = OrderManagerFactory.Create();
			Order editOrder = new Order();
			editOrder.OrderDate = orderDate;
			editOrder.OrderNumber = orderNumber;
			editOrder.CustomerName = customerName;
			editOrder.State = state;
			editOrder.TaxRate = taxRate;
			editOrder.ProductType = product;
			editOrder.Area = area;
			editOrder.CostPerSquareFoot = costPerFt;
			editOrder.LaborCostPerSqareFoot = laborPerFt;
			editOrder.MaterialCost = matCost;
			editOrder.LaborCost = laborCost;
			editOrder.Tax = tax;
			editOrder.Total = total;

			EditOrderResponse response = manager.EditOrder(orderDate, orderNumber);

			Assert.IsNotNull(response.Order);
			Assert.AreEqual(response.Success, expectedResult);
		}

		[TestCase("Laminate", 1.75, 2.10, true)]
		public void LoadProduct(string productType, decimal costPerFt, decimal laborPerFt, bool expectedResult)
		{
			ProductRepository repo = new ProductRepository();
			Product product = new Product();

			product.ProductType = productType;
			product.CostPerSquareFoot = costPerFt;
			product.LaborCostPerSqareFoot = laborPerFt;

			Product actual = repo.LoadProduct(productType);

			Assert.AreEqual(productType, actual.ProductType);
			Assert.AreEqual(costPerFt, actual.CostPerSquareFoot);
			Assert.AreEqual(laborPerFt, actual.LaborCostPerSqareFoot);
		}

		[TestCase("OH", "Ohio", 6.25, true)]
		public void LoadTaxes(string stateAbb, string stateName, decimal taxRate, bool expectedResult)
		{
			TaxRepository repo = new TaxRepository();
			Taxes tax = new Taxes();

			tax.StateAbbreviation = stateAbb;
			tax.StateName = stateName;
			tax.TaxRate = taxRate;

			Taxes actual = repo.LoadTaxes(stateAbb);

			Assert.AreEqual(stateAbb, actual.StateAbbreviation);
			Assert.AreEqual(stateName, actual.StateName);
			Assert.AreEqual(taxRate, actual.TaxRate);
		}
	}
}
