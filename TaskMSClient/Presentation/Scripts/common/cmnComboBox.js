var CmnKendoDatePicker = {
    DateMonthYear :function(id) {
        $(id).kendoDatePicker({ format: "dd-MMM-yyyy", value: new Date() });
    }
};

var CmnComboBoxManager = {
    GetStatusByFlagNo(flagNo) {
        var list = ApiManager.GetList(_baseUrl + "/api/CmnStatues/GetStatuesByFlagNo?id=" + flagNo);
        return list == null ? [] : list;
    },
};

var CmnComboBoxHelper = {
    LoadStatusByFlagNo(id, flagNo) {
        UtilityHelper.LoadCombo(id, "statusId", "statusName", CmnComboBoxManager.GetStatusByFlagNo(flagNo), "--Select Status--");
    },
};