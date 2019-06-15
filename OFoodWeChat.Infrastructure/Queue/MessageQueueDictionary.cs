using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Queue
{
    public class MessageQueueDictionary : Dictionary<string, MessageQueueItem>
    {
        public MessageQueueDictionary(): base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
