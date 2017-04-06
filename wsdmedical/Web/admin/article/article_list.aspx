﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="article_list.aspx.cs" Inherits="Web.admin.article.article_list" %>

<%@ Import Namespace="Common" %>

<%--说明:--%>
<%--这里的权限用js控制.感觉控制效果不是特别好--%>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>内容管理</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.lazyload.min.js"></script>
    <%--延迟加载--%>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/authority.js"></script>
    <script type="text/javascript">
        $(function () {
            //图片延迟加载
            $(".pic img").lazyload({ effect: "fadeIn" });
            //点击图片链接
            $(".pic img").click(function () {
                var linkUrl = $(this).parent().parent().find(".foot a").attr("href");
                if (linkUrl != "") {
                    location.href = linkUrl; //跳转到修改页面
                }
            });
        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>内容列表</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHead" class="toolbar-wrap">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="l-list">
                        <ul class="icon-list">
                            <li id="Add">
                                <a class="add" runat="server" id="a_add"><i></i><span>新增</span></a>
                            </li>
                            <li id="Save">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>
                            <li id="Audit">
                                <asp:LinkButton ID="btnAudit" runat="server" CssClass="lock" OnClientClick="return ExePostBack('btnAudit','审核后前台将显示该信息，确定继续吗？');" OnClick="btnAudit_Click"><i></i><span>审核</span></asp:LinkButton></li>
                            <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li id="Delete">
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                        <div class="menu-list">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlCategoryId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryId_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlProperty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProperty_SelectedIndexChanged">
                                    <asp:ListItem Value="" Selected="True">所有属性</asp:ListItem>
                                    <asp:ListItem Value="isLock">待审核</asp:ListItem>
                                    <asp:ListItem Value="unIsLock">已审核</asp:ListItem>
                                    <asp:ListItem Value="unIsShow">不显示</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                        <asp:LinkButton ID="lbtnViewImg" runat="server" CssClass="img-view" OnClick="lbtnViewImg_Click" ToolTip="图像列表视图" />
                        <asp:LinkButton ID="lbtnViewTxt" runat="server" CssClass="txt-view" OnClick="lbtnViewTxt_Click" ToolTip="文字列表视图" />
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container">
            <!--文字列表-->
            <asp:Repeater ID="rptList1" runat="server" OnItemCommand="rptList_ItemCommand">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                        <tr>
                            <th width="6%">选择</th>
                            <th align="left">标题</th>
                            <th align="left" width="12%">所属类别</th>
                            <th align="left" width="16%">发布时间</th>
                            <th align="left" class="Edit" width="65">排序</th>
                            <th width="10%" class="Edit">操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                        </td>
                        <td>
                            <a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&qx=article_list.aspx&channel_id=<%#channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a>
                        </td>
                        <td><%#new BLL.article_categoryBll().GetTitle(Convert.ToInt32(Eval("category_id")))%></td>
                        <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
                        <td class="Edit">
                            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" /></td>
                        <td class="Edit" align="center">
                            <a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&qx=article_list.aspx&channel_id=<%#channel_id %>&id=<%#Eval("id")%>">修改</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList1.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
  </table>
                </FooterTemplate>
            </asp:Repeater>
            <!--/文字列表-->

            <!--图片列表-->
            <asp:Repeater ID="rptList2" runat="server" OnItemCommand="rptList_ItemCommand">
                <HeaderTemplate>
                    <div class="imglist">
                        <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <div class="details<%#Eval("img_url").ToString() != "" ? "" : " nopic"%>">
                            <div class="check">
                                <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
                                <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                            </div>
                            <%#Eval("img_url").ToString() != "" ? "<div class=\"pic\"><img src=\"../skin/default/loadimg.gif\" data-original=\"" + Eval("img_url") + "\" /></div><i class=\"absbg\"></i>" : ""%>
                            <h1><span><a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&qx=article_list.aspx&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></span></h1>
                            <div class="tools">
                                <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeypress="return (/[\d]/.test(String.fromCharCode(event.keyCode)));" />
                            </div>
                            <div class="foot">
                                <p class="time"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("add_time"))%></p>
                                <a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&qx=article_list.aspx&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>" title="编辑" class="edit">编辑</a>
                            </div>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList2.Items.Count == 0 ? "<div align=\"center\" style=\"font-size:12px;line-height:30px;color:#666;\">暂无记录</div>" : ""%>
    </ul>
  </div>
                </FooterTemplate>
            </asp:Repeater>
            <!--/图片列表-->
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
