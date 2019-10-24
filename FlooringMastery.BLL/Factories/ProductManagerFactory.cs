using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Data.Repositories;

namespace FlooringMastery.BLL
{
	public class ProductManagerFactory
	{
		public static ProductManager Create()
		{
			string mode = ConfigurationManager.AppSettings["Mode"].ToString();

			switch (mode)
			{
				case "Test":
					return new ProductManager(new TestProductRepository());
				case "Prod":
					return new ProductManager(new ProductRepository());
			}

			throw new Exception("Mode value in app config is not valid!");
		}
	}
}
