(function () {
    $('.form_year').datetimepicker(datetimepickerYearCfg);
    bindMovieList();
    $("#btnSearch").click(bindMovieList);
})(window);

function bindMovieList() {
    var search = GetFormInfo("filter");

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
            "url": "/Movie/Default/GetList",
            "type": "POST",
            "data": search
        }
    };
    tableData.columns = [
        { "data": "ID", title: "ID" },
        { "data": "MovieFileName", title: "电影文件名" },
        { "data": "CategoryByLocalName", title: "地区" },
        { "data": "SaveLocalName", title: "位置" },
        { "data": "PublicDate", title: "上映年份" },
        { "data": "CreateTime", title: "录入时间" },
        { "data": "", title: "操作" }
    ];
    tableData.columnDefs = [
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
                var tmp = '<a href="/Movie/Default/Edit/' + row["ID"] + '" title="编辑" ><i class="fa fa-edit" ></i></a>';
                tmp += '<a href="/Movie/Default/Detail/' + row["ID"] + '" title="详细" ><i class="fa fa-search" ></i></a>';
                return tmp;
            },
            "targets": -1
        }
    ];
    var table = $('#movieList').DataTable(tableData);
}




