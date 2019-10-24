using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
	public class ProductManager
	{
		private IProductRepository _productRepository;

		public ProductManager(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public List<Product> DisplayProducts()
		{
			List<Product> products = new List<Product>();

			products = _productRepository.ReadProducts();

			return products;
		}

		public Product ReturnProduct(string input)
		{
			var products = _productRepository.ReadProducts();
			foreach (Product product in products)
			{
				if (input.ToUpper() == product.ProductType.ToUpper())
				{
					return product;
				}
			}
			return null;
		}
	}
}
