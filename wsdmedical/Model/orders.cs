using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class orders
    {
       public int id { get; set; }
       public string order_no { get; set; }
       public int user_id { get; set; }
       public string user_name { get; set; }
       public int payment_id { get; set; }
       public double payment_fee { get; set; }
       public int payment_status { get; set; }

       public DateTime payment_time { get; set; }
       public int status { get; set; }
       public orders()
       {
           id = 0;
           order_no = string.Empty;
           user_id = 0;
           user_name = string.Empty;
           payment_id = 0;
           payment_fee = 0;
           payment_status = 0;
           payment_time = DateTime.MinValue;
           status = 0;
       }
    }
}
