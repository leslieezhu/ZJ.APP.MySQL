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
        url: '/Common/UploadAttachment/' //���о� �������������﷨���� $('#fileupload').fileupload('option', 'url'), ��widget�еĶ�����options�����ڵ���ʱ��option��ע��,���õ�ʱ��ûs
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
        context: $('#fileupload')[0] //form id=fileupload,��ֵ�ص�������this������
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) { //result�Ƿ��صĽ��
        $(this).fileupload('option', 'done') //���ֱ�̷���ģʽֵ��ѧϰ,��ȡform��ע���fileupload����,����done����,����ͨ��call����ִ��ִ��done����
            .call(this, $.Event('done'), { result: result });//done function ������jquery.fileupload-ui.js����
    });

});
