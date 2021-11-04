using System;
using System.Collections.Generic;
using System.Text;

namespace TDS.Domain.Authorization
{
    public class StellarSettings
    {
        public string ServerUrl { get; set; }
        public int FetchLimit { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyFilter { get; set; }
        public double TDSSynchronizationTimeInSec { get; set; }
    }
    public class JWTSettings
    {
        public string SecurityKey { get;  set; }
        public bool ValidateSigningKey { get;  set; }
        public bool ValidateIssuer { get;  set; }
        public string Issuer { get;  set; }
        public bool ValidateAudience { get;  set; }
        public string Audience { get;  set; }
        public bool ValidateLifeTime { get;  set; }
        public double DateToleranceMinutes { get;  set; }
        public double TimeOutSeconds { get; set; }
    }
}
