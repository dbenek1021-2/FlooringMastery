using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private string _productPath = ConfigurationManager.AppSettings["ProductFile"].ToString();

		public List<Product> ReadProducts()
		{
			List<Product> products = new List<Product>();

			if (File.Exists(_productPath))
			{
				string[] allLines = File.ReadAllLines(_productPath);

				for (int i = 1; i < allLines.Length; i++)
				{
					Product product = new Product();
					string[] filelines = allLines[i].Split(',');

					product.ProductType = filelines[0];
					product.CostPerSquareFoot = decimal.Parse(filelines[1]);
					product.LaborCostPerSqareFoot = decimal.Parse(filelines[2]);

					products.Add(product);
				}
			}
			return products;
		}

		public Product LoadProduct(string ProductInput)
		{
			var products = ReadProducts();
			foreach (Product product in products)
			{
				if (ProductInput == product.ProductType)
				{
					return product;
				}
			}
			return null;
		}
	}
}
