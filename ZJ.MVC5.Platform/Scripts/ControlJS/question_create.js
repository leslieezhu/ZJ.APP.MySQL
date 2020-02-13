(function () {
    KindEditor.ready(function (K) {
        window.editor = K.create('textarea', {
            
            langType: 'zh-CN',
            autoHeightMode: true,
            uploadJson: '/Common/UploadJsonKindEditor/',
            allowFileManager: true,//浏览图片空间
            afterBlur: function () { this.sync(); }, //编辑器失去焦点(blur)时执行的回调函数（将编辑器的HTML数据同步到textarea）
            afterCreate: function () {  //KindEditor 生成后执行
                var self = this;
                K.ctrl(document, 13, function () {
                    self.sync();
                    K('form[name=question_bank]')[0].submit();
                });
                K.ctrl(self.edit.doc, 13, function () {
                    self.sync();
                    K('form[name=question_bank]')[0].submit();
                });
            }
        });

    });

    $("#btnSave").click(SaveQuestion);

})(window);

function SaveQuestion() {
    var questionObj = GetFormInfo("question");
    $.ajax({
        processData: false,
        url: "/QuestionBank/Default/CreateQuestion",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(questionObj),
        success: function (data) {
            var obj = JSON.parse(data);
            if (obj.result == false) {
                alert(obj.message);
                //swal("操作失败!", obj.message, "error");
                return;
            }
            if (obj.message == "update")
            {
                alert("更新成功");
            }
            location.href = obj.returnUrl;
        },
        error: function () {
            swal("操作失败!", "服务器错误", "error");
        }
    });

}