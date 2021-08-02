function Validation(Object) {
    var ObjEventStartDate1 = document.getElementById('MainContent_txtPublishDateTime');
    var ObjEventEndDate1 = document.getElementById('MainContent_txtCloseDateTime');
   // var DatePart1 = ObjEventEndDate1.value.split(" ")[0];
    //DatePart1 = DatePart1.split('/')[1]+DatePart1.split('/')[0]+DatePart1.split('/')[2];
    if ((ObjEventStartDate1.value != "") && (ObjEventEndDate1.value != "")) {
        var dtSDate1 = ObjEventStartDate1.value.split(" ")[0];//ConvertTextToDate1(ObjEventStartDate1.value);
        
        var dtEDate1 = ObjEventEndDate1.value.split(" ")[0];//ConvertTextToDate1(ObjEventEndDate1.value);
        //alert('start=' + dtSDate1);
        //alert('end=' + dtEDate1);
        dtSDate1= new Date(dtSDate1.split('/')[2], dtSDate1.split('/')[1], dtSDate1.split('/')[0]);
        dtEDate1 = new Date(dtEDate1.split('/')[2], dtEDate1.split('/')[1], dtEDate1.split('/')[0]);
        alert('Start Date '  + dtSDate1);
        alert('End Date ' + dtEDate1);
        //alert(dtSDate1);
       // Date.parse(dtEDate1 < dtSDate1 ||
        if ( dtSDate1 > dtEDate1) {
           
            DateMessage1();
            Object.focus();
            ObjEventEndDate1.get_element().value = "";
            return false;
        }
        else {
            return true;
        }
        //if ( DadtSDate1 > dtEDate1) {

        //    DateMessage1();
        //    Object.focus();
        //    ObjEventEndDate1.get_element().value = "";
        //    return false;
        //}

    }

}
function DateMessage1() {
    alert('Closing Date Time should be greater than Publishing Date Time');
}

function ConvertTextToDate1(strDate) {
    DatePart = strDate.split(" ")[0];
    if (typeof (DatePart) != 'undefined' && DatePart != null) {
        return new Date(DatePart.split('/')[2], DatePart.split('/')[1] - 1, DatePart.split('/')[0]);
    }
}
