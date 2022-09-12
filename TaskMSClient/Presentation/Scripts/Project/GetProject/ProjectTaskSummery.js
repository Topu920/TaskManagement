var gbMember = [];
var gbStatus = [];
var gbGroup = [];
var ProjectTaskSummeryManager = {
    GetAllMember: function () {
        //var searchMember = $("#memberSearch").val();
        var objAllMember = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Member/GetMemberList";///search=" + searchMember;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
             
            objAllMember = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objAllMember;
    },
    GetAllGroup: function () {
        var objAllGroup = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/GroupMember/GetAllGroupList?userId=" + userEmpId;
        //debugger;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objAllGroup = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objAllGroup;
    },
    GetAllStatus: function () {
        //var searchMember = $("#memberSearch").val();
        var objAllStatus = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/CmnStatues/GetStatuesByFlagNo?id=2";///search=" + searchMember;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objAllStatus = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objAllStatus;
    },
    GetAlltaskByProjectId: function () {
        var projectId = $("#projectId").val();
        var objTask = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Task/GetAllTaskListByProjectId?projectId=" + projectId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objTask = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objTask;
    },
    SaveTask: function (rowData) {
        debugger;
        var task = ProjectTaskSummeryHelper.CreateTaskObj(rowData);
        var jsonParam = JSON.stringify(task);
        var serviceUrl = _baseUrl + "/api/Task/InsertTaskInfo";
        AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
             
            var msg = jsonData.message;
            console.log(jsonData);
            if (jsonData.success) {
                 
                var data = ProjectTaskSummeryManager.TaskDataSource();
                var grid = $("#gridTaskSummery").data("kendoGrid");
                grid.setDataSource(data);
                //debugger;
                toastr.success(msg);
            }

            else {
                 
                //AjaxManager.MsgBox('error', 'center', 'Error1', msg,
                //    [{
                //        addClass: 'btn btn-primary', text: 'Ok', onClick: function ($noty) {
                //            $noty.close();
                //        }
                //    }]);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: msg,
                })
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
    TaskDataSource: function () {
         
        var dataSource = new kendo.data.DataSource({
            pageSize: 10,
            data: ProjectTaskSummeryManager.GetAlltaskByProjectId(),
            //autoSync: false,
            schema: {
                model: {
                    id: "taskId",
                    fields: {
                        taskId: { editable: false, nullable: true },
                        taskName: { validation: { required: true } },
                        taskDescription: {},
                        // statueName: { editable: false},
                        memberInfo: { defaultValue: { empId: 0, name: "---Select Member---" } },
                        groupInfoDto: { defaultValue: { groupId: AjaxManager.DefaultGuidId(), groupName: "---Select Group---" } },
                        statuesInfoDto: { defaultValue: { statusId: 'de7ff021-aaca-4c45-8181-af8267a744a5', statusName: "To Do" } },
                        eddate: {
                            type: "date",
                            template: '#=kendo.toString("dd-MMM-yyyy")#',
                            editable: true
                        }
                    }
                }
            },
            change: function (e) {
               // debugger;

                if (e.action === "itemchange") {
                    var rowData = e.items[0];
                    if (e.field === "taskName") { //|| e.field === "taskDescription" || e.field === "eddate" || e.field === "statuesInfoDto"
                         
                        ProjectTaskSummeryManager.SaveTask(rowData);
                    }
                    else {
                        var gridSummary = $("#gridTaskSummery").data("kendoGrid");

                        var tr = $(e.currentTarget).closest("tr");
                        var row = gridSummary.select(tr);

                        var selectedItem = gridSummary.dataItem(row);
                        ProjectTaskSummeryManager.SaveTask(selectedItem);
                    }

                }
                //if (projectDetails[0].createBy !== userEmpId) {
                //    $('.k-grid-add').hide();

                //}

                //if (selectedItem != null) {
                //    debugger;
                //    //ProjectTaskSummeryHelper.CreateTaskObj(selectedItem);
                //    var obj = new Object();
                //    obj.TaskId = selectedItem.taskId;
                //    obj.ProjectId = selectedItem.projectId;
                //    //obj.StatusId = $("#txtDescription").val();
                //    obj.TaskDescription = selectedItem.taskDescription;
                //    //obj.StartingDate = $("#txtStartDate").data("kendoDatePicker").value();
                //    obj.Eddate = selectedItem.eddate.toString();
                //    //obj.StatusId = ;
                //    //obj.CreateBy = CurrentUser.USERID;
                //    //obj.CreateBy = 188807;
                //    return obj;
                //}


                //console.log(selectedItem);
            }

        });
        return dataSource;
    },
    TaskDataSourceBlank: function () {
         
        var dataSource = new kendo.data.DataSource({
            pageSize: 10,
            data: [],
            schema: {
                model: {
                    id: "taskId",
                    fields: {
                        taskId: { editable: false, nullable: true },
                        taskName: { validation: { required: true } },
                        memberInfo: { defaultValue: { empId: 0, name: "---Select Member---" } },
                        groupInfoDto: { defaultValue: { groupId: AjaxManager.DefaultGuidId(), groupName: "---Select Group---" } },
                        statuesInfoDto: { defaultValue: { statusId: 'de7ff021-aaca-4c45-8181-af8267a744a5', statusName: "To Do" } },
                        eddate: {
                            type: "date",
                            template: '#=kendo.toString("dd-MMM-yyyy")#',
                            editable: true
                        }
                    }
                }
            },

        });
        return dataSource;
    },
};
var ProjectTaskSummeryHelper = {
    Init: function () {
        gbMember = ProjectTaskSummeryManager.GetAllMember();
        gbStatus = ProjectTaskSummeryManager.GetAllStatus();
        gbGroup = ProjectTaskSummeryManager.GetAllGroup();
        ProjectTaskSummeryHelper.GenerateGrid();
        //var itemGrid = $("#gridTaskSummery").data("kendoGrid");
        //itemGrid.dataSource.bind("change", function (e) {
        //    ProjectTaskSummeryHelper.GridChangeEvent(e);
        //});
        var data = ProjectTaskSummeryManager.TaskDataSource();
        var grid = $("#gridTaskSummery").data("kendoGrid");
         
        grid.setDataSource(data);
        //debugger

        $("#taskListPanelbar").kendoPanelBar({
            expandMode: "multiple",
            expanded: true
        });
    },

    GenerateGrid: function () {
        $("#gridTaskSummery").kendoGrid({
            dataSource: ProjectTaskSummeryManager.TaskDataSourceBlank(),
            pageable: true,
            height: 550,
            toolbar: [{ name: "create", text: "Add Task" }
            ],
            columns: [
                { field: "taskId", hidden: true },
                { field: "projectId", hidden: true },
                { field: "taskName", title: "Task Name", width: 20, headerAttributes: { style: "text-align: center; justify-content: center" } },
                { field: "taskDescription", title: "Description", width: 20 }, //editor: categoryDropDownEditor, template: "#=Category.CategoryName#"
                {
                    field: "eddate", title: "ED Date", width: 15, headerAttributes: { style: "text-align: center; justify-content: center" },
                    attributes: { style: "text-align: center" },
                    template: '#=kendo.toString(eddate==null?"-":kendo.parseDate(eddate),"dd-MMM-yyyy")#',
                },
                { field: "statuesInfoDto", title: "Status", width: 15, editor: statusDropDownEditor, template: "#=statuesInfoDto.statusName#" },
                { field: "memberInfo", title: "Assign Member", width: 20, editor: memberDropDownEditor, template: $("#empTemplate").html() },/*template: "#=memberInfo==undefined? 'add mem ':memberInfo.name#"*/
                { field: "groupInfoDto", title: "Assign Group", width: 20, editor: groupDropDownEditor, template: $("#groupTemplate").html() },
                // { command: "destroy", title: " ", width: "150px" }
                {
                    command: [{
                        name: "edit", text: "", iconClass: "k-icon k-i-edit", className: "k-success", click: ProjectTaskSummeryHelper.ClickEventForEditButton
                    }], width: 5, title: "&nbsp;"
                }
            ],
            editable: true,
            selectable: "row",
            edit: function (e) {
                //debugger;

                if (e.model.createBy != userEmpId && e.model.createBy != undefined) {
                    this.closeCell();
                }

            },
            dataBound: function (e) {
               // debugger;
                var data = e.sender.dataSource._data;
                if (data.length > 0) {
                    if (data[0].createBy != userEmpId && data[0].createBy != undefined) {
                        $('.k-grid-toolbar').remove();
                    }
                }
                
            }
        });

        function memberDropDownEditor(container, options) {
            //$('<select data-bind="value:' + options.field + '" />')
            //    .appendTo(container)
            //    .kendoMultiSelect({
            //       // autoBind: false,
            //       // valuePrimitive: false,
            //        dataTextField: "name",
            //        dataValueField: "empId",
            //        optionLabel: '--Select--',

            //        dataSource: {
            //            data: gbMember,
            //        },
            //    });
            $('<input data-text-field="name" data-value-field="empId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoMultiSelect({
                    // autoBind: false,
                   // dataTextField: "name",
                    template: "#= name # (#=empCode #)",
                    dataValueField: "empId",
                    optionLabel: {
                        name: "Select Member",
                        empId: 0
                    },
                    dataSource: gbMember,
                    index: 0,
                    filter: "contains",

                });
        }
        function groupDropDownEditor(container, options) {
            $('<input data-text-field="groupName" data-value-field="groupId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoMultiSelect({
                    // autoBind: false,
                    dataTextField: "groupName",
                    dataValueField: "groupId",
                    optionLabel: '--Select--',
                    dataSource: gbGroup,
                    index: 0
                });
        }
        function statusDropDownEditor(container, options) {

            $('<input required name="' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    autoBind: false,
                    dataTextField: "statusName",
                    dataValueField: "statusId",
                    dataSource: {
                        data: gbStatus,
                    },
                });
        }
    },
    CreateTaskObj: function (selectedRowData) {
         
        var obj = new Object();

        obj.ProjectId = selectedRowData.projectId === null || selectedRowData.projectId === undefined ? $("#projectId").val() : selectedRowData.projectId;
        obj.TaskId = selectedRowData.taskId === null || selectedRowData.projectId === undefined ? AjaxManager.DefaultGuidId() : selectedRowData.taskId;
        obj.TaskName = selectedRowData.taskName;
        obj.TaskDescription = selectedRowData.taskDescription;
        obj.StatusId = selectedRowData.statuesInfoDto.statusId;
        obj.Eddate = selectedRowData.eddate;
        obj.memberInfo = ProjectTaskSummeryHelper.CreateMemberObj(selectedRowData.memberInfo);
        obj.groupInfoDto = ProjectTaskSummeryHelper.CreateGroupObj(selectedRowData.groupInfoDto);
        //obj.memberInfo = selectedRowData.memberInfo;
        //obj.groupInfoDto = selectedRowData.groupInfoDto;

        //var rowData;
        //obj.TaskName = rowData.taskName;
        //obj.StatusId = rowData.statuesInfoDto === '' || rowData.statuesInfoDto === undefined ? "de7ff021-aaca-4c45-8181-af8267a744a5" : rowData.statuesInfoDto.statusId;
        //obj.TaskDescription = rowData.taskDescription;
        ////obj.StartingDate = $("#txtStartDate").data("kendoDatePicker").value();
        //obj.Eddate = rowData.eddate;
        //obj.StatusId = ;
        //obj.CreateBy = CurrentUser.USERID;
        obj.CreateBy = userEmpId;
         
        return obj;
    },
    CreateMemberObj: function (memberInfo) {
        var memberList = [];
        for (var i = 0; i < memberInfo.length; i++) {
            var obj = new Object();
            obj = memberInfo[i];
            obj.isUserActive = true;
           // obj.EmailAddress = " ";
            memberList.push(obj);
        }
        return memberList;

    },
    CreateGroupObj: function (groupInfoDto) {
        var groupList = [];
        for (var i = 0; i < groupInfoDto.length; i++) {
            var obj = new Object();
            obj = groupInfoDto[i];
            obj.isUserActive = true;
            groupList.push(obj);
        }
        //debugger;
        return groupList;
    },
    //edit Button
    ClickEventForEditButton: function (e) {
        e.preventDefault();
        debugger;
        var grid = $("#gridTaskSummery").data("kendoGrid");
        var tr = $(e.currentTarget).closest("tr");
        var selectedItem = this.dataItem(tr);
        grid.select(tr);

        //console.log("sele", selectedItem);
        if (selectedItem.taskId != null) {
            ProjectTaskDetailsHelper.PassSelectedRowData(selectedItem);
            $("#TaskModalCenter").modal('show');

            //PIDetailsHelper.ResetForm();
            //$("#divPIDetails").show();
            //$("#divPISummary").hide();
            //$("#btnSavePIInfo").text(" Update");
            //$("#btnSavePIInfo").addClass("fa fa-save");
            //PIDetailsHelper.FillPIDetailsForm(selectedItem);
        }

    },
    
};