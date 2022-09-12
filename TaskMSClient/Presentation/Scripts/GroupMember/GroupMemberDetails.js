var GroupMemberDetailsManager = {
    GetAllMember: function () {
        //var searchMember = $("#memberSearch").val();
        var objAllMember = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Member/GetMemberList";///search=" + searchMember;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            debugger;
            objAllMember = jsonData;
        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        console.log(objAllMember);
        return objAllMember;
    },
    SaveGroupMember: function () {
        debugger;
        if (GroupMemberDetailsHelper.ValidateForm()) {
            var createGroup = GroupMemberDetailsHelper.CreateGroupObject();
            debugger;
            var jsonParam = JSON.stringify(createGroup);
            var serviceUrl = _baseUrl + "/api/GroupMember/InsertGroupMember";
            AjaxManager.PostJsonApi(serviceUrl, jsonParam, onSuccess, onFailed);
        }
        function onSuccess(jsonData) {
            debugger;
            var msg = jsonData.Message;
            console.log(jsonData);
            if (jsonData.success) {
                GroupMemberDetailsHelper.ResetForm();
                //$("#hdnProjectId").val(jsonData.projectInfoDto.projectId);
                //var grid = $("#gridGroupSummery").data("kendoGrid");
                //var data = GroupMemberSummeryManager.GetAllGroup();
                //var dataSource = new kendo.data.DataSource({
                //    data: data,
                //    pageSize: 10
                //});
                //grid.setDataSource(dataSource);
                $("#gridGroupSummery").data("kendoGrid").dataSource.read();

                debugger;
                $("#divGroupDetails").addClass('d-none');
                $("#divGroupSummery").removeClass('d-none');
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
var GroupMemberDetailsHelper = {
    Init: function () {

        GroupMemberDetailsHelper.GenerateMultiSelectMember();
        $("#orders").kendoDropDownList({
            template: '<span class="order-id">#= OrderID #</span> #= ShipName #, #= ShipCountry #',
            dataTextField: "ShipName",
            dataValueField: "OrderID",
            filter: "contains",
            //virtual: {
            //    itemHeight: 50,
            //    valueMapper: function (options) {
            //        debugger;
            //        $.ajax({
            //            url: "https://demos.telerik.com/kendo-ui/service/Orders/ValueMapper",
            //            type: "GET",
            //            dataType: "jsonp",
            //            data: convertValues(options.value),
            //            success: function (data) {
            //                options.success(data);
            //            }
            //        })
            //    }
            //},
            height: 520,
            dataSource: {
                type: "odata",
                //transport: {
                //    read: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Orders"
                //},
                schema: {
                    model: {
                        fields: {
                            OrderID: { type: "number" },
                            Freight: { type: "number" },
                            ShipName: { type: "string" },
                            OrderDate: { type: "date" },
                            ShipCity: { type: "string" }
                        }
                    }
                },
               
            }
        });
       
        //var loader = $('#loader').kendoLoader({
        //    visible: false,
        //    size: "small"
        //}).data('kendoLoader');

        //setTimeout(function () {
        //    loader.hide();
        //}, 3000);

        $("#btnBackGroup").click(function () {
            $("#divGroupDetails").addClass('d-none');
            $("#divGroupSummery").removeClass('d-none');
        });
        $("#btnNewGroup").click(function () {
            $("#divGroupSummery").addClass('d-none');
            $("#divGroupDetails").removeClass('d-none');
            GroupMemberDetailsHelper.ResetForm();
        });

        $("#btnAddGroup").click(function () {
            GroupMemberDetailsManager.SaveGroupMember();
        });

    },
    GenerateMultiSelectMember: function () {
        $("#memberList").kendoMultiSelect({
           // itemTemplate: '<span class="order-id">#= empCode #</span> #= name #, #= designationName #, #= departmentName #,',

            placeholder: "Select Member...",
            dataTextField: "fullInfoLine",
            dataValueField: "empId",
            //autoBind: false,
            virtual: {
                itemHeight: 26,
                //valueMapper: function (options) {
                //    debugger;
                //    $.ajax({
                //        url: "",
                //        type: "GET",
                //        dataType: "jsonp",
                //        data: convertValues(options.value),
                //        success: function (data) {
                //            options.success(data);
                //        }
                //    })
                //}
            },
            height: 300,
            dataSource: GroupMemberDetailsManager.GetAllMember(),
            filter: "contains",
            //pageSize: 10,
           // serverPaging: true,
           // serverFiltering: true
            
        });
        function convertValues(value) {
            debugger;
            var data = {};
           

           // value = $.isArray(value) ? value : [value];
            


            //for (var idx = 0; idx < value.length; idx++) {
            //    data["values[" + idx + "]"] = value[idx];
            //}
            
            return data;
        }
    },

    ResetForm: function () {
        $("#hdnGroupMemberId").val("");
        $("#grpName").val("");
        debugger;
        var x =$("#memberList").data("kendoMultiSelect");
        x.value("");
    },
    CreateGroupObject: function () {
        var obj = new Object();
        obj.groupId = $("#hdnGroupMemberId").val() === "" ? "00000000-0000-0000-0000-000000000000" : $("#hdnProjectId").val();
        obj.name = $("#grpName").val().trim();
        obj.empIdListCollection = $("#memberList").data('kendoMultiSelect').value();
        obj.isPrivate = $('input[name=isPrivate]:checked', '.card-body').val()
        obj.CreateBy = userEmpId;
        debugger;
        return obj;
    },
    ValidateForm: function () {
        var res = true;
        var groupName = $("#grpName").val();
        if (groupName === "" || groupName === null) {
            AjaxManager.NotifyMsg("txtPINo", "error", "right", 1500, "Required");
            res = false;
        }

        return res;
    }
};