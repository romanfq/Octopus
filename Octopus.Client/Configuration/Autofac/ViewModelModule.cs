using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Octopus.Client.ViewModel;

namespace Octopus.Client.Configuration.Autofac
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<StatusBarViewModel>();
        }
    }
}
