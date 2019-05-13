using System;
using Autofac;
using Xunit;
using Xunit.Abstractions;

namespace AutofacPlayground
{
    public class FactoryExample
    {
        private readonly A _a;

        public FactoryExample(Func<A> a)
        {
            _a = a();
        }

        public override string ToString() => _a.ToString();
    }

    public class Can_Resolve_B_Using_Factory_Test
    {
        private readonly ITestOutputHelper _output;

        public Can_Resolve_B_Using_Factory_Test(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Resolve_FactoryExample()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.RegisterType<FactoryExample>();

            using (var container = builder.Build())
            {
                using (var lifetimeScope = container.BeginLifetimeScope())
                {
                    var c = lifetimeScope.Resolve<FactoryExample>();
                    _output.WriteLine(c.ToString());
                }
            }
        }
    }
}


