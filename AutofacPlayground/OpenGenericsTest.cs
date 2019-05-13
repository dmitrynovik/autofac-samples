using System.Collections.Generic;
using Autofac;
using Xunit;

namespace AutofacPlayground
{
    public class OpenGenericsTest
    {
        [Fact]
        public void Can_Resolve_Generic_Collection()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(List<>))
                .As(typeof(ICollection<>))
                .InstancePerLifetimeScope();

            using (var container = builder.Build())
            {
                var collectionOfStrings = container.Resolve<ICollection<string>>();
                Assert.True(collectionOfStrings is List<string>);
            }
        }
    }
}
