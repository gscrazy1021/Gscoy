using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Gscoy.Common.Web
{
    public class JsHelper
    {

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message)
        {
            string js = @"<script type='text/javascript'>alert('" + message + "');</script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 弹出JavaScript小窗口,此重载函数是为了解决Alert(string message)函数弹出提示框后样式丢失的问题
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        /// <param name="page">页面</param>
        public static void Alert(string message, System.Web.UI.Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.ClientScript.GetType(), "BaseDataAlertScript", string.Format("<script type=\"text/javascript\" >alert(\"{0}\");</script>", message));
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            #region
            string js = "<script type='text/javascript'>alert('{0}');window.location.href='{1}';</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }
        public static void ConfirmAndRedirect(string message, string toURL)
        {
            #region
            var str = string.Format("<script type='text/javascript'>var r= confirm('{0}');if(r==true)window.location.href='{1}';else window.history.back();</script>", message, toURL);
            HttpContext.Current.Response.Write(str);
            #endregion
        }

        public static void ConfirmAndClose(string message, string toURL)
        {
            #region
            var str = string.Format("<script type='text/javascript'>var r= confirm('{0}');if(r==true)window.location.href='{1}';else window.close();</script>", message, toURL);
            HttpContext.Current.Response.Write(str);
            #endregion
        }
        /// <summary>
        /// 弹出消息框并且重新加载当前页
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndReload(string message)
        {
            string js = "<script type='text/javascript'>alert('{0}');window.location.reload();</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }
        /// <summary>
        /// 弹出消息框并且转向到主页面新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirectParent(string message, string toURL)
        {
            #region
            string js = "<script type='text/javascript'>alert('{0}');window.parent.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }

        /// <summary>
        /// 弹出信息并返回
        /// </summary>
        /// <param name="message"></param>
        public static void AlertAndGoBack(string message)
        {
            #region
            string js = "<script type='text/javascript'>alert('{0}');window.history.back();</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
            #endregion
        }

        public static void AlertAndClose(string message)
        {
            string js = "<script type='text/javascript'>alert('{0}');window.close();</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));            
        }

        public static void ToRedirect(string toURL)
        {

            string js = "<script type='text/javascript'>window.location.href='{0}';</script>";
            HttpContext.Current.Response.Write(string.Format(js, toURL));

        }
        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script type='text/javascript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 刷新父页面
        /// </summary>
        public static void RefreshTopPage()
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.parent.location.reload()</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            string js = @"<Script type='text/javascript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script type='text/javascript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 转向Url指定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 转向Url制定的页面（top页）
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void RedirectTopHref(string url)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.top.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            #region
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script type='text/javascript'>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }

    }
}
