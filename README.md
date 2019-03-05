# Polly.LightInject

```csharp
public void RetryTest()
{
    using (ServiceContainer container = new ServiceContainer())
    {
        container.Register<IFoo, Foo>();
        container.Register<IInterceptor, RetryInterceptor>();
        container.Intercept(sr => sr.ServiceType == typeof(IFoo), sf => sf.GetInstance<IInterceptor>());

        var foo = container.GetInstance<IFoo>();
        Console.WriteLine(foo.GetInt());
    }
}

public interface IFoo
{
    int GetInt();
}

public class Foo : IFoo
{
    [Retryable(3,1000,2,typeof(ArgumentException), typeof(ArgumentException))]
    public int GetInt()
    {
        return 1;
    }
}
