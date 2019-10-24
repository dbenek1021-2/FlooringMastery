using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data
{
	public class TestRepository : IOrderRepository
	{
		List<Order> _orders = new List<Order>();
		Order order = new Order();

		public List<Order> Order()
		{
			order.OrderDate = "06012013";
			order.OrderNumber = 1;
			order.CustomerName = "Wise";
			order.State = "OH";
			order.TaxRate = 6.25M;
			order.ProductType = "Wood";
			order.Area = 100.00M;
			order.CostPerSquareFoot = 5.15M;
			order.LaborCostPerSqareFoot = 4.75M;
			order.MaterialCost = 515.00M;
			order.LaborCost = 475.00M;
			order.Tax = 61.88M;
			order.Total = 1051.88M;
			_orders.Add(order);

			return _orders;
		}

		public List<Order> ReadOrders(string orderDate)
		{
			List<Order> orders = new List<Order>();
			if (orderDate == "06012013")
			{
				Order();
				return _orders;
			}
			else
			{
				return orders;
			}
		}

		public void OverwriteFile(List<Order> orders, string date)
		{
			_orders = orders;
		}
	}
}
