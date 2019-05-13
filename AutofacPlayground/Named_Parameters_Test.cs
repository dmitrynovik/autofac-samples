using Autofac;
using FluentAssertions;
using Xunit;

namespace AutofacPlayground
{
    public class CreditCard {  }

    public class GoldCard : CreditCard
    {
        public GoldCard(string accountId) {  }
    }

    public class StandardCard : CreditCard
    {
        public StandardCard(string accountId) { }
    }

    public class Named_Parameters_Test
    {
        [Fact]
        public void Resolves_Credit_Card_With_Named_Parameter()
        {
            var builder = new ContainerBuilder();
            builder.Register<CreditCard>(
                    (c, p) =>
                    {
                        var accountId = p.Named<string>("accountId");
                        if (accountId.StartsWith("9"))
                        {
                            return new GoldCard(accountId);
                        }

                        return new StandardCard(accountId);
                    });

            using (var container = builder.Build())
            {

                var card = container.Resolve<CreditCard>(new NamedParameter("accountId", "12345"));
                card.GetType().Name.Should().Be("StandardCard");
            }
        }
    }
}
