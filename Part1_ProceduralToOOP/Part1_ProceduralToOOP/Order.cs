

namespace Part1_ProceduralToOOP;

internal class Order
{


    const int MaxOrders = 100;
    const int MaxLinesPerOrder = 20;
    private static int _orderCount = 0;

    private int OrderId { get; }
    public Customer Customer { get; }
    public DateTime OrderDate { get; }
    public bool IsPaid { get; private set; }
    public List<OrderLine> OrderLines { get; } = new();

    public static List<Order> Orders = new();

    private Order(int id, Customer customer, DateTime orderDate)
    {
        OrderId = id;
        Customer = customer;
        OrderDate = orderDate;
        IsPaid = false;
    }

    public static Order? FindOrderById(int id)
    {

        if (Orders.Count == 0)
        {
            Console.WriteLine("There is no orders yet");
            return null;
        }

        foreach (Order order in Orders)
        {
            if (order.OrderId == id)
                return order;
        }
        return null;
    }

    public static Order CreateOrder(int orderId, int customerId)
    {
        if (_orderCount >= MaxOrders)
        {
            Console.WriteLine("ERROR: order list is full.");
            throw new ArgumentException();
        }

        if (FindOrderById(orderId) is not null)
        {
            Console.WriteLine($"ERROR: order id  {orderId} already exists.");
            throw new ArgumentException();
        }

        Customer? customder = Customer.FindCustomerById(customerId);

        if (customder is null)
        {
            Console.WriteLine($"ERROR: customer id {customerId} not found. ");
            throw new ArgumentException();
        }


        Order order = new(orderId, customder, DateTime.Now);
        Orders.Add(order);
        _orderCount++;
        Console.WriteLine("Order Created successfully");
        return order;
    }

    public void AddProduct(Product product, int quantity)
    {

        if (IsPaid)
        {
            Console.WriteLine("ERROR: cannot change a paid order.");
            return;
        }

        if (OrderLines.Count >= MaxLinesPerOrder)
        {
            Console.WriteLine("ERROR: order has too many lines.");
            return;
        }

        product.ReduceStock(quantity);
        var orderLine = new OrderLine(product, quantity);
        OrderLines.Add(orderLine);
    }

    public static void PrintAllOrders()
    {
        if (Orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
            return;
        }

        foreach (Order order in Orders)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Order ID: {order.OrderId}");
            Console.WriteLine($"Customer: {order.Customer.Name}");
            Console.WriteLine($"Order Date: {order.OrderDate}");
            Console.WriteLine($"Paid: {order.IsPaid}");

            Console.WriteLine("Order Lines:");

            foreach (OrderLine line in order.OrderLines)
            {
                Console.WriteLine($"  {line}");
            }

            Console.WriteLine("--------------------------------");
        }
    }

    public static double TotalSalesPaidOnly()
    {
        double sum = 0.0;
        foreach (Order order in Orders)
        {
            if (order.IsPaid)
                sum += order.CalculateOrderTotal();
        }
        return sum;
    }


    public void PrintOrder()
    {

        Console.WriteLine($" === ORDER #{this.OrderId}  === ");
        Console.WriteLine($"Date: {this.OrderDate.ToString("f")}");
        Console.WriteLine($"Customer: {this.Customer.Name} \n" +
             $" #{this.Customer.Id} \n" +
           $"Paid: {(this.IsPaid ? "Yes" : "No")} \n" +
           " Lines: \n");
        //cout << fixed << setprecision(2);

        foreach (OrderLine orderLine in OrderLines)
        {
            Console.WriteLine($" Product Name : {orderLine.Product.ProductName} - Quantity : {orderLine.Quantity} - Price {orderLine.Product.ProductPrice} - Line Total : {orderLine.LineTotal}");
        }

        Console.WriteLine($"TOTAL: {CalculateOrderTotal():F2}");
    }


    public void MarkOrderPaid()
    {


        if (this.OrderLines.Count == 0)
        {
            Console.WriteLine("ERROR: cannot pay an empty order.");
            return;
        }

        this.IsPaid = true;
    }

    public double CalculateOrderTotal()
    {
        double total = 0.0;

        foreach (OrderLine orderLine in OrderLines)
        {
            total += orderLine.Quantity * orderLine.Product.ProductPrice;

        }


        if (this.Customer.IsVip)
            total = total * 0.90;

        return total;
    }

}
