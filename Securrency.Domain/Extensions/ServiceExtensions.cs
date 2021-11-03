using Microsoft.Extensions.DependencyInjection;
using SecurrencyTDS.Domain.Logging;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SecurrencyTDS.Domain.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }


        public static string GetSHA512(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);


                return hash;
            }
        }
    }

        
}
