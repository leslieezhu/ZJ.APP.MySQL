(function () {
    bindQuestionList();

})(window);

function bindQuestionList() {
    var search = {};

    var tableData = {
        "paging": true,
        "lengthChange": false,
        "autoWidth": false,
        "ordering": false,
        "serverSide": true,
        "pageLength": 20,
        "destroy": true,
        "searching": false,
        "info": true,
        "ajax": {
            "url": "/QuestionBank/Default/GetList",
            "type": "POST",
            "data": search
        }
    };
    tableData.columns = [
        { "data": "ID", title: "ID", class: "", width: "2%"},
        { "data": "QuestionTitle", title: "题面" },
        { "data": "SubjectTypeName", title: "学科", width: "7%"  },
        { "data": "QuestionTypeName", title: "题型", width: "7%"  },
        { "data": "CreateTime", title: "录入时间", width: "8%"  },
        { "data": null, title: "操作", class: "opt", width: "4%"}
    ];
    tableData.columnDefs = [
        {
            "render": function (data, type, row) {
                return '<div class="ellipsis" title="' + data + '">' + data + '</div>';
            },
            "targets": 1
        },
        {
            "render": function (data, type, row) {
                if (data !== null) {
                    return (new Date(data)).Format("yyyy-MM-dd hh:mm");
                }
                return null;
            },
            "targets": -2
        },
        {
            "render": function (data, type, row) {
                var tmp = '<a href="/QuestionBank/Default/EditQuestion/' + row["ID"] + '" title="编辑" ><i class="fa fa-search" ></i></a>';
                return tmp;
            },
            "targets": -1
        }
    ];
    var table = $('#questionList').DataTable(tableData);
}