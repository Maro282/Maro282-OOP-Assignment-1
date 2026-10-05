

namespace Part3_BuilderPattern;

public class OldInvoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; }

    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingState { get; }
    public string ShippingZipCode { get; }
    public string ShippingCountry { get; }
    public string BillingStreet { get; }
    public string BillingCity { get; }
    public string BillingState { get; }
    public string BillingZipCode { get; }
    public string BillingCountry { get; }

    public DateTime OrderDate { get; }
    public OldPaymentMethod PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;


    public OldInvoice(
      OldInvoiceBuilder obj)
    {
        InvoiceId = obj.InvoiceId;
        CustomerName = obj.CustomerName;
        CustomerEmail = obj.CustomerEmail;
        CustomerPhone = obj.CustomerPhone;

        ShippingStreet = obj.ShippingStreet;
        ShippingCity = obj.ShippingCity;
        ShippingState = obj.ShippingState;
        ShippingZipCode = obj.ShippingZipCode;
        ShippingCountry = obj.ShippingCountry;

        BillingStreet = obj.BillingStreet;
        BillingCity = obj.BillingCity;
        BillingState = obj.BillingState;
        BillingZipCode = obj.BillingZipCode;
        BillingCountry = obj.BillingCountry;

        PaymentMethod = obj.PaymentMethod;
        Currency = obj.Currency;
        SubTotal = obj.SubTotal;
        DiscountAmount = obj.DiscountAmount;
        TaxAmount = obj.TaxAmount;
        OrderDate = obj.OrderDate;
    }


}



public enum OldPaymentMethod
{
    Cash,
    Visa,
    Wallet
}



public class OldInvoiceBuilder
{
    // Mandatory fields
    public string CustomerName { get; }
    public string CustomerPhone { get; }
    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingCountry { get; }
    public OldPaymentMethod PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public DateTime OrderDate { get; }

    // Optional fields with defaults
    public int InvoiceId { get; private set; }
    public string CustomerEmail { get; private set; }
    public string ShippingState { get; private set; }
    public string ShippingZipCode { get; private set; }
    public string BillingStreet { get; private set; }
    public string BillingCity { get; private set; }
    public string BillingState { get; private set; }
    public string BillingZipCode { get; private set; }
    public string BillingCountry { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }

    public OldInvoiceBuilder(
        string customerName,
        string customerPhone,
        string shippingStreet,
        string shippingCity,
        string shippingCountry,
        OldPaymentMethod paymentMethod,
        string currency,
        decimal subTotal,
        DateTime orderDate)
    {
        CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
        CustomerPhone = customerPhone ?? throw new ArgumentNullException(nameof(customerPhone));
        ShippingStreet = shippingStreet ?? throw new ArgumentNullException(nameof(shippingStreet));
        ShippingCity = shippingCity ?? throw new ArgumentNullException(nameof(shippingCity));
        ShippingCountry = shippingCountry ?? throw new ArgumentNullException(nameof(shippingCountry));
        PaymentMethod = paymentMethod;
        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        SubTotal = subTotal;
        OrderDate = orderDate;
    }



    public OldInvoiceBuilder WithInvoiceId(int invoiceId)
    {
        InvoiceId = invoiceId;
        return this;
    }

    public OldInvoiceBuilder WithCustomerEmail(string customerEmail)
    {
        CustomerEmail = customerEmail;
        return this;
    }

    public OldInvoiceBuilder WithShippingStateAndZip(string state, string zipCode)
    {
        ShippingState = state;
        ShippingZipCode = zipCode;
        return this;
    }

    public OldInvoiceBuilder WithBillingAddress(string street, string city, string state, string zipCode, string country)
    {
        BillingStreet = street;
        BillingCity = city;
        BillingState = state;
        BillingZipCode = zipCode;
        BillingCountry = country;
        return this;
    }

    public OldInvoiceBuilder WithDiscount(decimal discountAmount)
    {
        DiscountAmount = discountAmount;
        return this;
    }

    public OldInvoiceBuilder WithTax(decimal taxAmount)
    {
        TaxAmount = taxAmount;
        return this;
    }

    public OldInvoice Build()
    {
        return new OldInvoice(this);
    }
}