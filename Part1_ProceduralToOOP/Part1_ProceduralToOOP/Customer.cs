


namespace Part1_ProceduralToOOP;

internal class Customer
{
    public const int MaxCusomers = 50;
    public static int _customerCount { get; private set; } = 0;
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string City { get; }
    public bool IsVip { get; }
    public static List<Customer> Customers { get; private set; } = new();


    private Customer(int id, string name, string email, string city, bool isVip)
    {

        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;

    }

    public static Customer AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (_customerCount >= MaxCusomers)
        {
            Console.WriteLine("ERROR: customer list is full.");
            throw new ArgumentException("List is full");
        }



        if (FindCustomerById(id) is not null)
        {
            Console.WriteLine($"ERROR: customer id  {id}  already exists.");
            throw new Exception("Customer Id Exists");
        }


        Customer customer = new(id, name, email, city, isVip);
        Customers.Add(customer);
        _customerCount++;

        return customer;
    }



    public static Customer? FindCustomerById(int id)
    {
        foreach (Customer customer in Customers)
        {

            if (customer.Id == id) return customer;
        }
        return null;
    }


    public static void PrintCustomers()
    {
        Console.WriteLine($" === CUSTOMERS {_customerCount} === ");
        foreach (Customer customer in Customers)
        {
            Console.WriteLine(
           $"#{customer.Id}  " +
           $"{customer.Name}  " +
           $"<{customer.Email}>  " +
           $"{customer.City}  " +
           $"vip={(customer.IsVip ? "yes" : "no")}");


        }
    }

}

