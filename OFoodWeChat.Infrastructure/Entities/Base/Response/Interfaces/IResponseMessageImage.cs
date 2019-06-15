using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFoodWeChat.Infrastructure.Entities
{
    public interface IResponseMessageImage: IResponseMessageBase
    {
        Image Image { get; set; }
    }
}
