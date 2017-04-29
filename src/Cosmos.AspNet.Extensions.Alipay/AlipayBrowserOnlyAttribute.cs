﻿using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cosmos.AspNet.Extensions.Alipay
{
    /// <summary>
    /// 唯支付宝浏览器可访问
    /// </summary>
    public class AlipayBrowserOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 302 跳转目标
        /// </summary>
        public string RedirectUrl { get; }
        private string UserAgent { get; set; }
        private static readonly Regex RegexRule = new Regex(@"Alipay", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 唯支付宝浏览器可访问
        /// </summary>
        /// <param name="message"></param>
        /// <param name="redirectUrl"></param>
        public AlipayBrowserOnlyAttribute(string message = "Alipay Browser Only", string redirectUrl = "")
        {
            Message = message;
            RedirectUrl = redirectUrl;
        }

        /// <summary>
        /// when action is executing...
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserAgent = filterContext.HttpContext.Request.UserAgent;

            if (!string.IsNullOrWhiteSpace(UserAgent) && RegexRule.IsMatch(UserAgent))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(RedirectUrl))
            {
                filterContext.Result = new RedirectResult(RedirectUrl);
                return;
            }

            filterContext.Result = new ContentResult()
            {
                Content = Message
            };
        }
    }
}
