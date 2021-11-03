using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SecurrencyTDS.WalletManager.Application.Extensions
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(Dependencies).Assembly);
        }
    }
}
