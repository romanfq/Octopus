using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octopus.Proxies.Exceptions
{
    [Serializable]
    public class ProxyException : Exception
    {
        public ProxyException() { }
        public ProxyException(string message) : base(message) { }
        public ProxyException(string message, Exception inner) : base(message, inner) { }
        protected ProxyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
