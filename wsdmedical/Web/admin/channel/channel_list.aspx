<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channel_list.aspx.cs" Inherits="Web.admin.channel.channel_list" %>

<%@ Import Namespace="Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>频道管理</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script src="../../scripts/layer/layer.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript">
        function builerTip(channel_id)
        {
            layer.open({
                type: 2,
                fixed: false, //不固定
                area: ['100%', '100%'],
                content: "ChannelToNavigation.aspx?channel_id="+channel_id
            });
        }
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>频道管理</span>
            <span  style="float:right;font-size:larger; color:red;">注意:此页面慎重操作-非开发者勿动.</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHead" class="toolbar-wrap">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="l-list">
                        <ul class="icon-list">
                            <li><a class="add" id="a_add" runat="server" href="channel_edit.aspx?action=Add"><i></i><span>新增</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','如果该频道下还存在内容则无法删除，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
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
                            <th width="8%">频道Id</th>
                            <th width="8%">调用名</th>
                            <th align="left" width="18%">名称</th>
                            <th width="10%">视频</th>
                            <th width="10%">相册</th>
                            <th width="10%">附件</th>
                            <th width="10%">详情页面</th>
                            <th align="left" width="8%">排序</th>
                            <th width="10%">操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                        </td>
                        <td align="center"><%#Eval("id")%></td>
                         <td align="center"><%#Eval("name")%></td>
                        <td><%#Eval("title")%></td>
                        <td align="center"><%#Convert.ToInt32(Eval("is_video")) == 0 ? "×" : "√"%></td>
                        <td align="center"><%#Convert.ToInt32(Eval("is_albums")) == 0 ? "×" : "√"%></td>
                        <td align="center"><%#Convert.ToInt32(Eval("is_attach")) == 0 ? "×" : "√"%></td>
                        <td align="center"><%#Convert.ToInt32(Eval("is_details")) == 0 ? "×" : "√"%></td>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" /></td>
                        <td align="center"><a href="channel_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&qx=channel_list.aspx&id=<%#Eval("id")%>">修改</a>|<a href="#" onclick="builerTip(<%#Eval("id") %>)">配置信息</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
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
