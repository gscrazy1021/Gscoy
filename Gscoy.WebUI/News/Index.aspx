<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Gscoy.WebUI.News.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <link href="../Style/css/animation.css" rel="stylesheet" />
    <link href="../Style/css/font-awesome.css" rel="stylesheet" />
    <link href="../Style/css/reset.css" rel="stylesheet" />
    <link href="../Style/css/live-preview.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.js"></script>
    <script src="../Scripts/modernizr.js"></script>
    <script src="../Scripts/motor.js"></script>
    <div class="row block">
        <div id="main-content" class="col-3-3">
            <div class="wrap-col">
                <div class="st-accordion" id="st-accordion">
                    <ul>
                        <asp:Repeater ID="newsRpt" runat="server">
                            <ItemTemplate>
                                <li>
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
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <%--<section class="nav">
                    <asp:Repeater ID="newsRpt" runat="server">
                        <ItemTemplate>
                            <div>
                                <input type="radio" checked="checked" name="lida" id="label-1">
                                <label id="item1" for="label-1"><i id="i1"></i>
                                    <strong><%# Eval("title") %></strong>
                                </label>
                                <div id="a1" class="content" style="display: block;">

                                    <%# Eval("content") %>
                                    <h6>The following collection of Web sites offers some outstanding references on
        CSS and its proper use on well-crafted Web pages. The very first reference
        from <a href="#">W3Schools.com</a> is terrific</h6>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </section>--%>
            </div>
        </div>
    </div>
</asp:Content>
