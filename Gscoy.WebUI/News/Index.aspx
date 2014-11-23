<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Gscoy.WebUI.News.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <div class="row block">
        <div id="main-content" class="col-3-3">
            <div class="wrap-col">
                <asp:Repeater ID="newsRpt" runat="server">
                    <ItemTemplate>
                        <article>
                            <div class="heading">
                                <h2><a href="#"><%# Eval("Title") %></a></h2>
                            </div>
                            <div class="content">
                                <%# Eval("Content") %>
                            </div>
                            <div class="info">
                                <p><%# Eval("PubDate") %></p>
                            </div>
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
