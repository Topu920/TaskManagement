var gbSelectedTask = {};
var gbFileArray = [];

var ProjectTaskDetailsManager = {
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
    },
    SaveCommentAndFile: function () {
        if (ProjectTaskDetailsHelper.ValidateForm()) {
            var commentAndFile = ProjectTaskDetailsHelper.CreateCommentAndFileObj();
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

                ProjectTaskDetailsHelper.ClearForm();

                toastr.success(msg);
                var gridData = ProjectTaskDetailsManager.GetCommentAndFileByTaskId(gbSelectedTask.taskId);
                var grid = $("#gridTaskCommentInProject").data("kendoGrid");
                grid.setDataSource(gridData);
                $("#gridTaskCommentInProject .k-grid-header").css("display", "none");
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
};

var ProjectTaskDetailsHelper = {
    Init: function () {
        ProjectTaskDetailsHelper.ModalGridGenerate();

        $("#btnTaskHistory").click(function () {
            var gridData = ProjectTaskDetailsManager.GetTaskHistoryById(gbSelectedTask.taskId);
            var grid = $("#gridTaskHistoryInProject").data("kendoGrid");
            grid.setDataSource(gridData);
            $("#gridTaskHistoryInProject .k-grid-header").css("display", "none");
        });

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
            files: files,
        });
        //$("div").removeClass("k-upload");
        $(".k-upload-button span").text('');
        $(".k-upload-button span").html('<i class="fa fa-paperclip text-secondary" aria-hidden="true"></i>');

        $("#saveTaskCommentAndFile").click(function () {
            ProjectTaskDetailsManager.SaveCommentAndFile();
        });
    },
    ModalGridGenerate: function () {
        $("#gridTaskHistoryInProject").kendoGrid({
            dataSource: [],
            columns: [
                {
                    template: "<p>#: historyDescription # <span class='ml-3 text-muted'>#= $.timeago(createDate)#</span></p>" //- #: createDate#
                },
            ],
            scrollable: false,
            //height: 300,
        });
        $("#gridTaskCommentInProject").kendoGrid({
            dataSource: [],
            columns: [
                {
                    template: `<p>#: createByName # <span class='ml-3 text-muted'>#= $.timeago(createDate)#</span></p> <p>#=commentDescription# </p>
                                    #if(files.length > 0){#
                                      <p>
                                        #for(var i=0; i<files.length; i++ ){#
                                            <a class='btn btn-secondary' href='../../DocumentFile/#: files[i].fileUniqueName #' target='_blank'>#: files[i].fileName # </a>
                                        #}#
                                      </p>
                                    #}#` //- #: createDate#
                },

            ],
            scrollable: false,
            //height: 300,
        });
    },
    
    PassSelectedRowData: function (selectedTask) {
        gbSelectedTask = selectedTask;

        var gridData = ProjectTaskDetailsManager.GetCommentAndFileByTaskId(gbSelectedTask.taskId);
        var grid = $("#gridTaskCommentInProject").data("kendoGrid");
        grid.setDataSource(gridData);
        $("#gridTaskCommentInProject .k-grid-header").css("display", "none");

        //details

        $("#TaskModalCenter #taskId").val(gbSelectedTask.taskId);
        $("#TaskModalCenter #projectTitle").text("Project Name : " + gbSelectedTask.projectName);
        $("#TaskModalCenter #projectDescription").text("Project Description : " + gbSelectedTask.projectDescription);
        $("#TaskModalCenter #taskName").text("Task Name : " + gbSelectedTask.taskName);
        $("#TaskModalCenter #taskDescription").text("Task Description : " + gbSelectedTask.taskDescription);
        $("#TaskModalCenter #eddate").text("ED Date : " + kendo.toString(kendo.parseDate(gbSelectedTask.eddate), "dd-MMM-yyyy"));
        $("#TaskModalCenter #statueName").text("Status : " + gbSelectedTask.statueName);

        //if (selectedItem.statusId === '5aec56e3-f864-4988-875c-4843d5cb24bc') {
        //    $("#TaskModalCenter #statueName").text("Status : " + selectedItem.statueName);
        //    $(".changeStatus").hide();
        //    $("#statueName").show();

        //}
        //else {
        //    $("#statueName").hide();
        //    $(".changeStatus").show();

        //    var dropdownlist = $("#txtTaskStatus").data("kendoDropDownList");
        //    dropdownlist.value(selectedItem.statusId);
        //}

    },
    TextareaHeight: function (elem) {
        elem.style.height = "1px";
        elem.style.height = (elem.scrollHeight) + "px";
    },
    CreateCommentAndFileObj: function () {
        var obj = new Object;
        obj.CommentId = AjaxManager.DefaultGuidId;
        //obj.CommentDescription = $("#texTaskComment").val().trim();
        var trimComment = $("#texTaskComment").val().trim();
        
        obj.CommentDescription = ProjectTaskDetailsHelper.CheckHyperLink(trimComment);
        obj.ProjectId = gbSelectedTask.projectId;
        obj.TaskId = gbSelectedTask.taskId;
        obj.Files = gbFileArray;
        obj.CreateBy = userEmpId;

        debugger;
        return obj;
    },
    ValidateForm: function () {
        //debugger;
        var res = true;
        var comment = $("#texTaskComment").val().trim();
        if ((comment === "" || comment === null) && gbFileArray.length < 1) {
            res = false;
        }
        return res;
    },
    ClearForm: function () {
        gbFileArray = [];
        $("#texTaskComment").val("");
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