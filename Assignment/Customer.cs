using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment
{
    internal class Customer : User
    {
        private string phoneNumber;
        private string email;
        private string address;
        private List<OrderDetail> carts = new List<OrderDetail>();
        private List<Order> orders = new List<Order>();     
        public Customer(string phone, string email, string address)
        {
            PhoneNumber = phone;
            Email = email;
            Address = address;
        }
        public Customer()
        {
            if (Program.customers.Count > 0)
            {
                int setID = Program.customers.ElementAt(Program.customers.Count - 1).ID;
                this.id = setID + 1;
            }
            else { this.id = 1; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The phone number can't be null");
                }
                if (!IsNumber(value))
                {
                    throw new ArgumentException("The phone number must be numeric");
                }
                phoneNumber = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The email can't be null");
                }
                if (!IsValidEmail(value))
                {
                    throw new ArgumentException("The email isn't valided");
                }
                email = value;
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The address can't be null");
                }
                address = value;
            }
        }
        public override void ViewSystem()
        {
            Console.WriteLine("     --------------------Welcome to the FKT shop  --------------");
            Console.WriteLine("     | 1,Enter 1 to view your profile                            |");
            Console.WriteLine("     | 2,Enter 2 to edit your profile                            |");
            Console.WriteLine("     | 3,Enter 3 to view products in shop                        |");
            Console.WriteLine("     | 4,Enter 4 to search products in shop                      |");
            Console.WriteLine("     | 5,Enter 5 to add a product into your cart                 |");
            Console.WriteLine("     | 6,Enter 6 to view your cart                               |");
            Console.WriteLine("     | 7,Enter 7 to remove a product from your cart              |");
            Console.WriteLine("     | 8,Enter 8 to perform payment                              |");
            Console.WriteLine("     | 9,Enter 9 to view your orders                             |");
            Console.WriteLine("     | 10,Enter 10 to logout                                     |");
            Console.WriteLine("     -------------------------------------------------------------");
        }

        public void ViewProfile(int id)
        {
            foreach(var Customer in Program.customers)
            {
                if(Customer.ID == id)
                {
                    Console.WriteLine("--------------------------Your Profile-------------------------");
                    Console.WriteLine($"1, Your name:{Customer.UserName}");
                    Console.WriteLine($"2, Your phone number:{Customer.PhoneNumber}");
                    Console.WriteLine($"3, Your email:{Customer.Email}");
                    Console.WriteLine($"4, Your address:{Customer.Address}");
                }
            }
       
        }
        public void EditProfile(int id)
        {
            Console.WriteLine("Enter your phone number:");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your address:");
            string address = Console.ReadLine();
            try
            {
                Customer customer = Program.customers.Find(c => c.ID == id);
                customer.PhoneNumber = phone;
                customer.Email = email;
                customer.Address = address;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ViewAllProduct()
        {
            Console.WriteLine("------------------The list of products--------------------");
            foreach (var product in Program.products)
            {
                if (product.CheckAvailable() == true)
                {
                    Console.WriteLine($"ID: {product.ID} Name: {product.Name}" +
                        $" Price: {product.Price} Description: {product.Description}");
                }
            }
            Console.WriteLine("-----------------------------------------------------------");
        }

        public void AddProductToCart()
        {
            bool checkCart = false;
            int id_product = -1;
            Console.WriteLine("Enter the id of the product you want to buy:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity of the product you want to buy:");
            int quantity = int.Parse(Console.ReadLine());
            foreach (Product product in Program.products)
            {
                if (product.ID == id)
                {
                    id_product = product.ID;
                    
                        for (int i = 0; i < carts.Count; i++)
                        {
                            if (carts[i].Product == product)
                            {
                            Console.WriteLine("This product existed in your cart, so the quantity will increase");
                            checkCart = true;
                                carts[i].Quantity += quantity;
                            }
                        }
                    if (checkCart == true)
                    {
                        break;
                    }
                    else
                    {
                        OrderDetail orderDetail = new OrderDetail(product, quantity);
                        carts.Add(orderDetail);
                    }
                }
            }
            if(id_product < 0)
            {
                Console.WriteLine("This product doesn't exist");
            }
        }

        public void ViewCart()
        {
            Console.WriteLine("--------------------------Your Cart-------------------------");
            for(int i =0; i < carts.Count; i++)
            {
               
                Console.WriteLine($"{i+1},Name:{carts[i].Product.Name},Price:{carts[i].Product.Price}," +
                    $"Quantity: {carts[i].Quantity}, Total: {carts[i].SubTotal()}");
            }
            
        }

        public void RemoveProduct()
        {
            Console.WriteLine("Enter the name of the product you want to remove");
            string name = Console.ReadLine();
            for(int i = 0; i < carts.Count; i++)
            {
                if(carts[i].Product.Name == name)
                {
                    carts.RemoveAt(i);
                    Console.WriteLine($"The product {name} was removed from your cart");
                }
            }
        }

        public void Search()
        {
            Console.WriteLine("Enter the name of the products you want to search");
            string name = Console.ReadLine();
            Console.WriteLine("------------------The list of products--------------------");
            foreach (var product in Program.products)
            {
                if (product.Name.Contains(name))
                {
                    Console.WriteLine($"ID: {product.ID} Name: {product.Name}" +
                        $" Price: {product.Price} Description: {product.Description}");
                }
            }
        }

        public void OrderProduct(int idCustomer)
        {
            Order order = new Order();
            order.CustomerID = idCustomer;
            Customer customer = Program.customers.Find(c => c.ID==idCustomer);
            if (customer.PhoneNumber != null && customer.Email != null && customer.Address != null)
            {
                if (carts.Count > 0)
                {
                    Console.WriteLine("You need to select a method payment here. Do you select banking or cash?");
                    string choice;
                    foreach (OrderDetail cart in carts)
                    {
                        OrderDetail orderDetail = new OrderDetail(cart.Product, cart.Quantity);
                        order.OrderDetails.Add(orderDetail);
                        Product product = Program.products.Find(p => p.ID == cart.Product.ID);
                        product.Quantity -= cart.Quantity;
                    }
                    decimal total = order.calculateTotal();
                    do
                    {
                        choice = Console.ReadLine();
                        if (choice == "cash")
                        {
                            Payment cash = new Cash();
                            order.Payment = cash;
                            order.Payment.PerformPayment(total);
                            order.Status = "ordered";
                        }
                        else if (choice == "banking")
                        {
                            Payment banking = new Banking();
                            order.Payment = banking;
                            order.Payment.PerformPayment(total);
                            order.Status = "done";
                            Console.WriteLine("The process of banking is done");
                        }
                        else
                        {
                            Console.WriteLine("You must select cash or banking");
                        }
                    } while (choice != "cash" && choice != "banking");
                    Console.WriteLine("Done!!!");
                    Console.WriteLine("You will receive your products in the next few days");
                    carts.Clear();
                    Program.orders.Add(order);
                    orders.Add(order);
                }
                else
                {
                    Console.WriteLine("you haven't ordered anything!!");
                }
            }
            else
            {
                Console.WriteLine("You have to add your information into your profile before order");
            }
        }

        public void ViewOrder(int idCustomer)
        {
            Console.WriteLine("----------------------Your orders--------------------");
            foreach(Order order in orders)
            {
                if (order.CustomerID == idCustomer)
                {
                    Console.WriteLine($"ID Order: {order.OrderID}");
                    Console.WriteLine($"Odered day: {order.DayCreated.ToString()}");
                    Console.WriteLine($"The payment method:{order.Payment.Type}");
                    Console.WriteLine($"The current status:{order.Status}");
                    Console.WriteLine("|    Name    |    Price    |    Quantity    |    Total    |");
                    foreach (OrderDetail orderDetail in order.OrderDetails)
                    {
                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine($"   {orderDetail.Product.Name}        {orderDetail.Product.Price}$" +
                            $"     {orderDetail.Quantity}              {orderDetail.SubTotal()}$");
                    }
                }
            }
            
        }
        
    }

        
}
