using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Mq.Service.Model
{
    public class RabbitQueueDto
    {
        public RabbitQueueDto()
        {
            NumberOfAttempts = 1;
            Key = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
        public Guid Key { get; set; }
        public DateTime Timestamp { get; set; }
        public int NumberOfAttempts { get; set; }
        public string Data { get; set; }
    }
}
