﻿/*----------------------------------------------------------------

    文件名：OrderResult.cs
    文件功能描述：根据订单ID获取订单详情返回结果
----------------------------------------------------------------*/


using System.Collections.Generic;
using OFoodWeChat.MP.Entities;

namespace OFoodWeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 根据订单ID获取订单详情返回结果
    /// </summary>
    public class GetByIdOrderResult : WxJsonResult
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        public Order order { get; set; }
    }

    public class Order
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 订单总价格(单位 : 分)
        /// </summary>
        public int order_total_price { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string order_create_time { get; set; }
        /// <summary>
        /// 订单运费价格(单位 : 分)
        /// </summary>
        public int order_express_price { get; set; }
        /// <summary>
        /// 买家微信OPENID
        /// </summary>
        public string buyer_openid { get; set; }
        /// <summary>
        /// 买家微信昵称
        /// </summary>
        public string buyer_nick { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receiver_name { get; set; }
        /// <summary>
        /// 收货地址省份
        /// </summary>
        public string receiver_province { get; set; }
        /// <summary>
        /// 收货地址城市
        /// </summary>
        public string receiver_city { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string receiver_address { get; set; }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string receiver_mobile { get; set; }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string receiver_phone { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 商品价格(单位 : 分)
        /// </summary>
        public int product_price { get; set; }
        /// <summary>
        /// 商品SKU
        /// </summary>
        public string product_sku { get; set; }
        /// <summary>
        /// 商品个数
        /// </summary>
        public int product_count { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string product_img { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 物流公司编码
        /// </summary>
        public string delivery_company { get; set; }
        /// <summary>
        /// 交易ID
        /// </summary>
        public string trans_id { get; set; }

        /// <summary>
        /// 收货人区
        /// </summary>
        public string receiver_zone { get; set; }
    }

    /// <summary>
    /// 根据订单状态/创建时间获取订单详情返回结果
    /// </summary>
    public class GetByFilterResult : WxJsonResult
    {
        public List<Order> order_list { get; set; }
    }
}

