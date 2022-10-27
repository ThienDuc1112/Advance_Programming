using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Admin : User, IManageProduct, ImanageOrder
    {
        private string phone;
        private string email;

        public string PhoneNumber
        {
            get { return phone; }
            set
            {
                if (value == " ")
                {
                    throw new ArgumentException("The phone number can't be null");
                }
                if (!IsNumber(value))
                {
                    throw new ArgumentException("The phone number must be numeric");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == " ")
                {
                    throw new ArgumentException("The email can't be null");
                }
                if (!IsValidEmail(value))
                {
                    throw new ArgumentException("The email isn't valided");
                }
                email = value;
            }
        }

        public override void ViewSystem()
        {
                Console.WriteLine("     --------------------Welcome to the admin system--------------");
                Console.WriteLine("     | 1,Enter 1 to view all product                             |");
                Console.WriteLine("     | 2,Enter 2 to add a new product                            |");
                Console.WriteLine("     | 3,Enter 3 to update for a product                         |");
                Console.WriteLine("     | 4,Enter 4 to remove a product                             |");
                Console.WriteLine("     | 5,Enter 5 to view all orders                              |");
                Console.WriteLine("     | 6,Enter 6 to view detail of an order                      |");
                Console.WriteLine("     | 7,Enter 7 to update for an order                          |");
                Console.WriteLine("     | 8,Enter 8 to logout                                       |");
                Console.WriteLine("     -------------------------------------------------------------");           
        }

        public void viewAllOrder()
        {
            Console.WriteLine("----------------------The list orders--------------------");
            for(int i = 0; i < Program.orders.Count; i++)
            {
                Console.WriteLine($"ID order: {Program.orders[i].OrderID}, ID customer: {Program.orders[i].CustomerID} " +
                    $"Day created: {Program.orders[i].DayCreated.ToString("F")}, Status: {Program.orders[i].Status}");
            }
        }

        public void ViewDetailOrder()
        {
            bool check = false;
            Console.WriteLine("Please enter the id of order you want to view:");
            int id = int.Parse(Console.ReadLine()); 
            for(int i = 0; i < Program.orders.Count; i++)
            {
                if(Program.orders[i].OrderID == id)
                {
                    check = true;
                    Console.WriteLine("----------------------The detail order--------------------");
                    Console.WriteLine($"ID order:{Program.orders[i].OrderID}");
                    Console.WriteLine($"Day ordered:{Program.orders[i].DayCreated.ToString("F")}");
                    Console.WriteLine($"Status:{Program.orders[i].Status}");
                    Console.WriteLine($"Total:{Program.orders[i].calculateTotal()}");
                    Console.WriteLine($"Type of payment:{Program.orders[i].Payment.Type}");
                    Console.WriteLine("------------------Information of customer------------------");
                    foreach(Customer customer in Program.customers)
                    {
                        if(customer.ID == Program.orders[i].OrderID)
                        {
                            Console.WriteLine($"Name of customer: {customer.UserName}");
                            Console.WriteLine($"Phone number of customer: {customer.PhoneNumber}");
                            Console.WriteLine($"Email of customer: {customer.Email}");
                            Console.WriteLine($"Address of customer: {customer.Address}");
                        }
                        break;
                    }
                    Console.WriteLine("------------------The list of products---------------------");
                    foreach(OrderDetail orderDetail in Program.orders[i].OrderDetails)
                    {
                        Console.WriteLine($"ID: {orderDetail.ID}, Name product: {orderDetail.Product.Name}, " +
                            $"Price: {orderDetail.Product.Price}$, Quantity: {orderDetail.Quantity}, Total: {orderDetail.SubTotal()}");
                    }
                }
            }
            
        }

        public void EditOrder()
        {
            Console.WriteLine("Enter the id of order you want to edit");
            int id = int.Parse(Console.ReadLine());
            Order order = Program.orders.Find(o => o.OrderID == id);
            Console.WriteLine("Set status for this order");
            string status = Console.ReadLine();
            try
            {
                order.Status = status;
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void addProduct()
        {
            Console.WriteLine("Enter name for the product:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter price for the product:");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity for the product:");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter description for the product:");
            string description = Console.ReadLine();
            try
            {
                Product product = new Product(name, description, price, quantity);
                if (!product.CheckDuplicated(name))
                {
                    Program.products.Add(product);
                    Console.WriteLine("The product was added");
                }
                else { Console.WriteLine("This name was used for other product"); }
                
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void ViewAllProduct()
        {
            Console.WriteLine("------------------The list of products--------------------");
            foreach(var product in Program.products)
            {
                Console.WriteLine($"ID: {product.ID} Name: {product.Name}" +
                    $" Price: {product.Price} Quantity: {product.Quantity}");
            }
            Console.WriteLine("-----------------------------------------------------------");
        }

        public void DeleteProduct()
        {
            int IDRemovedProduct = -1;
            Console.WriteLine("Enter the ID of the product you want to remove:");
            int id = int.Parse(Console.ReadLine());
            foreach (Product product in Program.products)
            {
                if (product.ID == id)
                {
                    IDRemovedProduct = product.ID;
                }
            }
            if (IDRemovedProduct > 0)
            {
                Program.products.Remove(Program.products.Find(p => p.ID == IDRemovedProduct));
                Console.WriteLine("The product was removed");
            }
            else
            {
                Console.WriteLine("This product doesn't exits!!!");
            }

        }

        public void UpdateProduct()
        {
            int IDProductUpdate = -1;
            Console.WriteLine("Please enter id of the product that you want to update");
            int idProduct = int.Parse(Console.ReadLine());
            foreach(var product in Program.products)
            {
                if( idProduct == product.ID)
                {
                    IDProductUpdate = product.ID;
                }
            }
        if(IDProductUpdate > 0)
            {
                Console.WriteLine("Enter name for the product:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter price for the product:");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter quantity for the product:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter description for the product:");
                string description = Console.ReadLine();
                try
                {
                    Product UpdatedProduct = Program.products.Find(p => p.ID == IDProductUpdate);
                    UpdatedProduct.Price = price;
                    UpdatedProduct.Quantity = quantity;
                    UpdatedProduct.Description = description;
                    UpdatedProduct.Name = name;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else { Console.WriteLine("This product doesn't exist"); }
        }

    }
}
