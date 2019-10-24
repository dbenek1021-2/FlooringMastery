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
	public class TaxRepository : ITaxRepository
	{
		private string _taxPath = ConfigurationManager.AppSettings["TaxFile"].ToString();

		public List<Taxes> ReadTaxes()
		{
			List<Taxes> taxes = new List<Taxes>();

			if (File.Exists(_taxPath))
			{
				string[] allLines = File.ReadAllLines(_taxPath);

				for (int i = 1; i < allLines.Length; i++)
				{
					Taxes tax = new Taxes();
					string[] filelines = allLines[i].Split(',');

					tax.StateAbbreviation = filelines[0];
					tax.StateName = filelines[1];
					tax.TaxRate = decimal.Parse(filelines[2]);

					taxes.Add(tax);
				}
			}
			return taxes;
		}

		public Taxes LoadTaxes(string State)
		{
			var taxes = ReadTaxes();
			foreach (Taxes tax in taxes)
			{
				if (State == tax.StateAbbreviation)
				{
					return tax;
				}
			}
			return null;
		}
	}
}
