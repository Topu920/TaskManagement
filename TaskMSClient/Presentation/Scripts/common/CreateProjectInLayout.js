$(document).ready(function () {
    ProjectInLayoutHelper.Init();
});

var ProjectInLayoutManager = {

    SaveProject: function () {
        debugger;

        var createProject = ProjectInLayoutHelper.CreateProjectObject();
        var jsonParam = JSON.stringify(createProject);
        var serviceUrl = _baseUrl + "/api/Project/InsertProjectInfo";
        AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.message;
            console.log('Json',jsonData);
            if (jsonData.success) {
                debugger;
                toastr.success(msg);
                ProjectInLayoutHelper.ResetForm();
                //gblNav();
                var getUrl = window.location;
                var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[0];
                console.log('baseUrl', baseUrl);
                console.log("jsonData.projectInfoDto.projectId", jsonData.projectInfoDto.projectId);
                window.location.replace(baseUrl + "Project/GetProject/" + jsonData.projectInfoDto.projectId);
            }

            else {
                debugger;
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: msg,
                })
                //AjaxManager.MsgBox('error', 'center', 'Error1', msg,
                //    [{
                //        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
                //            $noty.close();
                //        }
                //    }]);
            }
        }
        function onFailed(error) {
            debugger;
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: error.responseJSON.title,
            })
            //AjaxManager.MsgBox('error', 'center', 'Error', error.statusText,
            //    [{

            //        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
            //            $noty.close();
            //        }
            //    }]);
        }
    },

};

var ProjectInLayoutHelper = {
    Init: function () {
        ProjectInLayoutHelper.GenerateDate();
        CmnComboBoxHelper.LoadStatusByFlagNo("cmbStatus", 1);

        $('#projectModalCenter').on("hidden.bs.modal", function () {
            debugger;
            ProjectInLayoutManager.SaveProject();
        });
    },
    GenerateDate: function () {
        //$("#txtStartDate").kendoDatePicker({
        //    format: "dd/MM/yyyy"
        //});
        $("#txtDueDate").kendoDatePicker({
            format: "dd/MM/yyyy"
        });
    },

    CreateProjectObject: function () {
        var obj = new Object();
        obj.ProjectId = $("#hdnProjectId").val() === "" ? "00000000-0000-0000-0000-000000000000" : $("#hdnProjectId").val();
        obj.ProjectName = $("#txtTitle").val().trim() === "" || $("#txtTitle").val().trim() === null
            ? "Untitle" : $("#txtTitle").val();
        obj.ProjectDescription = $("#txtDescription").val();
        //obj.StartingDate = $("#txtStartDate").data("kendoDatePicker").value();
        obj.DueDate = $("#txtDueDate").data("kendoDatePicker").value();
        obj.StatusId = $("#cmbStatus").data("kendoComboBox").value() === "" || $("#cmbStatus").data("kendoComboBox").value() === undefined
            ? "5297d227-724f-47ea-8667-311f5c86cf18" : $("#cmbStatus").data("kendoComboBox").value();
        //obj.CreateBy = CurrentUser.USERID;
        obj.CreateBy = userEmpId;
        return obj;
    },
    ResetForm: function () {
        $("#hdnProjectId").val("");
        $("#txtTitle").val("");
        $("#txtDescription").val("");
    }

};