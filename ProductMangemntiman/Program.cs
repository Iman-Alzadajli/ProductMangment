using System.Xml.Linq;

namespace ProductMangemntiman

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Inventory Management System!");



            //user name password 

            bool flag = true;


            while (flag)
            {
                Console.Write("Please enter your username :");
                string username = Convert.ToString(Console.ReadLine());
                Console.Write("Please enter your Password :");
                string password = Convert.ToString(Console.ReadLine());

                if (username == "admin" && password == "adminpass")
                {

                    Console.WriteLine($"Authentication successful! Welcome, {username}");

                    flag = false;

                }
                else
                {
                    Console.WriteLine("You Entered a Wrong Username or Password try again");
                }



            }


            // end of checking user name and pass 



            Console.WriteLine("================================================");




            List<Product> products = new List<Product>(); // add products to one list 


            bool flag2 = true;
            while (flag2)
            {

                Console.WriteLine("Options: \r\n1. Add a new product \r\n2. Update product quantity \r\n3. Display product list \r\n4. Record sale \r\n5. Generate product report \r\n6. Generate sales report \r\n7. Exit ");
                Console.Write("Select an operation (1-7):");
                int option = Convert.ToInt32(Console.ReadLine());

                string name;
                double price;
                int quantity;

                if (option == 1)
                {
                    // this to add product 

                    Product.AddProduct(products);


                }

                else if (option == 2) //this for update Quantity  
                {

                    
                    Product.UpdateَQuantity(products);

                }

                else if (option == 3)
                {

                    //to get product details 

                    Console.WriteLine("Product List :");
                    Product.GetDetails(products);

                }

                else if (option == 4)
                {

                    //how many got sold  recod sold 

                    Product.RecordSale(products);
        
                }

                else if (option == 5) //full report 
                {
                    Product.GetReport(products);

                }

                else if (option == 6)
                {
                    //to get  Revenue sale report 
                    
                    Product.Totalsalereport(products);
                }

                else if (option == 7) //exit 
                {
                    Console.WriteLine("Thank you for using the Inventory Management System, admin!");
                    flag2 = false;

                }

                else //wrong choice 
                {
                    Console.WriteLine("You Enterd a Wrong Option try again");
                }


            }


        }
    }

    public class Product
    {

        public string name;
        public double price;
        public int quantity;

        public int sold = 0;         // what sold for product 
        public double revenue = 0;   // we will do sold * price to get this one 

        public static void AddProduct(List<Product> products)
        {


            Product newProduct = new Product();

            while (true)
            {
                Console.Write("Enter product name: ");
                newProduct.name = Console.ReadLine();

                // bool exists = products.Any(p => p.name.Equals(newProduct.name, StringComparison.OrdinalIgnoreCase));

                bool exists = false;

                foreach (Product p in products)
                {
                    if (p.name.ToLower() == newProduct.name.ToLower())
                    {
                        exists = true;
                        break; // if we have same name break 
                    }
                }

                if (exists)
                {
                    Console.WriteLine("A product with this name already exists. Try another name!");
                }
                else //add new product 
                {

                    Console.Write("Enter product price: ");
                    double price = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter initial quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    Product product1 = new Product
                    {
                        name = newProduct.name, // what user entered for new product 
                        price = price,
                        quantity = quantity
                    };

                    products.Add(product1);

                    Console.WriteLine("Product added successfully!");
                    Console.WriteLine("================================================");
                }
                break; 
                }

        }

     

        public static void RecordSale(List<Product> products)

        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Product foundProduct = null;

            foreach (Product p in products)
            {
                if (p.name.ToLower() == name.ToLower())
                {
                    foundProduct = p;
                    break;
                }
            }

            if (foundProduct == null)
            {
                Console.WriteLine("Product not found.");
            }

            else
            {
                Console.Write("Enter quantity sold: ");
                int quantitySold = Convert.ToInt32(Console.ReadLine());

                if (quantitySold > foundProduct.quantity)
             
                {
                    Console.WriteLine("Not enough stock available.");
                }
                else
                {

                    foundProduct.quantity = foundProduct.quantity -  quantitySold;
                    foundProduct.sold += quantitySold;

                    Console.WriteLine("Sale recorded successfully!");

                    if (foundProduct.quantity == 0)
                    {
                        Console.WriteLine($"Product '{foundProduct.name}' is now sold out!");
                    }
                }
            }

            Console.WriteLine("================================================");
        }



        // for report sale
        public static void Totalsalereport(List<Product> products)
        {
            int totalSales = 0;
            double totalRevenue = 0;

            foreach (Product p in products)
            {
               

                    p.revenue = p.sold * p.price; // for revenue
                    totalSales += p.sold;         // total sold
                    totalRevenue += p.revenue;    //total revenue 
                
            }

            Console.WriteLine("=============== Sales Report ===============");
            Console.WriteLine($"Total Sales: {totalSales}");
            Console.WriteLine($"Total Revenue: ${totalRevenue}");
            Console.WriteLine("============================================");
        }


        public static void GetDetails(List<Product> products)
        {


            foreach (Product p in products)
            {
                Console.WriteLine($"{p.name} - Price: {p.price}, Quantity: {p.quantity}");
            }
        }

        
        

            public static void GetReport(List<Product> products)
        {
            Console.WriteLine("=============== Product Report ===============");

            if (products.Count == 0)
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                foreach (Product p in products)
                {
                    Console.WriteLine($"- {p.name} | Price: ${p.price} | Remaining: {p.quantity} | Sold: {p.sold} | Revenue: ${p.revenue}");
                }
            }

            Console.WriteLine("==============================================");
        }


        public static void UpdateَQuantity(List<Product> products)
        {
            Console.Write("Enter product name to update quantity: ");
            string name = Console.ReadLine();

            Product foundProduct = null;

            foreach (Product p in products)
            {
                if (p.name.ToLower() == name.ToLower())
                {
                    foundProduct = p;
                    break;
                }
            }

            if (foundProduct == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                Console.Write("Enter new quantity: ");
                int newQuantity = Convert.ToInt32(Console.ReadLine());
                foundProduct.quantity = newQuantity;

                Console.WriteLine($"Quantity for product '{foundProduct.name}' updated successfully!");
            }

            Console.WriteLine("================================================");
        }








    }
}
