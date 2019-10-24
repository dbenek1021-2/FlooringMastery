using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;

namespace FlooringMastery.UI.Validate
{
	public class AddOrderCheck
	{
		public static string GetDate(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				string inputDate = Console.ReadLine();
				CultureInfo provider = CultureInfo.InvariantCulture;
				string dateFormat = "MMddyyyy";
				DateTime orderDate;
				bool validDate = DateTime.TryParseExact(inputDate, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out orderDate);

				if (string.IsNullOrEmpty(inputDate) || inputDate.Length != 8 || !validDate)
				{
					Console.WriteLine("You must enter a valid date.");
					Console.WriteLine("Press any key to try again...");
					Console.WriteLine();
					Console.ReadKey();
					Console.Clear();
				}

				while (inputDate.Length == 8 && validDate)
				{
					DateTime date = DateTime.ParseExact(inputDate, dateFormat, provider);

					if (date <= DateTime.Today)
					{
						Console.WriteLine("You must enter a future date.");
						Console.WriteLine("Press any key to try again...");
						Console.WriteLine();
						Console.ReadKey();
						Console.Clear();
						break;
					}
					else
					{
						Console.Clear();
						return inputDate;
					}
				}
			}
		}

		public static string GetName(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				string input = Console.ReadLine();

				if (string.IsNullOrEmpty(input) || input.Contains(","))
				{
					Console.WriteLine("You must enter a name. Does not accept comma characters.");
					Console.WriteLine("Press any key to try again...");
					Console.WriteLine();
					Console.ReadKey();
					Console.Clear();
				}
				else
				{
					Console.Clear();
					return input;
				}
			}
		}
		public static string GetState(string prompt)
		{
			while (true)
			{
				TaxManager taxManager = TaxManagerFactory.Create();
				var taxLookup = taxManager.DisplayTaxes();
				var listOfStateAbb = taxLookup.Select(p => p.StateAbbreviation.ToUpper()).ToList();

				Console.Write(prompt);
				string input = Console.ReadLine().ToUpper();

				if (!listOfStateAbb.Contains(input) || input.Length != 2)
				{
					Console.WriteLine("We're sorry. We do not sell products in your region. Check your input.");
					Console.WriteLine("Press any key to try again...");
					Console.WriteLine();
					Console.ReadKey();
					Console.Clear();
				}
				else
				{
					Console.Clear();
					return input;
				}
			}
		}
		public static string GetProduct(string prompt)
		{
			while (true)
			{
				ProductManager productManager = ProductManagerFactory.Create();
				var productLookup = productManager.DisplayProducts();
				var listOfProductNames = productLookup.Select(p => p.ProductType.ToUpper()).ToList();

				Console.Write(prompt);
				string format = "{0,-10} {1,20:c} {2,35:c}";
				Console.WriteLine(format, "\nProduct: ", "Cost per sq. ft.: ", "Labor cost per sq. ft.: ");

				for (int i = 0; i < productLookup.Count; i++)
				{
					Console.WriteLine(format, $"{productLookup[i].ProductType}", $"${productLookup[i].CostPerSquareFoot}", $"${productLookup[i].LaborCostPerSqareFoot}");
					Console.WriteLine();
				}

				string input = Console.ReadLine().ToUpper();
				if (!listOfProductNames.Contains(input))
				{
					Console.WriteLine("You must enter a correct product name. Check your spelling.");
					Console.WriteLine("Press any key to try again...");
					Console.WriteLine();
					Console.ReadKey();
					Console.Clear();
				}
				else
				{
					Console.Clear();
					return input.ToLower();
				}
			}
		}

		public static string GetArea(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				string input = Console.ReadLine();

				if (string.IsNullOrEmpty(input) || decimal.Parse(input) < 100)
				{
					Console.WriteLine("You must enter an amount no less than 100sq feet.");
					Console.WriteLine("Press any key to try again...");
					Console.WriteLine();
					Console.ReadKey();
					Console.Clear();
				}
				else
				{
					Console.Clear();
					return input;
				}
			}
		}
	}
}
