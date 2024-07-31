namespace RefactoringToPatterns.Builder.TDD;

// the complete code is from https://www.kenneth-truyers.net/2013/07/15/flexible-and-expressive-unit-tests-with-the-builder-pattern/
/*
0. decoupled the construction from the constructor and provided an API(builder) for constructing employee-objects.
1. Only relevant data for that particular unit test is present in the test. default+override pattern
2. When the constructor changes, we only need to modify the builder, our existing tests will remain untouched.
3. The Builder is part of your test suite. In general, it should live inside your test-project, not inside the production code.
 */
public class EmployeeBuilder
{
    private int id = 1;
    private string firstname = "first";
    private string lastname = "last";
    private DateTime birthdate = DateTime.Today;
    private string street = "street";

    /// <summary>
    /// make the code a bit more concise than Build() method by using a c# feature called operator overloading. 
    /// </summary>
    /// <param name="instance"></param>
    public static implicit operator Employee(EmployeeBuilder instance) => instance.Build();

    public EmployeeBuilder WithFirstName(string firstname)
    {
        this.firstname = firstname;
        return this;
    }

    public EmployeeBuilder WithLastName(string lastname)
    {
        this.lastname = lastname;
        return this;
    }

    public EmployeeBuilder WithBirthDate(DateTime birthdate)
    {
        this.birthdate = birthdate;
        return this;
    }

    public EmployeeBuilder WithStreet(string street)
    {
        this.street = street;
        return this;
    }

    public Employee Build()
    {
        return new Employee(id, firstname, lastname, birthdate, street);
    }
}

public class Employee
{
    public Employee(int id, string firstname, string lastname, DateTime birthdate, string street)
    {
        this.ID = id;
        this.FirstName = firstname;
        this.LastName = lastname;
        this.BirthDate = birthdate;
        this.Street = street;
    }

    public int ID { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime BirthDate { get; }
    public string Street { get; }

    public string getFullName()
    {
        return this.FirstName + " " + this.LastName;
    }

    public int getAge()
    {
        DateTime today = DateTime.Today;
        int age = today.Year - BirthDate.Year;

        if (BirthDate > today.AddYears(-age))
            age--;

        return age;
    }
}