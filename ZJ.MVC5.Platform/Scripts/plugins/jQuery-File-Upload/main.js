/*
 * jQuery File Upload Plugin JS Example
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/* global $, window */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: '/Common/UploadAttachment/' //待研究 可以以这样的语法访问 $('#fileupload').fileupload('option', 'url'), 在widget中的定义是options，而在调用时是option，注意,调用的时候没s
    });

    // Enable iframe cross-domain access via redirect option:
    //$('#fileupload').fileupload(
    //    'option',
    //    'redirect',
    //    window.location.href.replace(
    //        /\/[^\/]*$/,
    //        '/cors/result.html?%s'
    //    )
    //);

    // Load existing files:
    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: '/Common/ImgTest',
        dataType: 'json',
        context: $('#fileupload')[0] //form id=fileupload,赋值回调函数的this的内容
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) { //result是返回的结果
        $(this).fileupload('option', 'done') //这种编程风格和模式值得学习,获取form上注册的fileupload对象,访问done方法,并且通过call函数执行执行done方法
            .call(this, $.Event('done'), { result: result });//done function 定义在jquery.fileupload-ui.js里面
    });

});
