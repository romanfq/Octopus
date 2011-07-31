using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.Client.ViewModel
{
    interface IViewModel
    {
        void NotifyLoaded();
        void NotifyUnloaded();
    }
}
