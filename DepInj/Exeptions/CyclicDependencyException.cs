namespace DepInj.Exeptions;

public class CyclicDependencyException: Exception
{
    public CyclicDependencyException()
    {
        
    }

    public CyclicDependencyException(string message) : base(message)
    {
        
    }
}