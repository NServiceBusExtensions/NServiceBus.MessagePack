using Xunit.Abstractions;

public class Tests :
    XunitLoggingBase
{
    public Tests(ITestOutputHelper output) :
        base(output)
    {
    }
}