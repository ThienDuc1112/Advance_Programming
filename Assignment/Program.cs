using Assignment;
using System.Data;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    public static List<Customer> customers = new List<Customer>();
    public static List<Admin> admins = new List<Admin>();
    public static List<Product> products = new List<Product>();
    public static List<Payment> payments = new List<Payment>();
    public static List<Order> orders = new List<Order>();
    public static List<OrderDetail> orderDetails = new List<OrderDetail>();
    private static void Main(string[] args)
    {
        Admin admin = new Admin();
        admin.UserName = "admin";
        admin.Password = "12345";
        admins.Add(admin);
        Product product = new Product("Laptop dell", "for graphic", 120, 40);
        products.Add(product);
        Welcome();

    }

    public static void Welcome()
    {
        int currentID;
        string choice = "";
        bool check = true;
        do
        {
            Console.WriteLine("     --------------------Welcome to the system-------------------");
            Console.WriteLine("     | 1,Enter 1 to register an account                          |");
            Console.WriteLine("     | 2,Enter 2 to login as a user                              |");
            Console.WriteLine("     | 3,Enter 3 to login as a administrator                     |");
            Console.WriteLine("     -------------------------------------------------------------");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Customer customer = new Customer();
                    customer.Register();
                    try
                    {
                        customers.Add(customer);
                    }catch(ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "2":
                    string cmd = "";
                    int checkCustomerID = LoginCustomer();
                    Console.Write("Your ID: "+checkCustomerID);
                    if (checkCustomerID >= 0)
                    {
                        Console.WriteLine("You login successfully");
                        currentID = checkCustomerID;
                        Customer customer2 = CurrentCustomer(currentID);
                        do
                        {
                            customer2.ViewSystem();
                            cmd = Console.ReadLine();
                            switch (cmd)
                            {
                                case "1":
                                    customer2.ViewProfile(currentID);
                                    break;
                                case "2":
                                    customer2.EditProfile(currentID);
                                    break;
                                case "3":
                                    customer2.ViewAllProduct();
                                    break;
                                case "4":
                                    customer2.Search();
                                    break;
                                case "5":
                                    customer2.AddProductToCart();
                                    break;
                                case "6":
                                    customer2.ViewCart();
                                    break;
                                case "7":
                                    customer2.RemoveProduct();
                                    break;
                                case "8":
                                    customer2.OrderProduct(currentID);
                                    break;
                                case "9":
                                    customer2.ViewOrder(currentID);
                                    break;
                                case "10":
                                    Console.WriteLine("Goodbye");
                                    break;
                                default:
                                    Console.WriteLine("Please enter 1 to 10");
                                    break;
                            }

                        } while (cmd != "10");
                    }
                    else
                    {
                        Console.WriteLine("Password or username isn't correct");
                    }
                    break;
                case "3":
                    string command = "";
                    int checkAdminID = LoginAdmin();
                    if (checkAdminID >= 0)
                    {
                        Console.WriteLine("You login successfully");
                        currentID = checkAdminID;
                        Admin admin = CurrentAdmin(currentID);
                        do
                        {
                            admin.ViewSystem();
                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1":
                                    admin.ViewAllProduct();
                                    break;
                                case "2":
                                    admin.addProduct();
                                    break;
                                case "3":
                                    admin.UpdateProduct();
                                    break;
                                case "4":
                                    admin.DeleteProduct();
                                    break;
                                case "5":
                                    admin.viewAllOrder();
                                    break;
                                case "6":
                                    admin.ViewDetailOrder();
                                    break;
                                case "7":
                                    admin.EditOrder();
                                    break;
                                case "8":
                                    Console.WriteLine("Goodbye");
                                    break;
                                default:
                                    Console.WriteLine("Please input from 1 to 8");
                                    break;
                            }
                        } while (command != "8");
                    }
                    else
                    {
                        Console.WriteLine("Password or username isn't correct");
                    }
                    break;
                default:
                    Console.WriteLine("Please enter from 1 to 3");
                    break;
            }
        } while (check == true);
    }

    public static int LoginCustomer()
    {
            Console.WriteLine("Please enter user name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter password");
            string pass = Console.ReadLine();
            for (int i = 0; i < customers.Count; i++)
            {
                if (name == customers.ElementAt(i).UserName && pass == customers.ElementAt(i).Password)
                {
                    return customers.ElementAt(i).ID;
                }
            }
        return -1;
    }

    public static int LoginAdmin()
    {
        Console.WriteLine("Please enter user name");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter password");
        string pass = Console.ReadLine();
        for(int i = 0; i < admins.Count; i++)
        {
            if (name == admins.ElementAt(i).UserName && pass == admins.ElementAt(i).Password)
            {
                return admins.ElementAt(i).ID;
            }
        }
        return -1;
    }

    public static Admin CurrentAdmin(int id)
    {
        for(int i = 0; i < admins.Count; i++)
        {
            if (admins[i].ID == id) return admins[i];
        }
        return admins[0];
    }

    public static Customer CurrentCustomer(int id)
    {
        for(int i = 0; i < admins.Count; i++)
        {
            if(customers[i].ID == id) return customers[i];
        }
        return customers[0];
    }
}