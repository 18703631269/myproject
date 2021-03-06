﻿
//页面加载完成时执行
$(function () {
    //点击切换按钮
    $(".icon-menu").click(function () {
        toggleMainMenu(); //切换按钮显示事件
    });
    loadMenuTree(true); //加载管理首页左边导航菜单
    mainPageResize(); //主页面响应式

    //页面尺寸改变时
    $(window).resize(function () {
        //延迟执行,防止多次触发
        setTimeout(function () {
            mainPageResize(); //主页面响应式
        }, 100);
    });
});

//导航菜单显示和隐藏
function mainPageResize() {
    var docWidth = $(window).width();
    if (docWidth > 800) {
        $("body").removeClass("lay-mini");
        $("#main-nav").show();
        $(".nav-right").show();
    } else {
        $("body").addClass("lay-mini");
        $("#main-nav").hide();
    }
}

//切换按钮显示事件
function toggleMainMenu(){
	$("body").toggleClass("lay-mini");
	if(!$("body").hasClass("lay-mini") && $(window).width() > 800){
		$("#main-nav").show();
		$(".nav-right").show();
	}else{
		$("#main-nav").hide();
		if(($(".main-top").width()-42) < $(".nav-right").width()){
			$(".nav-right").hide();
		}else{
			$(".nav-right").show();
		}
	}
}

//加载管理首页左边导航菜单
function loadMenuTree(_islink) {
	//判断是否跳转链接
	var islink = false;
	if (arguments.length == 1 && _islink) {
		islink = true;
	}
    //发送AJAX请求
    $.ajax({
        type: "post",
        url: "../tools/admin_ajax.ashx?action=get_navigation_list&time=" + Math.random(),
        dataType: "html",
        success: function (data, textStatus) {
            //将得到的数据插件到页面中
            $("#sidebar-nav").html(data);
            //$("#pop-menu .list-box").html(data);
            //初始化导航菜单
            initMenuTree(islink);
            //initPopMenuTree();
        }
    });
}

//初始化导航菜单
function initMenuTree(islink) {
	var navObj = $("#main-nav");
	var navGroupObj = $("#sidebar-nav .list-group");
	var navItemObj = $("#sidebar-nav .list-group .list-wrap");
	//先清空NAV菜单内容
	navObj.html('');
	navGroupObj.each(function (i) {
	    //添加菜单导航
        var navHtml = $('<a>' + $(this).children("h1").attr("title") + '</a>').appendTo(navObj);
		//默认选中第一项
		if (i == 0) {
			$(this).show();
			navHtml.addClass("selected");
		}
		//为菜单添加事件
		navHtml.click(function () {
			navObj.children("a").removeClass("selected");
			$(this).addClass("selected");
			navGroupObj.hide();
			navGroupObj.eq(navObj.children("a").index($(this))).show();
        });
		//首先隐藏所有的UL
		$(this).find("ul").hide();
		//绑定树菜单事件.开始
		$(this).find("ul").each(function (j) { //遍历所有的UL
			//遍历UL第一层LI
			$(this).children("li").each(function () {
				var liObj = $(this);
				var spanObj = liObj.children("a").children("span");
				//判断是否有子菜单和设置距左距离
				var parentIconLenght = liObj.parent().parent().children("a").children(".icon").length; //父节点的左距离
				//设置左距离
				var lastIconObj;
				for (var n = 0; n <= parentIconLenght; n++) { //注意<=
					lastIconObj = $('<i class="icon"></i>').insertBefore(spanObj); //插入到span前面
				}

				//如果有下级菜单
				if (liObj.children("ul").length > 0) {
					liObj.children("a").removeAttr("href"); //删除链接，防止跳转
					liObj.children("a").append('<b class="expandable close"></b>'); //最后插件一个+-
					//如果a有自定义图标则将图标插入，否则使用默认的样式
					if(typeof(liObj.children("a").attr("icon"))!="undefined"){
						lastIconObj.append('<img src="' + liObj.children("a").attr("icon") + '" />')
					}else{
						lastIconObj.addClass("folder");
					}
					//隐藏下级的UL
					liObj.children("ul").hide();
					//绑定单击事件
					liObj.children("a").click(function () {
						//如果菜单已展开则闭合
						if($(this).children(".expandable").hasClass("open")){
							//设置自身的右图标为+号
							$(this).children(".expandable").removeClass("open");
							$(this).children(".expandable").addClass("close");
							//隐藏自身父节点的UL子菜单
							$(this).parent().children("ul").slideUp(300);
						}else{
							//搜索所有同级LI且有子菜单的右图标为+号及隐藏子菜单
							$(this).parent().siblings().each(function () {
								if ($(this).children("ul").length > 0) {
									//设置自身的右图标为+号
									$(this).children("a").children(".expandable").removeClass("open");
									$(this).children("a").children(".expandable").addClass("close");
									//隐藏自身子菜单
									$(this).children("ul").slideUp(300);
								}
							});
							//设置自身的右图标为-号
							$(this).children(".expandable").removeClass("close");
							$(this).children(".expandable").addClass("open");
							//显示自身父节点的UL子菜单
							$(this).parent().children("ul").slideDown(300);
						}
					});
					
				}else{
					//如果a有自定义图标则将图标插入，否则使用默认的样式
					if(typeof(liObj.children("a").attr("icon"))!="undefined"){
						lastIconObj.append('<img src="' + liObj.children("a").attr("icon") + '" />');
		            } else if (typeof (liObj.children("a").attr("href")) == "undefined" || liObj.children("a").attr("href").length < 2) { //如果没有链接
						liObj.children("a").removeAttr("href");
						lastIconObj.addClass("folder");
					}else{
						lastIconObj.addClass("file");
					}
					if(typeof(liObj.children("a").attr("href"))!="undefined"){
						//绑定单击事件
						liObj.children("a").click(function () {
							//删除所有的选中样式
							navGroupObj.find("ul li a").removeClass("selected");
							//删除所有的list-group选中样式
							navGroupObj.removeClass("selected");
							//删除所有的main-nav选中样式
							navObj.children("a").removeClass("selected");
							//自身添加样式
							$(this).addClass("selected");
							//设置父list-group选中样式
							$(this).parents(".list-group").addClass("selected");
							//设置父main-nav选中样式
							navObj.children("a").eq(navGroupObj.index($(this).parents(".list-group"))).addClass("selected");
							//隐藏所有的list-group
							navGroupObj.hide();
							//显示自己的父list-group
							$(this).parents(".list-group").show();
						});
					}
				}

			});
			//显示第一个UL
			if (j == 0) {
				$(this).show();
				//展开第一个菜单
				if ($(this).children("li").first().children("ul").length > 0) {
					$(this).children("li").first().children("a").children(".expandable").removeClass("close");
					$(this).children("li").first().children("a").children(".expandable").addClass("open");
					$(this).children("li").first().children("ul").show();
				}
			}
		});
		//绑定树菜单事件.结束
	});
	//定位或跳转到相应的菜单
    linkMenuTree(islink);
}

//定位或跳转到相应的菜单
function linkMenuTree(islink, navid) {
	var navObj = $("#main-nav");
	var navGroupObj = $("#sidebar-nav .list-group");
	var navItemObj = $("#sidebar-nav .list-group .list-wrap");
	navGroupObj.find("ul li a").removeClass("selected");
}