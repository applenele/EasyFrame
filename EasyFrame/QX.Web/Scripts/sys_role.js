function show(checkid) {
    var s = '#check_' + checkid;
    //alert( $(s).attr("id"));
    // alert($(s)[0].checked);
    /*选子节点*/
    var nodes = $("#apermissionLst").treegrid("getChildren", checkid);
    for (i = 0; i < nodes.length; i++) {
        $(('#check_' + nodes[i].id))[0].checked = $(s)[0].checked;
    }
    //选上级节点
    if (!$(s)[0].checked) {
        var parent = $("#apermissionLst").treegrid("getParent", checkid);
        $(('#check_' + parent.id))[0].checked = false;
        while (parent) {
            parent = $("#apermissionLst").treegrid("getParent", parent.id);
            console.log(parent);
            $(('#check_' + parent.id))[0].checked = false;
        }
    } else {
        var parent = $("#apermissionLst").treegrid("getParent", checkid);
        var flag = true;
        var sons = parent.sondata.split(',');
        for (j = 0; j < sons.length; j++) {
            if (!$(('#check_' + sons[j]))[0].checked) {
                flag = false;
                break;
            }
        }
        if (flag)
            $(('#check_' + parent.id))[0].checked = true;
        while (flag) {
            parent = $("#apermissionLst").treegrid("getParent", parent.id);
            if (parent) {
                sons = parent.sondata.split(',');
                for (j = 0; j < sons.length; j++) {
                    if (!$(('#check_' + sons[j]))[0].checked) {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
                $(('#check_' + parent.id))[0].checked = true;
        }
    }
}

//获取选中的结点
function getSelected() {
    var idList = "";
    $("input:checked").each(function () {
        var id = $(this).attr("id");
        if (id.indexOf('check_type') == -1 && id.indexOf("check_") > -1)
            idList += id.replace("check_", '') + ',';
    })
    alert(idList);

}
///提交授权
function roleAuthorize() {
    var idList = "";
    $("input:checked").each(function () {
        var id = $(this).attr("id");
        if (id.indexOf('check_type') == -1 && id.indexOf("check_") > -1)
            idList += id.replace("check_", '') + ',';
    })
    $.post("/Admin/Role/Authorize", { "authorties": idList, "rid": $("#authoirzeRid").val() }, function (data) {
        alert(data.Msg);
        $("#wauthorize").window("close");
    });
}
function formatcheckbox(val, row) {
    return "<input type='checkbox' onclick=show(" + row.id + ") id='check_" + row.id + "' " + (row.Checked ? 'checked' : '') + "/>" + row.text;
}

$(document).ready(function () {
    if ($("#roleLst").length > 0) {
        $('#roleLst').datagrid({
            width: 1000,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/Role/GetRoles',
            // queryParams: { "rate": 0 },
            loadMsg: '数据加载中请稍后……',
            pagination: true,
            rownumbers: true,
            rowStyle: function (index, row) {
                return 'height:100px;';
            },
            columns: [[
                   { field: 'RoleName', title: '角色名称', align: 'center', width: 200, height: 200, },
                   {
                       field: 'Department', title: '部门', align: 'center', width: 200, height: 200,
                       formatter: function (value, rowData, rowIndex) {
                           if (rowData.Department != null) {
                               return rowData.Department.DepartmentName;
                           }
                       }
                   },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }
    //删除角色
    $("#btnDeleteRole").click(function () {
        $.messager.confirm('确认', '确认将选中的角色删除?', function (row) {
            if (row) {
                var selectedRow = $('#roleLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除的角色");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Role/Delete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#roleLst").datagrid('reload');
                    });
                }
            }
        })
    });

    ///修改角色
    $("#btnUpdateRole").click(function () {
        var selectedRow = $('#roleLst').datagrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择要修改的角色");
            return;
        } else {
            var id = selectedRow.ID;
            location.href = "/Admin/Role/Update?id=" + id;
        }
    });


    //回收站
    if ($("#recycleRoleLst").length > 0) {
        $('#recycleRoleLst').datagrid({
            width: 1000,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/Role/GetRoleRecycles',
            // queryParams: { "rate": 0 },
            loadMsg: '数据加载中请稍后……',
            pagination: true,
            rownumbers: true,
            rowStyle: function (index, row) {
                return 'height:100px;';
            },
            columns: [[
                   { field: 'RoleName', title: '部门名称', align: 'center', width: 200, height: 200, },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }

    //彻底删除角色
    $("#btnDeleteRole").click(function () {
        $.messager.confirm('确认', '确认将选中的角色删除?', function (row) {
            if (row) {
                var selectedRow = $('#recycleRoleLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除的角色");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Role/CompleteDelete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#recycleRoleLst").datagrid('reload');
                    });
                }
            }
        })
    });

    //还原角色
    $("#btnReturn").click(function () {
        $.messager.confirm('确认', '确认将选中的角色还原?', function (row) {
            if (row) {
                var selectedRow = $('#recycleRoleLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要还原的角色");
                    return;
                } else {
                    var id= selectedRow.ID;
                    $.post("/Admin/Role/Return", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#recycleRoleLst").datagrid('reload');
                    });
                }
            }
        })
    });

    //删除角色
    $("#btnCompleteDeleteRole").click(function () {
        $.messager.confirm('确认', '确认将选中的角色删除?', function (row) {
            if (row) {
                var selectedRow = $('#recycleRoleLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除选中的角色");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Role/CompleteDelete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#recycleRoleLst").datagrid('reload');
                    });
                }
            }
        })
    });

    //角色授权
    $("#btnAuthorize").click(function () {
        var selectedRow = $('#roleLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择要授权的角色");
            return;
        }
        $("#authorizeWarning").html("<input type='hidden' id='authoirzeRid' value=" + selectedRow.ID + ">");
        $("#wauthorize").window("open");
        if ($("#apermissionLst").length > 0) {
            $('#apermissionLst').treegrid({
                width: 600,
                height: 300,
                striped: true,
                rownumbers: true,
                singleSelect: false,
                url: '/Admin/Role/GetPermissionsToAuthorize?rid=' + selectedRow.ID,
                loadMsg: '数据加载中请稍后……',
                idField: 'id',
                treeField: 'text',
                columns: [[
                       {
                           field: 'text', title: '权限名称', align: 'center', width: 200, height: 200,
                           formatter: formatcheckbox
                       },
                ]]
            });
        }
    });

    //查看角色权限
    $("#btnLookAuthorize").click(function () {
        var selectedRow = $('#roleLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请选择要查看的角色");
            return;
        }
        console.log();
        $("#wlookauthorize").window("open");
        if ($("#detailpermissionLst").length > 0) {
            $('#detailpermissionLst').treegrid({
                width: 600,
                height: 300,
                striped: true,
                rownumbers: true,
                singleSelect: false,
                url: '/Admin/Role/GetRolePermissions?rid=' + selectedRow.ID,
                loadMsg: '数据加载中请稍后……',
                idField: 'id',
                treeField: 'text',
                columns: [[
                       { field: 'id', title: 'ID', align: 'center', width: 200, height: 200, },
                       {
                           field: 'text', title: '权限名称', align: 'center', width: 200, height: 200,
                       }
                ]]
            });
        }
    });

});