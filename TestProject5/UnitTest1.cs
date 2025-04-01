using System.Diagnostics;

namespace TestProject5;

public class UnitTest1
{
    [Fact]
    public Task Test1_works_since_Verify_version_29_1_0()
    {
        var homer = new Person(1, "Homer changed", "Simpson");
        return Verify(homer);
    }
        
    [Fact]
    public void Test2_doesnt_open_vscode_diff_viewer_when_executed_from_vscode()
    {
        var info = new ProcessStartInfo(@"%PROGRAMFILES%\Microsoft VS Code\Code.exe");
        info.CreateNoWindow = true;
        info.Arguments = @"--diff ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.received.txt"" ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.verified.txt""";
   
        Process.Start(info);
    }
    [Fact]
    public void Test3_doesnt_open_vscode_diff_viewer_when_executed_from_vscode()
    {
        var info = new ProcessStartInfo(@"C:\Program Files\Microsoft VS Code\Code.exe");
        info.CreateNoWindow = true;
        info.Arguments = @"--diff ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.received.txt"" ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.verified.txt""";
   
        Process.Start(info);
    }

    [Fact]
    public void Test4_doesnt_open_vscode_diff_viewer_when_executed_from_vscode()
    {
        var info = new ProcessStartInfo(@"%PROGRAMFILES%\Microsoft VS Code\bin\code.cmd");
        info.CreateNoWindow = true;
        info.Arguments = @"--diff ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.received.txt"" ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.verified.txt""";
   
        Process.Start(info);
    }

    [Fact]
    public void Test5_opens_vscode_diff_viewer_when_executed_from_vscode()
    {
        // This works as expected:
        var info = new ProcessStartInfo(@"C:\Program Files\Microsoft VS Code\bin\code.cmd");
        info.CreateNoWindow = true;
        info.Arguments = @"--diff ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.received.txt"" ""C:\tmp\TestProject5\TestProject5\UnitTest1.Test1.verified.txt""";
   
        Process.Start(info);
    }
}

public record Person(int Id, string FirstName, string LastName);

