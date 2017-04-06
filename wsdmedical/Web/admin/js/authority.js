/*针对权限的设定*/
$(function () {
    //<%--说明:--%>
    //<%--这里的权限用js控制.感觉控制效果不是特别好,已改成cs页面控制--%>

    //var AuthorityArrays = ["Show", "Add", "Save", "Edit", "Delete", "Audit"];//权限数组
    //var tname = window.location.pathname;
    //var pagename = tname.substr(tname.lastIndexOf('/') + 1);

    //$.ajax({
    //    type: "post",
    //    url: "../../tools/admin_ajax.ashx?action=GetPageAuthority",//获取页面权限
    //    data: { pagename: pagename },
    //    dataType: 'text',
    //    async: false,
    //    success: function (data) {
    //        if (data == "[]" || data == "" || data == []) {
    //            return;
    //        }
    //        var rlt = eval(data);
    //        $.each(rlt, function (n, value) {
    //            for (var i = 0; i < AuthorityArrays.length; i++) {
    //                if (AuthorityArrays[i] == value.action_type) {
    //                    if (value.action_type == "Edit") {
    //                        $(".Edit").css("display", "");
    //                    }
    //                    else {
    //                        $("#" + value.action_type).css("display", "block");
    //                    }
    //                }
    //            }
    //        });
    //    },
    //    error: function () {
    //        alert('网络加载异常.');
    //    },
    //    timeout: 50000
    //});
});