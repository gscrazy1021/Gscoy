<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WapIndex.aspx.cs" Inherits="Gscoy.WebUI.Wap.WapIndex" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.css">
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.js"></script>
    <meta http-equiv="content-type" content="text/html; charset=gb2312">
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>

    <div data-role="page" id="Home">
        <div data-role="header" data-position="fixed" data-theme="d">
            <a href="#Home" class="ui-btn-active ui-state-persist" data-role="button" data-icon="home">��ҳ</a>
            <h1>��ӭ�����ҵ���ҳ</h1>
            <a href="#Search" data-role="button" data-icon="search">����</a>
        </div>

        <div data-role="content">
            <form method="post" action="wapindex.aspx">
                <fieldset data-role="collapsible">
                    <legend>����� - �ҿ����۵���</legend>
                    <label for="name">ȫ����</label>
                    <input type="text" name="text" id="name">
                    <p>ϲ������ɫ��</p>
                    <div data-role="controlgroup">
                        <label for="red">��ɫ</label>
                        <input type="checkbox" name="favcolor" id="red" value="red">
                        <label for="green">��ɫ</label>
                        <input type="checkbox" name="favcolor" id="green" value="green">
                        <label for="blue">��ɫ</label>
                        <input type="checkbox" name="favcolor" id="blue" value="blue">
                    </div>
                    <input type="submit" data-inline="true" value="�ύ">
                </fieldset>
            </form>
        </div>

        <div data-role="footer" data-position="fixed" data-theme="d">

            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="#Weather" data-icon="info">����Ԥ��</a></li>
                    <li><a href="#WechatUserManager" data-icon="grid">΢���û�����</a></li>
                    <li><a href="#" data-icon="star">�Ǳ�</a></li>
                </ul>
            </div>
        </div>
    </div>

    <div data-role="page" id="Search">
        <div data-role="header" data-position="fixed" data-theme="d">
            <a href="#Home" data-role="button" data-icon="home">��ҳ</a>
            <h1>��ӭ�����ҵ���ҳ</h1>
            <a href="#Search" class="ui-btn-active ui-state-persist" data-role="button" data-icon="search">����</a>
        </div>

        <div data-role="content">
            <p>��Щ��ť������ʾ�����κ�Ч����Search</p>
        </div>

        <div data-role="footer" data-position="fixed" data-theme="d">
            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="#" data-icon="plus">����</a></li>
                    <li><a href="#" data-icon="minus">����</a></li>
                    <li><a href="#" data-icon="delete">ɾ��</a></li>
                </ul>
            </div>
        </div>
    </div>

    <div id="Weather" data-role="page">
        <div data-role="header" data-position="fixed" data-theme="d">
            <a href="#Home" data-role="button" data-icon="home">��ҳ</a>
            <h1>��ӭ�����ҵ���ҳ</h1>
            <a href="#Search" class="ui-btn-active ui-state-persist" data-role="button" data-icon="search">����</a>
        </div>

        <div data-role="content">
            <div>
                <label for="fname" class="ui-hidden-accessible">���У�</label>
                <input type="text" placeholder="<% =weather_city %>" id="txtWeatherCity" name="txtWeatherCity">
                <input type="button" value="�ύ" id="weaherSubmit">
                <p id="weatherContent"></p>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#weaherSubmit").click(function () {
                            $.ajax({
                                type: "post",
                                data: { "txtWeatherCity": $("#txtWeatherCity").val() },
                                url: "wapindex.aspx?action=weather",
                                success: function (data, status) {
                                    data = $.trim(data);
                                    $("#weatherContent").text(data);
                                },
                                error: function (data, status) {
                                    alert();
                                }
                            });
                        });
                    });
                </script>
            </div>
        </div>
        <div data-role="footer" data-position="fixed" data-theme="d">
            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="#Weather" data-icon="info">����Ԥ��</a></li>
                    <li><a href="#WechatUserManager" data-icon="grid">΢���û�����</a></li>
                    <li><a href="#" data-icon="star">�Ǳ�</a></li>
                </ul>
            </div>
        </div>
    </div>

    <div id="WechatUserManager" data-role="page">
        <div data-role="header" data-position="fixed" data-theme="d">
            <a href="#Home" data-role="button" data-icon="home">��ҳ</a>
            <h1>��ӭ�����ҵ���ҳ</h1>
            <a href="#Search" class="ui-btn-active ui-state-persist" data-role="button" data-icon="search">����</a>
        </div>

        <div data-role="content">
            <div data-role="collapsible-set" id="groupinfo">
            </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    $.ajax({
                        type: "post",
                        url: "wapindex.aspx?action=usermanager",
                        success: function (data, status) {
                            data = $.trim(data);
                            $("#groupinfo").append(data);
                        },
                        error: function (data, status) {
                            alert();
                        }
                    });
                });
            </script>
        </div>

        <div data-role="footer" data-position="fixed" data-theme="d">

            <div data-role="navbar" data-iconpos="left">
                <ul>
                    <li><a href="#Weather" data-icon="info">����Ԥ��</a></li>
                    <li><a href="#WechatUserManager" data-icon="grid">΢���û�����</a></li>
                    <li><a href="#" data-icon="star">�Ǳ�</a></li>
                </ul>
            </div>
        </div>
    </div>

</body>
</html>
