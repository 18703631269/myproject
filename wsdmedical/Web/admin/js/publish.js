//调用方法 egg: var a='sdy'; a.getBytes();获取到字符串的长度
String.prototype.getBytes = function () {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
}

// 清除两边的空格  
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, '');
};

// 合并多个空白为一个空白  
String.prototype.ResetBlank = function () {
    var regEx = /\s+/g;
    return this.replace(regEx, ' ');
};

// 保留数字  
String.prototype.GetNum = function () {
    var regEx = /[^\d]/g;
    return this.replace(regEx, '');
};

// 保留中文  
String.prototype.GetCN = function () {
    var regEx = /[^\u4e00-\u9fa5\uf900-\ufa2d]/g;
    return this.replace(regEx, '');
};

// String转化为Number  
String.prototype.ToInt = function () {
    return isNaN(parseInt(this)) ? this.toString() : parseInt(this);
};

// 得到字节长度  
String.prototype.GetLen = function () {
    var regEx = /^[\u4e00-\u9fa5\uf900-\ufa2d]+$/;
    if (regEx.test(this)) {
        return this.length * 2;
    } else {
        var oMatches = this.match(/[\x00-\xff]/g);
        var oLength = this.length * 2 - oMatches.length;
        return oLength;
    }
};

// 获取文件全名  
String.prototype.GetFileName = function () {
    var regEx = /^.*\/([^\/\?]*).*$/;
    return this.replace(regEx, '$1');
};

// 获取文件扩展名  
String.prototype.GetExtensionName = function () {
    var regEx = /^.*\/[^\/]*(\.[^\.\?]*).*$/;
    return this.replace(regEx, '$1');
};

// 数字补零  
Number.prototype.LenWithZero = function (oCount) {
    var strText = this.toString();
    while (strText.length < oCount) {
        strText = '0' + strText;
    }
    return strText;
};

// 数字数组由小到大排序  
Array.prototype.Min2Max = function () {
    var oValue;
    for (var i = 0; i < this.length; i++) {
        for (var j = 0; j <= i; j++) {
            if (this[i] < this[j]) {
                oValue = this[i];
                this[i] = this[j];
                this[j] = oValue;
            }
        }
    }
    return this;
};

// 数字数组由大到小排序  
Array.prototype.Max2Min = function () {
    var oValue;
    for (var i = 0; i < this.length; i++) {
        for (var j = 0; j <= i; j++) {
            if (this[i] > this[j]) {
                oValue = this[i];
                this[i] = this[j];
                this[j] = oValue;
            }
        }
    }
    return this;
};

// 获得数字数组中最大项  
Array.prototype.GetMax = function () {
    var oValue = 0;
    for (var i = 0; i < this.length; i++) {
        if (this[i] > oValue) {
            oValue = this[i];
        }
    }
    return oValue;
};

// 获得数字数组中最小项  
Array.prototype.GetMin = function () {
    var oValue = 0;
    for (var i = 0; i < this.length; i++) {
        if (this[i] < oValue) {
            oValue = this[i];
        }
    }
    return oValue;
};

// 获取当前时间的中文形式  
Date.prototype.GetCNDate = function () {
    var oDateText = '';
    oDateText += this.getFullYear().LenWithZero(4) + new Number(24180).ChrW();
    oDateText += this.getMonth().LenWithZero(2) + new Number(26376).ChrW();
    oDateText += this.getDate().LenWithZero(2) + new Number(26085).ChrW();
    oDateText += this.getHours().LenWithZero(2) + new Number(26102).ChrW();
    oDateText += this.getMinutes().LenWithZero(2) + new Number(20998).ChrW();
    oDateText += this.getSeconds().LenWithZero(2) + new Number(31186).ChrW();
    oDateText += new Number(32).ChrW() + new Number(32).ChrW() + new Number(26143).ChrW() + new Number(26399).ChrW() + new String('26085199682010819977222352011620845').substr(this.getDay() * 5, 5).ToInt().ChrW();
    return oDateText;
};

//获取地址参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//定义Date的Format属性
Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "H+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
//使用
//1.当前时间格式化：new Date().Format("yyyy-MM-dd");
//2.指定js日期格式化：new Date(dataStr).Format("yyyy-MM-dd");
//3.要格式化的时间串不是js的日期格式(日期以‘-’分隔)：new Date(dateStr.replace(/-/g,"/")).Format("yyyy-MM-dd");

//时间间隔
function dateDiff(interval, date1, date2) {
    var objInterval = { 'D': 1000 * 60 * 60 * 24, 'H': 1000 * 60 * 60, 'M': 1000 * 60, 'S': 1000, 'T': 1 };
    interval = interval.toUpperCase();
    var dt1 = new Date(Date.parse(date1.replace(/-/g, '/')));
    var dt2 = new Date(Date.parse(date2.replace(/-/g, '/')));
    try {
        //alert(dt2.getTime()- dt1.getTime());
        //alert(eval_r('objInterval.'+interval));
        //alert((dt2.getTime()- dt1.getTime()) / eval_r('objInterval.'+interval));
        return Math.round((dt2.getTime() - dt1.getTime()) / eval('objInterval.' + interval));
    }
    catch (e) {
        return e.message;
    }
}
//interval ：D表示查询精确到天数的之差
//interval ：H表示查询精确到小时之差
//interval ：M表示查询精确到分钟之差
//interval ：S表示查询精确到秒之差
//interval ：T表示查询精确到毫秒之差
//使用方法:
//alert(dateDiff('D', '2007-4-1', '2007/04/19'));

//检测是否为空  
Object.prototype.IsNullOrEmpty = function () {
    var obj = this;
    var flag = false;
    if (obj == null || obj == undefined || typeof (obj) == 'undefined' || obj == '') {
        flag = true;
    } else if (typeof (obj) == 'string') {
        obj = obj.trim();
        if (obj == '') {//为空  
            flag = true;
        } else {//不为空  
            obj = obj.toUpperCase();
            if (obj == 'NULL' || obj == 'UNDEFINED' || obj == '{}') {
                flag = true;
            }
        }
    }
    else {
        flag = false;
    }
    return flag;
};
/**
  * id数组转换为json字符串
  */
function arrayTojson(arr) {
    var jsonIds = "[";
    for (var i = 0; i < arr.length; i++) {
        if (i == arr.length - 1) {
            jsonIds += arr[i].eventid;
        } else {
            jsonIds += arr[i].eventid + ",";
        }
    }
    return jsonIds += "]";
}

/**
  * 获取系统的url
  * @returns fullUrl：系统访问路径，例如：http://localhost:8080/amudraya/amudraya
  */
function getURL() {
    var fullUrl = window.location.href;
    var a = fullUrl.lastIndexOf('/');
    fullUrl = fullUrl.substring(0, a);
    var b = fullUrl.lastIndexOf('/');
    fullUrl = fullUrl.substring(0, b);
    var c = fullUrl.lastIndexOf('/');
    fullUrl = fullUrl.substring(0, c);
    return fullUrl;
}

// 判断输入是否是有效的电子邮件   
function isemail(str) {
    var result = str.match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/);
    if (result == null) return false;
    return true;
}

// 判断输入是否是一个由 0-9 / A-Z / a-z 组成的字符串   
function isalphanumber(str) {
    var result = str.match(/^[a-zA-Z0-9]+$/);
    if (result == null) return false;
    return true;
}

//移动电话  
function checkMobile(str) {
    if (!(/^1[3|5|8][0-9]\d{4,8}$/.test(str))) {
        return false;
    }
    return true;
}

//匹配身份证(15位或18位)   
function isidcard(str) {
    var result = str.match(/\d{15}|\d{18}/);
    if (result == null) return false;
    return true;
}

//匹配国内电话号码(0511-4405222 或 021-87888822)   
function istell(str) {
    var result = str.match(/\d{3}-\d{8}|\d{4}-\d{7}/);
    if (result == null) return false;
    return true;
}

//随机数时间戳  
function uniqueId() {
    var a = Math.random, b = parseInt;
    return Number(new Date()).toString() + b(10 * a()) + b(10 * a()) + b(10 * a());
}

//完美判断是否为网址  
function IsURL(strUrl) {
    var regular = /^\b(((https?|ftp):\/\/)?[-a-z0-9]+(\.[-a-z0-9]+)*\.(?:com|edu|gov|int|mil|net|org|biz|info|name|museum|asia|coop|aero|[a-z][a-z]|((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d))\b(\/[-a-z0-9_:\@&?=+,.!\/~%\$]*)?)$/i
    if (regular.test(strUrl)) {
        return true;
    } else {
        return false;
    }
}

//判断是否移动设备访问  
function isMobileUserAgent() {
    return (/iphone|ipod|android.*mobile|windows.*phone|blackberry.*mobile/i.test(window.navigator.userAgent.toLowerCase()));
}

//设为首页  
function setHomepage(homeurl) {
    if (document.all) {
        document.body.style.behavior = 'url(#default#homepage)';
        document.body.setHomePage(homeurl)
    } else if (window.sidebar) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect")
            } catch (e) {
                alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入about:config,然后将项 signed.applets.codebase_principal_support 值该为true");
            }
        }
        var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
        prefs.setCharPref('browser.startup.homepage', homeurl)
    }
}

//加入收藏夹  
function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle)
    } catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "")
        } catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加")
        }
    }
}

//判断某个值是否在数组中  
Array.prototype.in_array = function (e) {
    for (i = 0; i < this.length; i++) {
        if (this[i] == e)
            return true;
    }
    return false;
}

//判断数组中是否存在重复的元素  
function confirmRepeat(someArray) {
    tempArray = someArray.slice(0); //复制数组到临时数组  
    for (var i = 0; i < tempArray.length; i++) {
        for (var j = i + 1; j < tempArray.length;) {
            if (tempArray[j] == tempArray[i])
                //后面的元素若和待比较的相同，则删除并计数；  
                //删除后，后面的元素会自动提前，所以指针j不移动  
            {
                return true;
            }
            else {
                j++;
            }
            //不同，则指针移动  
        }
    }
    return false;
}

//删除数组中存在重复的元素  
function getUnique(someArray) {
    tempArray = someArray.slice(0); //复制数组到临时数组  
    for (var i = 0; i < tempArray.length; i++) {
        for (var j = i + 1; j < tempArray.length;) {
            if (tempArray[j] == tempArray[i])
                //后面的元素若和待比较的相同，则删除并计数；  
                //删除后，后面的元素会自动提前，所以指针j不移动  
            {
                tempArray.splice(j, 1);
            }
            else {
                j++;
            }
            //不同，则指针移动  
        }
    }
    return tempArray;
}
//全选取消按钮函数
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).children("span").text("取消");
        $(".checkall input:enabled").prop("checked", true);
    } else {
        $(chkobj).children("span").text("全选");
        $(".checkall input:enabled").prop("checked", false);
    }
}

//使用例子<input type="text" name="spec_stock_quantity" maxlength="10" class="td-input" onkeydown="return checkNumber(event);" />
//只允许输入数字
function checkNumber(e) {
    var keynum = window.event ? e.keyCode : e.which;
    if ((48 <= keynum && keynum <= 57) || keynum == 8) {
        return true;
    } else {
        return false;
    }
}

//只允许输入小数
function checkForFloat(obj, e) {
    var isOK = false;
    var key = window.event ? e.keyCode : e.which;
    if ((key > 95 && key < 106) || //小键盘上的0到9  
        (key > 47 && key < 60) ||  //大键盘上的0到9  
        (key == 110 && obj.value.indexOf(".") < 0) || //小键盘上的.而且以前没有输入.  
        (key == 190 && obj.value.indexOf(".") < 0) || //大键盘上的.而且以前没有输入.  
         key == 8 || key == 9 || key == 46 || key == 37 || key == 39) {
        isOK = true;
    } else {
        if (window.event) { //IE
            e.returnValue = false;   //event.returnValue=false 效果相同.    
        } else { //Firefox 
            e.preventDefault();
        }
    }
    return isOK;
}

//检查短信字数
function checktxt(obj, txtId) {
    var txtCount = $(obj).val().length;
    if (txtCount < 1) {
        return false;
    }
    var smsLength = Math.ceil(txtCount / 62);
    $("#" + txtId).html("您已输入<b>" + txtCount + "</b>个字符，将以<b>" + smsLength + "</b>条短信扣取费用。");
}

//四舍五入函数
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}