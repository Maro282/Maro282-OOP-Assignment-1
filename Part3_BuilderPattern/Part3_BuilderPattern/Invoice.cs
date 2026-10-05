

namespace Part3_BuilderPattern;

public class Invoice
{

    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; }
    public Address ShippingAddres { get; }
    public Address? BillingAddress { get; }
    public OrderPayment OrderPaymentInfo { get; }


    public Invoice(
      int invoiceId,
      string customerName,
      string customerEmail,
      string customerPhone,
      Address shippingAddress, Address billingAddress, OrderPayment orderPaymentInfo
      )
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        ShippingAddres = shippingAddress;
        BillingAddress = billingAddress;
        OrderPaymentInfo = orderPaymentInfo;
    }





}




public class InvoiceBuilder
{
    // mandatory properties
    private int _invoiceId { get; }
    private string _customerName { get; }
    private string _customerPhone { get; }
    private Address _shippingAddress { get; set; }
    private OrderPayment _orderPaymentInfo { get; set; }


    // Optional Data
    private string? _customerEmail { get; set; }
    private Address? _billingAddress { get; set; }

    public InvoiceBuilder(
       int invoiceId,
       string customerName,
       string customerPhone,
       Address shippingAddress,
       OrderPayment orderPaymentInfo
      )
    {
        _invoiceId = invoiceId;
        _customerName = customerName;
        _customerPhone = customerPhone;
        _shippingAddress = shippingAddress;
        _orderPaymentInfo = orderPaymentInfo;

    }

    public InvoiceBuilder SetCustomerEmail(string email)
    {
        _customerEmail = email;
        return this;
    }
    public InvoiceBuilder SetBillingAddress(Address billingAddress)
    {
        _billingAddress = billingAddress;
        return this;
    }



    public Invoice Build()
    {
        return new Invoice(
       _invoiceId, _customerName, _customerEmail, _customerPhone, _shippingAddress, _billingAddress ?? _shippingAddress, _orderPaymentInfo);


    }


}