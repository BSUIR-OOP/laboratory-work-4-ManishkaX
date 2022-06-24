namespace DepInj.SingletonTests;

public class Singleton
{
    private static int? _instance;

    public static int Instance
    {
        get => _instance ??= new int();
        set => _instance = value;
    }
}