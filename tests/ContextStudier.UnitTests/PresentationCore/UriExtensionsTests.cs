using ContextStudier.Presentation.Core.Extensions;

namespace ContextStudier.UnitTests.PresentationCore
{
    public class UriExtensionsTests
    {
        [Fact]
        public void AddQuery_ConstructsProperly()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" },
            };
            var uri = new Uri("/api/action/", UriKind.Relative);
            var expected = uri.ToString() + "?" + 
                parameters.First().Key + "=" + parameters.First().Value + "&" +
                parameters.Last().Key + "=" + parameters.Last().Value;

            var result = uri
                .AddQuery(parameters.First().Key, parameters.First().Value)
                .AddQuery(parameters.Last().Key, parameters.Last().Value)
                .ToString();

            Assert.Equal(expected, result);
        }
    }
}
