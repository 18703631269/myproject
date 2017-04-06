<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="article_edit.aspx.cs" Inherits="Web.admin.article.article_edit" ValidateRequest="false" %>

<%@ Import Namespace="Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>编辑内容</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script src="../../scripts/webuploader/webuploader.min.js"></script>
    <script src="../../scripts/webuploader/kindeditor-min.js"></script>
    <script src="../js/uploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script src="../../scripts/layui/layui.js"></script>
    <link href="../../scripts/layui/css/layui.css" rel="stylesheet" />
    <!-- 配置文件 -->
    <script type="text/javascript" src="../../ueditor/ueditor.config.js"></script>
    <!-- 编辑器源码文件 -->
    <script type="text/javascript" src="../../ueditor/ueditor.all.js"></script>
    <!-- 实例化编辑器 -->
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
            //初始化上传控件
            var ue = UE.getEditor('content');
            var ue_one = UE.getEditor('txtContent_one');
            var ue_two = UE.getEditor('txtContent_two');
            var ue_three = UE.getEditor('txtContent_three');
            var ue_four = UE.getEditor('txtContent_four');
            //上传图片
            $(".upload-img").InitUploader({ filesize: "1024", sendurl: "../../tools/file_upload.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "gif,jpg,png,bmp,rar,zip,doc,xls,txt" });
            //上传视频
            $(".upload-video").InitUploader({ filesize: "10240000", sendurl: "../../tools/file_upload.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "mp4,mp3" });

            //批量上传图片
            $(".upload-album").InitUploader({ btntext: "批量上传", multiple: true, water: true, thumbnail: true, filesize: "102400", sendurl: "../../tools/file_upload.ashx", swf: "../../scripts/webuploader/uploader.swf" });

            //设置封面图片的样式
            $(".photo-list ul li .img-box img").each(function () {
                if ($(this).attr("src") == $("#hidFocusPhoto").val()) {
                    $(this).parent().addClass("selected");
                }
            });

            //创建上传附件
            $(".attach-btn").click(function () {
                showAttachDialog();
            });
        });

        //初始化附件窗口
        function showAttachDialog(obj) {
            var objNum = arguments.length;
            var attachDialog = top.dialog({
                id: 'attachDialogId',
                title: "上传附件",
                url: 'dialog/dialog_attach.aspx',
                width: 500,
                height: 180,
                onclose: function () {
                    var liHtml = this.returnValue; //获取返回值
                    if (liHtml.length > 0) {
                        $("#showAttachList").children("ul").append(liHtml);
                    }
                }
            }).showModal();
            //如果是修改状态，将对象传进去
            if (objNum == 1) {
                attachDialog.data = obj;
            }
        }
        //删除附件节点
        function delAttachNode(obj) {
            $(obj).parent().remove();
        }


        function changeit() {
            alert(getAllHtml());
            $("#contenthidden").val(getAllHtml());
        }
        //   加载layer的编辑框
        //   layui.use('layedit', function () {
        //       var layedit = layui.layedit;
        //       layedit.build('content', {
        //           tool: ['face', 'link', 'unlink', '|', 'left', 'center', 'right']
        //, height: 200
        //       })
        //   });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <asp:HiddenField ID="Hsql" runat="server" />
         <asp:HiddenField ID="Hkql" runat="server" />
        <!--导航栏-->
        <div class="location">
            <a href="article_list.aspx?channel_id=<%=this.channel_id %>" class="back"><i></i><span>返回列表页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <a href="article_list.aspx?channel_id=<%=this.channel_id %>"><span>内容管理</span></a>
            <i class="arrow"></i>
            <span>编辑内容</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div id="floatHead" class="content-tab-wrap">
            <div class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a class="selected" href="javascript:;">基本信息</a></li>
                        <li id="field_tab_item" runat="server" visible="false"><a href="javascript:;">详情页面</a></li>
                        <%--visible="false"--%>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>所属类别</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlCategoryId" runat="server" datatype="*" sucmsg=" " AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryId_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>显示状态</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0">待审核</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                            <asp:ListItem Value="2">不显示</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>是否热点</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbIsHot" runat="server" />
                    </div>
                    <span class="Validform_checktip">热点内容</span>
                </dd>
            </dl>
            <dl>
                <dt>内容标题</dt>
                <dd>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">*标题最多100个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>关键字</dt>
                <dd>
                    <asp:TextBox ID="txt_gjz" runat="server" CssClass="input normal" datatype="*0-100" sucmsg=" " />
                    <span class="Validform_checktip">*关键字最多100个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>排序数字</dt>
                <dd>
                    <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                    <span class="Validform_checktip">*数字，越小越向前</span>
                </dd>
            </dl>
            <dl>
                <dt>浏览次数</dt>
                <dd>
                    <asp:TextBox ID="txtClick" runat="server" CssClass="input small" datatype="n" sucmsg=" ">0</asp:TextBox>
                    <span class="Validform_checktip">点击浏览该信息自动+1</span>
                </dd>
            </dl>
            <dl>
                <dt>发布时间</dt>
                <dd>
                    <asp:TextBox ID="txtAddTime" runat="server" CssClass="input rule-date-input" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}\s{1}(\d{1,2}:){2}\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
                    <span class="Validform_checktip">不选择默认当前发布时间</span>
                </dd>
            </dl>
            <dl>
                <dt>封面图片</dt>
                <dd>
                    <asp:Image ID="imgbeginPic" runat="server" ImageUrl="~/images/noneimg.jpg" Style="max-height: 80px; display: none;" />
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" OnTextChanged="txtImgUrl_TextChanged" />
                    <div class="upload-box upload-img"></div>
                </dd>
            </dl>
            <dl id="div_video_container" runat="server" visible="false">
                <dt>视频上传</dt>
                <dd>
                    <asp:TextBox ID="txtVedio" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-video"></div>
                </dd>
            </dl>
            <dl id="div_albums_container" runat="server" visible="false">
                <dt>图片相册</dt>
                <dd>
                    <div class="upload-box upload-album"></div>
                    <input type="hidden" name="hidFocusPhoto" id="hidFocusPhoto" runat="server" class="focus-photo" />
                    <div class="photo-list">
                        <ul>
                            <asp:Repeater ID="rptAlbumList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input type="hidden" name="hid_photo_name" value="<%#Eval("id")%>|<%#Eval("original_path")%>|<%#Eval("thumb_path")%>" />
                                        <input type="hidden" name="hid_photo_remark" value="<%#Eval("remark")%>" />
                                        <div class="img-box" onclick="setFocusImg(this);">
                                            <img src="<%#Eval("thumb_path")%>" bigsrc="<%#Eval("original_path")%>" />
                                            <span class="remark"><i><%#Eval("remark").ToString() == "" ? "暂无描述..." : Eval("remark").ToString()%></i></span>
                                        </div>
                                        <a href="javascript:;" onclick="setRemark(this);">描述</a>
                                        <a href="javascript:;" onclick="delImg(this);">删除</a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>
            <dl id="div_attach_container" runat="server" visible="false">
                <dt>上传附件</dt>
                <dd>
                    <a class="icon-btn add attach-btn"><span>添加附件</span></a>
                    <div id="showAttachList" class="attach-list">
                        <ul>
                            <asp:Repeater ID="rptAttachList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <input name="hid_attach_id" type="hidden" value="<%#Eval("id")%>" />
                                        <input name="hid_attach_filename" type="hidden" value="<%#Eval("file_name")%>" />
                                        <input name="hid_attach_filepath" type="hidden" value="<%#Eval("file_path")%>" />
                                        <input name="hid_attach_filesize" type="hidden" value="<%#Eval("file_size")%>" />
                                        <i class="icon"></i>
                                        <a href="javascript:;" onclick="delAttachNode(this);" class="del" title="删除附件"></a>
                                        <a href="javascript:;" onclick="showAttachDialog(this);" class="edit" title="更新附件"></a>
                                        <div class="title"><%#Eval("file_name")%></div>
                                        <div class="info">类型：<span class="ext"><%#Eval("file_ext")%></span> 大小：<span class="size"><%#Convert.ToInt32(Eval("file_size")) > 1024 ? Convert.ToDouble((Convert.ToDouble(Eval("file_size")) / 1024f)).ToString("0.0") + "MB" : Eval("file_size") + "KB"%></span> 下载：<span class="down"><%#Eval("down_num")%></span>次</div>
                                        <div class="btns">下载积分：<input type="text" name="txt_attach_point" onkeydown="return checkNumber(event);" value="<%#Eval("point")%>" /></div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </dd>
            </dl>           
            <dl>
                <dt>文章内容</dt>
                <dd>
                    <div>
                        <textarea id="content" name="content" runat="server">
                         </textarea>
                    </div>
                </dd>
            </dl>
        </div>
        <!--/内容-->
        <div class="tab-content" style="display: none">
            <dl>
                <dt>详情列表1</dt>
                <dd>
                    <asp:TextBox ID="txtlist_one" runat="server" CssClass="input" datatype="*0-20" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*标题最多20个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>详情描述1</dt>
                <dd>
                    <div>
                        <textarea id="txtContent_one" class="txtContent_one" runat="server"></textarea>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>详情列表2</dt>
                <dd>
                    <asp:TextBox ID="txtlist_two" runat="server" CssClass="input" datatype="*0-20" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*标题最多20个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>详情描述2</dt>
                <dd>
                    <div>
                        <textarea id="txtContent_two" class="txtContent_two" runat="server"></textarea>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>详情列表3</dt>
                <dd>
                    <asp:TextBox ID="txtlist_three" runat="server" CssClass="input" datatype="*0-20" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*标题最多20个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>详情描述3</dt>
                <dd>
                    <div>
                        <textarea id="txtContent_three" class="txtContent_three" runat="server"></textarea>
                    </div>

                </dd>
            </dl>
             <dl>
                <dt>详情列表4</dt>
                <dd>
                    <asp:TextBox ID="txtlist_four" runat="server" CssClass="input" datatype="*0-20" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*标题最多20个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>详情描述4</dt>
                <dd>
                    <div>
                        <textarea id="txtContent_four" class="txtContent_four" runat="server"></textarea>
                    </div>

                </dd>
            </dl>
        </div>
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-wrap">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
        </div>
        <!--/工具栏-->

    </form>
</body>
</html>
