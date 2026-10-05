
using Part3_BuilderPattern;

// Task3.2

OldInvoice oldInvoice = new OldInvoiceBuilder("marwan",
                                           "01275311282",
                                           "10 str",
                                           "alexandria",
                                           "egypt",
                                           OldPaymentMethod.Wallet, "egp", 1500, DateTime.Now).WithBillingAddress("10", "cairo", "cairo", "010", "egypt")
                                           .Build();





// Task3.3
var shippingAddres = new AddressBuilder("10 str", "Alexandria", "Egypt")
                                       .SetState("Alex")
                                       .SetZipCode("03")
                                       .Build();

var orderPaymentInfo = new OrderPaymentBuilder(DateTime.Now, PaymentMethod.Wallet, "EGP", 11110)
    .SetTax(250)
    .SetDiscount(0.30m)
    .Build();

Invoice invoice = new InvoiceBuilder(33, "Marwan", "01275311282", shippingAddres, orderPaymentInfo).Build();

invoice.CustomerEmail = "marwan@gmail.com";
Console.WriteLine(invoice.BillingAddress.Country);

