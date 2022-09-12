var ProjectSummaryManager = {
    GetAllProject: function () {
        var objProject = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Project/GetProjectList";
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objProject = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        //console.log(objProject)
        return objProject;
    },
};
var ProjectSummaryHelper = {
    Init: function () {
        ProjectSummaryHelper.GenerateGrid();
    },
    GenerateGrid() {
        $("#gridProjectSummery").kendoGrid({
            dataSource: {
                data: ProjectSummaryManager.GetAllProject(),
                pageSize:10
            },
            schema: {
                model: {
                    fields: {
                        dueDate: {
                            type: "date",
                            template: '#=kendo.toString("dd-MMM-yyyy")#',
                            editable: false

                        },
                    }
                }
            },
            filterable: false,
            sortable: true,
            noRecords: true,
            messages: {
                noRecords: "NO DATA FOUND"
            },
            pageable: {
                pageSizes: 10
            },
            columns: [
                { field: "projectId", hidden: true },
                { field: "statusId", hidden: true },
                { field: "projectName", title: "Project Name", width: 180, sortable: true },
                { field: "statusName", title: "Status", width: 80, sortable: true },
                { field: "dueDate", title: "Due Date", width: 140, sortable: true, template: '#=kendo.toString(dueDate==null?"-":kendo.parseDate(dueDate),"dd-MMM-yyyy")#'},
                //{ field: "dueDate", title: "Due Date", width: 140, sortable: true, template: '#= kendo.toString(kendo.parseDate(dueDate), "dd-MMM-yyyy")#'},
                {
                    command: [{
                        name: "edit", text: "View", iconClass: "k-icon k-i-edit", className: "k-success", click: ProjectSummaryHelper.ClickEventForViewButton
                    }], width: 20, title: "&nbsp;"
                }
            ],
            editable: false,
            selectable: "row",
            navigatable: true,
            scrollable: false
        });

    },

    ClickEventForViewButton: function (e) {
        e.preventDefault();
        debugger;
        var grid = $("#gridProjectSummery").data("kendoGrid");
        var tr = $(e.currentTarget).closest("tr");
        var selectedItem = this.dataItem(tr);
        grid.select(tr);
        if (selectedItem != null) {
            debugger;
            console.log("selected", selectedItem);
            $("#divProjectInfo").removeClass('d-none');
            $("#divProjectSummery").addClass('d-none');
            ProjectInfoHelper.FillProjectInfoInForm(selectedItem);
        }
    }
};