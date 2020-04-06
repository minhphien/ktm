using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KMS.Product.Ktm.Services.SlackService
{
    public static class SlackClientHelper
    {
        public static void AddSlackClient(this IServiceCollection service, IConfiguration configuration, bool optional = true)
        {
            SlackFixture.Intialize(configuration, optional);
        }
    }
}
