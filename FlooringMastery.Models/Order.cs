using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
	public class Order
	{
		public string OrderDate { get; set; }
		public int OrderNumber { get; set; }
		public string CustomerName { get; set; }
		public string State { get; set; }
		public decimal TaxRate { get; set; }
		public string ProductType { get; set; }
		public decimal Area { get; set; }
		public decimal CostPerSquareFoot { get; set; }
		public decimal LaborCostPerSqareFoot { get; set; }
		public decimal MaterialCost { get; set; }
		public decimal LaborCost { get; set; }
		public decimal Tax { get; set; }
		public decimal Total { get; set; }

		public DateTime ConvertToDate(string input)
		{
			CultureInfo provider = CultureInfo.InvariantCulture;
			string dateFormat = "MMddyyyy";
			DateTime date = DateTime.ParseExact(input, dateFormat, provider);
			return date;
		}

		public decimal MaterialCostCalc(decimal area, decimal sqFt)
		{
			decimal newMaterialCost = area * sqFt;
			return newMaterialCost;
		}

		public decimal LaborCostCalc(decimal area, decimal sqFt)
		{
			decimal newLaborCost = area * sqFt;
			return newLaborCost;
		}

		public decimal TaxCalc(decimal matCost, decimal labor, decimal taxRate)
		{
			decimal newTax = ((matCost + labor) * (taxRate/100));
			return newTax;
		}

		public decimal TotalCalc(decimal matCost, decimal labor, decimal tax)
		{
			decimal newTotal = MaterialCost + LaborCost + Tax;
			return newTotal;
		}
	}
}
