


using Part3_BuilderPattern;

public class OrderPayment
{

    public DateTime OrderDate { get; }
    public PaymentMethod PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;


    public OrderPayment(DateTime date, PaymentMethod paymentMethod, string currency, decimal subtotal, decimal discount, decimal tax)
    {

        OrderDate = date;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subtotal;
        DiscountAmount = discount;
        TaxAmount = tax;
    }


}


public enum PaymentMethod
{
    Cash,
    Visa,
    Wallet
}


public class OrderPaymentBuilder
{

    private DateTime _orderDate { get; }
    private PaymentMethod _paymentMethod { get; }
    private string _currency { get; }
    private decimal _subTotal { get; }
    private decimal _discountAmount { get; set; }
    private decimal _taxAmount { get; set; }

    public OrderPaymentBuilder(DateTime date, PaymentMethod paymentMethod, string currency, decimal subTotal)
    {
        _orderDate = date;
        _paymentMethod = paymentMethod;
        _currency = currency;
        _subTotal = subTotal;
    }

    public OrderPaymentBuilder SetDiscount(decimal discount)
    {
        _discountAmount = discount; return this;
    }
    public OrderPaymentBuilder SetTax(decimal tax)
    {
        _taxAmount = tax; return this;
    }

    public OrderPayment Build()
    {
        return new OrderPayment(_orderDate, _paymentMethod, _currency, _subTotal, _discountAmount, _taxAmount);
    }


}
