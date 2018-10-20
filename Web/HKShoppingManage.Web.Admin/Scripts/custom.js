$(function () {
    //错误
    var errCode = $("#errCode").val();
    if (errCode !== null && errCode !== "" && errCode !== undefined) {
        dialogHelper.Alert({
            content: GetErrorMsg(errCode),
            success: function () {
                if (errCode === "10000") {
                    window.location.href = "/Login/LogOff";
                }
            }
        });
        return;
    }

    //获取URL参数
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }
});
/**
 * 展示错误信息
 * @param msg 错误信息
 */
function ShowErrorMsg(msg) {
    if ($('.msgDiv').hasClass('rightMsg'))
        $('.msgDiv').removeClass('rightMsg');
    if (!$('.msgDiv').hasClass('errorMsg'))
        $('.msgDiv').addClass('errorMsg');
    $('.msgDiv').html('<i class="icons"></i>' + msg);
    $('.msgDiv').show();
    var timeId = setTimeout(function () {
        $('.msgDiv').hide();
        clearTimeout(timeId);
    }, 4000);
}
/**
 * 展示成功信息
 * @param msg 成功信息
 */
function ShowSuccessMsg(msg) {
    if ($('.msgDiv').hasClass('errorMsg'))
        $('.msgDiv').removeClass('errorMsg');
    if (!$('.msgDiv').hasClass('rightMsg'))
        $('.msgDiv').addClass('rightMsg');
    $('.msgDiv').html('<i class="icons"></i>' + msg);
    $('.msgDiv').show();
    var timeId = setTimeout(function () {
        $('.msgDiv').hide();
        clearTimeout(timeId);
    }, 4000);
}

/**
 * @arr 数组
 * @srcprop 数组中某项的属性
 * @srcvalue 数组中某项的属性值
 * @destprop 数组中某项的目标属性
 */
function getObjPropertyValue(arr, srcprop, srcvalue, destprop) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i][srcprop] == srcvalue) {
            return arr[i][destprop];
        }
    }
}

function getObjPropertyValueEx(arr, srcprop, srcvalue) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i][srcprop] == srcvalue) {
            return arr[i];
        }
    }
}

function deleteArrayItemByAttr(arr, srcprop, srcvalue){
    for (var i = 0; i < arr.length;) {
        if (arr[i][srcprop] == srcvalue) {
            arr.splice(i, 1);
        } else {
            i++;
        }
    }
}

function selectArrayItemByAttr(arr, srcprop, srcvalue) {
    var newarr = [];
    for (var i = 0; i < arr.length;) {
        if (arr[i][srcprop] == srcvalue) {
            newarr.push(arr[i]);
            break;
        } else {
            i++;
        }
    }
    return newarr;
}

if (!Object.assign) {
    // 定义assign方法
    Object.defineProperty(Object, 'assign', {
        enumerable: false,
        configurable: true,
        writable: true,
        value: function (target) { // assign方法的第一个参数
            'use strict';
            // 第一个参数为空，则抛错
            if (target === undefined || target === null) {
                throw new TypeError('Cannot convert first argument to object');
            }

            var to = Object(target);
            // 遍历剩余所有参数
            for (var i = 1; i < arguments.length; i++) {
                var nextSource = arguments[i];
                // 参数为空，则跳过，继续下一个
                if (nextSource === undefined || nextSource === null) {
                    continue;
                }
                nextSource = Object(nextSource);

                // 获取改参数的所有key值，并遍历
                var keysArray = Object.keys(nextSource);
                for (var nextIndex = 0, len = keysArray.length; nextIndex < len; nextIndex++) {
                    var nextKey = keysArray[nextIndex];
                    var desc = Object.getOwnPropertyDescriptor(nextSource, nextKey);
                    // 如果不为空且可枚举，则直接浅拷贝赋值
                    if (desc !== undefined && desc.enumerable) {
                        to[nextKey] = nextSource[nextKey];
                    }
                }
            }
            return to;
        }
    });
}
function deepCopy(source) {
    if (typeof source !== "object" || source == null) {
        return source;
    }
    var s = {};
    if (Object.prototype.toString.call(source).substr(8, 5) == "Array") {
        s = [];
    }
    for (var i in source) {
        s[i] = deepCopy(source[i]);
    }
    return s;
}
// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//by函数接受一个成员名字符串和一个可选的次要比较函数做为参数
//并返回一个可以用来包含该成员的对象数组进行排序的比较函数
//当o[age] 和 p[age] 相等时，次要比较函数被用来决出高下
var by = function (name, minor) {
    return function (o, p) {
        var a, b;
        if (o && p && typeof o === 'object' && typeof p === 'object') {
            a = o[name];
            b = p[name];
            if (a === b) {
                return typeof minor === 'function' ? minor(o, p) : 0;
            }
            if (typeof a === typeof b) {
                return a < b ? -1 : 1;
            }
            return typeof a < typeof b ? -1 : 1;
        } else {
            throw("error");
        }
    }
}

    /**
     * @name 错误信息数据集合
     * @remark 错误码需要跟后台一致
     */
    ; var msgList = {
        //10000-11000 通用
        "10000": "您尚未登录或者登录已过期!",
        "10001": "请勿访问未授权区域!",
        "10002": "网站出现异常，请联系管理员!",
        //11001-12000 账户管理
        "11001": "用户名不存在，请重新输入!",
        "11002": "用户名不能为空!",
        "11003": "姓名请限制在20字内!",
        "11004": "联系方式不能为空!",
        "11005": "请填写有效的11位手机号码!",
        "11006": "登录账号不能为空!",
        "11007": "登录账号只能为6-20位的数字、字母或下划线!",
        "11008": "该账号已存在!",
        "11009": "登录密码不能为空!",
        "11010": "登录密码只能为6-20位的字母、数字、符号!",
        "11011": "用户Id不能为空!",
        "11012": "用户名不能为空!",
        "11013": "密码不能为空!",
        "11014": "用户名不存在或密码错误或验证码不匹配!",
        "11015": "密码有误,请重新输入!",
        "11016": "用户不存在!",
        "11017": "验证码不匹配!",
        "11018": "验证码不能为空!",
        "11019": "原密码有误，请重新输入",
        "11020": "验证码失效!",
        "11021": "用户不能删除自身!",
        //12001-13000 其他
        "12001": "备注请限制在500字内!",
        "12002": "Id不能为空!",

    };

function GetErrorMsg(errCode) {
    var msg = msgList[errCode];
    if (msg === undefined || msg === "" || msg === null) {
        msg = errCode;
    }
    return msg;
}
