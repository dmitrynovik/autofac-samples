using System;
using Autofac;
using Xunit;
using Xunit.Abstractions;

namespace AutofacPlayground
{
    public class A : IDisposable
    {
        private bool _disposed;

        public override string ToString() => _disposed ? throw new ObjectDisposedException("I am disposed") : "A";

        public void Dispose() => _disposed = true;
    }

    public class B
    {
        private readonly A _a;

        public B(A a)
        {
            _a = a;
        }

        public override string ToString() => _a.ToString();
    }

    public class FactoryExample
    {
        private readonly A _a;

        public FactoryExample(Func<A> a)
        {
            _a = a();
        }

        public override string ToString() => _a.ToString();
    }

    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Can_Resolve_B()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.RegisterType<B>();

            using (var container = builder.Build())
            {
                using (var lifetimeScope = container.BeginLifetimeScope("outer"))
                {
                    var b = lifetimeScope.Resolve<B>();
                    _output.WriteLine(b.ToString());
                }
            }
        }

        [Fact]
        public void Can_Resolve_C()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.RegisterType<FactoryExample>();

            using (var container = builder.Build())
            {
                using (var lifetimeScope = container.BeginLifetimeScope("outer"))
                {
                    var c = lifetimeScope.Resolve<FactoryExample>();
                    _output.WriteLine(c.ToString());
                }
            }
        }
















        [Fact(Skip="TBD")]
        public void Cannot_Resolve_B()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<A>();
            builder.RegisterType<B>().SingleInstance();

            B b;
            using (var container = builder.Build())
            {
                using (var outerScope = container.BeginLifetimeScope("outer"))
                {
                    using (var innerScope = outerScope.BeginLifetimeScope("inner"))
                    {
                        b = innerScope.Resolve<B>();
                    }
                    _output.WriteLine(b.ToString());
                }
            }
        }
    }
}
