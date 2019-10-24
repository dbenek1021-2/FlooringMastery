using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Repositories
{
	public class TestProductRepository : IProductRepository
	{
		List<Product> products = new List<Product>();
		Product product1 = new Product();
		Product product2 = new Product();
		Product product3 = new Product();
		Product product4 = new Product();

		public List<Product> Products()
		{
			product1.ProductType = "Carpet";
			product1.CostPerSquareFoot = 2.25M;
			product1.LaborCostPerSqareFoot = 2.1M;
			products.Add(product1);

			product2.ProductType = "Laminate";
			product2.CostPerSquareFoot = 1.75M;
			product2.LaborCostPerSqareFoot = 2.1M;
			products.Add(product2);

			product3.ProductType = "Tile";
			product3.CostPerSquareFoot = 3.5M;
			product3.LaborCostPerSqareFoot = 4.15M;
			products.Add(product3);

			product4.ProductType = "Wood";
			product4.CostPerSquareFoot = 5.15M;
			product4.LaborCostPerSqareFoot = 4.75M;
			products.Add(product4);

			return products;
		}

		public List<Product> ReadProducts()
		{
			Products();
			return products;
		}

		public Product LoadProduct(string productInput)
		{
			Products();
			if (productInput == "Carpet")
			{
				return product1;
			}
			else if (productInput == "Laminate")
			{
				return product2;
			}
			else
			{
				return null;
			}
		}
	}
}
