using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Messages.Events
{
    public interface IPaymentCancelled
    {
        public Guid OrderId { get; set; }
        public string PaymentId { get; }
        public string Products { get; }
        public long CartId { get; }
    }
}
