using Autofac;
using Xunit;
using Xunit.Abstractions;

namespace AutofacPlayground
{
    public class Can_Resolve_B_Using_Lambda
    {
        private readonly ITestOutputHelper _output;

        public Can_Resolve_B_Using_Lambda(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Resolve_B_Using_Lambda_Registration()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.Register(c => new B(c.Resolve<A>())).As<B>();

            using (var container = builder.Build())
            {
                using (var lifetimeScope = container.BeginLifetimeScope())
                {
                    var b = lifetimeScope.Resolve<B>();
                    _output.WriteLine(b.ToString());
                }
            }
        }

    }
}