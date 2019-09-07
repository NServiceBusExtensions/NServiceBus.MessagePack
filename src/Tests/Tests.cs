using Xunit.Abstractions;

public class Tests :
    XunitApprovalBase
{
    public Tests(ITestOutputHelper output) :
        base(output)
    {
    }
}