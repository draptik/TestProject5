namespace TestProject5;

public class UnitTest1
{
    [Fact]
    public Task Test1()
    {
        var homer = new Person(1, "Homer", "Simpson");
        return Verify(homer);
    }
}

public record Person(int Id, string FirstName, string LastName);