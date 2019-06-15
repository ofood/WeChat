/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GiftCardResultJson.cs
    文件功能描述：礼品卡返回结果

----------------------------------------------------------------*/
using OFoodWeChat.MP.Entities;
using System.CodeDom;
using System.Collections.Generic;

namespace OFoodWeChat.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 设置支付后投放卡券接口返回结果
    /// </summary>
    public class AddCardAfterPayResultJson : WxJsonResult
    {
        public string rule_id { get; set; }
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public List<string> succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 设置失败的mchid列表
    /// </summary>
    public class Fail_mchid_Item
    {
        public string mchid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public int occupy_rule_id { get; set; }
        public string occupy_appid { get; set; }
    }

    /// <summary>
    /// 查询支付后投放卡券规则详情接口返回结果
    /// </summary>
    public class AfterPay_GetByIdResultJson : WxJsonResult
    {
        public Rule_Info rule_info { get; set; }
    }

    /// <summary>
    /// 批量查询支付后投放卡券规则详情接口返回结果
    /// </summary>
    public class AfterPay_BatchGetResultJson : WxJsonResult
    {
        public int total_count { get; set; }
        public List<Rule_Info> rule_list { get; set; }
    }

    /// <summary>
    /// 增加支付即会员规则接口返回结果
    /// </summary>
    public class AddPayMemberRuleResultJson : WxJsonResult
    {
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public string[] succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 删除支付即会员规则接口返回结果
    /// </summary>
    public class DeletePayMemberRuleResultJson : WxJsonResult
    {
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public List<Fail_mchid_Item> succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 查询商户号支付即会员规则接口返回结果
    /// </summary>
    public class GetPayMemberRuleResultJson : WxJsonResult
    {
        public string card_id { get; set; }
        public string occupy_appid { get; set; }
        public bool is_locked { get; set; }
    }

}
