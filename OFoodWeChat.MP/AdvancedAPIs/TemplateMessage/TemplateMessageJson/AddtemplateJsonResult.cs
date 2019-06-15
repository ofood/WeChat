using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFoodWeChat.MP.Entities;
namespace OFoodWeChat.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// AddtemplateJsonResult
    /// </summary>
    public class AddtemplateJsonResult : WxJsonResult
    {
        public string template_id { get; set; }

    }
}