
using Part1_ProceduralToOOP;


Customer c1 = Customer.AddCustomer(1, "Marwan", "M@gamil.com", "Alexandria", true);
Customer c2 = Customer.AddCustomer(2, "Mai", "Mai@gamil.com", "7osh 3esa", false);
Customer c3 = Customer.AddCustomer(3, "Amr", "Amr@gamil.com", "Luxur", true);
Customer c4 = Customer.AddCustomer(4, "Enas", "Enas@gamil.com", "Cairo", false);

Product p1 = Product.AddProduct(101, "USB Cable", 50.0, 100);
Product p2 = Product.AddProduct(102, "Wireless Mouse", 250.0, 40);
Product p3 = Product.AddProduct(103, "Mechanical Keyboard", 1200.0, 15);
Product p4 = Product.AddProduct(104, "Laptop Stand", 400.0, 25);



int choice = -1;

RunInteractiveMenu();

static void PrintMenu()
{
    Console.WriteLine("\n---------- MENU ----------");
    Console.WriteLine("1) Print customers");
    Console.WriteLine("2) Print products");
    Console.WriteLine("3) Print all orders");
    Console.WriteLine("4) Print one order by id");
    Console.WriteLine("5) Create order");
    Console.WriteLine("6) Add line to order");
    Console.WriteLine("7) Mark order paid");
    Console.WriteLine("8) Show paid sales total");
    Console.WriteLine("0) Exit");


}

void RunInteractiveMenu()
{
    while (choice != 0)
    {
        PrintMenu();
        Console.Write("Choice: ");
        bool isParsed = int.TryParse(Console.ReadLine(), out choice);
        while (!isParsed)
        {
            Console.WriteLine("Enter choice from list");
            isParsed = int.TryParse(Console.ReadLine(), out choice);
        }

        switch (choice)
        {
            case 1:
                Customer.PrintCustomers();
                break;

            case 2:
                Product.PrintProducts();
                break;

            case 3:
                Order.PrintAllOrders();
                break;

            case 4:
                int orderId;
                Console.Write("Order id: ");
                bool isConverted = int.TryParse(Console.ReadLine(), out orderId);
                while (!isConverted)
                {
                    Console.WriteLine("Enter valid id");
                    isConverted = int.TryParse(Console.ReadLine(), out orderId);
                }
                var wantedOrder = Order.FindOrderById(orderId);
                if (wantedOrder is not null)
                {
                    wantedOrder.PrintOrder();
                }
                break;

            case 5:
                Console.Write("Order ID: ");
                isConverted = int.TryParse(Console.ReadLine(), out orderId);
                while (!isConverted)
                {
                    Console.WriteLine("Enter an positivde integer value");
                    isConverted = int.TryParse(Console.ReadLine(), out orderId);
                }

                int customerId;
                Console.Write("Customer ID: ");
                isConverted = int.TryParse(Console.ReadLine(), out customerId);
                while (!isConverted)
                {
                    isConverted = int.TryParse(Console.ReadLine(), out customerId);
                }

                Order.CreateOrder(orderId, customerId);

                break;

            case 6:
                Console.Write("Order id: ");
                isConverted = int.TryParse(Console.ReadLine(), out orderId);
                while (!isConverted)
                {
                    isConverted = int.TryParse(Console.ReadLine(), out orderId);
                }

                Order? searchOrder = Order.FindOrderById(orderId);
                int productId;
                Console.Write("Product id: ");
                isConverted = int.TryParse(Console.ReadLine(), out productId);
                while (!isConverted)
                {
                    isConverted = int.TryParse(Console.ReadLine(), out productId);
                }

                Product? product = Product.FindProductById(productId);

                int quantity;
                Console.Write("Quantity: ");
                isConverted = int.TryParse(Console.ReadLine(), out quantity);
                while (!isConverted)
                {
                    isConverted = int.TryParse(Console.ReadLine(), out quantity);
                }

                Order.FindOrderById(orderId)?.AddProduct(p2, quantity);
                break;

            case 7:
                Console.Write("Order id: ");
                isConverted = int.TryParse(Console.ReadLine(), out orderId);
                while (!isConverted)
                {
                    isConverted = int.TryParse(Console.ReadLine(), out orderId);
                }
                Order.FindOrderById(orderId)?.MarkOrderPaid();

                break;

            case 8:
                Console.WriteLine(Order.TotalSalesPaidOnly());
                break;
            case 0:
                Console.WriteLine("BYE ");
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

}







