<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WapIndex.aspx.cs" Inherits="Gscoy.WebUI.Wap.WapIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8"/>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.css"/>
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.3.2/jquery.mobile-1.3.2.min.js"></script>
</head>
<body>

<div data-role="page">
 <div data-role="header"> 
  <div data-role="navbar">
    <ul>
      <li><a href="#anylink">首页</a></li>
      <li><a href="#anylink">页面二</a></li>
      <li><a href="#anylink">搜索</a></li>
    </ul>
  </div>
</div>

  <div data-role="content">
 <div data-role="collapsible">
    <h4>A</h4>
    <ul data-role="listview">
      <li><a href="#">Adam</a></li>
      <li><a href="#">Angela</a></li>
    </ul>
  </div>

    <div data-role="collapsible">
    <h4>B</h4>
    <ul data-role="listview">
       <li><a href="#">Bill</a></li>
       <li><a href="#">Bob</a></li>
    </ul>
    </div>

    <div data-role="collapsible">
    <h4>C</h4>
    <ul data-role="listview">
      <li><a href="#">Calvin</a></li>
      <li><a href="#">Cameron</a></li>
      <li><a href="#">Christina</a></li>
    </ul>
    </div>
  </div>

  <div data-role="footer">
 <div data-role="navbar">
    <ul>
      <li><a href="#anylink">首页1</a></li>
      <li><a href="#anylink">页面二1</a></li>
      <li><a href="#anylink">搜索1</a></li>
    </ul>
  </div>
  </div>
</div> 

</body>
</html>

