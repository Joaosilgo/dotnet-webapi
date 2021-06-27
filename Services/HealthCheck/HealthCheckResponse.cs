using System;
using System.Collections.Generic;

namespace dotnet_webapi.Services.HealthCheck
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public IEnumerable<HealthCheck> checks { get; set; }

        public TimeSpan Duration { get; set; }
    }
}