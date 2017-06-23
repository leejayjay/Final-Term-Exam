using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Models
{
    public class Order
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CustID { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustName { get; set; }
        /// <summary>
        /// 聯絡人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        public string ContactTitle { get; set; }
    }
}