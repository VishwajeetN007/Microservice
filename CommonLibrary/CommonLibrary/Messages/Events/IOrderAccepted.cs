using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Messages.Events
{
    public interface IOrderAccepted
    {
        public Guid OrderId { get; }
        public string PaymentId { get; }
        public string Products { get; }
        public long CartId { get; }
        public DateTime AcceptedDateTime { get; set; }
    }
}
