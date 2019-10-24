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
	public class TaxManagerFactory
	{
		public static TaxManager Create()
		{
			string mode = ConfigurationManager.AppSettings["Mode"].ToString();

			switch (mode)
			{
				case "Test":
					return new TaxManager(new TestTaxRepository());
				case "Prod":
					return new TaxManager(new TaxRepository());
			}

			throw new Exception("Mode value in app config is not valid!");
		}
	}
}
