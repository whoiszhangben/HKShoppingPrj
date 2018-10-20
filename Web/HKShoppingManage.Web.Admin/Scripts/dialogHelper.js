;var dialogHelper = (function ($, window, document, undefined) {
    var defaults = {
        Id:"",
        title: "&#x4FE1;&#x606F;",  //弹出框标题
        content: "",                //提示内容
        width: "",
        height: "",
		success: function(){},      //点击确认时事件
		cancle: function(){}        //点击取消时事件
    };

    /**
	 * 提示框
	 */
    var Alert = function (options) {
        var opt = $.extend({}, defaults, options || {});
        layer.alert(opt.content, { closeBtn: 0, move: false }, function (index) {
            if (typeof (opt.success) === "function") {
                opt.success();
            }
            layer.close(index);
        });
    }

    /**
	 * 确认提示框
	 */
	var Confirm = function (options) {
	    var opt = $.extend({}, defaults, options || {});

	    var lay = layer.confirm(opt.content, {
	        btn: ['确定', '取消'],
	        move: false
	    }, function () {
	        layer.close(lay);
	        if (typeof (opt.success) === "function") {
	            opt.success();
	        }
	    }, function () {
	        if (typeof (opt.cancle) === "function") {
	            opt.cancle();
	        }
	    });
	};

	var Show = function (options) {
	    var opt = $.extend({}, defaults, options || {});

	    layer.open({
	        id: opt.Id,
	        type: 1,
	        closeBtn: 0,
	        move: false,
	        btn: ['保存', '取消'],
	        title:opt.title,
	        area: [opt.width, opt.height],
	        content: opt.content,
	        yes: function (index, dom) {
	            if (typeof (opt.success) === "function") {
	                opt.success(index);
	            }
	            //layer.close(index);
	        },
	        cancel: function (index, dom) {
	            if (typeof (opt.cancel) === "function") {
	                opt.cancel();
	            }
	            //layer.close(index);
	        }
	    });
	};

	var ShowEx = function (options) {
	    var opt = $.extend({}, defaults, options || {});

	    layer.open({
	        id: opt.Id,
	        type: 1,
	        closeBtn: 0,
	        move: false,
	        btn: ['确认', '取消'],
	        title: opt.title,
	        area: [opt.width, opt.height],
	        content: opt.content,
	        yes: function (index, dom) {
	            if (typeof (opt.success) === "function") {
	                opt.success(index);
	            }
	            //layer.close(index);
	        },
	        cancle: function (index, dom) {
	            if (typeof (opt.cancle) === "function") {
	                opt.cancle();
	            }
	            //layer.close(index);
	        }
	    });
	};

	var ShowInfo = function (options) {
	    var opt = $.extend({}, defaults, options || {});

	    layer.open({
	        id: opt.Id,
	        type: 1,
            closeBtn: 0,
	        btn: ['关闭'],
	        title: opt.title,
	        area: [opt.width, opt.height],
	        content: opt.content,
	        yes: function (index, dom) {
	            if (typeof (opt.success) === "function") {
	                opt.success(index);
	            }
	            //layer.close(index);
	        }
	    });
	};

	var ShowPlusParam = function (options) {
	    var opt = $.extend({}, defaults, options || {});

	    layer.open({
	        id: opt.Id,
	        type: 2,
	        closeBtn: 0,
	        move: false,
	        btn: ['保存', '取消'],
	        title: opt.title,
	        area: [opt.width, opt.height],
	        content: opt.content,
	        yes: function (index, dom) {
	            if (typeof (opt.success) === "function") {
	                opt.success(index);
	            }
	            //layer.close(index);
	        },
	        cancle: function (index, dom) {
	            if (typeof (opt.cancle) === "function") {
	                opt.cancle();
	            }
	            //layer.close(index);
	        }
	    });
	};
	
	return {
	    Alert: Alert,
	    Confirm: Confirm,
	    Show: Show,
	    ShowEx: ShowEx,
        ShowInfo:ShowInfo
	};
})(jQuery, window, document);