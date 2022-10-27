using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class OrderDetail
    {
        private int id;
        private Product product;
        private int quantity;

        public int ID
        {
            get { return id; }
        }
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if(value < 0) { throw new ArgumentException("The quantity must be greater than 0"); }
                if(value == null) { throw new ArgumentException("the quantity must be a positive number"); }
                quantity = value;
            }
            
        }
        public OrderDetail(Product product, int quantity)
        {
            if (Program.orderDetails.Count > 0)
            {
                int setID = Program.orderDetails.ElementAt(Program.orderDetails.Count - 1).ID;
                this.id = setID + 1;
            }
            else { this.id = 1; }
            Product = product;
            Quantity = quantity;
        }
        public decimal SubTotal()
        {
            return this.Quantity * this.Product.Price;
        }
    }
}
