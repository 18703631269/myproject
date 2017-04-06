<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channel_edit.aspx.cs" Inherits="Web.admin.channel.channel_edit" ValidateRequest="false" %>
<%@ Import namespace="Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑频道</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
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
  <a href="channel_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="channel_list.aspx"><span>频道管理</span></a>
  <i class="arrow"></i>
  <span>编辑频道</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">基本信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
    <dl>
    <dt>调用名称</dt>
    <dd>
      <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" errormsg="请填写正确的名称！" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*调用名称，只允许使用英文、数字或下划线。</span>
    </dd>
  </dl>
  <dl>
    <dt>站点名称</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server"  CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*标题备注，允许中文。</span></dd>
  </dl>
   <dl>
    <dt>上传视频</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsVideo" runat="server" />
      </div>
      <span class="Validform_checktip">*开启上传视频</span>
    </dd>
  </dl>
    <dl>
    <dt>开启相册</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsAlbums" runat="server" />
      </div>
      <span class="Validform_checktip">*开启相册功能后可上传多张图片</span>
    </dd>
  </dl>
  <dl>
    <dt>开启附件</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsAttach" runat="server" />
      </div>
      <span class="Validform_checktip">*开启附件功能后可上传多个附件。</span>
    </dd>
  </dl>
     <dl>
    <dt>开启详情页面</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsDetail" runat="server" />
      </div>
      <span class="Validform_checktip">*开启详情页面后可添加四个详情列表</span>
    </dd>
  </dl>

     <dl>
    <dt>翻译类型</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsFylx" runat="server" />
      </div>
      <span class="Validform_checktip">*开启翻译人员服务类别 如生活翻译，医疗翻译</span>
    </dd>
  </dl>

    <dl>
    <dt>所在城市</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsCity" runat="server" />
      </div>
      <span class="Validform_checktip">*开启翻译人员所在城市</span>
    </dd>
  </dl>

     <dl>
    <dt>所在医院</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsHospl" runat="server" />
      </div>
      <span class="Validform_checktip">*开启翻译人员所在医院</span>
    </dd>
  </dl>

     <dl>
    <dt>专业学历状态</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsZyxlzt" runat="server" />
      </div>
      <span class="Validform_checktip">*开启人员的详细信息中</span>
    </dd>
  </dl>

     <dl>
    <dt>收费方式</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsPaymoney" runat="server" />
      </div>
      <span class="Validform_checktip">*开启人员的收费方式 如时薪，日薪等</span>
    </dd>
  </dl>

     <dl>
    <dt>住宿类型</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsZslx" runat="server" />
      </div>
      <span class="Validform_checktip">*开启住宿类型 如 民宿等</span>
    </dd>
  </dl>

   <dl>
    <dt>目的城市</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsMdcity" runat="server" />
      </div>
      <span class="Validform_checktip">*开启接机的目的城市</span>
    </dd>
  </dl>

   <dl>
    <dt>所在机场</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsSzjc" runat="server" />
      </div>
      <span class="Validform_checktip">*开启人员去机场接机</span>
    </dd>
  </dl>
     <dl>
    <dt>预约保证金</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsYybzj" runat="server" />
      </div>
      <span class="Validform_checktip">*开启预约保证金</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>