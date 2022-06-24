using System.Text;
using DependencyLibrary;
using DependencyLibrary.Exceptions;
using DepInj.CycleTests;
using DepInj.SingletonTests;
using DepInj.TransientTests;

namespace DepInj;

public static class Program
{
    private static void Main(string[] args)
    {
        
        var container = new DependencyInjectionContainer();
        container.RegisterType<StringBuilder>();
        container.RegisterType<string>();
        
        container.RegisterType<Inner>();
        container.RegisterType<Outer>();
        container.GetTransient<Inner>();
        var outer = container.GetTransient<Outer>();
        var outer2 = container.GetTransient<Outer>();
        Console.WriteLine($"{outer == outer2}");

        container.RegisterType<Singleton>();
        var single = container.GetSingleton<Singleton>();
        var single2 = container.GetSingleton<Singleton>();
        Console.WriteLine($"{single == single2}");

        try
        {
            container.RegisterType<First>();
            container.RegisterType<Second>();
            container.GetTransient<First>();
        }
        catch (CyclicException e)
        {
            Console.WriteLine("Cyclic Dependency");
        }

        Console.Read();
        
    }
}