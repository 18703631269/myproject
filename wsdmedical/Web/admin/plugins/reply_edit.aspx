<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply_edit.aspx.cs" Inherits="Web.admin.plugins.reply_edit" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>回复留言</title>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="index.aspx"><span>物流配送</span></a>
  <i class="arrow"></i>
  <span>其他管理</span>
  <i class="arrow"></i>
  <span>在线留言</span>
  <i class="arrow"></i>
  <span>回复留言</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">查看留言</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>联系人</dt>
    <dd><%=rly.user_name %></dd>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd><%=rly.user_tel %></dd>
  </dl>
  <dl>
    <dt>联系QQ</dt>
    <dd><%=rly.user_qq %></dd>
  </dl>
  <dl>
    <dt>电子邮箱</dt>
    <dd><%=rly.user_email %></dd>
  </dl>
  <dl>
    <dt>留言时间</dt>
    <dd><%=rly.add_time %></dd>
  </dl>
  <dl>
    <dt>留言标题</dt>
    <dd><%=rly.title %></dd>
  </dl>
  <dl>
    <dt>留言内容</dt>
    <dd><%=rly.content %></dd>
  </dl>
  <dl>
    <dt>回复留言</dt>
    <dd><asp:TextBox ID="txtReContent" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" " /></dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="回复留言" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
