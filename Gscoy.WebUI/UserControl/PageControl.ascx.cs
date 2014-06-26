using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gscoy.WebUI.UserControls
{
    public partial class PageControl : System.Web.UI.UserControl
    {
        private string pagename = string.Empty;
        private string pageurl = string.Empty;
        private int pagecount = 0;
        private int curpage = 0;
        private int showcount = 0;
        private int allcount = 0;

        protected bool ShowPage = true;
        protected string pagecontrol_pageurl = string.Empty;

        //控件名称
        public string PageName
        {
            set
            {
                if (value.Trim().Length > 0)
                {
                    pagename = value.Trim();
                }
            }
        }
        //显示的页数
        public int ShowCount
        {
            set
            { showcount = value; }
            get { return showcount; }
        }
        //链接
        public string PageUrl
        {
            set
            {
                pageurl = value;
            }
            get { return pageurl; }
        }
        //总页数
        public int PageCount
        {
            set { pagecount = value; }
            get { return pagecount; }
        }
        //当前页码
        public int CurPage
        {
            set { curpage = value; }
            get { return curpage; }
        }
        //总数
        public int AllCount
        {
            set { allcount = value; }
            get { return allcount; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.DataBind();
            }
        }

        public override void DataBind()
        {
            base.DataBind();

            if (pagecount <= 1)
                ShowPage = false;

            hlk_pre.Visible = false;
            hlk_first.Visible = false;
            hlk_next.Visible = false;
            hlk_last.Visible = false;

            if (curpage > 1 && curpage <= pagecount)
            {
                hlk_pre.Visible = true;
                hlk_first.Visible = true;

                hlk_first.NavigateUrl = pageurl.Replace(pagename + "=" + curpage, pagename + "=1");
                hlk_pre.NavigateUrl = pageurl.Replace(pagename + "=" + curpage, pagename + "=" + (curpage - 1));
            }
            if (curpage < pagecount)
            {
                hlk_next.Visible = true;
                hlk_last.Visible = true;

                hlk_last.NavigateUrl = pageurl.Replace(pagename + "=" + curpage, pagename + "=" + pagecount);
                hlk_next.NavigateUrl = pageurl.Replace(pagename + "=" + curpage, pagename + "=" + (curpage + 1));
            }
            DataTable dt = GetUrl();
            rpt_page.DataSource = dt;
            rpt_page.DataBind();
            dt.Dispose();

            pagecontrol_pageurl = pageurl.Replace(pagename + "=" + curpage, pagename + "=[0]");
        }

        public DataTable GetUrl()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("pageshow", typeof(string));

            if (showcount <= 0)
                showcount = 5;
            int temp = showcount / 2;

            if (temp >= curpage)
            {
                if (pagecount < showcount)
                {
                    for (int i = 1; i <= pagecount; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["pageshow"] = "<a href='" + pageurl.Replace(pagename + "=" + curpage, pagename + "=" + i) + "'>" + i + "</a>";
                        if (i == curpage)
                        {
                            dr["pageshow"] = "<span class='current'>" + i + "</span>";
                        }
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    for (int i = 1; i <= showcount; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["pageshow"] = "<a href='" + pageurl.Replace(pagename + "=" + curpage, pagename + "=" + i) + "'>" + i + "</a>";
                        if (i == curpage)
                        {
                            dr["pageshow"] = "<span class='current'>" + i + "</span>";
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                if (curpage > pagecount - temp)
                {
                    if (pagecount >= showcount)
                    {
                        for (int i = pagecount - showcount + 1; i <= pagecount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["pageshow"] = "<a href='" + pageurl.Replace(pagename + "=" + curpage, pagename + "=" + i) + "'>" + i + "</a>";
                            if (i == curpage)
                            {
                                dr["pageshow"] = "<span class='current'>" + i + "</span>";
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= pagecount; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["pageshow"] = "<a href='" + pageurl.Replace(pagename + "=" + curpage, pagename + "=" + i) + "'>" + i + "</a>";
                            if (i == curpage)
                            {
                                dr["pageshow"] = "<span class='current'>" + i + "</span>";
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    for (int i = curpage - temp; i <= curpage + temp; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["pageshow"] = "<a href='" + pageurl.Replace(pagename + "=" + curpage, pagename + "=" + i) + "'>" + i + "</a>";
                        if (i == curpage)
                        {
                            dr["pageshow"] = "<span class='current'>" + i + "</span>";
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
    }
}