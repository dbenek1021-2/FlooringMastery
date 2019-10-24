using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Data.Repositories;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
	public class ProdModeTests
	{
		private string _orderPath = ConfigurationManager.AppSettings["OrderFolder"].ToString();
		private string _pathForTest = ConfigurationManager.AppSettings["OrderFile"].ToString();
		private string _seedForTest = ConfigurationManager.AppSettings["SeedFile"].ToString();
		private string _productPath = ConfigurationManager.AppSettings["ProductFile"].ToString();
		private string _taxPath = ConfigurationManager.AppSettings["TaxFile"].ToString();

		[SetUp]
		public void Setup()
		{
			if (File.Exists(_pathForTest))
			{
				File.Delete(_pathForTest);
			}

			File.Copy(_seedForTest, _pathForTest);
		}

		[Test]
		public void CanDisplayOrder()
		{
			OrderRepository repo = new OrderRepository();

			List<Order> orders = repo.ReadOrders("06012013");

			Assert.AreEqual(1, orders.Count());

			Order check = orders[0];

			Assert.AreEqual("06012013", check.OrderDate);
			Assert.AreEqual(1, check.OrderNumber);
			Assert.AreEqual("Wise", check.CustomerName);
			Assert.AreEqual("Wood", check.ProductType);
			Assert.AreEqual(1051.88, check.Total);
		}

		[Test]
		public void CanAddOrder()
		{
			OrderRepository repo = new OrderRepository();

			List<Order> orders = repo.ReadOrders("06012013");

			Order addOrder = new Order();

			addOrder.OrderDate = "06012013";
			addOrder.OrderNumber = 2;
			addOrder.CustomerName = "Acme";
			addOrder.State = "OH";
			addOrder.TaxRate = 6.25M;
			addOrder.ProductType = "Wood";
			addOrder.Area = 100.00M;
			addOrder.CostPerSquareFoot = 5.15M;
			addOrder.LaborCostPerSqareFoot = 4.75M;
			addOrder.MaterialCost = 515.00M;
			addOrder.LaborCost = 475.00M;
			addOrder.Tax = 61.88M;
			addOrder.Total = 1051.88M;
			orders.Add(addOrder);

			repo.OverwriteFile(orders, addOrder.OrderDate);

			Assert.AreEqual(2, orders.Count());

			Order check = orders[1];

			Assert.AreEqual("06012013", check.OrderDate);
			Assert.AreEqual("Acme", check.CustomerName);
			Assert.AreEqual(1051.88M, check.Total);
		}

		[Test]
		public void CanEditOrder()
		{
			OrderRepository repo = new OrderRepository();

			List<Order> orders = repo.ReadOrders("06012013");

			Order editOrder = orders[1];
			editOrder.CustomerName = "TNT";

			repo.OverwriteFile(orders, editOrder.OrderDate);

			Assert.AreEqual(2, orders.Count());

			Order check = orders[1];

			Assert.AreEqual("06012013", check.OrderDate);
			Assert.AreEqual("TNT", check.CustomerName);
			Assert.AreEqual(1051.88M, check.Total);
		}

		[Test]
		public void LoadProduct()
		{
			ProductRepository repo = new ProductRepository();
			Product product = repo.LoadProduct("Carpet");

			product.ProductType = "Carpet";
			product.CostPerSquareFoot = 2.25M;
			product.LaborCostPerSqareFoot = 2.10M;

			Product actual = repo.LoadProduct("Carpet");

			Assert.AreEqual("Carpet", actual.ProductType);
			Assert.AreEqual(2.25M, actual.CostPerSquareFoot);
			Assert.AreEqual(2.10M, actual.LaborCostPerSqareFoot);
		}

		[Test]
		public void LoadTaxes()
		{
			TaxRepository repo = new TaxRepository();
			Taxes tax = repo.LoadTaxes("OH");

			tax.StateAbbreviation = "OH";
			tax.StateName = "Ohio";
			tax.TaxRate = 6.25M;

			Taxes actual = repo.LoadTaxes("OH");

			Assert.AreEqual("OH", actual.StateAbbreviation);
			Assert.AreEqual("Ohio", actual.StateName);
			Assert.AreEqual(6.25M, actual.TaxRate);
		}
	}
}
