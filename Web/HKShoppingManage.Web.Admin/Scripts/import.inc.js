var common_js_files = [
    '../Scripts/jquery-1.10.2.min.js',
    '../Scripts/bootstrap/bootstrap.min.js',
    '../Scripts/bootstrap-table.min.js',
    '../Scripts/bootstrap-table-zh-CN.min.js',
    '../Scripts/bootstrap-table-edit.js',
    '../Scripts/bootstrap-select.js'
];
//系统CSS文件对象
var common_css_files = [
    '../Content/bootstrap/css/bootstrap.min.css',
    '../Content/bootstrap-table.min.css'
];

/**
 * 导入CSS文件
 */
for (var i = 0; i < common_css_files.length; i++) {
    document.write("<link rel='stylesheet' type='text/css' href='" + common_css_files[i] + "'>");
}
/**
 * 导入JS文件
 */
for (var i = 0; i < common_js_files.length; i++) {
    document.write("<script type='text/javascript' src='" + common_js_files[i] + "'></script>");
}