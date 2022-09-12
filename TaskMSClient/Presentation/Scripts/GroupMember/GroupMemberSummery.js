var GroupMemberSummeryManager = {
    GetAllGroup: function () {
        var objGroup = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/GroupMember/GetAllGroupMemberList?userId=" + userEmpId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            objGroup = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objGroup;
    },
    gridDataSource: function () {
        var gridDataSource = new kendo.data.DataSource({
            type: "json",
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            allowUnsort: true,
            pageSize: 10,
            transport: {
                read: {
                    url: _baseUrl + "/api/GroupMember/GetAllGroupMemberList?userId=" + userEmpId,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json",
                    cache: false,
                    async: false
                },
                parameterMap: function (options) {
                    return JSON.stringify(options);
                }
            },
            batch: true,
            group: [{
                field: "groupName",
               
                    //    dir: "asc"
            }],
            schema: {
                model: {
                    fields: {
                        groupId: { editable: false },
                        groupName: { editable: false },
                        groupMemberDetailsId: { editable: false },
                        memberUserId: { editable: false },
                        memberName: { editable: false },
                        designationName: { editable: false },
                        departmentName: { editable: false },
                    }
                }
            }

        });
        return gridDataSource;
    },
};
var GroupMemberSummeryHelper = {
    Init: function () {
        GroupMemberSummeryHelper.GenerateGrid();

    },
    GenerateGrid() {

        $("#gridGroupSummery").kendoGrid({
            dataSource: GroupMemberSummeryManager.gridDataSource(),
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
                { field: "groupId", hidden: true },
                { field: "groupName", width: 180, hidden: true, groupHeaderTemplate: 'Group Name: #=data.value #' },
                { field: "groupMemberDetailsId", hidden: true },
                { field: "memberUserId", hidden: true },
                { field: "memberName", title: "Member Name", width: 180, sortable: true },
                { field: "designationName", title: "Designation Name", width: 180, sortable: true },
                { field: "departmentName", title: "Department Name", width: 180, sortable: true },
                //{ field: "statusName", title: "Status", width: 80, sortable: true },
                //{ field: "dueDate", title: "Due Date", width: 140, sortable: true, template: '#=kendo.toString(dueDate==null?"-":kendo.parseDate(dueDate),"dd-MMM-yyyy")#' },
                ////{ field: "dueDate", title: "Due Date", width: 140, sortable: true, template: '#= kendo.toString(kendo.parseDate(dueDate), "dd-MMM-yyyy")#'},
                //{
                //    command: [{
                //        name: "edit", text: "View", iconClass: "k-icon k-i-edit", className: "k-success", click: GroupMemberSummeryHelper.ClickEventForViewButton
                //    }], width: 20, title: "&nbsp;"
                //}
            ],
           
            editable: false,
            selectable: "row",
            navigatable: true,
            scrollable: false,
            groupable: {
                enabled: false,
                showFooter: false
            },
            dataBound: function (e) {
                //this.expandRow(this.tbody.find("tr.k-master-row").first());

                var grid = this;
                $(".k-grouping-row").each(function (e) {
                    grid.collapseGroup(this);
                });

            },
        });

    },

    ClickEventForViewButton: function (e) {
        e.preventDefault();
        debugger;
        var grid = $("#gridGroupSummery").data("kendoGrid");
        var tr = $(e.currentTarget).closest("tr");
        var selectedItem = this.dataItem(tr);
        grid.select(tr);
        if (selectedItem != null) {
            debugger;
            console.log("selected", selectedItem);
            $("#divGroupDetails").removeClass('d-none');
            $("#divGroupSummery").addClass('d-none');
            //ProjectInfoHelper.FillProjectInfoInForm(selectedItem);
        }
    }
};