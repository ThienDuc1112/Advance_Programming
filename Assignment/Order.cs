using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Order
    {
        private int orderID;
        private int customerID;
        private Payment payment;
        private DateTime dayCreated;
        private string status;
        public List<OrderDetail> OrderDetails = new List<OrderDetail>();
        
        public int OrderID
        {
            get { return orderID; }
        }
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }
        public DateTime DayCreated
        {
            get { return dayCreated; }
            set { dayCreated = value; }
        }
        public string Status
        {
            get { return status; }
            set
            {
                if(value != "ordered"&&value != "delivering"&& value != "done")
                {
                    throw new ArgumentException("This status isn't valid. The valid status is:" +
                        " ordered, delivering, or done ");
                }
                status = value;
            }
        }
        public Order()
        {
            if (Program.orders.Count > 1)
            {
                int setId = Program.orders[Program.orders.Count - 1].OrderID;
                this.orderID = setId + 1;
            }
            else { this.orderID = 1; }
          
            this.dayCreated = DateTime.Now;
            
        }
        public List<OrderDetail> ListDetails()
        {
            List<OrderDetail> ListDetail = new List<OrderDetail>();
            return ListDetail;
        }

        public decimal calculateTotal()
        {
            decimal total = 0;
           foreach(OrderDetail detail in OrderDetails)
            {
                total += detail.SubTotal();
            }
           return total;
        }
    }
}
