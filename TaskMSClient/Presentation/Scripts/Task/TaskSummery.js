var gbSelectitem = {};
var gbFileArray = [];
var TaskSummeryManager = {
    GetTaskDetailsByUserId: function () {
        var empId = userEmpId;//userEmpId;//19281
        var objTask = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Task/GetAllTaskListByUser?empId=" + empId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            objTask = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objTask;
    },
    SaveTask: function (rowData) {
        var task = TaskSummeryHelper.CreateTaskObj(rowData);
        var jsonParam = JSON.stringify(task);
        var serviceUrl = _baseUrl + "/api/Task/UpdateTaskInfoByMember";
        AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.message;
            console.log(jsonData);
            if (jsonData.success) {
                debugger;
                var data = TaskSummeryManager.GetTaskDetailsByUserId();
                var grid = $("#gridMyTaskSummery").data("kendoGrid");
                grid.setDataSource(data);
                $("#TaskModalCenter").modal('hide');
                toastr.success(msg);
            }

            else {
                debugger;
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
        }
    },
    SaveCommentAndFile: function () {
        if (TaskSummeryHelper.ValidateForm()) {
            var commentAndFile = TaskSummeryHelper.CreateCommentAndFileObj();
            var jsonParam = JSON.stringify(commentAndFile);
            var serviceUrl = _baseUrl + "/api/Comment/InsertComment";
            AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.message;
            console.log(jsonData);
            if (jsonData.success) {
                debugger;

                TaskSummeryHelper.ClearForm();

                toastr.success(msg);
                var gridData = TaskSummeryManager.GetCommentAndFileByTaskId(gbSelectitem.taskId);
                var grid = $("#gridComment").data("kendoGrid");
                grid.setDataSource(gridData);
                $("#gridComment .k-grid-header").css("display", "none");
            }

            else {
                debugger;
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
        }
        
    },
    GetTaskHistoryById: function (taskId) {
        var objHistory = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/History/GetHistoryInfoList?taskId=" + taskId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objHistory = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objHistory;
    },
    GetCommentAndFileByTaskId: function (taskId) {
        var objComments = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Comment/GetCommentListByTaskId?taskId=" + taskId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objComments = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return objComments;
    }
};
var TaskSummeryHelper = {
    Init: function () {

        $("#myTaskListPanelbar").kendoPanelBar({
            expandMode: "multiple",
            expanded: true
        });
        TaskSummeryHelper.GenerateGrid();
        TaskSummeryHelper.Dropdown();

        var dataSource = CmnComboBoxManager.GetStatusByFlagNo(2);
        debugger;
        var dropdownlist = $("#txtTaskStatus").data("kendoDropDownList");
        dropdownlist.setDataSource(dataSource);
        //var data = ProjectTaskSummeryManager.TaskDataSource();
        //var grid = $("#gridMyTaskSummery").data("kendoGrid");
        //grid.setDataSource(data);


        $("#btnHistory").click(function () {
            var gridData = TaskSummeryManager.GetTaskHistoryById(gbSelectitem.taskId);
            var grid = $("#gridTaskHistory").data("kendoGrid");
            grid.setDataSource(gridData);
            $("#gridTaskHistory .k-grid-header").css("display", "none");
        });

        //$("#btnUploadFile").click(function(){
        //    debugger;
        //    $("#cmntFileUpload").click();
            
        //});
        $("#files").kendoUpload({
            success: function (jsonData) {
                debugger;

                if (jsonData.response.actionType === "Save") {
                    var obj = new Object();
                    obj.FileName = jsonData.files[0].name;
                    obj.FileExtension = jsonData.files[0].extension;
                    obj.FileSize = jsonData.files[0].size;
                    obj.FileUniqueName = jsonData.response.fileNameUniuqe;
                    //obj.ModuleMasterId = gbSelectitem.taskId;
                    //obj.ModuleName = "Task Comment";
                    //obj.ActionType = jsonData.response.actionType;
                    obj.CommentId = AjaxManager.DefaultGuidId();
                    obj.FileId = AjaxManager.DefaultGuidId();
                    jsonData.files[0].fileUniq = jsonData.response.fileNameUniuqe;
                    gbFileArray.push(obj);

                    var file = JSON.stringify(obj);
                }
                else {
                    gbFileArray = gbFileArray.filter(file => file.FileUniqueName != jsonData.response.fileNameUniuqe);
                }
                //$(".k-file-progress .file-wrapper .view-test").html('<a href="../DocumentFile/' + obj.FileUniqueName + '" type="button" class="k-button "  target="_blank"><span class="k-button-icon k-icon k-i-eye" title="View"></span></a>');
                //DocumentHelper.SaveDocumentDetails(obj);
            },
            remove: function (e) {
                debugger;
                //  var name = e.files[0].uid + "_" + e.files[0].name;
                if (e.files !== undefined) {
                    e.files[0].name = e.files[0].fileUniq;
                    console.log(e);
                }
            },
            async: {
                chunkSize: 11000, // bytes
                saveUrl: "/Upload/ChunkSave",
                removeUrl: "/Upload/remove",
                autoUpload: true
            },
           // template: kendo.template($('#fileTemplate').html()),
            files: files,
            //dropZone: ".dropZoneElement",
        });
        //$("div").removeClass("k-upload");
        $(".k-upload-button span").text('');
        $(".k-upload-button span").html('<i class="fa fa-paperclip text-secondary" aria-hidden="true"></i>');

        $("#saveCommentAndFile").click(function () {
            TaskSummeryManager.SaveCommentAndFile();
        });
    },
    Dropdown: function () {
        $("#txtTaskStatus").kendoDropDownList({
            dataSource: [],
            dataTextField: "statusName",
            dataValueField: "statusId",
            select: onSelect
        });
        //function onChange(e) {
        function onSelect(e) {
            debugger;
            e.preventDefault();
            gbSelectitem.statusId = e.dataItem.statusId;
            gbSelectitem.statusName = e.dataItem.statusName;

            Swal.fire({
                title: 'Are you sure?',
                text: "You want to save the change!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Change it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    TaskSummeryManager.SaveTask(gbSelectitem);

                    //Swal.fire(
                    //    'Deleted!',
                    //    'Your file has been deleted.',
                    //    'success'
                    //)
                }
            })
        };
    },
    GenerateGrid: function () {
        $("#gridMyTaskSummery").kendoGrid({
            dataSource: TaskSummeryManager.GetTaskDetailsByUserId(),
            pageable: true,
            height: 550,
            columns: [
                { field: "taskId", hidden: true },
                { field: "projectId", hidden: true },
                { field: "projectName", title: "Project Name", width: 20, headerAttributes: { style: "text-align: center; justify-content: center" } },
                { field: "taskName", title: "Task Name", width: 20, headerAttributes: { style: "text-align: center; justify-content: center" } },
               // { field: "taskDescription", title: "Description", width: 20 }, //editor: categoryDropDownEditor, template: "#=Category.CategoryName#"
                { field: "statueName", title: "Statue", width: 20 }, //editor: categoryDropDownEditor, template: "#=Category.CategoryName#"
                {
                    field: "eddate", title: "ED Date", width: 15, headerAttributes: { style: "text-align: center; justify-content: center" },
                    attributes: { style: "text-align: center" },
                    template: '#=kendo.toString(eddate==null?"-":kendo.parseDate(eddate),"dd-MMM-yyyy")#',
                },
                // { field: "createBy", title: "Assign By", width: 20 }, //editor: categoryDropDownEditor, template: "#=Category.CategoryName#"

                //{ field: "statuesInfoDto", title: "Status", width: 15, editor: statusDropDownEditor, template: "#=statuesInfoDto.statusName#" },
                //{ field: "memberInfo", title: "Assign Member", width: 20, editor: memberDropDownEditor, template: $("#empTemplate").html() },/*template: "#=memberInfo==undefined? 'add mem ':memberInfo.name#"*/
                //{ field: "groupInfoDto", title: "Assign Group", width: 20, editor: groupDropDownEditor, template: $("#groupTemplate").html() },
                // { command: "destroy", title: " ", width: "150px" }
            ],
            editable: false,
            selectable: "row",
        });
        $("#gridMyTaskSummery").on("dblclick", "tr.k-state-selected", function () {
            debugger
            gbSelectitem = {};
            var grid = $("#gridMyTaskSummery").data("kendoGrid");
            if (grid.select().length > 0) {
                var selectedItem = grid.dataItem(grid.select());
                console.log(selectedItem);
                if (selectedItem != null) {
                    gbSelectitem = selectedItem;
                    $("#TaskModalCenter").modal('show');
                    $("#TaskModalCenter #taskId").val(selectedItem.taskId);
                    $("#TaskModalCenter #projectTitle").text("Project Name : " + selectedItem.projectName);
                    $("#TaskModalCenter #projectDescription").text("Project Description : " + selectedItem.projectDescription);
                    $("#TaskModalCenter #taskName").text("Task Name : " + selectedItem.taskName);
                    $("#TaskModalCenter #taskDescription").text("Task Description : " + selectedItem.taskDescription);
                    $("#TaskModalCenter #eddate").text("ED Date : " + kendo.toString(kendo.parseDate(selectedItem.eddate), "dd-MMM-yyyy"));

                    if (selectedItem.statusId === '5aec56e3-f864-4988-875c-4843d5cb24bc') {
                        $("#TaskModalCenter #statueName").text("Status : " + selectedItem.statueName);
                        $(".changeStatus").hide();
                        $("#statueName").show();

                    }
                    else {
                        $("#statueName").hide();
                        $(".changeStatus").show();

                        var dropdownlist = $("#txtTaskStatus").data("kendoDropDownList");
                        dropdownlist.value(selectedItem.statusId);
                    }
                    //comments
                    var gridData = TaskSummeryManager.GetCommentAndFileByTaskId(selectedItem.taskId);
                    var grid = $("#gridComment").data("kendoGrid");
                    grid.setDataSource(gridData);
                    $("#gridComment .k-grid-header").css("display", "none");

                    //$("#TaskModalCenter").addClass("show");
                    //$("#TaskModalCenter").removeAttr("aria-hidden");

                    //$("#TaskModalCenter").attr("aria-modal","true");
                    //$("#TaskModalCenter").css("display","block");

                    //for ( var i = 0; i < gbBBLCSummList.length; i++ )
                    //{
                    //    if ( gbBBLCSummList[i].Bblcid === selectedItem.Bblcid )
                    //    {
                    //        selectedItem = gbBBLCSummList[i];
                    //        break;
                    //    }
                    //}
                    //gbSelectedPi = [];
                    //gbUnSelectedPi = [];
                    //$("#gridSelectedPIList").data("kendoGrid").dataSource.data([]);
                    //$("#gridPIList").data("kendoGrid").dataSource.data([]);

                    //BackToBackInfoHelper.ClearForm();
                    //$("#divBBModal").data("kendoWindow").close();
                    //BackToBackInfoHelper.FillForm(selectedItem, true);


                }
            }
        });
        $("#gridTaskHistory").kendoGrid({
            dataSource: [],
            columns: [
                {
                    template: "<p>#: historyDescription # <span class='ml-3 text-muted'>#= $.timeago(createDate)#</span></p>" //- #: createDate#
                },
                //{
                //    /*field: "createDate",*/
                //    template: "<p>#= $.timeago(createDate)#</p>" //- #: createDate#
                //},
            ],
            scrollable: false,
            //height: 300,
        });
        $("#gridComment").kendoGrid({
            dataSource: [],
            columns: [
                {
                    template: `<p>#: createByName # <span class='ml-3 text-muted'>#= $.timeago(createDate)#</span></p> <p>#=commentDescription#</p>
                                    #if(files.length > 0){#
                                      <p>
                                        #for(var i=0; i<files.length; i++ ){#
                                            <a class='btn btn-secondary' href='../DocumentFile/#: files[i].fileUniqueName #' target='_blank'>#: files[i].fileName # </a>
                                        #}#
                                      </p>
                                    #}#` //- #: createDate#
                },

            ],
            scrollable: false,
            //height: 300,
        });
    },
    CreateTaskObj: function (rowData) {
        var obj = new Object;
        //rowData.groupInfoDto = [];
        //rowData.memberInfo = [];
        obj = rowData;
        //obj.CreateBy = userEmpId;
        obj.FinishedBy = userEmpId;

        debugger;
        return obj;
    },

    TextareaHeight: function (elem) {
        elem.style.height = "1px";
        elem.style.height = (elem.scrollHeight) + "px";
    },

    CreateCommentAndFileObj: function () {
        var obj = new Object;
        obj.CommentId = AjaxManager.DefaultGuidId;
        //obj.CommentDescription = $("#texComment").val().trim();
        var trimComment = $("#texComment").val().trim();
        obj.CommentDescription = TaskSummeryHelper.CheckHyperLink(trimComment);

        obj.ProjectId = gbSelectitem.projectId;
        obj.TaskId = gbSelectitem.taskId;
        obj.Files = gbFileArray;
        obj.CreateBy = userEmpId;

        debugger;
        return obj;
    },
    ValidateForm: function () {
        //debugger;
        var res = true;
        var comment = $("#texComment").val().trim();
        if ((comment === "" || comment === null) && gbFileArray.length < 1) {
            res = false;
        }
        return res;
    },
    ClearForm: function () {
        gbFileArray = [];
        $("#texComment").val("");
        var upload = $("#files").data("kendoUpload");
        upload.clearAllFiles();
    },
    CheckHyperLink: function (textContent) {
        if (textContent.length > 0) {
            return textContent.replace(
                /(https?:\/\/[^\s]+)/g,
                (href) => `<a style="color:blue" target="_blank" href="${href}">${href}</a>`
            );
        }
    }
       
};
