(function () {
    uploadAttachment();//@common.js
    $("#btnSave").click(SubmitMovie);
    $('.form_year').datetimepicker(datetimepickerYearCfg);
})(window);



function SubmitMovie() {
    var movieObj = GetFormInfo("movie");
    $.ajax({
        processData: false,
        url: "/Movie/Default/Create",
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
            location.href = obj.returnUrl;
        },
        error: function () {
            swal("操作失败!", "服务器错误", "error");
        }
    });
}