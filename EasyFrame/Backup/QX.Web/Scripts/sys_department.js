$(document).ready(function () {

    if ($("#departmentLst").length > 0) {
        $('#departmentLst').datagrid({
            width: 1000,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/Department/GetDepartments',
            // queryParams: { "rate": 0 },
            loadMsg: '数据加载中请稍后……',
            pagination: true,
            rownumbers: true,
            rowStyle: function (index, row) {
                return 'height:100px;';
            },
            columns: [[
                   { field: 'DepartmentName', title: '部门名称', align: 'center', width: 200, height: 200, },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }

    ///删除部门
    $("#btnDeleteDepartment").click(function () {
        $.messager.confirm('确认', '确认将选中的部门删除?', function (row) {
            if (row) {
                var selectedRow = $('#departmentLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除的部门");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Department/Delete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#departmentLst").datagrid('reload');
                    });
                }
            }
        })
    });

    ///修改部门
    $("#btnUpdateDepartment").click(function () {
        var selectedRow = $('#departmentLst').datagrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择要修改的部门");
            return;
        } else {
            var id = selectedRow.ID;
            location.href = "/Admin/Department/Update?id="+ id;
        }
    });
});