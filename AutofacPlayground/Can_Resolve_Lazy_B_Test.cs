using System;
using Autofac;
using Xunit;
using Xunit.Abstractions;

namespace AutofacPlayground
{
    public class X
    {
        public readonly Lazy<B> _b;

        public X(Lazy<B> b)
        {
            _b = b;
        }

        public override string ToString()
        {
            // The component implementing B is created the
            // first time Method() is called
            return _b.Value.ToString();
        }
    }

    public class Can_Resolve_Lazy_B_Test
    {
        private readonly ITestOutputHelper _output;

        public Can_Resolve_Lazy_B_Test(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Resolve_B()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.RegisterType<B>();
            builder.RegisterType<X>();

            using (var container = builder.Build())
            {
                using (var lifetimeScope = container.BeginLifetimeScope())
                {
                    var x = lifetimeScope.Resolve<B>();
                    _output.WriteLine(x.ToString());
                }
            }
        }
    }
}