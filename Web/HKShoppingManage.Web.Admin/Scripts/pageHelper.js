//1.  用自调用匿名函数包裹代码,可以避免跟其他人的代码变量冲突
//2.  将系统变量以变量形式传递到插件内部,window等系统变量在插件内部就有了一个局部的引用，可以提高访问速度，会有些许性能的提升
//2.1 为了得到没有被修改的undefined，我们并没有传递这个参数，但却在接收时接收了它，因为实际并没有传，所以‘undefined’那个位置接收到的就是真实的'undefined'
//3.  在代码开头加一个分号，这在任何时候都是一个好的习惯
var $PageParam = {};
; var pageHelper = (function ($, window, document, undefined) {
    //默认参数
    var defaults = {
        /*****   查询用参数   *****/
        pageIndex: 0,       //第几页
        pageSize: 10,       //每页显示数量
        pageNum: 0,          //当前页的条数
        initFlag:true,
        type: "GET",        //请求方式
        pageDiv: "",        //显示分页控件的容器ID
        /*****   ajax参数   *****/
        url: "",
        data: {},
        async: true,
        dataType: "json",
        bind: function (data) { },
        completed: function () { },
    };
    //其他全局参数
    var pageCount = 1;
    var opt = {};

    var setOptions = function (data) {
        opt = $.extend(opt, data || {});
    };

    var Init = function (options) {
        //将默认参数与自定义参数合并
        opt = {};
        opt = $.extend({}, defaults, options || {});
        //调用goPage方法
        goPage(opt.pageIndex);
    };
    var goPage = function (pageIndex) {
        //不存在时，设为1
        if (!pageIndex)
            pageIndex = 0;

        //设置参数
        opt.pageIndex = pageIndex + 1;
        opt.skip = (opt.pageIndex - 1) * opt.pageSize;
        opt.top = opt.pageSize;
        var dataObj = {};
        dataObj["PageIndex"] = opt.pageIndex;
        dataObj["PageSize"] = opt.pageSize;
        dataObj["rId"] = Math.random();
        var ajaxData = $.extend({}, dataObj, opt.data || {});
        //ajax访问后台
        $.ajax({
            url: opt.url,
            type: opt.type,
            async: opt.async,
            dataType: opt.dataType,
            data: ajaxData,
            //成功，绑定数据
            success: function (data) {
                $PageParam = data;
                if (data != null && data != "") {
                    if (typeof (opt.bind) == "function") {
                        opt.pageIndex = data.PageIndex;
                        opt.pageSize = data.PageSize;
                        if (data.PageIndex < data.TotalPages) {
                            opt.pageNum = data.PageSize;
                        } else {
                            opt.pageNum = data.TotalCount - (data.PageIndex - 1) * data.PageSize;
                        }
                        opt.bind(data);
                    }

                    if (opt.initFlag) {
                        $("#Pagination").pagination(data.TotalCount, {
                            items_per_page: data.PageSize,
                            num_edge_entries: 1,
                            num_display_entries: 8,
                            callback: goPage//回调函数
                        });
                        opt.initFlag = false;
                    }
                    ////分页拼接
                    //var page = $(opt.pageDiv);
                    //page.html("");
                    //$('<li>共' + data.TotalCount + '条记录</li>').appendTo(page);
                    //if (data.PageIndex === 1) {
                    //    $('<li><<</li>').appendTo(page);
                    //} else {
                    //    $('<li><a href="javascript:void(0);"><<</a></li>').unbind("click").bind("click",
                    //    function () {
                    //        if (data.HasPreviousPage) {
                    //            goPage(data.PageIndex - 1);
                    //        }
                    //        return;
                    //    }).appendTo(page);
                    //}

                    //for (var i = 1; i <= data.TotalPages; i++) {
                    //    if (i === data.PageIndex) {
                    //        $('<li><a href="javascript:void(0);" class="select">' + i + '</a></li>').appendTo(page);
                    //    } else {
                    //        $('<li><a href="javascript:void(0);">' + i + '</a></li>').bind('click', { pageIndex: i }, function (event) {
                    //            goPage(event.data.pageIndex);
                    //            return;
                    //        }).appendTo(page);
                    //    }
                    //}

                    //if (data.PageIndex == data.TotalPages) {
                    //    //下一页
                    //    $('<li>>></li>').appendTo(page);
                    //} else {
                    //    //下一页
                    //    $('<li><a href="javascript:void(0);">>></a></li>').bind('click', function () {
                    //        if (data.HasNextPage) {
                    //            goPage(data.PageIndex + 1);
                    //        }
                    //        return;
                    //    }).appendTo(page);
                    //}
                }
            },
            //失败，提示
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("textStatus:" + textStatus + ",errorThrown:" + errorThrown);
            },
            //完成，调用自定义方法
            complete: opt.completed()
        });
    };

    return {
        setOptions: setOptions,
        Init: Init,
        goPage: goPage
    };
})(jQuery, window, document);