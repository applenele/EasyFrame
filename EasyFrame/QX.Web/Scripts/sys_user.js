$(document).ready(function () {
    if ($("#userLst").length > 0) {
        $('#userLst').datagrid({
            width: 1000,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/User/GetUsers',
            // queryParams: { "rate": 0 },
            loadMsg: '数据加载中请稍后……',
            pagination: true,
            rownumbers: true,
            columns: [[
                   { field: 'Username', title: '用户名', align: 'center', width: 200, height: 200, },
                   {
                       field: 'Department', title: '部门', align: 'center', width: 200, height: 200,
                       formatter: function (value, rowData, rowIndex) {
                           if (rowData.Department != null) {
                               return rowData.Department.DepartmentName;
                           }
                       }
                   },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }

    $("#btnDeleteUser").click(function () {
        $.messager.confirm('确认', '确认将选中的用户删除?', function (row) {
            if (row) {
                var selectedRow = $('#userLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除选中的用户");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/User/Delete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#userLst").datagrid('reload');
                    });
                }
            }
        })
    });

    ///修改用户
    $("#btnUpdateUser").click(function () {
        var selectedRow = $('#userLst').datagrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择要修改的用户");
            return;
        } else {
            var id = selectedRow.ID;
            location.href = "/Admin/User/Update?id=" + id;
        }
    });

    //查看用户角色
    $("#btnLookUserRole").click(function () {
        var selectedRow = $('#userLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择用户查看角色");
            return;
        }
        $("#wUserRoles").window("open");
        $("#userRoleLst").datagrid({
            width: 650,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/User/GetUserRoles?uid=' + selectedRow.ID,
            loadMsg: '数据加载中请稍后……',
            columns: [[
                   { field: 'RoleName', title: '角色名称', align: 'center', width: 200, height: 200, },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    });

    //授予角色
    $("#btnInvokeRole").click(function () {
        $(".SearchRole").val("");
        var selectedRow = $('#userLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择用户授予角色");
            return;
        }
        $("#wInvokeUserRoles").window("open");
        $("#userID").val(selectedRow.ID);
        //$("#invokeWarning").html("<input type='hidden' value=" + selectedRow.ID + " id='userID'  />");
        $(".SearchRole").select4({ "ajax_url": "/Admin/Role/GetRoleByRoleNameByUser?uid=" + $("#userID").val() });
        $("#userRoleLst1").datagrid({
            width: 650,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/User/GetUserRoles?uid=' + selectedRow.ID,
            loadMsg: '数据加载中请稍后……',
            columns: [[
                   { field: 'RoleName', title: '角色名称', align: 'center', width: 200, height: 200, },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    });

    ///给用增加角色
    $("#btnAddRoleToUser").click(function () {
        var rid = $("#txtRid").val();
        var uid = $("#userID").val();
        $.post("/Admin/User/InvokeRole", { "rid": rid, "uid": uid }, function (data) {
            alert(data.Msg);
            $("#userRoleLst1").datagrid("reload");
        });
    });

    $("#btnDeleteRoleFromUser").click(function () {
        var uid = $("#userID").val();
        var selectedRow = $('#userRoleLst1').treegrid('getSelected');  //获取选中行        
        $.post("/Admin/User/RevokeRole", { "rid": selectedRow.ID, "uid": uid }, function (data) {
            alert(data.Msg);
            $("#userRoleLst1").datagrid("reload");
        });
    });


});

