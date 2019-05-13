using Autofac;
using Autofac.Features.Indexed;
using Xunit;

namespace AutofacPlayground
{
    public class DerivedB : B {
        public DerivedB(A a) : base(a)
        {
        }
    }

    public class AnotherDerivedB : B {
        public AnotherDerivedB(A a) : base(a)
        {
        }
    }

    public class Keyed
    {
        IIndex<string, B> _b;
        public Keyed(IIndex<string, B> b) { _b = b; }

        public string M()
        {
            var b = _b["first"];
            return b.ToString();
        }
    }


    public class Keyed_Services_Test
    {
        [Fact]
        public void Can_Resolve_Keyed_Service()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DerivedB>().Keyed<B>("first");
            builder.RegisterType<AnotherDerivedB>().Keyed<B>("second");
            builder.RegisterType<A>();
            builder.RegisterType<Keyed>();

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var keyed = scope.Resolve<Keyed>();
                    keyed.M();
                }
            }
        }
    }
}
