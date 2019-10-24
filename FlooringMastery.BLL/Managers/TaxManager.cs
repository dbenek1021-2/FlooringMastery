using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
	public class TaxManager
	{
		private ITaxRepository _taxRepository;

		public TaxManager(ITaxRepository taxRepository)
		{
			_taxRepository = taxRepository;
		}

		public List<Taxes> DisplayTaxes()
		{
			List<Taxes> taxes = new List<Taxes>();

			taxes = _taxRepository.ReadTaxes();

			return taxes;
		}

		public Taxes LoadTax(string State)
		{
			var taxes = DisplayTaxes();
			foreach (Taxes tax in taxes)
			{
				if (State.ToUpper() == tax.StateAbbreviation.ToUpper())
				{
					return tax;
				}
			}
			return null;
		}
	}
}
