// 清除两边的空格  
String.prototype.Trim = function() {  
    return this.replace(/(^\s*)|(\s*$)/g, '');  
};
//限制字段长度，超出超度用...显示
String.prototype.ToLeft = function(num) {  
    if(isNaN(parseInt(num))){
        return this;
    } else {
        if(this.length > parseInt(num)){
            return this.substring(0, parseInt(num)) + "...";
        }
        return this;
    }
};

;var StringHelper = (function ($, window, document, undefined) {
    /**
	 * 格式化字符窜
	 */
    var FormatStr = function () {
        if (arguments.length == 0)
            return null;
        var str = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
			var parms = arguments[i];
			if(typeof(parms) == "string"){
				//针对双引号处理
				parms = parms.replace(/["]/g,"&quot;");
			}
			if (parms == null || parms == undefined) {
			    parms = "";
			}
            str = str.replace(re, parms);
        }
        return str;
    };
	
	return {
		FormatStr: FormatStr,
	};
})(jQuery, window, document);
