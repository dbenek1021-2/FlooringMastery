using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Repositories
{
	public class TestTaxRepository : ITaxRepository
	{
		List<Taxes> taxes = new List<Taxes>();
		Taxes state1 = new Taxes();
		Taxes state2 = new Taxes();
		Taxes state3 = new Taxes();
		Taxes state4 = new Taxes();

		public List<Taxes> Taxes()
		{
			state1.StateAbbreviation = "OH";
			state1.StateName = "Ohio";
			state1.TaxRate = 6.25M;
			taxes.Add(state1);

			state2.StateAbbreviation = "PA";
			state2.StateName = "Pennsylvania";
			state2.TaxRate = 6.75M;
			taxes.Add(state1);

			state3.StateAbbreviation = "MI";
			state3.StateName = "Michigan";
			state3.TaxRate = 5.75M;
			taxes.Add(state3);

			state4.StateAbbreviation = "IN";
			state4.StateName = "Indiana";
			state4.TaxRate = 6.75M;
			taxes.Add(state4);

			return taxes;
		}

		public List<Taxes> ReadTaxes()
		{
			Taxes();
			return taxes;
		}
	}
}
