var yellow = '#FFD300';
var green = '#25523B';
var red = '#FF0000';
var black = '#000000';
var white = '#FFFFFF';

var CalendarDetailsManager = {
    GetEventData: function () {
        debugger;
        var events = [];
        var objTask = "";
        var jsonParam = "";
        var serviceUrl = _baseUrl + "/api/Task/GetOverAllTaskListByUser?empId=" + userEmpId;
        AjaxManager.GetJsonResult(serviceUrl, jsonParam, false, false, onSuccess, onFailed);
        function onSuccess(jsonData) {
            objTask = jsonData;
            for (var i = 0; i < objTask.length; i++) {
                var obj = {};
                obj.title = objTask[i].taskName;
                obj.start = objTask[i].createDate;
                obj.end = objTask[i].eddate;
                obj.taskDescription = objTask[i].taskDescription;
                obj.statusId = objTask[i].statusId;
                obj.statueName = objTask[i].statueName;
                obj.projectName = objTask[i].projectName;
                obj.projectDescription = objTask[i].projectDescription;
                obj.taskName = objTask[i].taskName;
                obj.createDate = objTask[i].createDate;
                obj.eddate = objTask[i].eddate;
                obj.finishingDate = objTask[i].finishingDate;
                obj.createBy = objTask[i].createByName;
                obj.memberInfo = objTask[i].memberInfo;
                obj.groupInfoDto = objTask[i].groupInfoDto;

                if (obj.statusId == '68f1eb9c-0057-4488-b42e-13630e46071c') { //inprocess
                    obj.backgroundColor = yellow;
                    obj.borderColor = yellow;
                    obj.textColor = black;
                }
                if (obj.statusId == '5aec56e3-f864-4988-875c-4843d5cb24bc') {
                    obj.backgroundColor = green;
                    obj.borderColor = green;
                    obj.textColor = white;

                }
                events.push(obj);
                //events.push({
                //    title: objTask[i].taskName,
                //    start: objTask[i].createDate,
                //    end: objTask[i].eddate,
                //    description: 'AAAAAAAAAAAAA',
                //    backgroundColor:'', //yellow: '#FFD300',// GreeN:'#25523B',       //red: '#FF0000'
                //    borderColor:"#FFD300",
                //});
            }
        }
        function onFailed(error) {
            
            alert(error);
            window.alert(error.statusText);
        }
        return events;
    }
};


var CalendarDetailsHelper = {
    Init: function () {
        var events = CalendarDetailsManager.GetEventData();
        CalendarDetailsHelper.GenerateCalender(events);
    },
    GenerateCalender: function (e) {
        var calendarEl = document.getElementById('calendar');
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
            },
            //eventLimit: false,
            events: e,
            eventClick: function (calEvent, jsEvent, view) {
                debugger;
                $("#calendarModal #projectTitle").text("Project Name : " + calEvent.event.extendedProps.projectName);
                $("#calendarModal #projectDescription").text("Project Description : " + calEvent.event.extendedProps.projectDescription);
                $("#calendarModal #taskName").text("Task Name : " + calEvent.event.extendedProps.taskName);
                $("#calendarModal #taskDescription").text("Task Description : " + calEvent.event.extendedProps.taskDescription);
                $("#calendarModal #taskOwner").text("Task Owner : " + calEvent.event.extendedProps.createBy);
                $("#calendarModal #taskCreateDate").text("Task Create Date: " + moment(calEvent.event.extendedProps.createDate).format('DD-MMM-YYYY'));
                $("#calendarModal #taskDueDate").text("Task Due Date: " + moment(calEvent.event.extendedProps.eddate).format('DD-MMM-YYYY'));

                var finishingDate = calEvent.event.extendedProps.finishingDate == null ? '' : moment(calEvent.event.extendedProps.finishingDate).format('DD-MMM-YYYY');
                $("#calendarModal #taskFinishDate").text("Task FinishDate: " + finishingDate);
                $("#calendarModal #statueName").text("Statue : " + calEvent.event.extendedProps.statueName);

                var member = [];
                member.push('<p class="font-weight-bold text-muted text-uppercase">Assigned To: ');
                var memberList = calEvent.event.extendedProps.memberInfo;
                for (var i = 0; i < memberList.length; i++) {
                    member.push(`<span>${memberList[i].name},<span>`);
                }
                member.push('<p/>');
                $('#taskMember').html(member.join(''));

                var group = [];
                group.push('<p class="font-weight-bold text-muted text-uppercase">Assigned Group: ');
                var groupList = calEvent.event.extendedProps.groupInfoDto;
                for (var i = 0; i < groupList.length; i++) {
                    group.push(`<span>${groupList[i].groupName},<span>`);
                }
                group.push('<p/>');
                $('#group').html(group.join(''));

                $('#calendarModal').modal('show')
            }
        });
        calendar.render();
        //$("#calendar").fullCalendar('destroy');
        //$("#calendar").fullCalendar({
        //    contentHeight: 400,
        //    defaultDate: new Date(),
        //    timeFormat: 'h(:mm)',
        //    header: {
        //        left: 'prev,next,today',
        //        center: 'Title',
        //        right: 'month,basicWeek,basicDay,agenda'
        //    },
        //    eventLimit: true,
        //    eventColor: '#378006',
        //    events:e
        //});
    }
};