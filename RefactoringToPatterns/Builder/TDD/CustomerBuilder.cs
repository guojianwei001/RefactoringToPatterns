namespace RefactoringToPatterns.Builder.TDD;

/// <summary>
/// This CustomerBuilder is used in TDD to construct kinds of customer data, not applicable to application code.
/// </summary>
public class CustomerBuilder
{
    private string firstName = "Bill";
    private string lastName = "Gates";
    private string countryCode = "US";
    private decimal monthlyIncome = 15_500M;
    private Address address = new("US", "98052", "Redmond", "3521 NE 16rd St");
    private DateOnly birthday = DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-20));

    public static CustomerBuilder GivenCustomer() => new();

    public CustomerBuilder WithCountryCode(string ccd)
    {
        this.countryCode = ccd;
        return this;
    }

    public CustomerBuilder WithName(string first, string last)
    {
        (this.firstName, this.lastName) = (first, last);
        return this;
    }

    public CustomerBuilder BornOn(DateOnly birthDate)
    {
        this.birthday= birthDate;
        return this;
    }

    public CustomerBuilder WithIncome(decimal income)
    {
        this.monthlyIncome = income;
        return this;
    }

    public CustomerBuilder WithAddress(string country, string zip, string city, string street)
    {
        this.address = new Address(country, zip, city, street);
        return this;
    }

    public Customer Build()
    {
        return new Customer
        (
            this.firstName,
            this.lastName,
            this.countryCode,
            this.birthday,
            this.monthlyIncome,
            address
        );
    }
}

public class Customer
{
    public string First { get; }
    public string Last { get; }
    public string CountryCode { get; }
    public DateOnly Birthday { get; }
    public decimal MonthlyIncome { get; }
    public Address Address { get; }

    public Customer
    (
        string firstName,
        string lastName,
        string countryCode,
        DateOnly birthday,
        decimal monthlyIncome,
        Address address
    )
    {
        if (string.IsNullOrWhiteSpace(countryCode) == true)
            throw new ArgumentException("countryCode cannot be null");

        if (string.IsNullOrWhiteSpace(firstName) == true ||
            string.IsNullOrWhiteSpace(lastName) == true)
            throw new ArgumentException("firstName and lastName cannot be null");

        if (monthlyIncome <= 0)
            throw new ArgumentException("Monthly income cannot be less than 0");

        if (address == null)
            throw new ArgumentException("Address cannot be null");

        if (birthday == default)
            throw new ArgumentException("Birthday cannot be empty");

        First = firstName;
        Last = lastName;
        CountryCode = countryCode;
        Birthday = birthday;
        MonthlyIncome = monthlyIncome;
        Address = address;
    }
}

public class Address
{
    public string Country { get; }
    public string ZipCode { get; }
    public string City { get; }
    public string Street { get; }

    public Address(string country, string zipCode, string city, string street)
    {
        Country = country;
        ZipCode = zipCode;
        City = city;
        Street = street;
    }
}