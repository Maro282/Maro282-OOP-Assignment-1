

namespace Part1_ProceduralToOOP;

internal class OrderLine
{
    public int Quantity { get; private set; }
    public Product Product { get; }

    public OrderLine(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public double LineTotal => Product.ProductPrice * Quantity;

}
