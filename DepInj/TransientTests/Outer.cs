using System.Text;

namespace DepInj.TransientTests;

public abstract class Outer
{
    protected Outer(Inner inner, StringBuilder builder)
    {
        
    }
}