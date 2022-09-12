var LoginDetailsManager = {
    CheckMember: function () {
        if (LoginDetailsHelper.ValidateForm()) {
            LoginDetailsHelper.CreateStore();
            var loginUser = LoginDetailsHelper.CreateLoginUserObject();
            var jsonParam = JSON.stringify(loginUser);
            var serviceUrl = _baseUrl + "/api/User/UserLogIn";
            AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.Message;
            console.log(jsonData);
            if (jsonData.success) {
                debugger;
                var getUrl = window.location;
                var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[0];
                window.location.replace(baseUrl + "Home/Privacy?logInDto=" + JSON.stringify(jsonData.logInDto));
            }
            else {
                debugger;
                AjaxManager.MsgBox('error', 'center', 'Error1', msg,
                    [{
                        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
                            $noty.close();
                        }
                    }]);
            }
        }
        function onFailed(error) {
            debugger;
            console.log(error.status);
            if (error.status === 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: "You Enter Wrong UserId Or Password!!!",
                })
            }
            //AjaxManager.MsgBox('error', 'center', 'Error', error.statusText,
            //    [{

            //        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
            //            $noty.close();
            //        }
            //    }]);
        }
    }
};

var LoginDetailsHelper = {
    Init: function () {
        LoginDetailsHelper.FillData();

        $("#btnLogin").click(function () {
            LoginDetailsManager.CheckMember();
        });
        $('input').on("keypress", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                $("#btnLogin").click();
            }
        });
    },
    CreateLoginUserObject: function () {
        var obj = new Object();
        obj.EmpId = $("#userId").val();
        obj.UserPass = $("#password").val()
        return obj;
    },
    ValidateForm: function () {
        var res = true;
        var userId = $("#userId").val();
        if (userId === "" || userId === null || userId === undefined) {
            toastr.error("User Id Is Required");
            res = false;
        }
        var password = $("#password").val();
        if (password === "" || password === null || password === undefined) {
            toastr.error("Password Is Required");
            res = false;
        }
        return res;
    },
    FillData: function() {
        if (localStorage.user != undefined && localStorage.user != "" && localStorage.user != null) {
            var obj = JSON.parse(localStorage.user);
            $("#userId").val(obj.userId);
            $("#password").val(obj.password);
            $("#rememberMe").attr('checked','checked');
            $("#rememberMe").val(obj.rememberMe);
        }
    },
    CreateStore: function () {
        if ($("#rememberMe").is(':checked')) {
            localStorage.removeItem('user');
            var obj = new Object();
            obj.userId = $("#userId").val();
            obj.password = $("#password").val();
            $("#rememberMe").attr('value', true);
            obj.rememberMe = $("#rememberMe").attr('value');
            var user = JSON.stringify(obj);
            localStorage.setItem('user', user);
        }
    }
};