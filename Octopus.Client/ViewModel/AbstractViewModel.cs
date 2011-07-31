using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace Octopus.Client.ViewModel
{
    class AbstractViewModel : ViewModelBase, IViewModel
    {
        public virtual void NotifyLoaded()
        {
        }

        public virtual void NotifyUnloaded()
        {
        }
    }
}
