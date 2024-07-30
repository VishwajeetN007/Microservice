using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Messages.Events
{
    public interface IOrderCancelled
    {
        public Guid OrderId { get; }
        public string PaymentId { get; }
        public long CartId { get; }
    }
}
