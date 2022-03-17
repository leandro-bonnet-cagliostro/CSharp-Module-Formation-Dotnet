using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Core.Exceptions
{
    [Serializable] // parce qu'on hérite de la classe Exception
    public class BusinessException : Exception
    {
        public BusinessException() { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message,innerException) { }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info,context) { }
     }
}
