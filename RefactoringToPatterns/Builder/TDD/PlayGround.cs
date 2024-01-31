namespace RefactoringToPatterns.Builder.TDD;

public class PlayGround
{
    public static void Test()
    {
        var customer = CustomerBuilder.GivenCustomer()
            .BornOn(new DateOnly(1971, 5, 10))
            .WithAddress("American", "110235", "Los Angeles", "ST 15th")
            .WithCountryCode("US")
            .WithIncome(987_123m)
            .Build();

        Console.WriteLine(customer.Address.City);
    }

    public static void GetFullNameReturnsCombination()
    {
        // Arrange
        Employee emp = new EmployeeBuilder()
            .WithFirstName("Kenneth")
            .WithLastName("Truyers");

        // Act
        string fullname = emp.getFullName();

        // Assert
        Console.WriteLine("Kenneth Truyers");
        Console.WriteLine(fullname);
    }

    public static void GetAgeReturnsCorrectValue()
    {
        // Arrange
        Employee emp = new EmployeeBuilder()
            .WithBirthDate(new DateTime(1983, 1, 1));

        // Act
        int age = emp.getAge();

        // assert
        Console.WriteLine(DateTime.Today.Year - 1983);
        Console.WriteLine(age);
    }
}