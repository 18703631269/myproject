<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelToNavigation.aspx.cs" Inherits="Web.admin.channel.ChannelToNavigation" %>
<%@ Import Namespace="Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>频道管理/频道信息</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--列表-->
        <div class="table-container">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                        <tr>
                            <th width="8%">频道Id</th>
                            <th width="8%">调用名称</th>
                            <th align="left" width="18%">标题</th>
                            <th width="10%">显示</th>
                            <th width="10%">权限控制</th>
                            <th width="10%">排序</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center"><%#Eval("id")%></td>
                        <td align="center"><%#Eval("name")%></td>
                        <td><%#Eval("title")%></td>
                        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 0 ? "×" : "√"%></td>
                        <td><%# Utils.ExsitsString(Convert.ToString(Eval("action_type"))) %></td>
                        <td align="center"><%#Eval("sort_id") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
  </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--/列表-->
    </form>
</body>
</html>
