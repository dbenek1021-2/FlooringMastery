using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
	public interface IOrderRepository
	{
		List<Order> ReadOrders(string orderDate);
		void OverwriteFile(List<Order> orders, string date);
	}
}
