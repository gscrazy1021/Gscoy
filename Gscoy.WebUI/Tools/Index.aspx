<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Gscoy.WebUI.Tools.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <div class="row block">
        <div id="main-content" class="col-3-2">
            <form action="Index.aspx" method="post">
                <table style="width=90%; align-content: center; border-bottom-width: 1px">
                    <tr>
                        <td>
                            <textarea style="border: 1px; height: 100px;" onmouseover="this.focus();this.select();" id="content" name="content"><%=content %></textarea></td>
                    </tr>
                    <tr>
                        <td>
                            <select id="charsetSelect" name="charsetSelect">
                                <option value="utf-8">utf-8</option>
                                <option value="gb2312">gb2312</option>
                            </select>
                            <%= SelectScript("charsetSelect",charset) %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="submit" style="width: 120px;" value="UrlEncode编码" name="en"></td>
                        <td>
                            <input type="submit" style="width: 120px;" value="UrlDecode解码" name="de"></td>
                    </tr>
                </table>

            </form>
        </div>
    </div>
</asp:Content>
