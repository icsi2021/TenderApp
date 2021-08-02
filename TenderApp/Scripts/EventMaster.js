
function ValidationExtend(Object) {
    var ObjEventStartDate = document.getElementById('MainContent_txtPublishDateTime');
    var ObjEventEndDate = document.getElementById('MainContent_txtCloseDateTime');
    var ObjEventExtDate = document.getElementById('MainContent_txtExtendDateTime');
    if (ObjEventExtDate.value != "") {
        if ((ObjEventStartDate.value != "") && (ObjEventEndDate.value != "")) {
            var dtSDate = ObjEventStartDate.value.split(" ")[0];
            var dtEDate = ObjEventEndDate.value.split(" ")[0];
            var dtExDate = ObjEventExtDate.value.split(" ")[0];
            dtSDate = new Date(dtSDate.split('/')[2], dtSDate.split('/')[1], dtSDate.split('/')[0]);
            dtEDate = new Date(dtEDate.split('/')[2], dtEDate.split('/')[1], dtEDate.split('/')[0]);
            dtExDate = new Date(dtExDate.split('/')[2], dtExDate.split('/')[1], dtExDate.split('/')[0]);
            if (dtSDate > dtExDate || dtExDate < dtSDate || dtEDate > dtExDate || dtExDate < dtEDate) {
                DateMessage();
                ObjEventExtDate.get_element().value = "";
                Object.focus();
                return false;
            }
            else {
                return true;
            }


        }
    }
   

}
function DateMessage() {
    alert('Extend Date Time should be greater than Closing Date Time');
}

function ConvertTextToDate(strDate) {
    DatePart = strDate.split(" ")[0];
    if (typeof (DatePart) != 'undefined' && DatePart != null) {
        return new Date(DatePart.split('/')[2], DatePart.split('/')[1] - 1, DatePart.split('/')[0]);
    }
}

