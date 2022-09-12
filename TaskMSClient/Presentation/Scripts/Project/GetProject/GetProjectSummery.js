var gbProjectDetails = [];
var GetProjectSummeryManager = {
    GetProjectByid: function (id) {
        var objProject = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Project/GetProjectListById?id=" + id;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objProject = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objProject;
    },
    EditProjectInfo: function () {
        var editProject = GetProjectSummeryHelper.EditProjectObject();
        var jsonParam = JSON.stringify(editProject);
        var serviceUrl = _baseUrl + "/api/Project/InsertProjectInfo";
        AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.Message;
            console.log(jsonData);
            if (jsonData.success) {
   
                $("#editProjectTitle").replaceWith(`<h5 ondblclick="GetProjectSummeryHelper.DoubleClickTitle()"  id="projectTitle" class="font-weight-bold text-uppercase">${jsonData.projectInfoDto.projectName}</h5>`);
                gblNav();
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
            AjaxManager.MsgBox('error', 'center', 'Error', error.statusText,
                [{

                    addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
                        $noty.close();
                    }
                }]);
        }
    },
    GetProjectHistoryById: function (id) {
        var objHistory = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/History/GetHistoryInfoList?projectId=" + id ;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objHistory = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objHistory;
    },
};
var GetProjectSummeryHelper = {
    Init: function () {
        var id = $("#projectId").val();
        var projectDetails = GetProjectSummeryManager.GetProjectByid(id);
        debugger;
        if (projectDetails.length > 0) {
            debugger;
            $("#projectTitle").text(projectDetails[0].projectName);
            gbProjectDetails = projectDetails[0];
        }

        $("#projectTitle").dblclick(function () {
            debugger
            if (projectDetails[0].createBy === userEmpId) {
                var projectTitle = $(this).text().trim();
                //$("div#projectTitle").replaceWith(`<input id="editProjectTitle"/>`);
                //$("#editProjectTitle").val(projectTitle);
                $(this).contents().text("");
                //$(this).contents().unwrap().wrap('<input id="editProjectTitle"/>');
                $(this).replaceWith("<input  id='editProjectTitle' onChange='GetProjectSummeryManager.EditProjectInfo()' style=' border: none; font-size:16px'/ >");
                $("#editProjectTitle").val(projectTitle);
            }
        });
        //$("#editProjectTitle").change(function () {
        //    debugger;
        //    GetProjectSummeryHelper.EditProjectObject();
        //});
        GetProjectSummeryHelper.GridProjectHistory();
        $("#btnHistory").click(function () {
            var gridData = GetProjectSummeryManager.GetProjectHistoryById(id);
            var grid = $("#gridProjectHistory").data("kendoGrid");
            grid.setDataSource(gridData);
            $("#gridProjectHistory .k-grid-header").css("display", "none");
        });
    },
    GridProjectHistory: function () {
        $("#gridProjectHistory").kendoGrid({
            dataSource: [],
            columns: [
                {
                    template: "<p>#: historyDescription # <span class='ml-3 text-muted'>#= $.timeago(createDate)#</span></p>"
                },
            ],
            scrollable: false,
            //height: 300,
        });
    },
    DoubleClickTitle: function () {
        var projectTitle = $("#projectTitle").text().trim();
        $("#projectTitle").contents().text("");
        $("#projectTitle").replaceWith("<input  id='editProjectTitle' onChange='GetProjectSummeryManager.EditProjectInfo()' style=' border: none;'/ >");
        $("#editProjectTitle").val(projectTitle);
    },
    EditProjectObject: function () {
        debugger;
        var obj = new Object();
        obj.ProjectId = $("#projectId").val();
        obj.ProjectName = $("#editProjectTitle").val().trim() === "" || $("#editProjectTitle").val().trim() === null
            ? "Untitle" : $("#editProjectTitle").val();
        //obj.ProjectDescription = $("#editTxtDescription").val();
        //obj.StartingDate = kendo.toString($("#editTxtStartDate").data("kendoDatePicker").value(), "dd-MMM-yyyy");
        //obj.DueDate = kendo.toString($("#editTxtDueDate").data("kendoDatePicker").value(), "dd-MMM-yyyy");
        ////obj.FinishingDate = "";
        //obj.StatusId = $("#editCmbStatus").data("kendoComboBox").value() === "" || $("#cmbStatus").data("kendoComboBox").value() === undefined
        //    ? "5297d227-724f-47ea-8667-311f5c86cf18" : $("#editCmbStatus").data("kendoComboBox").value();
        obj.ProjectDescription = gbProjectDetails.projectDescription;
        obj.StartingDate = gbProjectDetails.startingDate;
        obj.DueDate = gbProjectDetails.dueDate;
        obj.FinishingDate = gbProjectDetails.finishingDate;
        obj.StatusId = gbProjectDetails.statusId;
        obj.CreateBy = userEmpId;
        debugger;
        return obj;
    },
};