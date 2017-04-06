<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_edit.aspx.cs" Inherits="Web.admin.order.order_edit" %>

<%@ Import Namespace="Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>查看订单</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnPrint").click(function () { OrderPrint(); });       //打印订单
        });
        //打印订单
        function OrderPrint() {
            var winDialog = top.dialog({
                title: '打印订单',
                url: 'dialog/dialog_print.aspx?order_no=' + $("#spanOrderNo").text(),
                width: 850
            }).showModal();
        }
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="express_list.aspx" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="order_list.aspx"><span>订单管理</span></a>
            <i class="arrow"></i>
            <span>订单详细</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div id="floatHead" class="content-tab-wrap">
            <div class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a class="selected" href="javascript:;">订单详细信息</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <dl id="dlUserInfo" runat="server">
            <dt></dt>
            <dd>
                <div class="table-container">
                    <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
                        <tr>
                            <th width="20%">会员账户</th>
                            <td><asp:Literal ID="lbUserName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th width="20%">订单号</th>
                            <td><span id="spanOrderNo"><asp:Literal ID="litBh" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <th width="20%">服务类别</th>
                            <td><span id="span1"><asp:Literal ID="litFwtype" runat="server"></asp:Literal></span></td>
                        </tr>
                         <tr>
                            <th width="20%">支付状态</th>
                            <td><span id="span3"><asp:Literal ID="litZt" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <th width="20%">下订单日期</th>
                            <td><span id="span2"><asp:Literal ID="litUyy" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <th width="20%">预约时间</th>
                            <td><span id="span5"><asp:Literal ID="litYysj" runat="server"></asp:Literal></span></td>
                        </tr>
                         <tr id="yydw" runat="server" visible="false">
                            <th width="20%">预约单位</th>
                            <td><span id="span6"><asp:Literal ID="litYydw" runat="server"></asp:Literal></span></td>
                        </tr>
                         <tr id="Yyjz" runat="server" visible="false">
                            <th width="20%">预约截至</th>
                            <td><span id="span10"><asp:Literal ID="litYYjz" runat="server"></asp:Literal></span></td>
                        </tr>
                        <asp:Panel ID="Pjj" runat="server" Visible="false">
                        <tr>
                            <th width="20%">服务类型</th>
                            <td><span id="span4"><asp:Literal ID="litJjlx" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <th width="20%">目的城市</th>
                            <td><span id="span7"><asp:Literal ID="litMdcy" runat="server"></asp:Literal></span></td>
                        </tr>
                         <tr>
                            <th width="20%">航空公司</th>
                            <td><span id="span8"><asp:Literal ID="litHkgs" runat="server"></asp:Literal></span></td>
                        </tr>
                        <tr>
                            <th width="20%">航班号</th>
                            <td><span id="span9"><asp:Literal ID="litHbh" runat="server"></asp:Literal></span></td>
                        </tr>
                         </asp:Panel>
                    </table>

                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable" style="font-size:14px;">
                        </HeaderTemplate>
                        <ItemTemplate>
                          <tr >
                            <td align="center" rowspan="3" style="width:150px;"> <img src="<%#Eval("img_url") %>" alt="" /></td>
                         </tr>
                          <tr>
                            <td><%#Eval("title") %>&nbsp;<asp:Literal ID="litJsr" runat="server" Text='<%#new BLL.dtorderBll().Getjsr(Convert.ToString(Eval("id")),litBh.Text,lbUserName.Text) %>' Visible="false"></asp:Literal>
                                <asp:Literal ID="litOzt" runat="server"></asp:Literal></td>
                          </tr>
                           <tr>
                                <td align="left"><asp:LinkButton ID="litBtn" runat="server" CommandArgument='<%#Eval("id") %>'  OnClick="litBtn_Click">设为订单接受人</asp:LinkButton></td>
                           </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                          <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
                        </table>
                        </FooterTemplate>
                     </asp:Repeater>
                </div>
            </dd>
        </dl>
        <!--/内容-->
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-wrap">
             <%--   <input id="btnPrint" type="button" value="打印订单" class="btn violet" />--%>
                <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
        </div>
        <!--/工具栏-->

    </form>
</body>
</html>
