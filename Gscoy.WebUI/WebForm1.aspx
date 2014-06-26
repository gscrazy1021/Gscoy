<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Gscoy.WebUI.WebForm1" %>

<%@ Register Src="~/UserControl/PageControl.ascx" TagName="PageControl" TagPrefix="pc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pc:PageControl ID="pc" runat="server" />
        </div>
    </form>
</body>
</html>
