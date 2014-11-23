<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Gscoy.WebUI.Blog.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <div class="row block">
        <div id="main-content" class="col-3-3">
            <div class="wrap-col">
                <asp:Repeater ID="blogRpt" runat="server">
                    <ItemTemplate>
                        <article>
                            <div class="heading">
                                <h2><a href="#">Sed accumsan libero quis mi commodo et suscipit</a></h2>
                            </div>
                            <div class="content">
                                <img src="<%=imgHost %>/images/img1.jpg" width="250px" height="100px" />
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam viverra convallis auctor. Sed accumsan libero quis mi commodo et suscipit enim lacinia. Morbi rutrum vulputate est sed faucibus. Nulla sed nisl mauris, id tristique tortor. Sed iaculis dapibus urna nec dictum. Morbi rutrum vulputate est sed faucibus. Nulla sed nisl mauris, id tristique tortor. Sed iaculis dapibus urna nec dictum [...]</p>

                            </div>
                            <div class="info">
                                <p>By Admin on December 01, 2012 - <a href="#">01 Commnets</a></p>
                            </div>
                        </article>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <%=barHtml %>
    </div>
</asp:Content>
