// JScript File
DateFormatSymbols = function () {
    this.eras = new Array('BC', 'AD');
    this.months = new Array('January', 'February', 'March', 'April',
      'May', 'June', 'July', 'August', 'September', 'October',
      'November', 'December');
    this.shortMonths = new Array('Jan', 'Feb', 'Mar', 'Apr',
      'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct',
      'Nov', 'Dec');
    this.weekdays = new Array('Sunday', 'Monday', 'Tuesday',
      'Wednesday', 'Thursday', 'Friday', 'Saturday');
    this.shortWeekdays = new Array('Sun', 'Mon', 'Tue',
      'Wed', 'Thu', 'Fri', 'Sat');
    this.ampms = new Array('AM', 'PM');
    this.zoneStrings = new Array(new Array(0, 'long-name', 'short-name'));
    var threshold = new Date();
    threshold.setYear(threshold.getYear() - 80);
    this.twoDigitYearStart = threshold;
}

SimpleDateFormatParserContext = function () {
    this.newIndex = 0;
    this.retValue = 0;
    this.year = 0;
    this.ambigousYear = false;
    this.month = 0;
    this.day = 0;
    this.dayOfWeek = 0;
    this.hour = 0;
    this.min = 0;
    this.sec = 0;
    this.ampm = 0;
    this.dateStr = "";
}

SimpleDateFormat = function (pattern, dateFormatSymbols) {
    this.pattern = pattern;
    this.dateFormatSymbols
      = dateFormatSymbols ? dateFormatSymbols : new DateFormatSymbols();
}

SimpleDateFormat.prototype._handle = function (dateStr, date, parse) {
    var patternIndex = 0;
    var dateIndex = 0;
    var commentMode = false;
    var lastChar = 0;
    var currentChar = 0;
    var nextChar = 0;
    var patternSub = null;

    var context = new SimpleDateFormatParserContext();

    if (date != null) {
        context.year = this._fullYearFromDate(date.getYear());
        context.month = date.getMonth();
        context.day = date.getDate();
        context.dayOfWeek = date.getDay();
        context.hour = date.getHours();
        context.min = date.getMinutes();
        context.sec = date.getSeconds();
    }

    while (patternIndex < this.pattern.length) {
        currentChar = this.pattern.charAt(patternIndex);

        if (patternIndex < (this.pattern.length - 1)) {
            nextChar = this.pattern.charAt(patternIndex + 1);
        } else {
            nextChar = 0;
        }

        if (currentChar == '\'' && lastChar != '\\') {
            commentMode = !commentMode;
            patternIndex++;
        } else {
            if (!commentMode) {
                if (currentChar == '\\' && lastChar != '\\') {
                    patternIndex++;
                } else {
                    if (patternSub == null)
                        patternSub = "";

                    patternSub += currentChar;

                    if (currentChar != nextChar) {
                        this._handlePatternSub(context, patternSub,
                dateStr, dateIndex, parse);

                        patternSub = null;

                        if (context.newIndex < 0)
                            break;

                        dateIndex = context.newIndex;
                    }

                    patternIndex++;
                }
            } else {
                if (parse) {
                    if (this.pattern.charAt(patternIndex) != dateStr.charAt(dateIndex)) {
                        //invalid character in dateString
                        return null;
                    }
                } else {
                    context.dateStr += this.pattern.charAt(patternIndex);
                }

                patternIndex++;
                dateIndex++;
            }
        }

        lastChar = currentChar;
    }

    this._handlePatternSub(context, patternSub, dateStr, dateIndex, parse);
    return context;
};

SimpleDateFormat.prototype.parse = function (dateStr) {
    if (!dateStr || dateStr.length == 0) {
        return null;
    }

    var context = this._handle(dateStr, null, true);

    if (context.retValue == -1) {
        return null;
    }

    this._adjustTwoDigitYear(context);

    return this._createDateFromContext(context);
};

SimpleDateFormat.prototype._createDateFromContext = function (context) {
    return new Date(context.year, context.month,
      context.day, context.hour, context.min, context.sec);
};

SimpleDateFormat.prototype.format = function (date) {
    var context = this._handle(null, date, false);

    return context.dateStr;
};

SimpleDateFormat.prototype._parseString
    = function (context, dateStr, dateIndex, strings) {
        var fragment = dateStr.substr(dateIndex);
        var index = this._prefixOf(strings, fragment);
        if (index != -1) {
            context.retValue = index;
            context.newIndex = dateIndex + strings[index].length;
            return context;
        }

        context.retValue = -1;
        context.newIndex = -1;
        return context;
    };

SimpleDateFormat.prototype._parseNum
    = function (context, dateStr, posCount, dateIndex) {
        for (var i = Math.min(posCount, dateStr.length - dateIndex); i > 0; i--) {
            var numStr = dateStr.substring(dateIndex, dateIndex + i);

            context.retValue = this._parseInt(numStr);
            if (context.retValue == -1) {
                continue;
            }

            context.newIndex = dateIndex + i;
            return context;
        }

        context.retValue = -1;
        context.newIndex = -1;
        return context;
    };

SimpleDateFormat.prototype._handlePatternSub
    = function (context, patternSub, dateStr, dateIndex, parse) {
        if (patternSub == null || patternSub.length == 0)
            return;

        if (patternSub.charAt(0) == 'y') {
            if (parse) {
                /* XXX @Arvid: whatever we do, we need to try to parse
                the full year format - length means nothing for
                parsing, only for formatting, so says SimpleDateFormat javadoc.
                only if we run into problems as there are no separator chars, we
                should use exact length parsing - how are we going to handle this?

                Additionally, the threshold was not quite correct - it needs to
                be set to current date - 80years...

                this is done after parsing now!

                if (patternSub.length <= 3) {
                this._parseNum(context, dateStr,2,dateIndex);
                context.year = (context.retValue < 26)
                ? 2000 + context.retValue : 1900 + context.retValue;
                } else {
                this._parseNum(context, dateStr,4,dateIndex);
                context.year = context.retValue;
                }*/
                this._parseNum(context, dateStr, 4, dateIndex);

                if ((context.newIndex - dateIndex) < 4) {
                    context.year = context.retValue + 1900;
                    context.ambigousYear = true;
                } else {
                    context.year = context.retValue;
                }
            } else {
                this._formatNum(context, context.year,
          patternSub.length <= 3 ? 2 : 4, true);
            }
        } else if (patternSub.charAt(0) == 'M') {
            if (parse) {
                if (patternSub.length == 3) {
                    var fragment = dateStr.substr(dateIndex, 3);
                    var index = this._indexOf(this.dateFormatSymbols.shortMonths, fragment);
                    if (index != -1) {
                        context.month = index;
                        context.newIndex = dateIndex + 3;
                    }
                } else if (patternSub.length >= 4) {
                    var fragment = dateStr.substr(dateIndex);
                    var index = this._prefixOf(this.dateFormatSymbols.months, fragment);
                    if (index != -1) {
                        context.month = index;
                        context.newIndex = dateIndex
              + this.dateFormatSymbols.months[index].length;
                    }
                } else {
                    this._parseNum(context, dateStr, 2, dateIndex);
                    context.month = context.retValue - 1;
                }
            } else {
                if (patternSub.length == 3) {
                    context.dateStr += this.dateFormatSymbols.shortMonths[context.month];
                } else if (patternSub.length >= 4) {
                    context.dateStr += this.dateFormatSymbols.months[context.month];
                } else {
                    this._formatNum(context, context.month + 1, patternSub.length);
                }
            }
        } else if (patternSub.charAt(0) == 'd') {
            if (parse) {
                this._parseNum(context, dateStr, 2, dateIndex);
                context.day = context.retValue;
            } else {
                this._formatNum(context, context.day, patternSub.length);
            }
        } else if (patternSub.charAt(0) == 'E') {
            if (parse) {
                // XXX dayOfWeek is not used to generate date at the moment
                if (patternSub.length <= 3) {
                    var fragment = dateStr.substr(dateIndex, 3);
                    var index = this._indexOf(this.dateFormatSymbols.shortWeekdays, fragment);
                    if (index != -1) {
                        context.dayOfWeek = index;
                        context.newIndex = dateIndex + 3;
                    }
                } else {
                    var fragment = dateStr.substr(dateIndex);
                    var index = this._prefixOf(this.dateFormatSymbols.weekdays, fragment);
                    if (index != -1) {
                        context.dayOfWeek = index;
                        context.newIndex = dateIndex
              + this.dateFormatSymbols.weekdays[index].length;
                    }
                }
            } else {
                if (patternSub.length <= 3) {
                    context.dateStr
            += this.dateFormatSymbols.shortWeekdays[context.dayOfWeek];
                } else {
                    context.dateStr += this.dateFormatSymbols.weekdays[context.dayOfWeek];
                }
            }
        } else if (patternSub.charAt(0) == 'H' || patternSub.charAt(0) == 'h') {
            if (parse) {
                this._parseNum(context, dateStr, 2, dateIndex);
                context.hour = context.retValue;
            } else {
                this._formatNum(context, context.hour, patternSub.length);
            }
        } else if (patternSub.charAt(0) == 'm') {
            if (parse) {
                this._parseNum(context, dateStr, 2, dateIndex);
                context.min = context.retValue;
            } else {
                this._formatNum(context, context.min, patternSub.length);
            }
        } else if (patternSub.charAt(0) == 's') {
            if (parse) {
                this._parseNum(context, dateStr, 2, dateIndex);
                context.sec = context.retValue;
            } else {
                this._formatNum(context, context.sec, patternSub.length);
            }
        } else if (patternSub.charAt(0) == 'a') {
            if (parse) {
                this._parseString(context, dateStr, dateIndex, this.dateFormatSymbols.ampms);
                context.ampm = context.retValue;
            } else {
                context.dateStr += this.dateFormatSymbols.ampms[context.ampm];
            }
        } else {
            if (parse) {
                context.newIndex = dateIndex + patternSub.length;
            } else {
                context.dateStr += patternSub;
            }
        }
    };

SimpleDateFormat.prototype._formatNum
    = function (context, num, length, ensureLength) {
        var str = num + "";

        while (str.length < length)
            str = "0" + str;

        // XXX do we have to distinguish left and right 'cutting'
        //ensureLength - enable cutting only for parameters like the year, the other
        if (ensureLength && str.length > length) {
            str = str.substr(str.length - length);
        }

        context.dateStr += str;
    };

// perhaps add to Array.prototype
SimpleDateFormat.prototype._indexOf = function (array, value) {
    for (var i = 0; i < array.length; ++i) {
        if (array[i] == value) {
            return i;
        }
    }
    return -1;
};

SimpleDateFormat.prototype._prefixOf = function (array, value) {
    for (var i = 0; i < array.length; ++i) {
        if (value.indexOf(array[i]) == 0) {
            return i;
        }
    }
    return -1;
};

SimpleDateFormat.prototype._parseInt = function (value) {
    var sum = 0;
    for (var i = 0; i < value.length; i++) {
        var c = value.charAt(i);

        if (c < '0' || c > '9') {
            return -1;
        }
        sum = sum * 10 + (c - '0');
    }
    return sum;
};

SimpleDateFormat.prototype._fullYearFromDate = function (year) {
    var yearStr = year + "";
    if (yearStr.length < 4) {
        return year + 1900;
    }
    return year;
};

SimpleDateFormat.prototype._adjustTwoDigitYear = function (context) {
    if (context.ambigousYear) {
        var date = this._createDateFromContext(context);
        var threshold = this.dateFormatSymbols.twoDigitYearStart;
        if (date.getTime() < threshold.getTime()) {
            context.year += 100;
        }
    }
};

function getDateFromFormat(input, format) {
    var f = new SimpleDateFormat(format);
    return f.parse(input);
}

function getDateFromFormatNew(input, format) {
    var f = new SimpleDateFormat(format);
    return f.parse(input);
}

function formatDate(date, format) {
    var f = new SimpleDateFormat(format);
    return f.format(date);
}

function parseDate(val) {
    var preferEuro = (arguments.length == 2) ? arguments[1] : false;
    generalFormats = new Array(
      'y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d');
    monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'MMM-d', 'M/d', 'M-d');
    dateFirst = new Array('dd/MM/yyyy', 'd-M-y', 'd.M.y', 'd-MMM', 'd/M', 'd-M');
    var checkList = new Array('generalFormats', preferEuro ? 'dateFirst'
      : 'monthFirst', preferEuro ? 'monthFirst' : 'dateFirst');
    var d = null;
    for (var i = 0; i < checkList.length; i++) {
        var l = window[checkList[i]];
        for (var j = 0; j < l.length; j++) {
            var format = new SimpleDateFormat(l[j]);
            d = format.parse(val);
            if (d != 0) {
                return new Date(d);
            }
        }
    }
    return null;
}

/* Function to convert string date from text to date object */
function ConvertTextToDate(strDate) {
    if (typeof (strDate) != 'undefined' && strDate != null) {
        return new Date(strDate.split('/')[2], strDate.split('/')[1] - 1, strDate.split('/')[0]);
    }
}


function ConvertTextToDateTime(strDateTime) {
    if (typeof (strDateTime) != 'undefined' && strDateTime != null) {
        var hh = 0, mm = 0;
        strDate = strDateTime.split(' ')[0];
        try {
            var strTime = strDateTime.split(' ')[1];
            hh = parseFloat(strTime.split(':')[0]);
            mm = parseFloat(strTime.split(':')[1]);
        } catch (e) { }
        return new Date(strDate.split('/')[2], strDate.split('/')[1] - 1, strDate.split('/')[0], hh, mm);
    }
}

/* Function to convert string date from date object to text  */
function ConvertDateToText(dtDate) {
    if (typeof (dtDate) != 'undefined' && dtDate != null) {
        dtDate = new Date(dtDate);
        var strDate = (dtDate.getDate() <= 9 ? '0' + dtDate.getDate() : dtDate.getDate()) + '/' + ((dtDate.getMonth() + 1) <= 9 ? '0' + (dtDate.getMonth() + 1) : (dtDate.getMonth() + 1)) + '/' + dtDate.getFullYear();
        return strDate;
    }
}


/* Function to convert string date time from date object to text  */
function ConvertDateTimeToText(dtDate) {
    if (typeof (dtDate) != 'undefined' && dtDate != null) {
        dtDate = new Date(dtDate);
        var strDate = (dtDate.getDate() <= 9 ? '0' + dtDate.getDate() : dtDate.getDate()) + '/' + ((dtDate.getMonth() + 1) <= 9 ? '0' + (dtDate.getMonth() + 1) : (dtDate.getMonth() + 1)) + '/' + dtDate.getFullYear() + " " + (dtDate.getHours() <= 9 ? "0" : "") + dtDate.getHours() + ":" + (dtDate.getMinutes() <= 9 ? "0" : "") + dtDate.getMinutes();
        return strDate;
    }
}

function numberOnly(txtName) {
    if (txtName.value != '') {
        if (!/^\d*(\.{0,1}\d{1,2})?$/.test(txtName.value)) {
            alert('Please enter number Only.');
            txtName.focus();
            //txtName.value="";
            return false;
        }
    }
    return true;
}
//*** Check Number Validation
function extractNumber(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;

    // avoid changing things if already formatted correctly
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\.?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of .
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }

    obj.value = temp;
}
function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }

    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

    return isFirstN || isFirstD || reg.test(keychar);
}

function trimDate(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}


// Get give date month start date
function MonthStartDate(date) {
    var varStart = new Date(date.getFullYear(), date.getMonth(), 1);
    var startDate = ConvertDateToText(varStart);
    return startDate;
}

function MonthEndDate(date) {
    var varStart = new Date(date.getFullYear(), date.getMonth(), 1);
    var nextMonth = new Date(varStart.setMonth(varStart.getMonth() + 1));
    var varEnd = nextMonth.setDate(nextMonth.getDate() - 1);
    var endDate = ConvertDateToText(varEnd);
    return endDate;
}
function ParseDateOnChange(dtDate) {
    var NewDate = new Date();
    var resultDate = '';
    if (dtDate) {
        dtDate = trimDate(dtDate);
    }
    if (dtDate == '') return;
    //if (dtDate == "<") {
    //    dtDate = getCookieData("_fromDate");
    //}
    //if (dtDate == ">") {
    //    dtDate = getCookieData("_toDate");
    //}

    if (dtDate.substring(0, 1) == '+' || dtDate.substring(0, 1) == '-') {
        var intvalue = 0;
        try {
            intvalue = eval(dtDate);
        } catch (e) { }
        if (intvalue) {
            NewDate = new Date(NewDate.setDate(NewDate.getDate() + intvalue))
            dtDate = formatDate(NewDate, "dd/MM/yyyy");
        }
        else {
            dtDate = "Error";
        }
    }
    if (dtDate.length >= 1) {
        var arDate;
        var DD = NewDate.getDate(), MM = NewDate.getMonth(), YY = NewDate.getFullYear();

       
        arDate = dtDate.replace(/ /g, '/');
        arDate = arDate.replace(/[.]/g, '/');
        arDate = arDate.replace(/-/g, '/');
        arDate = arDate.split('/');
        if (arDate.length >= 1) {
            switch (arDate.length) {
                case 2:
                    YY = arDate[2];                      
                    if (YY.length == 1)
                        YY = "0" + YY.toString();
                    if (YY.length == 2)
                        YY = "20" + YY.toString();
                    if (parseFloat(YY) > 2100 || parseFloat(YY) < 1900) {
                        YY = "Error";
                    }
                //case 2:
                //    if (arDate[1] != "") {
                //        MM = arDate[1];
                //        if (parseFloat(MM) >= 4 && getCookieData("_fromDate") != null && arDate[2] == null) {
                //            YY = "20" + getCookieData("_fromDate").split('/')[2];
                //        } else if (parseFloat(MM) <= 3 && getCookieData("_toDate") != null && arDate[2] == null) {
                //            YY = "20" + getCookieData("_toDate").split('/')[2];
                //        }
                //    }
                //    if (parseFloat(MM) > 12) {
                //        MM = "Error";
                //    }
                   
                case 1:
                    if (arDate[0] != "")
                        DD = arDate[0];
                    if (parseFloat(DD) > 31) {
                        DD = "Error";
                    }
                    break;
                default:
                    YY = "Error";
            }
            NewDate = new Date(MM + "/" + DD + "/" + YY);
            if (!isNaN(NewDate))
                resultDate = formatDate(NewDate, "dd/MM/yyyy");
            else {
                alert('Please check date');
                try {
                    //control.focus();
                    return "";
                } catch (e) { }
            }
        }
        else {
            resultDate = "";
        }
    }
    if (resultDate) {
        return resultDate;
    }
}


function PopulateChangedDate(control, isCompulsory) {

    var dtDate = control.value;
    dtDate = ParseDateOnChange(dtDate);
    if (dtDate) {
        control.value = dtDate;
        //setDayName(control);
    }
    else if (isCompulsory) {
        alert('Please check date');
        try {
            control.focus();
            return;
        } catch (e) { }
    }

}

function PopulateChangedDateWithTimeSecond(control, isCompulsory) {
    var NewDateTime = new Date();
    var dtDateTime = control.value;
    //var dtDateTime1 = control.value;
    var DatePart = "", TimePart = "";
    if (dtDateTime) {
        if (dtDateTime.split(" ").length > 2) {
            DatePart = "error";
        }
        else {
            DatePart = dtDateTime.split(" ")[0];
            if (DatePart.length<10) {
                DatePart = "0" + DatePart;
            }
           // if (dtDateTime.split(" ")[1]) {
            TimePart = dtDateTime.split(" ")[1];
            //}
        }
    }
    if (DatePart == ".") {
        TimePart = ".";
    }
    //alert(DatePart);
    //alert(TimePart);
    DatePart = ParseDateOnChange(DatePart);
    TimePart = ParseTimeSecond(TimePart);
   
    NewDateTime = new Date(DatePart + " " + TimePart);
    alert(NewDateTime);
    if (DatePart) {
        NewDateTime = new Date(ParseDateOnChange(DatePart) + " " + TimePart);
        if (!isNaN(NewDateTime)) {
            dtDateTime = DatePart + " " + TimePart;
            control.value = dtDateTime;
            setDayName(control);
        }
    }
    else if (isCompulsory) {
        alert('Please check date');
        try {
            control.focus();
            return;
        } catch (e) { }
    }

}


function setDayName(control) {
   try{
    var elements = control.parentNode.getElementsByTagName("span");
    var El = null;
    for (var Idx = 0; i <= elements.length - 1; Idx++) {
        var tEl = elements[Idx];
        if (tEl.id == "eldaynamefordate") {
            El = tEL;
            break;
        }
    }
    if (El == null) {
        El = document.createElement("span");
        control.parentNode.appendChild(El);
        El.className = "subheading";
        El.style.position = 'absolute';
        El.id = "eldaynamefordate";
        El.disabled = true;
        document.attachEvent("onkeyup", function () {
            setDayName(control);
        }
        );
        document.attachEvent("onmousemove", function () {
            setDayName(control);
        }
        );
    }
   
    var clientRec = getOffset(control);  
   
    El.style.left =  (clientRec.left + control.offsetWidth - 30 ) + 'px';
    El.style.top = (clientRec.top ) + 'px';

    var DT = ConvertTextToDateNew(control.value, true);
    var DOW ="";
    if (isNaN(DT)) {
        DOW="";
    }
    else {
        DOW = formatDate(DT, "EEE");
        if (DOW == "undefined"||DOW == "EEE") {
            DOW = "";
        }
    }
    
    El.innerHTML = DOW;
   }catch(e){}


}
function getOffset(el) {
    var _x = 0; var _y = 0;
    while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
        _x += el.offsetLeft - el.scrollLeft;
        _y += el.offsetTop - el.scrollTop; 
        el = el.offsetParent;
    }
    return { top: _y, left: _x };
}  

function PopulateChangedDateDateTime(control, isCompulsory) {
    var NewDateTime = new Date();
    var dtDateTime = control.value;
    var DatePart = "", TimePart = "";
    if (dtDateTime) {
        if (dtDateTime.split(" ").length > 2) {
            DatePart = "error";
        }
        else {
            DatePart = dtDateTime.split(" ")[0];
            if (dtDateTime.split(" ")[1]) {
                TimePart = dtDateTime.split(" ")[1];
            }
        }
    }
    if (DatePart == ".") {
        TimePart = ".";
    }
    DatePart = ParseDateOnChange(DatePart);
    TimePart = ParseTime(TimePart);

    if (DatePart) {
        NewDateTime = new Date(convertDateFormatMDY(DatePart) + " " + TimePart);
        if (!isNaN(NewDateTime)) {
            dtDateTime = DatePart + " " + TimePart;
            control.value = dtDateTime;
            setDayName(control);
        }
        else {
            alert('Please check date');
            try {
                control.focus();
                return;
            } catch (e) { }
        }
    }
    else if (isCompulsory) {
        alert('Please check date');
        try {
            control.focus();
            return;
        } catch (e) { }
    }

}


function convertDateFormatMDY(dtDate) {
    return dtDate.split("/")[1] + "/" + dtDate.split("/")[0] + "/" + dtDate.split("/")[2];
}

function ParseTime(dtTime) {
    var NewDate = new Date();
    if (dtTime == ".") {
        var HH = NewDate.getHours();
        var MM = NewDate.getMinutes();
        dtTime = HH.toString() + ":" + MM.toString();
    }

    if (!dtTime) {
        dtTime = "00:00";     
    }
    dtTime = dtTime.replace(/[.]/g, ':');
    var saTime = dtTime.split(":");
    var hh = "00", mm = "00";

    if (saTime.length >= 1) {
        switch (saTime.length) {

            case 2:
                if (saTime[1] != "")
                    mm = saTime[1];
                mm = parseFloat(mm);
                if (mm.toString().length == 1)
                    mm = "0" + mm.toString();

                if (mm > 59 || mm == NaN) {
                    hh = "Error";
                }

            case 1:
                hh = saTime[0];
                if (hh.length == 1)
                    hh = "0" + hh.toString();
                if (parseFloat(hh) > 23 || parseFloat(hh) < 0) {
                    hh = "Error";
                }
                break;
            default:
                hh = "Error";
        }
    }
    return hh + ":" + mm;
}


function PopulateHoursTime(control, isCompulsory) {
    var dtTime = control.value;

    if (!dtTime) {
        dtTime = "00:00";
    }
    dtTime = dtTime.replace(/[.]/g, ':');
    var saTime = dtTime.split(":");
    var hh = "00", mm = "00";

    if (saTime.length >= 1) {
        switch (saTime.length) {

            case 2:
                if (saTime[1] != "")
                    mm = saTime[1];
                mm = parseFloat(mm);
                if (mm.toString().length == 1)
                    mm = "0" + mm.toString();

                if (mm > 59 || mm == NaN) {
                    hh = "Error";
                    break;
                }

            case 1:
                hh = parseFloat(saTime[0]);
                if (isNaN(hh)) {
                    hh = "Error";
                }
                hh = hh.toString();
                if (hh.length == 1)
                    hh = "0" + hh.toString();
                break;
            default:
                hh = "Error";
        }
    }

    var result = "";
    if (isNaN(parseFloat(hh)))
        result = "Error";
    if (isNaN(parseFloat(mm)))
        result = "Error";

    if (result == "Error") {
        alert("Kindly enter value in HH:MM format");
        try {
            control.focus();
        } catch (e) { }
    }
    else {
        result = hh + ":" + mm;
        control.value = result;
    }
}



//* Function to convert string date from text to date object */
function ConvertTextToDateNew(strDate, ParseTime) {
    if (typeof (strDate) != 'undefined' && strDate != null) {
        var DatePart = strDate.split(' ')[0];

        var TimePart = '00:00';
        if (strDate.split(' ').length > 1) {
            TimePart = strDate.split(' ')[1];
        }
        if (ParseTime)
            return new Date(DatePart.split('/')[2], DatePart.split('/')[1] - 1, DatePart.split('/')[0], TimePart.split(':')[0], TimePart.split(':')[1]);
        else
            return new Date(DatePart.split('/')[2], DatePart.split('/')[1] - 1, DatePart.split('/')[0]);
    }
}


function getCookieData(variable) {

    variable = variable.toLowerCase();
    var cookies = new String();
    try {
        cookies = document.cookie.toLowerCase();
        var lng = cookies.indexOf(".emistcompany");
        lng = cookies.indexOf(variable, lng);
        var lasIdx = cookies.indexOf("&", lng);
        if (lasIdx == -1) {
            return cookies.substring(lng).split("=")[1];
        }
        var cookies = cookies.substring(lng, lasIdx).split("=")[1];
    } catch (e) { }
    return cookies;

}
/* Function to convert string date time from date object to text  */
function ConvertDateTimeToText(dtDate) {
    if (typeof (dtDate) != 'undefined' && dtDate != null) {
        dtDate = new Date(dtDate);
        var strDate = (dtDate.getDate() <= 9 ? '0' + dtDate.getDate() : dtDate.getDate()) + '/' + ((dtDate.getMonth() + 1) <= 9 ? '0' + (dtDate.getMonth() + 1) : (dtDate.getMonth() + 1)) + '/' + dtDate.getFullYear() + " " + (dtDate.getHours() <= 9 ? "0" : "") + dtDate.getHours() + ":" + (dtDate.getMinutes() <= 9 ? "0" : "") + dtDate.getMinutes();
        return strDate;
    }
}
//VDS Mergion
function PopulateChangedDateDateTimeWithSecond(control, isCompulsory) {
    var NewDateTime = new Date();
    var dtDateTime = control.value;
    var DatePart = "", TimePart = "";
    if (dtDateTime) {
        if (dtDateTime.split(" ").length > 2) {
            DatePart = "error";
        }
        else {
            DatePart = dtDateTime.split(" ")[0];
            if (dtDateTime.split(" ")[1]) {
                TimePart = dtDateTime.split(" ")[1];
            }
        }
    }
    DatePart = ParseDateOnChangeNew(control, DatePart);
    TimePart = ParseTimeSecond(TimePart);

    if (DatePart) {
        NewDateTime = new Date(convertDateFormatMDY(DatePart) + " " + TimePart);
        if (!isNaN(NewDateTime)) {
            dtDateTime = DatePart + " " + TimePart;
            control.value = dtDateTime;
            return true;
        }
        else {
            alert('Please check date');
            try {
                control.focus();
                return false;
            } catch (e) { }
        }
    }
    else if (isCompulsory) {
        alert('Please check date');
        try {
            control.focus();
            return false;
        } catch (e) { }
    }
    else if (DatePart == undefined) {
        return false;
    }
}

function ConvertTextToDateTimeWithSecond(strDateTime) {
    if (typeof (strDateTime) != 'undefined' && strDateTime != null) {
        var hh = 0, mm = 0, ss = 0;
        strDate = strDateTime.split(' ')[0];
        try {
            var strTime = strDateTime.split(' ')[1];
            hh = parseFloat(strTime.split(':')[0]);
            mm = parseFloat(strTime.split(':')[1]);
            ss = parseFloat(strTime.split(':')[2]);
        } catch (e) { }
        return new Date(strDate.split('/')[2], strDate.split('/')[1] - 1, strDate.split('/')[0], hh, mm, ss);
    }
}


function ParseDateOnChangeNew(control, dtDate) {
    var NewDate = new Date();
    var resultDate = '';
    if (dtDate) {
        dtDate = trimDate(dtDate);
    }
    if (dtDate == '') return;
    if (dtDate == "<") {
        dtDate = getCookieData("_fromDate");
    }
    if (dtDate == ">") {
        dtDate = getCookieData("_toDate");
    }

    if (dtDate.substring(0, 1) == '+' || dtDate.substring(0, 1) == '-') {
        var intvalue = 0;
        try {
            intvalue = eval(dtDate);
        } catch (e) { }
        if (intvalue) {
            NewDate = new Date(NewDate.setDate(NewDate.getDate() + intvalue))
            dtDate = formatDate(NewDate, "dd/MM/yyyy");
        }
        else {
            dtDate = "Error";
        }
    }
    if (dtDate.length >= 1) {
        var arDate;
        var DD = NewDate.getDate(), MM = NewDate.getMonth() + 1, YY = NewDate.getFullYear();
        arDate = dtDate.replace(/ /g, '/');
        arDate = arDate.replace(/[.]/g, '/');
        arDate = arDate.replace(/-/g, '/');
        arDate = arDate.split('/');
        if (arDate.length >= 1) {
            switch (arDate.length) {
                case 3:
                    YY = arDate[2];
                    if (YY.length == 1)
                        YY = "0" + YY.toString();
                    if (YY.length == 2)
                        YY = "20" + YY.toString();
                    if (parseFloat(YY) > 2100 || parseFloat(YY) < 1900) {
                        YY = "Error";
                    }
                case 2:
                    if (arDate[1] != "")
                        MM = arDate[1];
                    if (parseFloat(MM) > 12) {
                        MM = "Error";
                    }
                case 1:
                    if (arDate[0] != "")
                        DD = arDate[0];
                    if (parseFloat(DD) > 31) {
                        DD = "Error";
                    }
                    break;
                default:
                    YY = "Error";
            }
            NewDate = new Date(MM + "/" + DD + "/" + YY);
            if (!isNaN(NewDate))
                resultDate = formatDate(NewDate, "dd/MM/yyyy");
            else {
                alert('Please check date');
                try {
                    control.focus();
                    return;
                } catch (e) { }
            }
        }
        else {
            resultDate = "";
        }
    }
    if (resultDate) {
        return resultDate;
    }
}

function ParseTimeSecond(dtTime) {
    var NewDate = new Date();
    if (!dtTime) {

        var HH = NewDate.getHours();
        var MM = NewDate.getMinutes();
        var SS = NewDate.getSeconds();
        if (HH.toString().length == 1) {
            HH = "0" + HH;
        }
        if (MM.toString().length == 1) {
            MM = "0" + MM;
        }
        if (SS.toString().length == 1) {
            SS = "0" + SS;
        }
        dtTime = HH + ":" + MM + ":" + SS;
    }
    dtTime = dtTime.replace(/[.]/g, ':');
    var saTime = dtTime.split(":");
    var hh = "00", mm = "00", ss = "00";

    if (saTime.length >= 1) {
        switch (saTime.length) {
            case 3:
                if (saTime[2] != "")
                    ss = saTime[2];
                ss = parseFloat(ss);
                if (ss.toString().length == 1)
                    ss = "0" + ss.toString();

                if (ss > 59 || ss == NaN) {
                    hh = "Error";
                }
            case 2:
                if (saTime[1] != "")
                    mm = saTime[1];
                mm = parseFloat(mm);
                if (mm.toString().length == 1)
                    mm = "0" + mm.toString();

                if (mm > 59 || mm == NaN) {
                    hh = "Error";
                }

            case 1:
                hh = saTime[0];
                if (hh.length == 1)
                    hh = "0" + hh.toString();
                if (parseFloat(hh) > 23 || parseFloat(hh) < 0) {
                    hh = "Error";
                }
                break;
            default:
                hh = "Error";
        }
    }
    return hh + ":" + mm + ":" + ss;
}


//End VDS Merging