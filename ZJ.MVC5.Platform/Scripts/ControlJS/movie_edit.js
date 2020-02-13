(function () {
    uploadAttachment();
    $("#btnSave").click(SubmitMovie);
    $("#preview_img")[0].src = $("#ImgFileName").val();//加载图片
    $('.form_year').datetimepicker(datetimepickerYearCfg);
})(window);



function SubmitMovie() {
    var movieObj = GetFormInfo("movie");
    //movieObj.ImgFileName = $("#cid").val();
    $.ajax({
        processData: false,
        url: "/Movie/Default/Edit",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(movieObj),
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj.result == false) {
                alert(obj.message);
                //swal("操作失败!", obj.message, "error");
                return;
            }
            swal({
                type: "success",
                title: "更新成功",
                text: "",//小字
                showConfirmButton: true,
                showCancelButton: false,
                timer:2000
            });
        },
        error: function () {
            swal("操作失败!", "服务器错误", "error");
        }
    });
}