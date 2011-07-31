using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Octopus.Proxies.Services;
using Octopus.Proxies;

namespace Octopus.Client.Configuration.Autofac
{
    public class ProxiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // proxy
            builder
                .Register(ctx => new ProxyConfiguration("tcp://localhost:5555"))
                .As<IProxyConfiguration>();
            
            // services
            builder
                .RegisterType<PingService>()
                .As<IPingService>()
                .SingleInstance();
        }
    }
}
