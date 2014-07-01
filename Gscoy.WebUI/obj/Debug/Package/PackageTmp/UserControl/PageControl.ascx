<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageControl.ascx.cs" Inherits="Gscoy.WebUI.UserControls.PageControl" %>
<%@ Import NameSpace="System.Data" %>
<script type="text/javascript">
    function gopage() {
        var pagecontrol_gopage = document.getElementById('pagecontrol_gopage').value;
        window.location.href = document.getElementById('pagecontrol_pageurl').value.replace('=[0]', '=' + pagecontrol_gopage);
    }
</script>
<div class="pagenumber">
    <span>共&nbsp;<%=AllCount%>&nbsp;条</span>&nbsp;<span>共&nbsp;<%=PageCount%>&nbsp;页</span>&nbsp;
    <%if (ShowPage)
      { %>
    <asp:hyperlink id="hlk_first" Visible="False" runat="server">首页</asp:hyperlink><asp:hyperlink id="hlk_pre" runat="server">上一页</asp:hyperlink>
    <asp:Repeater runat="server" ID="rpt_page"><ItemTemplate><%# ((DataRowView)Container.DataItem)["pageshow"].ToString()%></ItemTemplate></asp:Repeater>
    <asp:hyperlink id="hlk_next" runat="server">下一页</asp:hyperlink><asp:hyperlink id="hlk_last" Visible="False" runat="server">末页</asp:hyperlink><input type="hidden" id="pagecontrol_pageurl" value="<%=pagecontrol_pageurl %>" />
    <span>跳到 <input type="text" value="" id="pagecontrol_gopage" maxlength="5" size="4"/> 页<input id="PageSubmit" type="button" value="go" class="button1" onclick="gopage()" /></span>
    <%} %>
</div>