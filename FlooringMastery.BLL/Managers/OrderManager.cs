using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.BLL
{
	public class OrderManager
	{
		private IOrderRepository _orderRepository;
		private string _orderPath = ConfigurationManager.AppSettings["OrderFile"].ToString(); //where DI is

		public OrderManager(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public DisplayOrderResponse DisplayOrders(string date)
		{
			DisplayOrderResponse response = new DisplayOrderResponse();
			response.Orders = new List<Order>();
			var orders = _orderRepository.ReadOrders(date); //returns list of all orders from that order date

			if (orders == null || orders.Count == 0)
			{
				response.Success = false;
				response.Message = $"{date} is not a valid order date. Check your input.";
			}
			else
			{
				response.Success = true;
				response.Orders = orders; //returns list of orders
			}

			return response;
		}

		public AddOrderResponse AddOrder(Order newOrder, string date)
		{
			AddOrderResponse response = new AddOrderResponse();
			response.NewOrder = new Order();
			var orders = _orderRepository.ReadOrders(date); //returns list of all orders from that orders date

			if (orders == null)
			{
				response.Success = false;
				response.Message = "Unknown error. Please contact IT.";
			}

			int orderNumTotal = 0;
			if (orders.Count > 0)
			{
				orderNumTotal = orders.Select(o => o.OrderNumber).Max();  //returns the max number of orders. ex/ if 3 orders, it'll return the # 3
			}
			newOrder.OrderNumber = orderNumTotal + 1;  //adds 1 to the last order number to create new order number in sequence
			orders.Add(newOrder);  //adds new order to orders list
			_orderRepository.OverwriteFile(orders, date); //sends orders to be exported to a file
			response.Success = true;
			response.NewOrder = newOrder; //returns the new order info
			response.OrderDate = date; //returns original order date (so that won't change)

			return response;
		}

		public EditOrderResponse EditOrder(string date, int orderNumber)
		{
			EditOrderResponse response = new EditOrderResponse();
			var orders = _orderRepository.ReadOrders(date); //returns list of all orders from that order date

			try
			{
				response.Orders = orders; //returns the lists of orders with that order date
				var orderToBeEdited = response.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);  //filtered out one to be edited
				response.Order = orderToBeEdited; //returns the order to be edited
				response.Success = true;
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}
			return response;
		}

		public RemoveOrderResponse RemoveOrder(Order order, string date, int orderNumber)
		{
			RemoveOrderResponse response = new RemoveOrderResponse();
			var orders = _orderRepository.ReadOrders(date); //returns list of all orders from that order date

			if (orders == null)
			{
				response.Success = false;
				response.Message = $"{date} is not a valid order date.";
			}
			else if (orders.Count() == 1)
			{
				string orderFilename = "Orders_" + date + ".txt";
				var correctFileToDelete = _orderPath + "\\" + orderFilename;
				File.Delete(correctFileToDelete);
			}
			else
			{
				var ordersFiltered = orders.Where(o => o.OrderNumber != orderNumber).ToList(); //filtered out one to be removed
				response.Success = true;
				_orderRepository.OverwriteFile(ordersFiltered, date); //sending all orders that were filtered (ordersFiltered) to make a new file, ergo removing an order
				response.Orders = ordersFiltered; //returns the orders
				response.OrderDate = date; //returns original order date (so that won't change)
			}

			return response;
		}

		public EditOrderResponse ExportEditOrder(List<Order> orders, Order order, string date, int orderNumber)
		{
			EditOrderResponse response = new EditOrderResponse();

			response.Orders = orders; //the lists of orders with that order date
			response.Order = order; //the edited order
			var orderIndex = orders.FindIndex(o => o.OrderNumber == orderNumber);  //find index where to replace the edited order
			orders.RemoveAt(orderIndex);  //removes the old order
			orders.Insert(orderIndex, order); //replace the old order index with the edited one
			_orderRepository.OverwriteFile(orders, date); //sends orders to be exported to file

			response.Success = true;
			return response;
		}

		public RemoveOrderResponse GetOrderToRemove(string date, int orderNumber)
		{
			RemoveOrderResponse response = new RemoveOrderResponse();
			var orders = _orderRepository.ReadOrders(date);

			if (orders.Count() == 0 || orders == null)
			{
				response.Success = false;
				response.Message = $"{date} is not a valid order date.";
				return response;
			}
			try
			{
				response.Orders = orders; //returns the lists of orders with that order date
				var orderToBeRemoved = response.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);  //filtered out one to be edited
				if (orderToBeRemoved == null)
				{
					response.Success = false;
					response.Message = $"{orderNumber} is not a valid order number.";
					return response;
				}
				response.Success = true;
				response.Order = orderToBeRemoved; //returns the order to be edited
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}
			return response;
		}
	}
}