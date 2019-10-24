using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Workflows;

namespace FlooringMastery.UI
{
	public static class Menu
	{
		public static void Start()
		{
			while(true)
			{
				Console.Clear();
				Console.WriteLine(ConsoleIO.LineBar);
				Console.WriteLine("* Flooring Program");
				Console.WriteLine("* ");
				Console.WriteLine("* 1. Display Orders");
				Console.WriteLine("* 2. Add an Order");
				Console.WriteLine("* 3. Edit an Order");
				Console.WriteLine("* 4. Remove an Order");
				Console.WriteLine("* 5. Quit");
				Console.WriteLine(ConsoleIO.LineBar);
				Console.Write("\nEnter selection: ");

				string userinput = Console.ReadLine();

				switch (userinput.ToUpper())
				{
					case "1":
						DisplayOrderWorkflow displayWorkflow = new DisplayOrderWorkflow();
						displayWorkflow.Execute();
						break;
					case "2":
						AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
						addOrderWorkflow.Execute();
						break;
					case "3":
						EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
						editOrderWorkflow.Execute();
						break;
					case "4":
						RemoveOrderWorkflow removeOrderWorkflow = new RemoveOrderWorkflow();
						removeOrderWorkflow.Execute();
						break;
					case "5":
						return;
					case "Q":
						return;
				}
			}
		}
	}
}
