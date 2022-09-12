var ProjectInfoManager = {
    EditProjectInfo: function () {
        var editProject = ProjectInfoHelper.EditProjectObject();
        var jsonParam = JSON.stringify(editProject);
        var serviceUrl = _baseUrl + "/api/Project/InsertProjectInfo";
        AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.Message;
            console.log(jsonData);
            if (jsonData.success) {
                $("#hdnProjectId").val(jsonData.projectInfoDto.projectId);
                ProjectInfoHelper.FillProjectInfoInForm(jsonData.projectInfoDto);
                var grid = $("#gridProjectSummery").data("kendoGrid");
                var data = ProjectSummaryManager.GetAllProject();
                var dataSource = new kendo.data.DataSource({
                    data: data,
                    pageSize: 10
                });
                grid.setDataSource(dataSource);
                //AjaxManager.MsgBox('success', 'center', 'Success:', msg,
                //    [{
                //        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
                //            //$noty.close();
                //            $("#hdnProjectId").val(jsonData.projectInfoDto.projectId);
                //            //$("#txtPINo").val(jsonData.createPIDto.Pino);
                //            //$("#grdPISummary").data("kendoGrid").dataSource.read();
                //        }
                //    }]);

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
    }
};
var ProjectInfoHelper = {
    Init: function () {

        ProjectInfoHelper.GenerateEditFrom();

        $("#panelbarMember").kendoPanelBar({
            expandMode: "single"
        });
        $("#panelbar").kendoPanelBar({
            expandMode: "multiple",
            expanded: true
        });
        $("#btnBackProject").click(function () {
            $("#divProjectInfo").addClass('d-none');
            $("#divProjectSummery").removeClass('d-none');
        });

        $("input.edit, textarea.edit").change(function () {
            ProjectInfoManager.EditProjectInfo();
        });
    },

    //GenerateMemberGrid: function () {
    //    $("#gridProjectMember").kendoGrid({
    //        dataSource: {
    //            data: [
    //                { name: "Jane Doe", age: 30 },
    //                { name: "John Doe", age: 33 },
    //                { name: "Mike Doe", age: 31 },
    //                { name: "Tom Doe", age: 35 },
    //                { name: "Danny Doe", age: 37 },
    //                { name: "Danny Doe", age: 37 },
    //                { name: "Danny Doe", age: 37 },
    //                { name: "Danny Doe", age: 37 },
    //                { name: "Danny Doe", age: 37 },
    //                { name: "Danny Doe", age: 37 },
    //            ],
    //            pageSize: 10
    //        },
    //        sortable: true,
    //        pageable: {
    //            pageSizes: true,
    //        },
    //        columns: [{
    //            template: "<div class='customer-photo'" +
    //                "style='background-image: url(./img/smallLogo.png)'></div>" +
    //                "<div class='customer-name'>#: name  #</div>",
    //            field: "name",
    //            width: 240
    //        }, {
    //            field: "age",
    //            title:"Role"
    //        }]
    //    });
    //},
    GenerateDateEditFrom: function () {
        $("#editTxtStartDate").kendoDatePicker({
            format: "dd/MM/yyyy"
        });
        $("#editTxtDueDate").kendoDatePicker({
            format: "dd/MM/yyyy"
        });
    },

    GenerateEditFrom: function () {
        ProjectInfoHelper.GenerateDateEditFrom();
        //ProjectInfoHelper.GenerateMemberGrid();
        CmnComboBoxHelper.LoadStatusByFlagNo("editCmbStatus", 1);
    },

    FillProjectInfoInForm: function (selectedItem) {
        debugger;
        $("#editHdnProjectId").val(selectedItem.projectId);
        $("#editTxtTitle").val(selectedItem.projectName);
        $("#editTxtDescription").val(selectedItem.projectDescription);
        $("#editTxtStartDate").data("kendoDatePicker").value(selectedItem.startingDate);
        $("#editTxtDueDate").data("kendoDatePicker").value(selectedItem.dueDate);
        $("#editCmbStatus").data("kendoComboBox").value(selectedItem.statusId);
    },
    EditProjectObject: function () {
        var obj = new Object();
        obj.ProjectId = $("#editHdnProjectId").val();
        obj.ProjectName = $("#editTxtTitle").val().trim() === "" || $("#editTxtTitle").val().trim() === null
            ? "Untitle" : $("#editTxtTitle").val();
        obj.ProjectDescription = $("#editTxtDescription").val();
        obj.StartingDate = kendo.toString($("#editTxtStartDate").data("kendoDatePicker").value(), "dd-MMM-yyyy");
        obj.DueDate = kendo.toString($("#editTxtDueDate").data("kendoDatePicker").value(), "dd-MMM-yyyy");
        //obj.FinishingDate = "";
        obj.StatusId = $("#editCmbStatus").data("kendoComboBox").value() === "" || $("#cmbStatus").data("kendoComboBox").value() === undefined
            ? "5297d227-724f-47ea-8667-311f5c86cf18" : $("#editCmbStatus").data("kendoComboBox").value();
        //obj.CreateBy = CurrentUser.USERID;
        debugger;
        return obj;
    },
}