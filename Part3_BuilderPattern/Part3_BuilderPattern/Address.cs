
namespace Part3_BuilderPattern;

public class Address
{

    public string Street { get; }
    public string City { get; }
    public string? State { get; }
    public string? ZipCode { get; }
    public string Country { get; }


    internal Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;

    }



}


public class AddressBuilder
{

    // Mandatory
    private string _street { get; }
    private string _city { get; }
    private string _country { get; }


    //Optional
    private string? _state { get; set; }
    private string? _zipCode { get; set; }


    internal AddressBuilder(string street, string city, string country)
    {

        if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country))
            throw new InvalidOperationException("Street, city and country are required.");

        _street = street;
        _city = city;
        _country = country;

    }

    public AddressBuilder SetState(string state)
    {
        _state = state; return this;
    }

    public AddressBuilder SetZipCode(string zipcode)
    {
        _zipCode = zipcode; return this;
    }

    public Address Build()
    {
        return new Address(_street, _city, _state, _zipCode, _country);

    }

}
