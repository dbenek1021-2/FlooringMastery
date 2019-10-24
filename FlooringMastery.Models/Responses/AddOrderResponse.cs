using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
	public class AddOrderResponse : Response
	{
		public Order NewOrder { get; set; }
		public string OrderDate { get; set; }
	}
}
