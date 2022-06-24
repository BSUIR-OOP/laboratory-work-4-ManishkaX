namespace DepInj.Exeptions;

public class UnregisteredTypeException: Exception
{
    public UnregisteredTypeException()
    {
        
    }

    public UnregisteredTypeException(string message) : base(message)
    {
        
    }
}