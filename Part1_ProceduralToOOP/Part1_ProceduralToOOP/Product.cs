

namespace Part1_ProceduralToOOP;

internal class Product
{
    public const int MaxProducts = 50;
    private static int _productCount = 0;
    public int ProductId { get; }
    public string ProductName { get; }
    public double ProductPrice { get; }
    public int ProductStock { get; private set; }

    public static List<Product> Products = new();


    private Product(int id, string name, double price, int inStock)
    {
        ProductId = id;
        ProductName = name;
        ProductPrice = price;
        ProductStock = inStock;

    }


    public static Product? FindProductById(int id)
    {
        foreach (Product product in Products)
        {

            if (product.ProductId == id) return product;
        }

        return null;
    }


    public static Product AddProduct(int id, string name, double price, int stock)
    {
        if (_productCount >= MaxProducts)
        {
            Console.WriteLine("ERROR: product list is full.");
            throw new ArgumentException();
        }

        if (FindProductById(id) is not null)
        {
            Console.WriteLine($" ERROR: product id  {id}  already exists.\n");
            throw new ArgumentException();
        }

        Product product = new Product(id, name, price, stock);

        Products.Add(product);
        _productCount++;
        return product;
    }


    public void ReduceStock(int quantity)
    {

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive");
        }

        if (quantity > ProductStock)
        {
            throw new ArgumentException("Quantity more than stock");
        }

        ProductStock -= quantity;
    }


    public static void PrintProducts()
    {
        Console.WriteLine($"=== PRODUCTS {_productCount} === ");
        foreach (Product product in Products)
        {
            Console.WriteLine(
           $"#{product.ProductId}  " +
           $" {product.ProductName}  " +
           $" Price: {product.ProductPrice}>  " +
           $" In stock:{product.ProductStock}  ");

        }
    }
}
