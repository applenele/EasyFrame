document.onkeydown = function (e) {
    if ($(".bac").length == 0) {
        if (!e) e = window.event;
        if ((e.keyCode || e.which) == 13) {
            var obtnLogin = document.getElementById("btnLogin")
            obtnLogin.focus();
        }
    }
}

$(document).ready(function () {

    $('#btnLogin').click(function () {
        changeCheckCode();
        var username = $('#txtUsername').val();
        var password = $('#txtPassword').val();
        var code = $("#txtCode").val();
        if (username == '') {
            show_err_msg("用户名不能为空");
            return;
        }
        if (password == '') {
            show_err_msg("密码不能为空");
            return;
        }
        if (code == '') {
            show_err_msg("验证码不能为空");
            return;
        }

        $.ajax({
            url: "/Login/DoLogin",
            method: "post",
            data: { "username": username, "password": password, "code": code },
            success: function (data) {
                if (data.Statu == "ok") {
                    alert(data.Msg);
                    location.href = data.BackUrl;
                }
                else if (data.Statu == "err") {
                    show_err_msg(data.Msg);
                }
            },
            error: function (data) {
                show_err_msg("登陆出错请重试！");
            }
        })
    });



 
});