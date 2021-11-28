using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace z_AdminLTE
{
    public class DbFactory : IDbFactory
    {
        //private readonly IServiceProvider _services;
        private readonly IOptionsMonitor<DapperOptions> _optionsMonitor;

        public DbFactory(IServiceProvider services, IOptionsMonitor<DapperOptions> optionsMonitor)
        {
            //_services = services ?? throw new ArgumentNullException(nameof(services));
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
        }

        public MyDbBase CreateClient(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var client = new MyDbBase(new ConnectionConfig { });
            var option = _optionsMonitor.Get(name).DapperActions.FirstOrDefault();
            if (option != null)
                option(client.CurrentConnectionConfig);
            else
                throw new ArgumentNullException(nameof(option));

            return client;
        }
    }
}
