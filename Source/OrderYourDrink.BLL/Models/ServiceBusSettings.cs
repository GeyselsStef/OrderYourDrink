using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderYourDrink.BLL.Models
{
    public class ServiceBusSettings
    {
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; set; }

        public IDictionary<string, string> MessageQueues { get; set; }

        public ServiceBusSettings(IConfiguration configuration) {
            _configuration = configuration;
            _configuration.GetSection("ServiceBusSettings").Bind(this);
        }
    }
}
