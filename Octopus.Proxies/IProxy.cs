using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Proxies
{
    public interface IProxy
    {
        Task<TResult> Request<TInput, TResult>(TInput input);
    }
}
