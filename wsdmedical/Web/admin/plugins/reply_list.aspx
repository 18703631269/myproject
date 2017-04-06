<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply_list.aspx.cs" Inherits="Web.admin.plugins.reply_list" %>


<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>在线留言管理</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>其他管理</span>
            <i class="arrow"></i>
            <span>在线留言</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHead" class="toolbar-wrap">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="lbtnUnLock" runat="server" CssClass="lock" OnClientClick="return ExePostBack('lbtnUnLock','审核后前台将显示该信息，确定继续吗？');" OnClick="lbtnUnLock_Click"><i></i><span>审核</span></asp:LinkButton></li>
                            <li>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('lbtnDelete');" OnClick="lbtnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                        <div class="menu-list">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlProperty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                                    <asp:ListItem Value="" Selected="True">所有属性</asp:ListItem>
                                    <asp:ListItem Value="isLock">未审核</asp:ListItem>
                                    <asp:ListItem Value="unLock">已审核</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                        <tr>
                            <th width="8%">选择</th>
                            <th align="left">标题</th>
                            <th width="8%" align="left">审核状态</th>
                            <th width="12%" align="left">用户</th>
                            <th width="16%" align="left">发布时间</th>
                            <th width="8%">状态</th>
                            <th width="10%">操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                        </td>
                        <td><a href="reply_edit.aspx?qx=reply_list.aspx&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
                        <td><%# Convert.ToInt32(Eval("is_lock")) == 0 ? "(未审核)" : "[已审核]"%></td>
                        <td><%#Eval("user_name")%></td>
                        <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
                        <td align="center">
                            <%#Eval("reply_content").ToString() == "" ? "未回复" : "已回复"%>
                        </td>
                        <td align="center"><a href="reply_edit.aspx?qx=reply_list.aspx&id=<%#Eval("id")%>">回复</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%>
</table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"
                    OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
