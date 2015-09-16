$(document).ready(function () {

    if ($("#permissionLst").length > 0) {
        $('#permissionLst').treegrid({
            width: 1000,
            height: 500,
            striped: true,
            rownumbers: true,
            singleSelect: true,
            iconCls: 'icon-save',//头部标题图标  
            animate: true,//动画效果  
            collapsible: true,//是否折叠  
            url: '/Admin/Permission/GetPermissions1',
            loadMsg: '数据加载中请稍后……',
            idField: 'id',
            treeField: 'text',
            columns: [[
                   {field: 'id', title: 'ID', align: 'center', width: 200, height: 200, },
                   {
                       field: 'text', title: '权限名称', align: 'center', width: 200, height: 200,
                   },
                   //{ field: 'PURL', title: '访问路径', align: 'center', width: 200, height: 200, },
                   ////{ field: 'IsShow', title: '是否展示', align: 'center', width: 200, height: 200, },
                   // { field: 'AddTimeAsString', title: '增加时间', align: 'center', width: 200, height: 200, },
                   // { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }

    $("#btnDelPermission").click(function () {
        $.messager.confirm('确认', '确认删除?', function (row) {
            if (row) {
                var selectedRow = $('#permissionLst').treegrid('getSelected');  //获取选中行  
                var id = selectedRow.id;
                $.post("/Admin/Permission/Delete", "id=" + id, function (data) {
                    if (data.Statu == "ok") {
                        alert("删除成功！");
                        $("#permissionLst").treegrid('reload');
                    }
                });
            }
        })
    })

    //跳转到修改页面
    $("#btnUpdatePermisssion").click(function () {
        var selectedRow = $('#permissionLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            alert("请筛选要修改的内容！");
        } else {
            var id = selectedRow.id;
            location.href = "/Admin/Permission/Update?id=" + id;
        }
    });

    //权限回收站
    if ($("#recyclePermissionLst").length > 0) {
        $("#recyclePermissionLst").datagrid({
            width: 1000,
            height: 300,
            striped: true,
            singleSelect: true,
            url: '/Admin/Permission/GetRecycles',
            // queryParams: { "rate": 0 },
            loadMsg: '数据加载中请稍后……',
            pagination: true,
            rownumbers: true,
            rowStyle: function (index, row) {
                return 'height:100px;';
            },
            columns: [[
                   { field: 'PName', title: '权限名称', align: 'center', width: 200, height: 200, },
                   { field: 'PURL', title: '访问路径', align: 'center', width: 200, height: 200, },
                   { field: 'AddTime', title: '增加时间', align: 'center', width: 200, height: 200, },
                   { field: 'Remark', title: '备注', align: 'center', width: 200, height: 200, }
            ]]
        });
    }
    //彻底删除权限  
    $("#btnCompleteDelete").click(function () {
        $.messager.confirm('确认', '确认将选中的权限彻底删除?', function (row) {
            if (row) {
                var selectedRow = $('#recyclePermissionLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要删除的权限");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Permission/CompleteDelete", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#recyclePermissionLst").datagrid('reload');
                    });
                }
            }
        })
    });

    //还原权限     
    $("#btnReturn").click(function () {
        $.messager.confirm('确认', '确认将选中的返回到权限列表?', function (row) {
            if (row) {
                var selectedRow = $('#recyclePermissionLst').datagrid('getSelected');  //获取选中行  
                if (selectedRow == null) {
                    alert("请选择要还原的权限");
                    return;
                } else {
                    var id = selectedRow.ID;
                    $.post("/Admin/Permission/Return", "id=" + id, function (data) {
                        alert(data.Msg);
                        $("#recyclePermissionLst").datagrid('reload');
                    });
                }
            }
        })
    });

    //增加权限
    $("#btnAddPermission").click(function () {
        var selectedRow = $('#permissionLst').treegrid('getSelected');  //获取选中行  
        if (selectedRow == null) {
            location.href = "/Admin/Permission/Add?fid=" + 0;
        } else {
            var id = selectedRow.id;
            location.href = "/Admin/Permission/Add?fid=" + id;
        }
    });
});