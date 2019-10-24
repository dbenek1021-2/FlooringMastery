using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
	public class EditOrderResponse : Response
	{
		public Order Order { get; set; }
		public string OrderDate { get; set; }
		public int OrderNumber { get; set; }
		public List<Order> Orders { get; set; }
	}
}
