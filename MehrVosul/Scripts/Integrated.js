/* File Created: August 14, 2013 */

function StartGettingData() {
    return true;
}


function SaveOperation_Validate() {

}

//**************************** Change Language ****************************
function openserachDraft() {
    var x, y;
    x = (screen.width / 2) - 180;
    y = (screen.height / 2) - 240;
    window.open("SpecificDraftSearch.aspx", "Searchwin", "status=yes,width=750,height=300,top=" + y + ",left=" + x + "'");
    return false;
}
function IEn_click(txtSMSText_Name) {
    $(".ENTD").hide();
    $(".FrTD").show();
    $("#TxtareaLng").text("انگلیسی");
    var imgEn = document.getElementById("imgEn");
    var imgFa = document.getElementById("imgFa");
    sw = false;
    var txtSMSText = document.getElementById(txtSMSText_Name);
    txtSMSText.dir = "ltr";
    var tdSMS = document.getElementById("tdSMS");
    tdSMS.dir = "ltr";
    txtSMSText.focus();
}
function IEnNO_click(txtSMSText_Name) {

    $(".EN_NoTD").hide();
    $(".Fr_NoTD").show();
    $("#TxtareaNo").text("انگلیسی")
    var imgNOEnToFa = document.getElementById("imgNOEnToFa");
    var imgNOFaToEn = document.getElementById("imgNOFaToEn");
    nw = false;
    var EnglishNumber = new Array();
    EnglishNumber[1776] = '0';
    EnglishNumber[1777] = '1';
    EnglishNumber[1778] = '2';
    EnglishNumber[1779] = '3';
    EnglishNumber[1780] = '4';
    EnglishNumber[1781] = '5';
    EnglishNumber[1782] = '6';
    EnglishNumber[1783] = '7';
    EnglishNumber[1784] = '8';
    EnglishNumber[1785] = '9';
    var outputNumber = "";
    var txtSMSText = document.getElementById(txtSMSText_Name);
    for (var i = 0; i < txtSMSText.value.length; i++) {
        var arr = txtSMSText.value.substring(i, i + 1);
        var ch = arr.charCodeAt();
        var index;
        if (ch >= 1776 && ch <= 1785) {
            index = EnglishNumber[ch];
            outputNumber = outputNumber + index//String.fromCharCode(newChar);
        }
        else
            outputNumber = outputNumber + String.fromCharCode(ch);
    }
    txtSMSText.value = outputNumber;
    txtSMSText.focus();
}
function IFa_click(txtSMSText_Name) {
    $(".ENTD").show();
    $(".FrTD").hide();
    $("#TxtareaLng").text("فارسی");
    var imgEn = document.getElementById("imgEn");
    var imgFa = document.getElementById("imgFa");
    sw = true; 
    var txtSMSText = document.getElementById(txtSMSText_Name);
    txtSMSText.dir = "rtl";
    var tdSMS = document.getElementById("tdSMS");
    tdSMS.dir = "rtl";
    txtSMSText.focus();
}
function IFaNO_click(txtSMSText_Name) {

    $(".EN_NoTD").show();
    $(".Fr_NoTD").hide();
    $("#TxtareaNo").text("فارسی")
    var imgNOEnToFa = document.getElementById("imgNOEnToFa");
    var imgNOFaToEn = document.getElementById("imgNOFaToEn");
    nw = true;
    var farsiNumber = new Array();
    farsiNumber[0] = '\u06f0';
    farsiNumber[1] = '\u06f1';
    farsiNumber[2] = '\u06f2';
    farsiNumber[3] = '\u06f3';
    farsiNumber[4] = '\u06f4';
    farsiNumber[5] = '\u06f5';
    farsiNumber[6] = '\u06f6';
    farsiNumber[7] = '\u06f7';
    farsiNumber[8] = '\u06f8';
    farsiNumber[9] = '\u06f9';
    var outputNumber = "";

    var txtSMSText_Name = "<%=txtSMSText.ClientID%>";
    var txtSMSText = document.getElementById(txtSMSText_Name);
    for (var i = 0; i < txtSMSText.value.length; i++) {
        var arr = txtSMSText.value.substring(i, i + 1);
        var ch = arr.charAt();
        var index;

        if (ch >= 0 && ch <= 9 && ch != " ") {
            var index = farsiNumber[ch];
            outputNumber = outputNumber + index;
        }
        else
            outputNumber = outputNumber + ch;
    }
    txtSMSText.value = outputNumber;
    txtSMSText.focus();
}
//**************************** Change Language ****************************


//***************************************Count SMS******************************
function IIsFarsi(str) {
    var hdnLanguage = document.getElementById("ctl00_ContentPlaceHolder1_hdnLanguage");
   for (var i = 0, n = str.length; i < n; i++) {
        var arr = str.substring(i, i + 1);
        var ch = arr.charCodeAt();
        if (ch > 255) {
            hdnLanguage.value = 1;
            return true;
        }
    }
    hdnLanguage.value = 2;
    return false;
}





function ISMSCounter(txtSMSText_Name, lblSMSCounter_Name, lblCharchterCounter_Name, hdnSMSCounter_Name) {
    var txtSMSText = document.getElementById(txtSMSText_Name);
    var lblSMSCounter = document.getElementById(lblSMSCounter_Name);
    var lblCharchterCounter = document.getElementById(lblCharchterCounter_Name);
    var hdnSMSCounter = document.getElementById(hdnSMSCounter_Name);
    var hdnCharachterCounter = document.getElementById("ctl00_ContentPlaceHolder1_hdnLanguage");
    var intPage;
    var intCount;
    var newintcount;
    var newintPage;
    var text = txtSMSText.value.replace("/\r/g", '');
    var isFarsi = IIsFarsi(txtSMSText.value);
    var intCount = text.length;
    var intCharachter;
    var intPage;
    if (isFarsi) {
        if (intCount <= 70) //Persian
        {
            intPage = Math.ceil(intCount / 70);
            intCharachter = 70 - text.length;
        }
        else {
            intPage = Math.ceil(intCount / 67);
            var intNewCharachter
            if (intPage == 1) {
                intNewCharachter = text.length - 70;
            }
            else {
                intNewCharachter = text.length - (intPage - 1) * 67;
            }
            intCharachter = 67 - intNewCharachter;
        }
        if (intPage == 0) {
            intPage = 1;
        }
        lblCharchterCounter.innerHTML = intCharachter;
        lblSMSCounter.innerHTML = "(" + intPage + ")";
        hdnSMSCounter.value = intPage;
    }
    else {
        if (intCount <= 160) //English
        {
            intPage = Math.ceil(intCount / 160);
            intCharachter = 160 - text.length;
        }
        else {
            intPage = Math.ceil(intCount / 157);
            var intNewCharachter;
            if (intPage == 1) {
                intNewCharachter = text.length - 160;
            }
            else {
                intNewCharachter = text.length - (intPage - 1) * 157;
            }
            intCharachter = 157 - intNewCharachter;
        }
        if (intPage == 0) {
            intPage = 1;
            if (sw)
                intCharachter = 70;
            else
                intCharachter = 160;
        }
        lblCharchterCounter.innerHTML = intCharachter;
        lblSMSCounter.innerHTML = "(" + intPage + ")";
        hdnSMSCounter.value = intPage;
    }


}

function MakePersian(obj, event) {
//    if (sw == true)
//        IChangeToFarsi(obj, event);
//    else
//        if (window.event == null) {
//            if (e.preventDefault)
//                e.preventDefault();
//            e.returnValue = true;
//        }
//    if (nw == true)
//        IChangeNumbersToFarsi(obj, event);
//    else
//        if (window.event == null) {
//            if (e.preventDefault)
//                e.preventDefault();
//            e.returnValue = true;
//        }
}
//***************************************Count SMS******************************


//*******************************Count Reciever***********************************************
function CorrectNumber(ret) {
    var retval;
    if (ret.length >= 11) {
        if (ret.substring(0, 4) == "0098") {
            retval = ret.substring(4, ret.length);
            return retval;

        }
        else if (ret.substring(0, 3) == "098") {

            retval = ret.substring(3, ret.length);
            return retval;
        }
        else if (ret.substring(0, 3) == "+98") {

            retval = ret.substring(3, ret.length);
            return retval;
        }
        else if (ret.substring(0, 2) == "98") {

            retval = ret.substring(2, ret.length);
            return retval;
        }
        else if (ret.substring(0, 1) == "0") {

            retval = ret.substring(1, ret.length);
            return retval;
        }
    }
    return ret;
}
function IHamrahAvvalCounter(strNO, hdnHmarhAvval_Name) {
    var hdnHmarhAvval = document.getElementById(hdnHmarhAvval_Name);
    var HamrahavvalCount = parseInt(hdnHmarhAvval.value);
    var myRe = new RegExp("^[9][1-2][0-9]{8}$");
    if (myRe.test(strNO) == true) {
        HamrahavvalCount += 1;
        wrongNO = false;
    }
    hdnHmarhAvval.value = HamrahavvalCount;
}
function IIrancellCounter(strNO, hdnIrancell_Name) {
    var hdnIrancell = document.getElementById(hdnIrancell_Name);
    var IrancellCount = parseInt(hdnIrancell.value);
    var myRe = new RegExp("^[9][3][0|3|5-9][0-9]{7}$");
    if (myRe.test(strNO) == true) {
        IrancellCount += 1;
        wrongNO = false;
    }
    hdnIrancell.value = IrancellCount;
}
//*******************************Count Reciever***********************************************

//*******************************StateSent***********************************************
function TreeClick(evt) {

    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
    if (isChkBoxClick) {
        var parentTable = GetParentByTagName("table", src);
        var nxtSibling = parentTable.nextSibling;
        if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
        {
            if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
            {
               
                //check or uncheck children at all levels

               
                CheckUncheckChildren(parentTable.nextSibling, src.checked);
            }
        }
        
        if (src.checked) {
            //if (src.parentElement.innerHTML.indexOf("IsOstan") != -1) {
                CountCityCal("1");
          //  }
        }
        else {
         //   if (src.parentElement.innerHTML.indexOf("IsOstan") != -1) {
                CountCityCal("0");
         //   }
        }

       // if (src.checked) {
        CheckUncheckParents(src, src.checked);
        //  }
        globalNumCount = 0;
        $('#ctl00_ContentPlaceHolder1_CountNumberSelect').val("");
        DetectAllCheckBox();
        $('#ctl00_ContentPlaceHolder1_CountNumberSelect').val(globalNumCount);
       // alert($('#ctl00_ContentPlaceHolder1_CountNumberSelect').val());
        $('.ostanCount').empty();
        $('.ostanCount').html(globalNumCount);
        $("#ctl00_ContentPlaceHolder1_CountCity").val(globalNumCount);
    }
}
function CountCityCal(type) {
   // debugger;
//    var CountCity = $("#ctl00_ContentPlaceHolder1_CountCity");
//    var lValue = parseInt(CountCity.val());
//    if (type == "1") {
//        lValue += 1;
//    } else {

//        lValue -= 1;
//    }
//    if (lValue<0) {
//        CountCity.val('0');
//    }
//    else {
//        CountCity.val(lValue);
//    }
}





var globalNumCount = 0;

/***********  function For Tree Check   ***********/
function CheckUncheckParents(srcChild, check) {
    var parentDiv = GetParentByTagName("div", srcChild);
    var parentNodeTable = parentDiv.previousSibling;
    if (parentNodeTable) {
        var checkUncheckSwitch;
        if (check) //checkbox checked
        {
            var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
            if (isAllSiblingsChecked)
                checkUncheckSwitch = true;
            else
                return; //do not need to check parent if any(one or more) child not checked
        }
        else //checkbox unchecked
        {
            checkUncheckSwitch = false;
        }
        var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
        if (inpElemsInParentTable.length > 0) {
            var parentNodeChkBox = inpElemsInParentTable[0];
            parentNodeChkBox.checked = checkUncheckSwitch;
            if (parentNodeChkBox.parentElement.innerHTML.indexOf("IsOstan") != -1) {
                if (checkUncheckSwitch) {
                    CountCityCal("1");
                }
                else {
                    var isSingleSiblingsChecked = AreSingleSiblingsChecked(srcChild);
                    if (isSingleSiblingsChecked) {
                    }
                    else {
                        CountCityCal("0");
                    }
                }
            }
            CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
        }
    }
}
function AreSingleSiblingsChecked(chkBox) {
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for (var i = 0; i < childCount; i++) {
        if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //if any of sibling nodes are not checked, return false
                if (prevChkBox.checked) {
                    return true;
                }
            }
        }
    }
    return false;
}
function GetParentByTagName(parentTagName, childElementObj) {
    var parent = childElementObj.parentNode;
    while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
        parent = parent.parentNode;
    }
    return parent;
}
function CheckUncheckChildren(childContainer, check) {
    var childChkBoxes = childContainer.getElementsByTagName("input");
    var childChkBoxCount = childChkBoxes.length;
    for (var i = 0; i < childChkBoxCount; i++) {
        childChkBoxes[i].checked = check;
    }
}
function AreAllSiblingsChecked(chkBox) {
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for (var i = 0; i < childCount; i++) {
        if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
        {
            if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //if any of sibling nodes are not checked, return false
                if (!prevChkBox.checked) {
                    return false;
                }
            }
        }
    }
    return true;
}
/***********  function For Tree Check   ***********/

function IFillTree(id, obj, OstanID, BtnHiddenID) {
    var hdnClien = $("#" + OstanID);
    hdnClien.val(id);
    //
    if (id == "3253") {
        $('.ostanTxt').html("خراسان رضوی");
        $('.CityResult').show();
        var strLi = "<ul class=\"ul\"><li><a onclick=\"FillTree(3254,this)\" class=\"tab_Li_a\">خراسان شمالی </a></li><li><a onclick=\"FillTree(3253,this)\" class=\"tab_Li_a\">خراسان رضوی </a></li><li><a onclick=\"FillTree(3255,this)\" class=\"tab_Li_a\">خراسان جنوبی </a></li></ul>";
        $('.tab').html(strLi);
        $('.ul li a').removeClass("activeLia");
        $($('.tab_Li_a')[1]).addClass("activeLia");
        $('.tab').show();
    }
    else if (id == "3254" || id == "3255") {
        $('.ul li a').removeClass("activeLia");
        $(obj).addClass("activeLia");
        //  
        $('.ostanTxt').html($(obj)[0].innerHTML);
        $('.CityResult').show();
    }
    else if (id == "3226") {
        $('.ostanTxt').html("تهران");
        $('.CityResult').show();
        $('.tab').empty();
        var strLi = "<ul class=\"ul\"><li><a onclick=\"FillTree(3226,this)\" class=\"tab_Li_a\">تهران </a></li><li><a onclick=\"FillTree(3256,this)\" class=\"tab_Li_a\">البرز </a></li></ul>";
        $('.tab').html(strLi);
        $('.ul li a').removeClass("activeLia");
        $($('.tab_Li_a')[0]).addClass("activeLia");
        $('.tab').show();

    }
    else if (id == "3256 ") {
        $('.ul li a').removeClass("activeLia");
        $(obj).addClass("activeLia");
        $('.ostanTxt').html($(obj)[0].innerHTML);
        $('.CityResult').show();
    }
    else {
        $('.tab').empty();
        $('.tab').hide();
        $('.CityResult').show();
    }
    $(".CityInfo").show();
    document.getElementById(BtnHiddenID).click();
    return false;
}

function EndRequestHandler(sender, args) {
    var div = $('.treeViewDiv');
    $('.treeViewDiv')[0].scrollTop = lastScTo;
    window.scrollTop = lastScToWin;
}
function beginRequest() {
    var div = $('.treeViewDiv');
    lastScTo = $('.treeViewDiv')[0].scrollTop;
    lastScToWin = window.scrollTop;
}
var lastObj;
var Lasttooltip;
function BulidIranMap() {

    var paper = Raphael('map', 500, 500);
    over = function (event) {
        this.c = this.c || this.attr("fill");
        this.stop().animate({ fill: "#bacabd" }, 500);
        // 
        var i = $(this)[0].yOffset;
        $("body").append("<div id='" + "ToolTip" + "'>" + $(this)[0].attrs.title + "</div>");
        Lasttooltip = $(this)[0].attrs.title;
        $(this)[0].attr("title", "");
        if ($('#ToolTip').css("display").toLowerCase() == "block") {
            $('#ToolTip').css("position", "absolute")
                            .css("top", (event.pageY) + "px")
                            .css("left", (event.pageX) + "px");
        }
        else {
            $('#ToolTip').css("position", "absolute")
                            .css("top", (event.pageY) + "px")
                            .css("left", (event.pageX) + "px")
                            .css("display", "block");
        }
    };
    out = function (event) {
        this.stop().animate({ fill: this.c }, 500);
        $(this)[0].attr("title", Lasttooltip);
        $('#ToolTip').remove();
    };
    click = function (event) {
       //  
        event.preventDefault();
        var $h = this.attr('href');
        $('.ostanTxt').empty();
        $('.ostanTxt').html(Lasttooltip);
        FillTree($h,null);
        $("#ctl00_ContentPlaceHolder1_CountCity").val('0');
        return false;
    };
    paper.setStart();
    //            Obj = paper.path('M 167.25359,126.0263 C 165.61959000000002,126.0643,165.43359,126.34255,166.00359,128.43255 C 166.68759,130.78855,169.38559,130.13729999999998,170.75359,132.1513 C 172.12159,134.20329999999998,169.36484000000002,134.55055,168.03484,137.93255 C 166.66684,141.31455,162.61984,140.62355,160.90984,141.30755 C 159.73184,141.76354999999998,159.88284000000002,144.17605,160.03484,145.62005 C 160.52884,145.46805,160.90984,145.37005,160.90984,145.37005 C 160.90984,145.37005,162.26909,147.0888,163.94109,147.0888 C 165.65109,147.0888,165.95334,147.43255,168.34734,147.43255 C 170.70334,147.43255,170.04859,149.8318,171.37859,148.4638 C 172.74659,147.0958,174.12859,148.4638,174.12859,148.4638 C 174.12859,148.4638,176.70902,150.09813,176.81609,152.05755L184.34734,131.18255 C 184.30664000000002,131.1821,184.26304,131.18255,184.22234,131.18255 C 182.51234,131.18255,181.52084,128.4638,178.78484,128.4638 C 176.08684,128.4638,170.32834,126.08879999999999,167.97234,126.08879999999999 C 167.74434,126.0508,167.48159,125.9883,167.25359,126.02629999999999 Z');
    //            Obj.attr({ 'href': '3240', 'cursor': 'pointer', 'title': 'کرج', 'fill': '#ffa71e', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    //           
    Obj = paper.path('M 82.7 37.8 C 82.2 37.8 81.5 38 80.4 38.3 C 76.9 39.2 76.1 39.8 74.6 42.7 C 73.4 45.1 75.5 46.5 73.7 48.9 C 72 51.1 70.6 49.7 70.6 53.3 C 70.6 55.5 70.6 57.7 70.6 60 C 70.6 62.2 68.2 64.1 67 65.3 C 65.2 67.1 60.1 65.5 59 64.4 C 57.3 62.7 53.8 63.7 52.3 64 C 50.2 64.4 50.1 63.3 47 62.7 C 43.8 62.1 45.2 66.7 45.7 67.6 C 47 70.2 47.7 72.4 48.4 75.1 C 49.3 78.5 47 79.5 49.7 82.2 C 51.5 84 56 82.7 55 86.6 C 54.3 89.4 51.7 88.2 52.8 92.4 C 53.3 94.5 50.6 96.1 50.6 99.1 C 50.6 101.8 51.9 102.8 51.9 106.2 C 51.9 107.5 56.3 109.2 57.2 111.1 C 57.4 111.5 56.4 114.5 55.9 115.5 C 55.8 116.2 55.6 117 55.5 117.7 C 55.1 119.8 54.8 123.1 55.1 124.4 C 55.7 126.8 57.3 125.9 57.3 129.3 C 57.3 131.9 57.9 134.5 56 136.4 C 54.5 137.9 54.8 140.4 54.2 142.6 C 53.5 145.3 54.5 146.9 56.4 147.9 C 59.3 149.3 61.8 146.4 64.4 147.5 C 66.8 148.5 65.6 152.1 64.8 153.7 C 64.2 154.9 61.2 156 60.4 156.8 C 58.7 158.5 59.3 160.8 58.2 163 C 57.1 165.2 55.9 167.1 53.8 169.2 C 52.4 170.6 51.2 172.3 50.2 173.6 C 48.3 176.2 47.3 176.7 46.7 179.4 C 46.1 181.8 45.8 183.8 45.8 186.1 C 45.8 189.3 50.6 188.2 52.9 188.8 C 56.3 189.7 55.2 190.2 56.4 192.8 C 57.2 194.3 59.8 194.1 60.8 195.5 C 62.4 197.7 66 196.7 67.9 198.6 C 69.3 200 68.1 203.3 67 204.4 C 66.1 205.3 64.9 205.6 64.3 206.2 C 63.4 207.1 67.7 212.8 67.9 213.3 C 68.4 215.2 67.4 219.1 66.1 220.4 C 64 222.5 65.2 225.2 67.4 225.7 C 69.8 226.3 70.8 226.4 71.4 228.8 C 71.7 230 75.6 231.7 76.3 232.4 C 78.7 234.8 78.8 235.9 79.4 239.1 C 79.7 240.6 76.1 242.7 76.7 245.8 C 77.3 249 76.6 248.8 75.8 252 C 75 255.1 74.2 254.1 78 256 C 80.6 257.3 82.4 257.4 82.4 260.4 C 82.4 262.6 80.3 263.8 81.1 266.6 C 82 270.1 81.5 270.6 78.9 273.3 C 76.7 275.5 79.6 278.6 81.6 278.6 C 82.1 278.6 82.5 278.9 82.9 279 C 87.3 279 88.1 282.6 90.5 284.3 C 92.5 285.8 91.5 290 90.1 291.4 C 88.8 292.7 87 297.7 86.5 299.4 C 86.2 300.6 89.2 300.8 89.2 302.1 C 89.2 305.5 90.5 306.5 91.9 309.2 C 93.2 311.7 96.7 311 98.6 309.6 C 100.7 308 102.6 312.8 103.5 314 C 104.5 315.4 102.2 319.4 102.2 321.1 C 102.2 323 104.9 323.2 104.9 326.4 C 104.9 329.3 101.9 330 105.3 331.7 C 108 333.1 105 337.5 104.4 338.4 C 102.6 340.9 103.1 343.3 106.2 343.3 C 108.8 343.3 111.2 341.5 114.6 341.5 C 116.2 341.5 117.4 341.8 118.2 342.4 C 118.9 339.9 119.7 337.7 120.5 336 C 125 326.2 125.9 328.8 125.9 328.8 C 125.9 328.8 125.9 324.4 132.2 322.6 C 138.5 320.8 141.1 322.6 146.5 321.7 C 151.9 320.8 148.3 312.8 152.8 309.2 C 154.2 308.1 155.3 307.7 156.4 307.6 C 158.7 307.6 160.7 309.5 164.4 310.1 C 169.8 311 181.3 314.6 192.1 311.9 C 202.8 309.2 201 309.2 205.5 316.4 C 210 323.5 219.8 320.9 225.2 322.7 C 230.6 324.5 229.6 312 235.9 310.2 C 236.2 310.1 236.4 310.1 236.7 310 C 236.5 309.2 236.1 308.1 235.9 307.5 C 233.8 299.8 228.5 297.1 228.8 291.6 C 225 291 222.1 290.6 220.8 290.6 C 213.7 290.6 210.1 295.1 207.4 288.8 C 204.7 282.6 211.8 278.1 208.3 273.6 C 204.7 269.1 206.5 265.6 201.2 265.6 C 195.8 265.6 198.5 257.6 194 262 C 189.5 266.5 191.3 262.9 186 263.8 C 180.6 264.7 183.3 272.7 178 271.8 C 172.6 270.9 176.2 276.3 171.7 271.8 C 167.2 267.3 164.6 259.3 156.5 256.6 C 148.4 253.9 143.1 246.8 137.7 245 C 132.3 243.2 123.4 240.5 121.6 235.2 C 119.8 229.9 119.8 224.5 119.8 219.1 C 119.8 213.7 116.2 209.3 114.5 203.9 C 112.8 198.5 113.6 177.1 113.6 171.8 C 113.6 166.5 103.8 159.3 105.6 154.8 C 107.4 150.3 119 142.3 121.7 137.9 C 124.4 133.4 126.2 136.1 124.4 128.1 C 122.6 120.1 121.7 118.3 127.1 111.1 C 128 109.9 130.3 107.3 133.1 104.3 C 132.6 103.9 132.2 103.6 131.9 103.5 C 129.2 102.1 129.2 100.3 129.2 97.3 C 129.2 93.3 126.9 92.3 126.1 89.3 C 125.2 85.8 120.5 86.3 118.1 84.9 C 117.3 84.4 116.2 83.9 115.4 83.6 C 114.7 83 113.9 82.4 113.2 81.8 C 111.6 80.6 111.4 75.9 110.9 74.2 C 110.5 72.8 107.9 69.8 106.9 68.9 C 104.6 66.6 102.4 65.8 102.4 62.2 C 102.4 59.8 101.6 57.9 101.1 56 C 100.4 53.4 98.8 51.5 98 50.7 C 96.3 49 93.9 48.2 91.8 45.4 C 89.9 42.9 88.1 42.2 86 40.1 C 84.6 38.7 84.3 38 83.4 37.9 C 83 37.8 82.9 37.8 82.7 37.8Z M 229.1 288.8 C 229 289 228.9 289.3 228.8 289.5 C 228.9 289.2 229 289 229.1 288.8Z');
    Obj.attr({ 'href': '3228', 'cursor': 'pointer', 'title': 'آذربایجان غربی', 'fill': '#920101', 'stroke': '#ffffff', 'stroke-width': '1', 'stroke-opacity': '1' });
    Obj = paper.path('M 1154.3 711 C 1153.8 715.5 1153.4 719.1 1153 720.8 C 1151.2 728.8 1144.1 743.1 1144.1 757.4 C 1144.1 771.7 1146.8 774.3 1150.4 784.2 C 1154 794 1150.4 797.6 1141.5 797.6 C 1132.6 797.6 1128.1 787.8 1123.6 780.6 C 1119.1 773.5 1104.9 772.6 1103.1 769 C 1101.3 765.4 1097.7 766.3 1093.3 769.9 C 1088.8 773.5 1054.9 779.7 1049.6 782.4 C 1044.2 785.1 1045.1 786.9 1043.3 794 C 1041.5 801.1 1034.4 844.8 1033.5 851.1 C 1032.6 857.4 1033.5 855.6 1039.8 860 C 1046 864.5 1043.4 871.6 1043.4 882.3 C 1043.4 893 1043.4 896.6 1038.9 903.7 C 1034.4 910.8 1038.9 914.4 1038.9 919.8 C 1038.9 925.2 1039.8 924.3 1044.3 930.5 C 1048.8 936.7 1023.8 946.6 1014.8 951.9 C 1005.9 957.3 1009.4 956.4 1016.6 961.7 C 1023.7 967.1 1021.1 970.6 1018.4 978.6 C 1015.7 986.6 1013.1 981.3 1013.1 990.2 C 1013.1 999.1 1014.9 1009 1014.9 1015.2 C 1014.9 1021.4 1007.8 1052.7 1007.8 1058.9 C 1007.8 1065.1 1010.5 1076.8 1014.1 1082.1 C 1017.7 1087.5 1021.2 1085.7 1021.2 1092.8 C 1021.2 1099.9 1019.4 1099.9 1021.2 1107.1 C 1023 1114.2 1021.2 1109.8 1019.4 1116 C 1017.6 1122.2 1022.1 1124 1020.3 1130.3 C 1018.5 1136.5 1017.6 1134.8 1017.6 1141.9 C 1017.6 1149 1014.9 1149.1 1014 1155.3 C 1013.1 1161.5 1022.9 1162.4 1028.3 1167.8 C 1033.7 1173.2 1031 1173.1 1036.3 1178.5 C 1041.7 1183.9 1043.4 1186.5 1044.3 1193.7 C 1044.5 1195.3 1044.9 1197.6 1045.4 1200.3 C 1048 1199.3 1051.3 1197.8 1052.2 1197.6 C 1054.3 1197.1 1055 1195.9 1056.9 1195.4 C 1058.3 1195 1060.7 1193.8 1061.9 1193.8 C 1064.8 1193.8 1067.1 1194.3 1069.1 1196.3 C 1070.8 1198 1072.4 1199.1 1075.4 1199.1 C 1077.6 1199.1 1078.6 1198.2 1080.7 1198.2 C 1082.6 1198.2 1083.5 1197.9 1085.4 1197.9 C 1086.8 1197.9 1090.5 1197.3 1091.4 1198.5 C 1091.6 1198.8 1094.3 1202.8 1094.8 1202.6 C 1095.2 1202.5 1095.6 1202.4 1096.1 1202.3 C 1096.4 1202.3 1097.4 1202.4 1097.7 1202.3 C 1099.3 1201.3 1101.3 1199.7 1103 1198.8 C 1104.8 1197.9 1107 1198.8 1108.9 1199.7 C 1110.7 1200.6 1111.6 1202.8 1113.3 1203.2 C 1114.6 1203.5 1116.7 1203.2 1117.7 1201.9 C 1118.9 1200.3 1119.4 1198 1122.4 1198.7 C 1124.3 1199.2 1125.3 1200.1 1126.5 1201.2 C 1127.5 1202.2 1130.8 1205.2 1132.8 1203.7 C 1133.8 1202.9 1135.5 1201.3 1133.7 1199.9 C 1132.3 1198.9 1130.4 1197.8 1132.1 1196.1 C 1132.9 1195.3 1134.2 1193.7 1135.9 1193.3 C 1136.5 1193.1 1141.1 1192.9 1141.6 1193.3 C 1142.9 1193.6 1144.3 1193.8 1145.4 1194.9 C 1146.5 1196 1147.9 1197.2 1147.9 1199 C 1147.9 1200.7 1149 1204.5 1150.1 1205 C 1153.4 1206.7 1172.9 1208.8 1174.2 1208.8 C 1176.9 1208.8 1181.6 1209.7 1184.2 1210.4 C 1186 1210.9 1188.2 1210.5 1189.9 1211.3 C 1191 1211.9 1193.1 1214.7 1194 1214.8 C 1196.1 1215.2 1198.2 1215.3 1200.3 1215.7 C 1201.9 1216 1203.8 1216 1205.6 1216 C 1207.3 1216 1208.9 1216 1210.6 1215.7 C 1211.8 1215.5 1214.5 1214.8 1215 1212.9 C 1215.5 1210.9 1213.7 1210.7 1213.7 1209.1 C 1213.7 1207.8 1214.8 1206.2 1215.9 1205.3 C 1218.4 1203.4 1219 1204.5 1220.3 1206.2 L 1220.5 1205.3 L 1222.3 1198.6 L 1221.9 1170.2 L 1224.1 1169.3 L 1224.1 1165.3 L 1222.8 1164 L 1222.8 1156.9 L 1226.8 1157.8 L 1228.6 1155.5 L 1230.8 1126.2 C 1230.8 1124.5 1231.6 1121.8 1233.5 1120.9 C 1234.8 1120.2 1237 1118.2 1239.3 1118.2 C 1242.4 1118.2 1243.2 1117.8 1245.1 1115.9 C 1246.5 1114.5 1247.7 1114.8 1250 1113.7 C 1251.6 1112.9 1250.5 1108.5 1252.2 1108.8 C 1255.4 1109.4 1256.5 1110.3 1259.7 1109.7 C 1264.1 1108.8 1260 1107 1261.5 1103.9 C 1262.4 1102 1262.7 1101.2 1263.3 1099 C 1263.9 1096.6 1264.7 1096.8 1268.2 1095.9 C 1269.6 1095.5 1269.4 1092.6 1270.9 1092.3 C 1272 1092.1 1276.1 1091 1278 1091 C 1281.4 1091 1282.1 1090.5 1284.7 1089.2 C 1286.4 1088.4 1288.3 1087.5 1290.5 1087 C 1293 1086.4 1294.4 1084.8 1296.3 1084.3 C 1298.9 1083.6 1302.3 1083.4 1305.2 1083.4 C 1307.3 1083.4 1310.1 1081 1311.9 1082.1 C 1314.8 1083.9 1316.1 1083.2 1318.6 1082.5 C 1320.3 1082.1 1322 1085.2 1323.5 1084.3 C 1324.2 1083.9 1325 1083.4 1325.7 1083 L 1325.3 1066.1 L 1327.5 1065.2 L 1330.6 1063.4 L 1331 1060.7 L 1327.9 1058.9 L 1327.9 1045.6 L 1329.7 1042.5 L 1331.9 1042.1 L 1330.1 1039.4 C 1330.1 1039.4 1329.7 1035.8 1327.9 1034.5 C 1326.1 1033.2 1322.1 1032.3 1322.1 1032.3 L 1319.9 1035 L 1308.4 1036.8 L 1303.9 1038.6 L 1299.9 1037.3 L 1296.3 1037.3 L 1293.2 1036 L 1292.8 1031.1 L 1291.9 1028.9 L 1291.9 1024.9 L 1295 1018.2 L 1294.6 1014.7 L 1290.6 991.6 L 1285.7 976.5 L 1286.6 958.3 L 1285.3 955.6 C 1284.3 955.8 1281.4 956.2 1280 956.5 C 1279.2 956.7 1273.9 957.5 1273.3 958.3 C 1272.1 960.2 1269.7 955.6 1268.4 954.3 C 1267 952.9 1266.9 949.4 1264.9 949.4 C 1260.7 949.4 1262.8 946.7 1259.6 945.9 C 1257 945.2 1253.4 944.5 1250.3 943.7 C 1247.7 943.1 1243.2 941.9 1241 941.9 C 1238.2 941.9 1236.3 940.1 1233.9 939.7 C 1232.8 939.5 1227.8 939.6 1226.8 938.8 C 1224.9 937.4 1223.2 935.9 1221 934.8 C 1218.4 933.5 1218.5 931.8 1216.6 930.4 C 1215 929.2 1213.2 926.4 1211.7 925.5 C 1210.2 924.6 1207 924.2 1205.9 921.9 C 1204.8 919.8 1202.9 918.5 1201.5 917 C 1200.6 916.1 1198.9 913.2 1198.4 912.1 C 1197.6 910.1 1195.6 906.9 1195.3 905.4 C 1194.8 902.9 1193.7 901.6 1193.1 898.7 C 1192.6 896.3 1189.1 895.3 1189.1 894.7 C 1189.1 890.6 1185.1 893 1184.7 891.1 C 1184.2 888.9 1184.8 886.5 1182.5 885.3 C 1181.9 885 1181.3 884.7 1180.7 884.4 C 1180.7 884.4 1180.7 874.6 1179.8 873.3 C 1178.9 872 1177.1 873.3 1177.1 873.3 L 1144.2 837.8 L 1202.4 756.5 L 1202.9 745.8 L 1205.1 744 L 1205.5 740 L 1203.3 738.7 L 1203.3 736.5 L 1201.1 732.5 C 1201.1 732.5 1201.5 728.5 1201.5 727.2 C 1201.5 725.9 1197.5 724.5 1197.5 724.5 L 1196.6 718.7 L 1194.4 713.4 L 1154.3 711 L 1154.3 711Z');
    Obj.attr({ 'href': '3241', 'cursor': 'pointer', 'title': 'سیستان و بلوچستان', 'fill': '#ca0202', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 870.6 179.5 C 865.5 179.5 867.8 181.8 865.3 183 C 863.5 183.9 862.4 186.6 860.4 186.6 C 859.7 186.6 858.9 186.6 858.2 186.6 C 855.7 186.6 852.6 187.1 850.2 186.6 C 845.9 185.7 844.9 181.8 842.2 181.3 C 838.2 180.5 837.2 181.3 835.1 183.5 C 832.2 186.4 831.2 181.7 827.1 181.7 C 823 181.7 820.1 181.4 816.9 183 C 815.4 183.8 811.7 187.6 813.4 189.2 C 814.3 190.1 817.4 194.1 814.7 195.4 C 812.5 196.5 811.5 198.4 808.5 197.6 C 808.2 197.5 807.9 197.5 807.7 197.4 C 807.8 200 807.9 202.7 807.9 203.6 C 807.9 208.6 805.4 217.5 805.4 223.8 C 805.4 230.1 805.4 228.9 811.7 231.4 C 818 233.9 807.9 240.2 799.1 242.7 C 790.3 245.2 783.9 259.1 783.9 259.1 C 783.9 259.1 791.5 275.5 796.5 275.5 C 801.5 275.5 796.5 278 800.3 283.1 C 804.1 288.1 801.6 289.4 800.3 295.7 C 799 302 805.4 302 811.7 309.6 C 818 317.2 824.3 313.4 829.4 315.9 C 834.4 318.4 838.2 313.4 845.8 315.9 C 853.4 318.4 844.5 328.5 842 333.6 C 839.5 338.6 848.3 362.6 850.8 367.7 C 853.3 372.7 873.5 386.6 873.5 391.7 C 873.5 396.8 859.6 408.1 859.6 414.4 C 859.6 420.7 852 422 839.4 429.6 C 832.5 433.8 821.3 450.1 811.9 465.1 C 813.2 465.2 814.3 465.2 814.8 465 C 820.5 463.1 836.2 460.6 840 454.9 C 843.8 449.2 858.3 439.1 864 435.3 C 866.5 433.6 869.4 433.1 872.7 433 C 876.9 432.9 881.5 433.7 886.1 434 C 894.3 434.6 901.9 440.3 898.7 449.8 C 895.5 459.3 898.1 468.1 889.9 468.7 C 881.7 469.3 880.4 475.6 877.3 480 C 874.1 484.4 879.8 482.5 874.1 495.2 C 868.4 507.8 864 510.4 864.6 517.3 C 865.2 524.2 863.3 530.6 869 533.7 C 874.7 536.9 878.5 550.1 884.2 552 C 889.9 553.9 900.6 552.7 904.4 549.5 C 908.2 546.3 915.1 549.5 913.2 552.7 C 911.3 555.9 905 565.9 911.3 571.6 C 917.6 577.3 913.8 587.4 917 595.6 C 920.2 603.8 918.9 611.4 924.6 611.4 C 930.3 611.4 942.3 627.8 942.3 634.7 C 942.3 641.6 953 651.7 950.5 657.4 C 948 663.1 937.2 674.4 934.1 680.1 C 930.9 685.8 933.5 683.9 929.1 697.1 C 928.4 699.3 927.1 702.5 925.6 706.1 C 967.3 721.9 1009.8 737.5 1013.6 740 C 1021.2 745 1028.7 767.8 1036.3 777.9 C 1039.6 782.3 1042.3 784.7 1045.2 786.3 C 1045.9 784.7 1047 783.6 1049.6 782.2 C 1055 779.5 1088.9 773.3 1093.3 769.7 C 1097.8 766.1 1101.4 765.3 1103.1 768.8 C 1104.8 772.3 1119.1 773.3 1123.6 780.4 C 1128.1 787.5 1132.6 797.4 1141.5 797.4 C 1150.4 797.4 1154 793.8 1150.4 784 C 1146.8 774.2 1144.1 771.5 1144.1 757.2 C 1144.1 742.9 1151.3 728.6 1153 720.6 C 1153.4 719 1153.8 715.4 1154.3 710.8 L 1131.8 709.2 L 1131.8 700.3 L 1130 697.6 L 1128.7 695.8 L 1131.4 690.5 L 1128.7 681.6 L 1127.8 671.4 C 1127.8 671.4 1126.5 665.6 1128.2 664.3 C 1130 663 1128.6 662.1 1128.6 662.1 L 1129.9 655.4 L 1129 648.3 L 1105.9 583.5 C 1109 585.1 1105 580 1105.9 578.2 C 1107.1 575.9 1108 573.8 1109.9 572 C 1111.5 570.4 1113.8 570 1114.8 568 C 1116 565.7 1116.5 563.1 1118.4 561.3 C 1119.1 560.6 1122.6 557.7 1122.8 556.9 C 1122.9 556.5 1123.4 550.9 1123.7 550.7 C 1123.8 550.6 1130.3 548.3 1129.9 547.6 C 1128 543.8 1125.4 545.4 1121.9 545.4 C 1116.1 545.4 1111.2 544.9 1105.9 543.6 C 1103.5 543 1102.1 542.5 1100.6 540.9 C 1099.6 539.9 1097.9 538 1097.9 536 C 1097.9 532.9 1098 532.5 1099.7 530.2 C 1101.1 528.3 1101.6 525.5 1100.6 523.5 C 1099.8 522 1100.2 519.2 1099.3 517.3 C 1098.3 515.3 1098 513.8 1098 511.1 C 1098 508.9 1098 506.7 1098 504.4 C 1098 501.7 1098.4 500.3 1098.9 498.2 C 1099.2 496.9 1101.6 493.4 1102.9 492.4 C 1104.6 491.1 1105.8 489.4 1106.9 488 C 1108.2 486.3 1109.5 484.9 1112.7 484.9 C 1114.6 484.9 1120.2 485.9 1121.1 484 C 1122.1 482 1122.1 480.3 1120.2 477.8 C 1118.8 475.9 1118 474.6 1115.8 472.9 C 1115.2 472.4 1109.9 468.1 1110.9 467.6 C 1111.8 467.1 1118.9 467.2 1119.3 466.3 C 1120.6 463.8 1119.2 461.6 1122.4 461 C 1125.7 460.3 1126.4 460.4 1126.4 456.6 C 1126.4 454.2 1127.1 450.1 1128.6 448.6 C 1130.6 446.6 1130.4 444.5 1130.4 441.5 C 1130.4 438.5 1130.4 436.9 1130.4 433.9 C 1130.4 431 1131.6 430 1132.2 427.7 C 1132.8 425.4 1133.5 423.6 1133.5 421 C 1133.5 418.6 1128 416.2 1129.9 414.8 C 1129.9 413.6 1129.2 408.1 1130.3 407.2 C 1132.7 405.4 1134.3 405.9 1134.3 402.3 C 1134.3 399 1136.7 398 1137.4 395.2 C 1138.1 392.2 1137.7 390.5 1139.6 388.5 C 1141.2 386.9 1140.9 384.9 1140.9 382.3 C 1140.9 378.9 1139.7 378.8 1136.9 377.4 C 1135.2 376.6 1134 373.7 1135.6 371.6 C 1136.9 369.9 1136.9 367.9 1136.9 364.9 C 1136.9 362.4 1136.9 360.1 1136.5 357.8 C 1136.1 355.8 1134.6 352.5 1132.1 352.5 C 1128.1 352.5 1128.3 352.2 1129.9 348.9 C 1130.6 347.5 1133.5 344.3 1133.5 342.2 C 1133.5 339.6 1133.5 337.6 1133.1 335.5 C 1132.5 332.4 1132 331.5 1130.9 329.3 C 1130.1 327.7 1129.9 324.7 1128.6 323.5 C 1126 320.9 1127.4 321.4 1128.2 318.2 C 1128.6 316.7 1129.3 313.2 1128.6 311.5 C 1127.6 309 1128.8 308.1 1129.5 305.3 C 1130.7 300.4 1128.2 303.5 1126.4 300.4 C 1126 299.7 1125.5 298.9 1125.1 298.2 L 1126 297.8 L 1076.7 300.5 C 1073.4 300.5 1073.4 299.4 1072.3 297.8 C 1072.1 297.4 1071.9 293 1071 292.5 C 1068.1 291 1066.8 288.2 1065.2 287.2 C 1062.7 285.7 1061.8 284.3 1060.3 281.4 C 1059.2 279.1 1058 276.7 1056.7 274.3 C 1055.8 272.5 1053.5 268.8 1050.9 269.4 C 1046.6 270.5 1045 267.9 1042 267.2 C 1039 266.5 1035.8 263.6 1032.7 263.6 C 1030 263.6 1028.8 265.9 1026.9 264 C 1025.8 262.9 1026.2 257.9 1023.8 259.1 C 1023.3 259.4 1015.5 262.2 1016.7 257.3 C 1017.1 255.6 1014.6 252.9 1012.7 252 C 1010.5 250.9 1009.2 249.7 1008.7 247.1 C 1008.2 244.8 1008.4 242.3 1007.8 240 C 1007.7 239.5 1007.2 233.3 1006.9 233.3 C 1004.6 233.3 1003 236.5 1001.6 236 C 999.3 235.1 996.4 233.5 994.9 232 C 992.7 229.8 990.5 228.8 989.1 228.4 C 987.2 227.9 985.9 224.4 983.8 224.4 C 981.7 224.4 977.1 225.2 975.8 223.5 C 973.4 220.3 974 220.8 971.8 223.1 C 971 223.9 967.5 227.5 965.6 227.1 C 963 226.6 960 222.2 958 222.2 C 952 222.2 955.1 226.4 952.7 227.5 C 949.5 229.1 947.8 227.5 944.3 226.6 C 942.7 226.2 934.8 224.8 934.5 223.5 C 934.1 221.5 933.4 217.5 930.9 216.8 C 927.9 216.1 924.4 216.9 921.6 215.5 C 920.3 214.8 917.1 213.5 916.3 212.4 C 913.7 208.9 913.9 211.5 911 211.5 C 908.9 211.5 905.4 210.4 904.3 209.3 C 904.2 209.2 904.1 208.9 903.9 208.9 C 900.5 207.6 895.6 206.7 891.9 206.7 C 889.2 206.7 887.8 207.6 885.2 206.3 C 883.6 205.5 879.4 203.8 879.4 201 C 879.4 199.9 880.1 194.6 879.4 193.9 C 878.7 193.2 876.3 191.4 875.9 189.9 C 875.4 188 873.2 186.8 873.2 185 C 873.3 183.2 873.4 179.5 870.6 179.5 L 870.6 179.5Z M 796.3 489.8 C 795.8 490.5 795.4 491.1 795.1 491.5 C 795.4 491.1 795.8 490.5 796.3 489.8Z M 1200.5 727.1 C 1200.6 727.2 1200.6 727.3 1200.7 727.4 C 1200.6 727.3 1200.5 727.2 1200.5 727.1Z');
    Obj.attr({ 'href': '3253', 'cursor': 'pointer', 'title': 'خراسان (شمالی ، رضوی ، جنوبی)', 'fill': '#ca0202', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 749.7 920.7 C 749.7 920.7 755.1 932.3 749.7 935.9 C 744.3 939.5 749.7 942.2 755.1 946.6 C 760.5 951.1 771.2 959.1 772.9 965.4 C 774.7 971.6 769.3 986.8 769.3 993.1 C 769.3 999.4 775.5 998.4 777.3 1004.7 C 779.1 1010.9 764.8 1012.7 760.3 1012.7 C 755.8 1012.7 759.4 1016.3 754 1018.1 C 748.6 1019.9 745.1 1034.2 738.8 1038.6 C 732.6 1043.1 725.4 1035.9 720 1035 C 714.6 1034.1 709.3 1037.7 703 1038.6 C 696.8 1039.5 697.7 1037.7 686.1 1037.7 C 674.5 1037.7 660.2 1044.8 663.8 1047.5 C 667.4 1050.2 658.4 1056.4 662.9 1059.1 C 667.4 1061.8 659.3 1063.6 654.9 1067.1 C 650.4 1070.7 636.2 1060.9 629 1062.7 C 621.9 1064.5 621.8 1057.3 612 1057.3 C 602.2 1057.3 599.5 1057.3 595.1 1052.9 C 590.6 1048.4 588.8 1047.5 588.8 1047.5 C 588.8 1047.5 587 1049.3 577.2 1052 C 575.4 1052.5 573.4 1053.5 571.3 1055 C 573 1055.5 573.4 1056.3 573.4 1056.3 L 577.8 1059.5 L 582.2 1063.6 L 586 1065.5 C 586 1065.5 593.8 1071.8 596 1073.4 C 598.2 1075 624.2 1083.8 624.2 1083.8 C 624.5 1084.1 625 1084.4 625.2 1084.8 C 625.9 1086.5 626.9 1087.5 627.4 1089.5 C 627.8 1091.3 628.1 1093 629.3 1094.3 C 630.9 1095.9 631.3 1097.4 633.4 1098.4 C 635.6 1099.5 637.4 1099.9 639.1 1101.6 C 640.3 1102.8 642 1104.4 643.5 1105.1 C 645 1105.9 646.8 1105.7 648.8 1105.7 C 650.9 1105.7 653 1105.7 655.1 1105.7 C 656.3 1105.7 660.5 1106.4 661.4 1105.7 C 662.1 1105.1 663.8 1103.2 665.2 1103.2 C 667 1103.2 668.7 1103.5 670.2 1103.5 C 672.9 1103.5 672.4 1104.6 674.6 1105.7 C 677 1106.9 679.1 1106.3 681.8 1106.3 C 683.6 1106.3 686.5 1105.4 688.1 1107 C 689.3 1108.2 691.1 1110 692.2 1111.4 C 693.2 1112.7 694 1115.3 695.6 1116.2 C 697.8 1117.3 699.6 1116.8 701.9 1117.2 C 703.9 1117.5 707.3 1117.9 708.2 1120 C 708.9 1121.8 711.2 1124.2 714.2 1123.5 C 716.1 1123 717.2 1120.9 719.2 1121.9 C 720.5 1122.5 722.9 1124.1 724.5 1122.8 C 726.1 1121.6 727.3 1119.9 729.2 1119 C 730.3 1118.4 732.9 1116.9 733.6 1115.5 C 734.8 1113.1 737.2 1113.6 738.6 1111.7 C 739.9 1110.4 741.1 1109.4 742.7 1108.2 C 744.5 1106.8 745.9 1106 748.4 1105.4 C 749.8 1105 752 1103.7 753.1 1102.6 C 754.2 1101.5 756.6 1099.3 758.7 1100.4 C 760.7 1101.4 762.4 1101.3 764.7 1101.3 C 766.4 1101.3 768 1101.3 769.7 1101.3 C 772.4 1101.3 773 1100.4 775 1099.4 C 776.1 1098.9 778.5 1099.1 779.7 1097.8 C 780.6 1096.9 781.1 1095.5 780.6 1093.7 C 780.2 1092 779.7 1091.1 779.7 1089.3 C 779.7 1087.4 780.6 1086.5 781.6 1085.2 C 782.9 1083.5 783.5 1082.9 785.7 1082.3 C 787.2 1081.9 788.6 1081.8 790.4 1081.4 C 792.1 1081 794.2 1080.5 796.1 1080.5 C 798 1080.5 800.5 1081 802.4 1080.5 C 804.6 1079.9 805 1079.5 806.5 1078 C 807.8 1076.7 809.6 1076.4 810.6 1075.5 C 812.2 1073.8 813.2 1073.5 814.7 1072 C 815.9 1070.8 816.2 1069.9 817.2 1068.5 C 818.3 1067 819.9 1066.2 821.9 1065.7 C 824 1065.2 826.2 1065.5 828.2 1065 C 829.8 1064.6 832.8 1064.2 834.5 1065 C 836.1 1065.8 838.4 1066.9 840.1 1066.9 C 842.7 1066.9 845.4 1068.2 847.9 1068.2 C 850 1068.2 852.5 1068 854.5 1068.5 C 855.9 1068.9 859 1068.3 860.2 1069.4 C 861.4 1070.6 862.5 1071.9 864.6 1071.9 C 867.3 1071.9 867.7 1073.4 869.3 1075.1 C 870.9 1076.7 872.5 1077.4 874 1078.9 C 875.5 1080.4 875.6 1081.5 876.5 1083.3 C 876.7 1083.7 876.9 1084.1 877.1 1084.6 C 877.1 1084.6 879.6 1089.1 881.2 1090 C 882.8 1091 884.3 1098.5 884.3 1098.5 C 884.3 1098.5 889 1111.2 889.6 1112.8 C 890.2 1114.4 888.3 1118.2 888.3 1118.2 C 888.1 1118.8 888.8 1118.3 888 1119.1 C 886.3 1120.8 886 1123 887.1 1125.1 C 887.9 1126.6 889.8 1127.3 889.3 1129.5 C 888.9 1131.1 889 1132.9 889.3 1134.3 C 889.6 1136 889.9 1136.5 890.9 1137.8 C 892.1 1139.4 893.2 1140.8 894.7 1142.2 C 896.1 1143.6 896.5 1144.6 896 1147 C 895.7 1148.3 894.3 1149.3 894.7 1151.1 C 895 1152.4 894.7 1154.4 895.3 1155.5 C 895.8 1156.4 896.6 1159.9 897.2 1160.6 C 898.5 1161.9 900.1 1162.3 901.3 1164.1 C 901.7 1164.7 902.1 1165.4 902.6 1166 C 902.8 1166.6 902.9 1167.3 903.2 1167.9 C 903.9 1169.3 904 1172.2 905.1 1173.3 C 906.4 1174.7 907.7 1177.2 909.8 1177.7 C 912 1178.3 912.6 1177.7 914.8 1177.7 C 916.5 1177.7 919.2 1179.6 920.5 1179 C 921.6 1178.4 923.2 1177.6 924.9 1178 C 927 1178.5 926.8 1179.6 929.3 1179.6 C 931.5 1179.6 932.9 1178 934.6 1178 C 936.1 1178 938.8 1179 939.3 1180.9 C 939.5 1181.6 940.8 1185 942.1 1185 C 944.5 1185 946.7 1182.2 948.7 1182.2 C 952 1182.2 950.9 1181.4 953.7 1182.8 C 955.2 1183.6 956.9 1186.3 957.8 1187.9 C 958.8 1189.6 959.8 1191.6 961.2 1192.7 C 962.9 1194 966 1195.5 968.4 1194.9 C 970.3 1194.4 973.7 1191.6 975.6 1191.1 C 976.7 1190.8 981.2 1189 982.8 1188.6 C 984 1188.3 986.4 1187.8 987.5 1188.6 C 989.9 1190.4 989.5 1188.3 991.3 1187.9 C 992.7 1187.6 995.1 1187.2 996.6 1187.6 C 998.7 1188.1 999.7 1189.2 1002.2 1189.2 C 1004.2 1189.2 1006.2 1189.2 1008.2 1189.2 C 1011 1189.2 1012.8 1189.5 1015.4 1190.1 C 1017.6 1190.7 1018.6 1193 1021.3 1193 C 1023.3 1193 1024.6 1194.4 1025.7 1195.8 C 1026.8 1197.3 1028.1 1199.1 1029.5 1200.6 C 1030.6 1201.7 1033.5 1203.6 1035.4 1203.1 C 1035.8 1203 1036.2 1202.9 1036.7 1202.8 C 1038.3 1202.6 1041.1 1201.8 1042.7 1201.8 C 1043.2 1201.8 1044.5 1201.3 1046 1200.7 C 1045.5 1198 1045.1 1195.6 1044.9 1194.1 C 1044 1187 1042.2 1184.3 1036.9 1178.9 C 1031.5 1173.5 1034.2 1173.6 1028.9 1168.2 C 1023.5 1162.8 1013.7 1161.9 1014.6 1155.7 C 1015.5 1149.5 1018.2 1149.4 1018.2 1142.3 C 1018.2 1135.2 1019.1 1137 1020.9 1130.7 C 1022.7 1124.5 1018.2 1122.7 1020 1116.4 C 1021.8 1110.2 1023.6 1114.6 1021.8 1107.5 C 1020.6 1102.6 1021 1101 1021.5 1098.2 C 1018.5 1098.7 1016 1099.1 1014.7 1099.5 C 1007.6 1101.3 1001.3 1100.4 995.9 1103.1 C 990.5 1105.8 987 1104.9 979.8 1104.9 C 972.6 1104.9 972.6 1100.4 972.6 1096.9 C 972.6 1093.3 961.9 1077.3 955.7 1077.3 C 949.5 1077.3 950.3 1077.3 946.8 1080 C 943.2 1082.7 941.5 1088 936.1 1084.5 C 930.7 1080.9 925.4 1074.7 918.2 1074.7 C 911 1074.7 909.3 1062.2 909.3 1054.2 C 909.3 1046.2 905.7 1030.1 902.1 1025.6 C 898.5 1021.1 902.1 1020.2 902.1 1015.8 C 902.1 1011.3 891.4 997.1 885.2 996.2 C 879 995.3 881.6 988.1 878.9 976.5 C 876.2 964.9 866.4 970.3 861.9 976.5 C 857.4 982.7 848.5 988.1 839.6 988.1 C 830.7 988.1 815.5 972.9 814.6 966.7 C 813.7 960.5 813.7 952.4 813.7 945.3 C 813.7 938.2 816.4 923.9 809.2 923.9 C 802 923.9 790.5 932.8 784.2 936.4 C 778 940 776.2 929.3 769 926.6 C 761.3 923.4 749.7 920.7 749.7 920.7 L 749.7 920.7Z');
    Obj.attr({ 'href': '3232', 'cursor': 'pointer', 'title': 'هرمزگان', 'fill': '#ffa71e', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 397.3 814.4 C 397 814.4 396.6 814.4 396.3 814.5 C 392.3 815.2 389.9 818.8 387.7 822 C 388.4 822.1 389.1 822.3 390.1 822.3 C 392.1 822.3 393.1 823.6 394.2 825.1 C 395.2 826.5 395.8 827.2 396.7 828.9 C 397.3 830.1 398.9 832.3 398.9 834 C 398.9 835.9 398.7 837.3 398.3 839.1 C 397.8 841.1 398.3 842.2 399.2 844.2 C 400 845.9 401.8 847.2 403.6 849 C 403.8 849.3 404.5 850.9 404.9 851.2 C 406 852.3 406.5 854.6 407.7 856 C 409.2 857.5 410.3 859.9 411.8 861.4 C 413.1 862.7 413.6 864.1 414.9 865.8 C 415.6 866.8 417.3 868.2 418.4 869.3 C 419.6 870.5 421 871.8 421.8 873.4 C 422.5 874.8 423.7 876.6 424.9 877.8 C 425.6 878.5 427 880.6 428.3 881.3 C 429 881.6 433.5 883.9 433 884.8 C 432.7 885.3 432.3 885.8 432.1 886.4 C 433.4 889.1 431.7 888.9 431.2 891.2 C 430.5 893.9 432.2 893.8 432.8 895.9 C 433 896.6 433.7 899.4 432.8 900.3 C 431.8 901.3 430.6 901.8 430.6 903.8 C 430.6 905.5 430.8 907.8 432.5 908.2 C 434.1 908.6 435.7 908.2 437.2 908.2 C 439.5 908.2 442 908.3 443.8 910.1 C 445.3 910.9 447.5 911.2 448.5 913.3 C 449.9 916.1 448.4 916.3 447.6 918 C 446.5 920.2 445 919.4 443.2 921.2 C 441.5 922.9 441.9 923.1 441.9 925.6 C 441.9 928.5 443.6 928.6 444.7 931 C 445.6 932.8 447 933.8 449.1 934.8 C 450.9 935.7 454.9 936.2 456 937.6 C 457.2 939.3 457.9 940.6 458.8 942.4 C 459.5 943.9 459.7 945.1 459.7 947.2 C 459.7 949.1 459.7 951 459.7 952.9 C 459.7 954.3 459.2 957 459.7 958.3 C 459.9 958.8 460.1 959.4 460.3 959.9 C 460.4 960.1 460.5 960.3 460.6 960.5 C 461.2 961.7 463 963.1 463.4 964.6 C 463.8 966.3 463.6 968.6 465 970 C 466.6 971.6 467.7 972.7 468.8 974.8 C 469 975.2 469.2 975.6 469.4 976.1 C 470.3 978 471.8 979.7 472.8 981.8 C 473.9 984 473 984.3 472.5 986.2 C 472.2 987.5 471.8 989.8 472.2 991.3 C 472.7 993.4 473.8 993.4 475.6 994.8 C 477.1 995.9 476.8 997.2 477.8 998.6 C 479 1000.2 480.5 1001.6 481.6 1003 C 482.9 1004.7 484 1003.6 486 1004.6 C 487.6 1005.4 489.7 1007.3 491 1008.7 C 491.8 1009.5 493.5 1012.2 494.8 1012.5 C 496.9 1013 498.2 1011.9 500.1 1012.8 C 502 1013.8 505.3 1012.5 506.4 1013.1 C 508 1012.7 510.5 1012.3 512 1013.1 C 513.6 1013.9 514.9 1015.2 517 1014.7 C 518.5 1014.3 520.9 1014.1 522.6 1014.4 C 525.3 1014.9 527.2 1015.2 529.2 1017.2 C 530.7 1018.7 531.4 1019.7 533 1021.3 C 534.6 1022.9 536.4 1023.5 538 1025.1 C 539.5 1026.6 541.3 1027.2 543.3 1028.3 C 544.9 1029.1 547.3 1029.6 549.2 1029.6 C 551.7 1029.6 552.7 1030.1 554.9 1031.2 C 556.8 1032.2 557.9 1033.6 559.6 1035.3 C 560.8 1036.6 562.6 1038.2 563.7 1039.7 C 564.7 1041.1 565.3 1042.7 566.8 1043.5 C 567.5 1043.9 571.6 1045.9 570.2 1047.3 C 568.4 1049.1 566.4 1047.2 564.3 1048.3 C 563.1 1048.9 561.4 1051.5 563.7 1052.4 C 564.2 1052.6 564.7 1052.8 565.3 1053 C 565.3 1053 567.5 1054.6 570.3 1054.9 C 570.8 1055 571.2 1055 571.6 1055.2 C 573.7 1053.8 575.7 1052.7 577.5 1052.2 C 587.3 1049.5 589.1 1047.7 589.1 1047.7 C 589.1 1047.7 589.2 1047.8 589.2 1047.8 C 582.3 1039.2 567.7 1031.1 564.1 1026.3 C 558.7 1019.2 548 993.3 548 989.7 C 548 986.1 548 988.8 540.9 986.1 C 533.8 983.4 528.4 962 528.4 954 C 528.4 946 523.9 933.5 523.9 933.5 C 523.9 933.5 502.5 904.9 501.6 896.9 C 500.7 888.9 491.8 880.8 485.5 880.8 C 479.3 880.8 473 862 473 857.6 C 473 853.1 464.1 849.6 457.8 849.6 C 451.6 849.6 447.1 849.6 441.7 850.5 C 436.3 851.4 438.1 838 434.5 837.1 C 430.9 836.2 426.5 832.7 426.5 826.4 C 426.5 820.1 416.7 819.2 411.3 819.2 C 406.4 818.9 402.2 814.2 397.3 814.4 L 397.3 814.4Z');
    Obj.attr({ 'href': '3246', 'cursor': 'pointer', 'title': 'بوشهر', 'fill': '#ffa71e', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 540.4 699.1 C 538.7 699 536.2 700.4 533.6 702.9 C 529.1 707.4 524.7 707.3 521.1 711.8 C 517.5 716.3 516.7 715.4 512.2 714.5 C 507.7 713.6 505.1 714.5 507.7 718.1 C 510.4 721.7 512.2 724.4 518.4 724.4 C 524.6 724.4 522 734.2 519.3 742.2 C 516.6 750.2 511.3 757.4 511.3 763.6 C 511.3 769.8 511.3 775.2 506 775.2 C 500.6 775.2 505.1 785 504.2 790.4 C 503.3 795.8 497.9 794 489 794 C 480.1 794 473.8 781.5 468.5 781.5 C 463.1 781.5 466.7 789.5 469.4 794.9 C 472.1 800.3 469.4 798.5 470.3 802.9 C 471.2 807.4 473.9 808.3 469.4 812.7 C 464.9 817.2 456 816.3 450.6 821.6 C 445.2 827 444.3 819.8 436.3 820.7 C 433.5 821 429.9 822.9 426.4 825.2 C 426.4 825.5 426.5 825.8 426.5 826.1 C 426.5 832.3 431 835.9 434.5 836.8 C 438.1 837.7 436.3 851.1 441.7 850.2 C 447.1 849.3 451.5 849.3 457.8 849.3 C 464 849.3 473 852.9 473 857.3 C 473 861.7 479.3 880.5 485.5 880.5 C 491.7 880.5 500.7 888.5 501.6 896.6 C 502.5 904.6 523.9 933.2 523.9 933.2 C 523.9 933.2 528.4 945.7 528.4 953.7 C 528.4 961.7 533.8 983.1 540.9 985.8 C 548 988.5 548 985.8 548 989.4 C 548 993 558.7 1018.9 564.1 1026 C 567.7 1030.8 582.3 1038.9 589.2 1047.5 C 589.2 1047.5 589.2 1047.5 589.2 1047.5 C 589.5 1047.7 591.3 1048.7 595.3 1052.8 C 599.8 1057.3 602.4 1057.2 612.2 1057.2 C 622 1057.2 622 1064.4 629.2 1062.6 C 636.3 1060.8 650.6 1070.6 655.1 1067 C 659.6 1063.4 667.6 1061.6 663.1 1059 C 658.6 1056.3 667.6 1050.1 664 1047.4 C 660.4 1044.7 674.7 1037.6 686.3 1037.6 C 697.9 1037.6 697 1039.4 703.2 1038.5 C 709.4 1037.6 714.8 1034 720.2 1034.9 C 725.6 1035.8 732.7 1043 739 1038.5 C 745.2 1034 748.8 1019.8 754.2 1018 C 759.6 1016.2 756 1012.6 760.5 1012.6 C 765 1012.6 779.3 1010.8 777.5 1004.6 C 775.7 998.4 769.5 999.3 769.5 993 C 769.5 986.7 774.8 971.6 773.1 965.3 C 771.3 959.1 760.6 951 755.3 946.5 C 749.9 942 744.6 939.4 749.9 935.8 C 753.7 933.3 752.1 926.9 750.9 923.3 C 745.9 916.2 741.8 908.5 741 902.8 C 739.2 890.3 740.1 885.8 721.3 884 C 702.6 882.2 702.6 865.2 699.9 859 C 697.2 852.8 690.1 846.5 681.1 844.7 C 672.2 842.9 670.4 834.9 670.4 826.9 C 670.4 818.9 665.9 806.4 657 799.2 C 648.1 792.1 636.5 760.8 630.2 746.5 C 623.9 732.2 615 718.8 608.8 716.2 C 602.6 713.6 592.7 710.8 582.9 714.4 C 573.1 718 564.1 718.9 553.4 716.2 C 542.7 713.5 542.7 706.4 542.7 701.9 C 542.6 700.1 541.8 699.1 540.4 699.1 L 540.4 699.1Z');
    Obj.attr({ 'href': '3230', 'cursor': 'pointer', 'title': 'فارس', 'fill': '#faf000', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 850.1 675.7 C 847.7 678.2 844.8 680.8 841.6 682.4 C 834.5 686 833.6 688.6 826.4 694 C 819.3 699.4 804.1 702.9 792.5 709.2 C 780.9 715.4 784.5 734.2 784.5 744.9 C 784.5 755.6 778.3 754.7 772.9 756.5 C 767.5 758.3 751.5 753.8 741.6 756.5 C 731.8 759.2 720.2 761.8 714.8 760.1 C 709.4 758.3 708.5 757.4 705 765.5 C 701.4 773.5 696.1 771.7 698.7 786 C 701.4 800.3 705.9 800.3 705.9 809.2 C 705.9 818.1 708.6 811 712.2 817.2 C 715.8 823.4 707.7 838.6 711.3 844.9 C 714.9 851.1 710.4 851.2 705 851.2 C 703.5 851.2 700 852.1 695.7 853.3 C 697.4 855.2 698.8 857.2 699.7 859.3 C 702.4 865.5 702.4 882.5 721.1 884.3 C 739.8 886.1 739 890.6 740.8 903.1 C 741.6 908.8 745.8 916.5 750.7 923.6 C 750.2 922.1 749.7 921 749.7 921 C 749.7 921 761.3 923.7 768.5 926.3 C 775.6 929 777.4 939.7 783.7 936.1 C 789.9 932.5 801.5 923.6 808.7 923.6 C 815.8 923.6 813.2 937.9 813.2 945 C 813.2 952.1 813.2 960.2 814.1 966.4 C 815 972.6 830.1 987.8 839.1 987.8 C 848 987.8 856.9 982.4 861.4 976.2 C 865.9 970 875.7 964.6 878.4 976.2 C 881.1 987.8 878.4 995 884.7 995.9 C 891 996.8 901.6 1011.1 901.6 1015.5 C 901.6 1020 898 1020.8 901.6 1025.3 C 905.2 1029.8 908.8 1045.8 908.8 1053.9 C 908.8 1062 910.6 1074.4 917.7 1074.4 C 924.8 1074.4 930.2 1080.6 935.6 1084.2 C 941 1087.8 942.7 1082.4 946.3 1079.7 C 949.9 1077 949 1077 955.2 1077 C 961.4 1077 972.1 1093.1 972.1 1096.6 C 972.1 1100.1 972.1 1104.6 979.3 1104.6 C 986.4 1104.6 990 1105.5 995.4 1102.8 C 1000.8 1100.1 1007 1101 1014.2 1099.2 C 1015.5 1098.9 1018 1098.4 1021 1097.9 C 1021.2 1096.6 1021.3 1095.1 1021.3 1092.9 C 1021.3 1085.8 1017.7 1087.5 1014.2 1082.2 C 1010.6 1076.8 1007.9 1065.2 1007.9 1059 C 1007.9 1052.8 1015 1021.5 1015 1015.3 C 1015 1009.1 1013.2 999.2 1013.2 990.3 C 1013.2 981.4 1015.9 986.7 1018.5 978.7 C 1021.2 970.7 1023.9 967.1 1016.7 961.8 C 1009.6 956.4 1006 957.3 1014.9 952 C 1023.8 946.6 1048.8 936.8 1044.4 930.6 C 1039.9 924.4 1039 925.2 1039 919.9 C 1039 914.6 1034.5 911 1039 903.8 C 1043.5 896.7 1043.5 893.1 1043.5 882.4 C 1043.5 871.7 1046.2 864.5 1039.9 860.1 C 1033.7 855.6 1032.8 857.4 1033.6 851.2 C 1034.5 845 1041.6 801.2 1043.4 794.1 C 1044.3 790.5 1044.5 788.2 1045.2 786.5 C 1042.4 784.9 1039.6 782.4 1036.3 778.1 C 1028.7 768 1021.2 745.3 1013.6 740.2 C 1006.8 735.6 883.7 692.1 850.1 675.7 L 850.1 675.7Z M 751.7 932.9 C 751.7 933 751.6 933.2 751.5 933.3 C 751.6 933.2 751.7 933.1 751.7 932.9Z M 1020.3 1100.2 C 1020.3 1100.6 1020.2 1100.9 1020.2 1101.3 C 1020.2 1100.9 1020.3 1100.5 1020.3 1100.2Z M 1020.3 1103.3 C 1020.3 1103.7 1020.4 1104.2 1020.5 1104.7 C 1020.3 1104.2 1020.3 1103.7 1020.3 1103.3Z M 1020.5 1105 C 1020.6 1105.7 1020.8 1106.4 1021 1107.2 C 1020.8 1106.4 1020.6 1105.7 1020.5 1105Z');
    Obj.attr({ 'href': '3229', 'cursor': 'pointer', 'title': 'کرمان', 'fill': '#faf000', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 783.8 259.3 C 783.3 259.3 782.9 259.4 782.6 259.4 C 778.1 259.4 771.9 276.3 770.1 282.6 C 768.3 288.8 766.5 294.2 766.5 302.3 C 766.5 310.4 741.5 301.4 735.2 302.3 C 729 303.2 711.1 321 708.4 326.4 C 705.7 331.8 685.2 328.2 678.9 328.2 C 672.7 328.2 665.5 337.1 663.7 341.6 C 661.9 346.1 661.9 351.4 656.5 357.7 C 651.1 363.9 648.5 364.8 643.1 370.2 C 637.7 375.6 635 373.8 629.7 373.8 C 624.3 373.8 611 378.3 611 378.3 C 611 378.3 611 378.3 613.7 385.5 C 616.4 392.6 615.5 398.9 609.3 405.1 C 603.1 411.3 588.7 415.8 583.4 415.8 C 578 415.8 565.6 409.5 557.5 409.5 C 549.5 409.5 540.6 403.3 537.9 399.7 C 535.2 396.1 534.3 399.7 529.9 399.7 C 525.4 399.7 527.2 404.2 527.2 413.1 C 527.2 422 530.8 422.9 537 428.3 C 543.2 433.7 538.8 433.7 535.2 439 C 531.6 444.4 528.1 443.5 530.7 448.8 C 533.4 454.2 527.1 456 528.9 464 C 530.7 472 522.6 475.6 522.6 475.6 C 522.6 475.6 531.5 482.8 537.8 484.5 C 544 486.3 555.6 483.6 561.9 483.6 C 568.1 483.6 588.7 493.4 594 497 C 599.4 500.6 757.3 493.5 762.7 494.3 C 762.8 494.3 763 494.4 763.2 494.5 C 763.2 494.5 763.2 494.2 763.2 494.2 C 763.2 494.2 789.7 498 794.7 491.7 C 799.7 485.4 826.3 437.4 838.9 429.9 C 851.5 422.3 859.1 421.1 859.1 414.7 C 859.1 408.3 873 397 873 392 C 873 387 852.8 373.1 850.3 368 C 847.8 363 838.9 339 841.5 333.9 C 844 328.9 852.9 318.8 845.3 316.2 C 837.7 313.7 833.9 318.7 828.9 316.2 C 823.9 313.7 817.6 317.5 811.2 309.9 C 804.9 302.3 798.6 302.3 799.8 296 C 801.1 289.7 803.6 288.4 799.8 283.4 C 796 278.4 801.1 275.8 796 275.8 C 791.4 275.6 784 259.6 783.8 259.3Z');
    Obj.attr({ 'href': '3252', 'cursor': 'pointer', 'title': 'سمنان', 'fill': '#C28D06', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 780.5 193.3 C 777.4 193.3 776.4 194.6 773.4 194.6 C 769.6 194.6 767.2 194.5 763.6 195.9 C 762.3 196.2 761.9 196.2 760.5 196.8 C 756.9 198.2 755.7 201.7 752.5 203 C 750.2 203.9 745.8 207.4 742.7 207.4 C 738.5 207.4 737.8 209.3 735.6 212.3 C 733.9 214.5 730.9 216.7 728.5 218.5 C 727.2 219.5 725.3 222.1 723.6 222.5 C 719.8 223.4 721.3 225.7 719.2 227.8 C 717.4 229.6 717.4 230.9 717.4 233.6 C 717.4 236.5 717.1 238 716.5 240.7 C 715.8 244 715.6 244.8 713.4 246.9 C 712.4 247.9 709.8 251.5 707.2 250.5 C 703.9 249.2 701.7 248.4 698.7 249.6 C 696.8 250.3 694.9 251.2 693.4 252.7 C 691.3 254.8 689.8 255 686.7 255.8 C 684.6 256.3 683 259 680.9 258.5 C 679.2 258.1 676.3 257.2 674.7 257.2 C 672.2 257.2 669.8 256.8 667.6 256.8 C 664.7 256.8 661.4 256.6 660 256.4 C 658.7 256.2 658.9 256.3 659.5 257.5 C 660.1 258.7 660.8 260.9 660.8 263.2 C 660.8 265 661.7 266.3 662.1 267.9 C 662.5 269.4 662.9 270.8 663.4 272.6 C 663.9 274.7 664.2 275.5 665 277 C 665.6 278.3 665.7 281 665.9 282.3 C 666.2 284 666.8 285.2 666.8 287 C 666.8 289 666.9 290 667.8 291.7 C 668.5 293.1 668.1 295.4 667.8 296.7 C 667.3 298.6 665.9 298.3 664 299.2 C 663.8 299.3 663.5 299.4 663.2 299.5 C 662.2 301.7 661.3 304 661.3 305.8 C 661.3 310.3 667.5 311.2 672.9 313.8 C 678.3 316.5 681 322.7 684.5 322.7 C 687.4 322.7 686.2 324.4 689.3 328.8 C 697.4 329.4 706.8 329.8 708.6 326.3 C 711.3 320.9 729.1 303.1 735.4 302.2 C 741.6 301.3 766.7 310.2 766.7 302.2 C 766.7 294.2 768.5 288.8 770.3 282.5 C 772.1 276.3 778.3 259.3 782.8 259.3 C 783 259.3 783.5 259.3 783.9 259.2 C 783.9 259.2 783.9 259.1 783.9 259.1 C 783.9 259.1 790.2 245.2 799.1 242.7 C 807.9 240.2 818 233.9 811.7 231.4 C 805.4 228.9 805.4 230.1 805.4 223.8 C 805.4 217.5 807.9 208.7 807.9 203.6 C 807.9 202.6 807.8 200 807.7 197.4 C 805.4 196.7 803.9 195.6 801 196.3 C 798.8 196.8 796.4 197.2 793.4 197.2 C 790.9 197.2 789 197.2 786.7 196.3 C 785.2 195.8 782.5 193.3 780.5 193.3 L 780.5 193.3Z');
    Obj.attr({ 'href': '3236', 'cursor': 'pointer', 'title': 'گلستان', 'fill': '#89cb2b', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 524.3 476.4 C 522.5 477.7 519.2 479.8 516.7 479.8 C 513.1 479.8 481 481.6 476.5 482.5 C 472 483.4 469.4 496.8 468.5 501.2 C 467.6 505.7 468.5 506.6 472.1 511.9 C 475.7 517.3 464.1 519.9 464.1 525.3 C 464.1 530.7 458.7 531.6 458.7 531.6 C 458.7 531.6 452.5 536.1 447.1 537 C 441.7 537.9 405.2 548.6 403.4 553.1 C 401.6 557.6 396.2 559.3 400.7 561.1 C 405.2 562.9 397.1 567.3 397.1 572.7 C 397.1 578.1 390 585.2 386.4 585.2 C 382.8 585.2 381.9 583.4 376.6 588.8 C 371.2 594.2 372.1 592.4 374.8 596.8 C 377.5 601.3 369.4 601.3 365.9 602.1 C 362.3 603 361.4 610.1 364.1 614.6 C 366.8 619.1 371.2 616.4 376.6 616.4 C 382 616.4 382 622.7 381.1 628 C 380.2 633.4 386.4 628.9 386.4 628.9 C 386.4 628.9 398 625.3 404.2 625.3 C 410.4 625.3 407.8 628 414 631.6 C 420.2 635.2 421.1 631.6 425.6 631.6 C 430.1 631.6 436.3 626.3 441.7 628 C 447.1 629.8 448.8 624.4 453.3 625.3 C 457.8 626.2 454.2 630.6 460.4 636.9 C 466.6 643.1 463.1 647.6 464 653 C 464.9 658.4 476.5 664.6 480.1 668.2 C 483.7 671.8 484.5 678.9 484.5 685.1 C 484.5 691.3 476.5 701.2 476.5 705.6 C 476.5 710 481.9 727 482.8 730.6 C 483.7 734.2 482.8 743.1 476.5 744 C 470.3 744.9 472.9 752 477.4 756.5 C 481.9 761 489 765.4 497 769.9 C 498.7 770.9 502.8 773.2 506.5 775.2 C 511.2 774.8 511.3 769.6 511.3 763.6 C 511.3 757.4 516.7 750.2 519.3 742.2 C 522 734.2 524.7 724.4 518.4 724.4 C 512.2 724.4 510.4 721.7 507.7 718.1 C 505 714.5 507.7 713.6 512.2 714.5 C 516.7 715.4 517.5 716.3 521.1 711.8 C 524.7 707.3 529.1 707.4 533.6 702.9 C 536.1 700.4 538.6 699 540.4 699.1 C 541.7 699.2 542.6 700.1 542.6 702 C 542.6 706.5 542.6 713.6 553.3 716.3 C 564 719 573 718.1 582.8 714.5 C 585.1 713.7 587.5 713.2 589.7 712.9 C 589.3 708.8 589 705.1 589 702.9 C 589 694.9 592.6 677.9 594.3 667.2 C 596.1 656.5 589.9 642.2 595.2 638.6 C 600.6 635 601.4 636.8 605 632.3 C 608.6 627.8 621.9 625.2 630 626 C 638 626.9 645.2 632.3 658.6 628.7 C 672 625.1 680 615.3 688.9 614.4 C 697.8 613.5 719.3 610.9 724.6 603.7 C 730 596.6 731.7 581.4 738 574.2 C 744.2 567.1 746.9 570.6 752.3 567.9 C 752.7 567.7 753.5 567.4 754.5 567.1 C 754.2 566.9 752.2 565.7 752.2 565.7 C 752.2 565.7 762.3 563.2 762.3 539.2 C 762.3 515.6 763.5 494.7 763.6 494 C 763.4 493.9 763.2 493.9 763.1 493.8 C 757.7 492.9 599.8 500 594.4 496.5 C 589 492.9 568.5 483.1 562.3 483.1 C 556.1 483.1 544.5 485.8 538.2 484 C 533.3 482.9 527 478.4 524.3 476.4Z M 597.1 713.1 C 597.4 713.1 597.8 713.2 598.1 713.3 C 597.8 713.2 597.5 713.1 597.1 713.1Z M 603.3 714.4 C 604.1 714.6 604.8 714.9 605.6 715.1 C 604.9 714.9 604.1 714.6 603.3 714.4Z');
    Obj.attr({ 'href': '3233', 'cursor': 'pointer', 'title': 'اصفهان', 'fill': '#C28D06', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 452.6 625.2 C 448.7 625 446.8 629.7 441.8 628 C 436.4 626.2 430.2 631.6 425.7 631.6 C 421.2 631.6 420.4 635.2 414.1 631.6 C 407.9 628 410.5 625.3 404.3 625.3 C 398.1 625.3 386.5 628.9 386.5 628.9 C 386.5 628.9 383.7 630.9 382.1 630.6 L 381.1 631.6 C 381.1 631.6 378.4 634.3 380.2 640.5 C 382 646.7 382.9 656.6 390 660.2 C 397.1 663.8 387.3 662 395.4 666.5 C 403.4 671 407 678.1 407 685.2 C 407 692.3 412.3 700.4 415.9 702.2 C 419.5 704 415.9 711.1 415.9 711.1 C 415.9 711.1 408.8 716.4 416.8 720 C 424.8 723.6 427.5 723.6 432.9 724.5 C 438.3 725.4 445.4 738.8 450.7 744.1 C 456.1 749.5 458.8 754.8 464.1 752.1 C 468.2 750 469.7 749.1 473 748.7 C 472.7 746.4 473.6 744.5 476.6 744.1 C 482.8 743.2 483.7 734.3 482.9 730.7 C 482 727.1 476.6 710.2 476.6 705.7 C 476.6 701.2 484.6 691.4 484.6 685.2 C 484.6 679 483.7 671.8 480.2 668.3 C 476.6 664.7 465 658.5 464.1 653.1 C 463.2 647.7 466.8 643.3 460.5 637 C 454.3 630.8 457.8 626.3 453.4 625.4 C 453.1 625.2 452.8 625.2 452.6 625.2 L 452.6 625.2Z');
    Obj.attr({ 'href': '3242', 'cursor': 'pointer', 'title': 'چهارمحال و بختیاری', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 413.5 717.4 C 411.7 718.2 410.3 719.1 409.7 719.9 C 406.1 724.4 405.2 727 399.9 727.9 C 394.5 728.8 386.5 735.9 386.5 742.2 C 386.5 748.5 394.5 757.4 399 759.2 C 403.5 761 396.3 771.7 407.9 775.3 C 419.5 778.9 422.2 781.6 418.6 787.8 C 415 794 411.5 799.4 415 802.1 C 416.6 803.3 417.3 811.2 417.6 819.5 C 421.8 820.2 425.9 821.7 426.5 825.3 C 430 823 433.6 821.1 436.4 820.8 C 444.4 819.9 445.3 827.1 450.7 821.7 C 456.1 816.3 465 817.2 469.5 812.8 C 474 808.3 471.3 807.4 470.4 803 C 469.5 798.5 472.2 800.3 469.5 795 C 466.8 789.6 463.2 781.6 468.6 781.6 C 474 781.6 480.2 794.1 489.1 794.1 C 498 794.1 503.4 795.9 504.3 790.5 C 505.2 785.1 500.7 775.3 506.1 775.3 C 506.3 775.3 506.5 775.3 506.7 775.3 C 503 773.2 498.9 770.9 497.2 770 C 489.2 765.5 482 761.1 477.6 756.6 C 475.3 754.3 473.5 751.2 473.1 748.7 C 469.8 749.1 468.3 750.1 464.2 752.1 C 458.8 754.8 456.1 749.4 450.8 744.1 C 445.4 738.7 438.3 725.4 433 724.5 C 427.6 723.6 425 723.6 416.9 720 C 415 719.1 414 718.2 413.5 717.4 L 413.5 717.4Z M 475.5 744.2 C 475.2 744.3 474.9 744.4 474.6 744.6 C 474.9 744.4 475.2 744.3 475.5 744.2Z M 511 770.7 C 510.9 771 510.9 771.3 510.8 771.5 C 510.9 771.2 510.9 771 511 770.7Z M 510.5 772.3 C 510.4 772.5 510.3 772.7 510.2 772.9 C 510.4 772.6 510.5 772.4 510.5 772.3Z M 509.8 773.6 C 509.7 773.7 509.6 773.8 509.5 773.9 C 509.6 773.9 509.7 773.7 509.8 773.6Z M 507.3 775.1 C 507.1 775.1 507 775.2 506.8 775.2 C 506.9 775.2 507.1 775.1 507.3 775.1Z');
    Obj.attr({ 'href': '3247', 'cursor': 'pointer', 'title': 'کهکیلویه و بویراحمد', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 305.1 587.7 C 301.4 587.7 297 588 293.7 589.6 C 288.3 592.3 282.1 587.8 276.7 587.8 C 271.3 587.8 269.6 603.9 262.4 612.8 C 255.2 621.7 245.5 637.8 242.8 643.1 C 240.1 648.5 246.4 652 241 656.5 C 239.8 657.5 236.6 659.5 232.5 661.8 C 233.4 662.7 233.7 663.7 234.2 665.4 C 234.9 668.3 236.6 666.9 237.3 669.9 C 238.1 673.1 240.1 673.4 240.9 676.6 C 241.2 677.9 243.1 680.9 243.6 681.9 C 244.8 684.4 247 683.1 247.6 686.4 C 247.8 687.4 247.8 687.5 248 688.6 L 236.9 716.1 C 236.9 716.1 234.7 737 235.1 739.2 C 235.5 741.4 234.7 751.2 234.7 751.2 L 256.9 752.1 L 254.6 788.6 C 254.6 789.5 254.3 790.4 254.6 791.2 C 256.6 796.3 258.5 792.9 262.1 795.6 C 264.4 797.3 264.3 798.1 264.3 801.4 C 264.3 805.5 265.7 806.8 269.2 808.5 C 273.8 810.8 272 810.9 274.5 814.3 C 275.6 815.8 276.7 817.6 276.7 819.6 C 276.7 822.1 276.6 823.9 278 825.8 C 279.5 827.8 278.2 829.8 279.3 832 C 281.2 835.9 282.1 833.3 285.1 835.5 C 288.1 837.7 286.9 835.9 287.2 835.8 C 287.5 835.7 288 835.4 288.5 835.5 C 289.6 835.8 290.2 836.3 291.2 836.8 C 291.8 837 293.1 837.4 293.9 837.1 C 294.9 836.7 295.3 836.3 296 835.7 C 296.5 835.2 297 834.9 297.4 834.1 C 297.8 833.3 298 832.8 298 831.7 C 298 830.5 297.7 830.2 297.2 829.2 C 297 828.9 297 826.6 297.5 827.3 C 298 827.9 298.7 828.8 299.4 829.3 C 300.2 829.9 300.8 830.9 301.6 831.3 C 302.5 831.7 303.8 832.7 304.6 832.9 C 305.2 833 306.9 833 307.6 833.4 C 308.9 834.2 309.9 834.4 311.2 834 C 312.3 833.7 312.9 833.4 313.9 832.9 C 314.4 832.6 315.8 832.5 316.3 832 C 316.7 831.6 317.8 830.8 318.3 830.6 C 319 830.2 319.4 829.8 319.9 829.2 C 320.3 828.7 321.1 827.7 320.7 826.7 C 320.4 825.9 320.1 825.2 320.4 824.2 C 320.5 823.6 320.8 822.8 321 822.2 C 321.3 821.5 321.3 820.8 321.3 819.9 C 321.3 818.9 321.3 818 321.1 817.2 C 320.9 816.2 320.8 815.7 320.5 815 C 320.2 814.4 319.6 813 319.1 812.5 C 318.6 812 317.3 811 316.9 810.3 C 316.8 810.2 316.8 810.1 316.7 810 C 316.4 809.7 315.3 808.7 315.4 808 C 315.6 807.4 315.5 805.8 316.3 805.6 C 317 805.4 318.2 804.6 318.2 803.9 C 318.2 803.1 318 802.1 318.2 801.2 C 318.4 800.4 318.7 799.4 319.5 799 C 320 798.8 321.2 797.5 321.6 797.6 C 322.6 797.8 323.6 798 324.3 798.9 C 324.6 799.3 325.4 800.9 326 800.9 C 327.1 800.9 327.7 800.7 328.7 800.7 C 330 800.7 330.5 800.4 331.4 799.6 C 331.9 799.1 332.5 798.6 332.5 797.7 C 332.5 796.8 332.5 795.8 333.3 795.4 C 333.7 795.2 335.3 794.2 335.6 794.6 C 336.3 795.3 336.8 795.8 337 796.8 C 337.2 797.8 337.5 798.5 337.8 799.5 C 338 800.3 338.2 801.3 337.5 802 C 337.1 802.4 336.2 803.6 335.5 803.6 C 334.9 803.6 333.1 803.3 332.7 803.8 C 332.2 804.3 332 804.9 330.8 804.9 C 329.8 804.9 328.8 804.7 327.8 804.7 C 326.8 804.7 326.3 804.8 325.4 805.2 C 324.8 805.5 323.3 806.1 322.9 806.6 C 322.3 807.4 321.6 808.3 321.6 809.3 C 321.6 810.2 321.7 810.9 322.2 811.5 C 322.7 812 324 812.8 324.4 813.6 C 324.7 814.1 325.7 815.2 326.3 815.3 C 326.5 815.4 326.7 815.4 327 815.5 C 327.9 815.7 329.2 816 329.7 816.5 C 331.8 818.7 331.8 816.5 334.1 816.5 C 336.4 816.5 335.7 818.5 335.7 820.6 C 335.7 822.2 335.8 825 338.2 825 C 340.7 825.6 341 824 343.2 824 C 345 824 350.2 824 351 825.6 C 351.7 827 352.2 828.3 352.9 829.7 C 353.6 831.1 354.1 833.1 354.8 834.5 C 355.6 836.2 358.3 838.9 360.8 837.7 C 362.4 836.9 362.8 836.1 364.6 835.2 C 365.9 834.5 368.9 832.8 369.9 831.4 C 371 829.9 372.1 827.7 373.7 827 C 375.5 826.1 377.5 826.3 379 825.1 C 381.6 823.1 381.7 822.9 385.3 822.2 C 386.4 822 387.1 822.1 387.9 822.3 C 390 819 392.5 815.4 396.5 814.8 C 396.8 814.7 397.2 814.7 397.5 814.7 C 402.4 814.6 406.7 819.3 411.7 819.3 C 413.4 819.3 415.7 819.4 417.9 819.7 C 417.6 811.4 416.9 803.5 415.3 802.3 C 411.7 799.6 415.3 794.3 418.9 788 C 422.5 781.8 419.8 779.1 408.2 775.5 C 396.6 771.9 403.8 761.2 399.3 759.4 C 394.8 757.6 386.8 748.7 386.8 742.4 C 386.8 736.1 394.8 729 400.2 728.1 C 405.6 727.2 406.4 724.5 410 720.1 C 410.7 719.3 412 718.4 413.8 717.6 C 411.8 714.5 416.2 711.2 416.2 711.2 C 416.2 711.2 419.8 704 416.2 702.3 C 412.6 700.5 407.3 692.5 407.3 685.3 C 407.3 678.1 403.7 671 395.7 666.6 C 387.7 662.1 397.5 663.9 390.3 660.3 C 383.2 656.7 382.3 646.9 380.5 640.6 C 378.7 634.4 381.4 631.7 381.4 631.7 L 382.4 630.7 C 381.6 630.6 381.1 629.9 381.4 628.1 C 382.3 622.7 382.3 616.5 376.9 616.5 C 371.5 616.5 367.1 619.2 364.4 614.7 C 363.3 612.9 362.8 610.7 362.8 608.6 C 359.2 608.5 355.9 608.4 353.6 608.4 C 345.6 608.4 344.7 606.6 341.1 601.3 C 337.5 595.9 326.8 594.1 321.5 594.1 C 316.1 594.1 314.4 587.8 310.8 587.8 C 309.2 587.8 307.3 587.7 305.1 587.7Z');
    Obj.attr({ 'href': '3243', 'cursor': 'pointer', 'title': 'خوزستان', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 253.5 479.8 C 249 479.8 245.5 481.6 245.5 481.6 C 245.5 481.6 247.3 491.4 242.8 492.3 C 238.3 493.2 231.2 496.7 231.2 496.7 C 231.2 496.7 232.1 501.2 234.8 504.7 C 237.5 508.3 236.6 511.8 233 517.2 C 229.4 522.6 226.7 526.1 219.6 526.1 C 212.5 526.1 200.9 529.7 197.3 533.2 C 193.7 536.8 189.3 537.7 196.4 548.4 C 203.5 559.1 207.1 562.7 217.8 562.7 C 228.5 562.7 237.5 581.4 245.5 586.8 C 253.3 592 254.3 608.3 257.7 619.1 C 259.3 616.8 260.9 614.6 262.4 612.7 C 269.5 603.8 271.3 587.7 276.7 587.7 C 282.1 587.7 288.3 592.2 293.7 589.5 C 297 587.8 301.4 587.5 305.1 587.6 C 307.3 587.6 309.3 587.7 310.6 587.7 C 314.2 587.7 316 594 321.3 594 C 326.7 594 337.4 595.8 340.9 601.2 C 344.5 606.6 345.4 608.3 353.4 608.3 C 355.6 608.3 358.9 608.4 362.6 608.5 C 362.6 605.4 363.8 602.6 365.9 602.1 C 369.5 601.2 377.5 601.2 374.8 596.8 C 372.1 592.3 371.2 594.1 376.6 588.8 C 382 583.4 382.8 585.2 386.4 585.2 C 390 585.2 397.1 578.1 397.1 572.7 C 397.1 567.3 405.1 562.9 400.7 561.1 C 398.3 560.1 398.8 559.1 400 557.8 C 399.6 557.6 399.3 557.5 398.9 557.5 C 393.5 557.5 397.1 553 397.1 549.5 C 397.1 546 393.5 540.6 388.2 545.9 C 382.8 551.3 387.3 541.5 379.3 543.2 C 371.3 545 373.1 537.8 374 532.5 C 374.9 527.1 371.3 522.7 365.1 522.7 C 358.9 522.7 356.1 525.4 355.3 530.7 C 354.4 536.1 349.1 533.4 346.4 537 C 343.7 540.6 337.5 534.3 333 534.3 C 328.5 534.3 333.9 527.1 333 519.1 C 332.1 511.1 325 511.1 321.4 505.7 C 317.8 500.3 314.3 503.9 310.7 503 C 307.1 502.1 306.3 504.8 298.2 504.8 C 290.1 504.8 286.6 503 281.2 501.2 C 275.8 499.4 273.2 492.3 266.9 492.3 C 260.6 492.3 257.9 479.8 253.5 479.8Z');
    Obj.attr({ 'href': '3244', 'cursor': 'pointer', 'title': 'لرستان', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 124.9 498.6 C 119.5 498.6 124 503.1 124 506.6 C 124 510.1 118.6 523.5 116.8 528 C 116.6 528.5 116.3 529.8 115.9 531.5 C 116 531.6 116.1 531.6 116.3 531.7 C 119.2 533.1 113.9 536 118.5 534.8 C 121.4 534.1 123.4 533.4 126.1 534.8 C 128.2 535.9 127.2 540 126.1 541.5 C 124 544.3 127.2 544.6 130.1 544.6 C 133 544.6 133.7 548 133.7 550.4 C 133.7 553.8 135.8 554.7 136.8 556.6 C 137.3 557.6 137.8 558.5 138.6 559.3 C 140.3 562.7 143.9 561.9 143.9 566 C 143.9 568.9 145.1 570.6 143 572.7 C 141.7 574 137.1 574.1 135 574.5 C 131.6 575.2 136.6 578.7 137.7 578.9 C 140.2 579.5 137.2 584.9 136.8 585.6 C 135.7 587.8 139.6 589.2 141.2 589.2 C 144.3 589.2 147.4 589.2 150.5 589.2 C 153.1 589.2 156.7 590.5 158.5 592.3 C 159.6 593.4 163.2 594.7 164.7 595.4 C 166.5 596.3 168.7 598.9 170 600.7 C 171.8 603.1 173.5 605.1 176.2 606.5 C 177 606.9 180.3 609.7 181.5 610.9 C 183 612.4 184.3 613.3 185.9 614.9 C 187.7 616.7 189 617.2 190.3 619.8 C 191.4 621.9 194 624.4 195.6 626 C 196 626.7 196.5 627.5 196.9 628.2 C 198 630 201.2 630.1 202.2 632.2 C 203.2 634.1 207.6 632.7 209.3 632.7 C 211.9 632.7 213.6 630.5 216.9 630.5 C 218.6 630.5 219.2 634.5 220 635.4 C 222 637.4 222.7 635.5 222.7 639.8 C 222.7 642.3 220.1 642.9 222.7 645.6 C 224.1 647 223.9 648.7 225.8 649.6 C 228.8 651.1 227.3 654.5 226.7 655.8 C 225 659.1 228.3 659.7 230.3 660.7 C 231.1 661.1 231.7 661.5 232.2 662 C 236.3 659.7 239.5 657.7 240.7 656.7 C 246.1 652.2 239.8 648.7 242.5 643.3 C 244.6 639.1 251.2 628.1 257.4 619.3 C 254 608.6 253 592.3 245.2 587 C 237.2 581.6 228.2 562.9 217.5 562.9 C 206.8 562.9 203.2 559.3 196.1 548.6 C 189 537.9 193.4 537 197 533.4 C 200.6 529.8 212.2 526.3 219.3 526.3 C 226.4 526.3 229.1 522.7 232.7 517.4 C 236.3 512 237.2 508.5 234.5 504.9 C 233.8 504 233.3 503 232.8 502.1 C 228.8 503.3 224.6 504.5 222.9 504.9 C 219.3 505.8 214 509.4 219.3 511.2 C 224.7 513 221.1 517.5 216.6 517.5 C 212.1 517.5 210.3 515.7 205.9 517.5 C 201.4 519.3 194.3 522 188.9 522 C 183.5 522 180.9 522.9 175.5 519.3 C 170.1 515.7 166.6 507.7 161.2 506.8 C 155.8 505.9 151.4 506.8 147.8 504.1 C 144.2 501.4 143.3 499.6 138.9 499.6 C 134.8 499.4 130.3 498.6 124.9 498.6Z M 233.4 663.2 C 233.5 663.4 233.5 663.5 233.6 663.7 C 233.6 663.5 233.5 663.3 233.4 663.2Z');
    Obj.attr({ 'href': '3250', 'cursor': 'pointer', 'title': 'ایلام', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 423 382.5 C 423 382.5 415.8 384.3 409.6 387 C 403.4 389.7 384.6 391.4 382.8 395.9 C 381 400.4 378.3 399.5 378.3 399.5 C 378.3 399.5 376.5 399.5 373 399.5 C 369.4 399.5 367.6 399.5 370.3 403.9 C 373 408.4 370.3 410.2 365.8 408.4 C 361.3 406.6 361.3 406.6 361.3 413.8 C 361.3 421 364.9 417.4 366.7 420.9 C 368.5 424.5 363.1 427.1 359.5 426.2 C 355.9 425.3 353.3 427.1 357.7 430.7 C 362.2 434.3 363.9 431.6 369.3 435.2 C 374.7 438.8 374.7 444.1 375.6 448.6 C 376.5 453.1 375.6 456.6 371.1 457.5 C 366.6 458.4 370.2 454.8 365.8 448.6 C 361.3 442.4 365.8 440.6 362.2 440.6 C 358.6 440.6 350.6 438.8 346.1 438.8 C 341.6 438.8 348.8 448.6 344.3 451.3 C 339.8 454 340.7 457.5 340.7 461.1 C 340.7 464.7 338.9 462 332.7 462 C 326.5 462 328.2 463.8 332.7 467.3 C 337.2 470.9 337.2 468.2 336.3 474.4 C 335.4 480.6 336.3 480.7 339 484.2 C 341.7 487.8 343.5 497.6 343.5 497.6 C 343.5 497.6 342.6 504.7 339 509.2 C 338.1 510.4 335.4 512.9 331.9 515.9 C 332.3 516.8 332.6 517.8 332.7 519 C 333.6 527 328.2 534.2 332.7 534.2 C 337.2 534.2 343.4 540.5 346.1 536.9 C 348.8 533.3 354.1 536 355 530.6 C 355.9 525.2 358.6 522.6 364.8 522.6 C 371 522.6 374.6 527.1 373.7 532.4 C 372.8 537.8 371 544.9 379 543.1 C 387 541.3 382.6 551.1 387.9 545.8 C 393.3 540.4 396.8 545.8 396.8 549.4 C 396.8 553 393.2 557.4 398.6 557.4 C 398.9 557.4 399.3 557.5 399.7 557.7 C 400.7 556.5 402.3 555.1 403.1 553 C 404.9 548.5 441.5 537.8 446.8 536.9 C 452.2 536 458.4 531.5 458.4 531.5 C 458.4 531.5 463.8 530.6 463.8 525.2 C 463.8 519.8 475.4 517.2 471.8 511.8 C 469.1 507.8 468 506.3 467.9 503.9 C 462.9 504.2 455.7 504.8 450.4 506.4 C 441.5 509.1 442.4 503.7 437.9 503.7 C 433.4 503.7 432.6 491.2 427.2 492.1 C 421.8 493 417.4 498.4 412 498.4 C 406.6 498.4 409.3 489.5 404.9 482.3 C 400.4 475.2 405.8 475.1 408.5 468.9 C 411.2 462.7 412.1 460.9 413.8 452.8 C 415.6 444.8 419.1 449.2 425.4 446.5 C 431.6 443.8 440.6 442 445.9 442 C 451.3 442 453.1 430.4 454.8 423.3 C 456.6 416.2 464.6 405.4 464.6 400.1 C 464.6 394.7 457.5 390.3 457.5 390.3 C 457.5 390.3 453.9 386.7 450.3 390.3 C 446.7 393.9 448.5 387.6 442.3 387.6 C 436.1 387.6 435.2 386.7 430.7 386.7 C 426.6 387 423 382.5 423 382.5Z');
    Obj.attr({ 'href': '3239', 'cursor': 'pointer', 'title': 'مرکزی', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 455.9 421.3 C 455.6 422.1 455.3 422.9 455.1 423.6 C 453.3 430.7 451.5 442.3 446.2 442.3 C 440.8 442.3 431.9 444.1 425.7 446.8 C 419.5 449.5 415.9 445 414.1 453.1 C 412.3 461.1 411.4 462.9 408.8 469.2 C 406.1 475.4 400.8 475.5 405.2 482.6 C 409.7 489.7 407 498.7 412.3 498.7 C 417.7 498.7 422.1 493.3 427.5 492.4 C 432.9 491.5 433.7 504 438.2 504 C 442.7 504 441.8 509.4 450.7 506.7 C 456 505.1 463.3 504.4 468.2 504.2 C 468.2 503.4 468.3 502.5 468.5 501.4 C 469.4 496.9 472.1 483.6 476.5 482.7 C 481 481.8 513.1 480 516.7 480 C 519.2 480 522.5 477.9 524.3 476.6 C 523.5 476 523 475.6 523 475.6 C 523 475.6 531 472 529.3 464 C 527.5 456 533.8 454.2 531.1 448.8 C 530 446.6 530 445.4 530.6 444.5 C 530.5 444.5 530.4 444.4 530.3 444.4 C 520.5 441.7 519.6 436.3 512.5 436.3 C 505.4 436.3 477.7 422 470.5 422 C 468.3 421.8 462.7 421.6 455.9 421.3 L 455.9 421.3Z M 468.3 504.7 C 468.3 504.9 468.4 505 468.4 505.2 C 468.3 505 468.3 504.8 468.3 504.7Z M 469.2 507.4 C 469.4 507.8 469.7 508.2 470 508.7 C 469.7 508.2 469.4 507.8 469.2 507.4Z');
    Obj.attr({ 'href': '3238', 'cursor': 'pointer', 'title': 'قم', 'fill': '#C28D06', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 439.7 331.7 C 435.4 331.8 434.9 332.5 436.4 338 C 438.2 344.2 445.3 342.5 448.9 347.8 C 452.5 353.2 445.3 354.1 441.8 363 C 438.2 371.9 427.5 370.1 423 371.9 C 419.9 373.1 420.3 379.4 420.7 383.2 C 422 382.8 423 382.6 423 382.6 C 423 382.6 426.6 387.1 431 387.1 C 435.5 387.1 436.3 388 442.6 388 C 448.8 388 447.1 394.3 450.6 390.7 C 454.2 387.1 457.8 390.7 457.8 390.7 C 457.8 390.7 464.9 395.2 464.9 400.5 C 464.9 405.3 458.5 414.4 455.8 421.4 C 462.6 421.7 468.2 421.9 470.2 421.9 C 477.3 421.9 505 436.2 512.2 436.2 C 519.3 436.2 520.2 441.6 530 444.3 C 530.1 444.3 530.2 444.4 530.3 444.4 C 531.1 443 533.2 442.1 535.3 438.9 C 538.9 433.5 543.3 433.5 537.1 428.2 C 530.9 422.8 527.3 422 527.3 413 C 527.3 404 525.5 399.6 530 399.6 C 534.5 399.6 535.4 396 538 399.6 C 540.7 403.2 549.6 409.4 557.6 409.4 C 565.6 409.4 578.1 415.7 583.5 415.7 C 588.9 415.7 603.2 411.2 609.4 405 C 615.6 398.8 616.5 392.5 613.8 385.4 C 611.1 378.3 611.1 378.2 611.1 378.2 C 611.1 378.2 611.9 377.9 612.7 377.7 C 607.9 374.2 603.7 371.2 602.2 370.2 C 596.8 366.6 598.6 370.2 594.2 375.5 C 589.7 380.9 587.1 370.2 579.9 370.2 C 572.8 370.2 574.5 362.2 567.4 366.6 C 560.3 371.1 560.3 370.2 557.6 376.4 C 554.9 382.6 547.8 378.2 543.3 377.3 C 538.8 376.4 530.8 370.1 529 365.7 C 527.2 361.2 514.7 357.7 507.6 357.7 C 500.5 357.7 504 358.6 498.7 352.4 C 493.3 346.2 488.9 345.2 484.4 345.2 C 479.9 345.2 477.3 338.1 470.1 338.1 C 463 338.1 447.8 331.8 441.6 331.8 C 441 331.7 440.3 331.6 439.7 331.7 L 439.7 331.7Z M 455.9 421.4 C 455.8 421.8 455.6 422.1 455.5 422.5 C 455.6 422.1 455.7 421.7 455.9 421.4Z');
    Obj.attr({ 'href': '3226', 'cursor': 'pointer', 'title': 'تهران و البرز', 'fill': '#1BC6DD', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 449.4 283.3 C 446.1 285.9 443.6 287.6 442.6 287.9 C 437.2 289.7 438.1 289.7 437.3 294.2 C 436.4 298.7 437.3 302.2 433.7 303.1 C 430.1 304 434.6 309.4 434.6 309.4 C 434.6 309.4 452.5 324.6 457.8 326.4 C 461.1 327.5 463 332.5 465 337.3 C 467 337.7 468.8 338 470.3 338 C 477.4 338 480.1 345.1 484.6 345.1 C 489.1 345.1 493.5 346 498.9 352.3 C 504.3 358.5 500.7 357.6 507.8 357.6 C 514.9 357.6 527.4 361.2 529.2 365.6 C 531 370.1 539 376.3 543.5 377.2 C 548 378.1 555.1 382.5 557.8 376.3 C 560.5 370.1 560.5 370.9 567.6 366.5 C 574.7 362 573 370.1 580.1 370.1 C 587.2 370.1 589.9 380.8 594.4 375.4 C 598.9 370 597.1 366.5 602.4 370.1 C 604 371.1 608.1 374.1 612.9 377.6 C 616.3 376.5 625.7 373.6 630 373.6 C 635.4 373.6 638.1 375.4 643.4 370 C 648.8 364.6 651.4 363.7 656.8 357.5 C 662.2 351.3 662.2 345.9 664 341.4 C 665.8 336.9 672.9 328 679.2 328 C 680.9 328 683.9 328.3 687.1 328.6 L 687.6 325.8 C 686.6 323.6 686.6 322.6 684.5 322.6 C 680.9 322.6 678.2 316.3 672.9 313.7 C 667.5 311 661.3 310.1 661.3 305.7 C 661.3 303.9 662.2 301.6 663.2 299.4 C 662.1 299.7 660.7 299.7 659.3 300 C 657.1 300.4 654.8 299.8 652.7 300.3 C 651.2 300.7 648.9 300.3 647.4 300.3 C 644.9 300.3 643.5 300 642.1 298.1 C 641.4 297.1 640.9 296.5 641 296.1 C 641.1 295.6 641.7 295.3 643.1 295 C 645.4 294.7 644.4 294.8 646.3 294.5 C 644.8 295 629.6 294.8 622.5 296.2 C 621.9 296.3 621.3 296.2 620.7 296.2 C 615.1 296.2 608.9 299.4 603.8 300.6 C 598.8 301.8 594 303.8 589.6 305.9 C 585.6 307.9 578 307.5 573.6 308.6 C 568.3 309.9 562.7 309.2 558.5 311.3 C 555.4 312.9 548.1 313 545.5 313.6 C 540.8 314.8 535.3 315.7 530 315.7 C 523.9 315.7 519.4 315 514 313.9 C 507.3 312.6 501.2 308.6 495.3 308.6 C 492.3 308.6 482.8 304.6 478.4 303.5 C 473.4 302.2 464.3 294.7 463.3 294 C 458.3 290.5 453.7 287.2 449.4 283.3Z');
    Obj.attr({ 'href': '3235', 'cursor': 'pointer', 'title': 'مازندران', 'fill': '#89cb2b', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 152.8 402.5 C 152.7 402.6 152.6 402.7 152.4 402.9 C 152.4 403.8 152.4 404.7 152.4 405.6 C 152.4 407.5 154.8 407.5 156.4 408.3 C 158.3 409.2 153.7 411.9 153.7 414.1 C 153.7 418.2 153.1 418.7 149.3 417.2 C 146.4 416.1 145.8 416.3 144.4 419 C 143.1 421.5 138.5 417.3 136 418.6 C 134.4 419.4 135.1 421.9 132.4 422.6 C 130.3 423.1 133.7 426.7 133.7 428.8 C 133.7 432.8 131.3 432.6 128.4 431.9 C 125.1 431.1 124.5 431.9 125.3 435 C 126.1 438.4 122.4 437.6 120.4 438.6 C 119.9 438.8 118.9 441.8 117.3 442.6 C 114.6 444 116.3 446.6 117.7 447.9 C 119.1 449.3 119.5 451.8 119.5 454.1 C 119.5 457.2 119.3 458.5 115.9 458.5 C 113.8 458.5 111.5 455.4 109.2 454.5 C 108.9 454.4 108.6 454.5 108.3 454.5 C 103.5 452.6 107.7 460.3 107.9 460.7 C 109.4 463.7 105.9 465.6 103.9 465.1 C 100.4 464.2 100 468.4 100.4 470 C 100.8 471.7 101.7 474 102.6 474.9 C 104.1 476.4 106.8 474.9 108.8 477.6 C 110.4 479.8 109.6 483 108.8 484.7 C 107.7 486.9 107.5 488 107.5 490.9 C 107.5 491.6 103 494.3 102.2 494.5 C 100.1 495 100.4 498.9 100.4 500.7 C 100.4 502.6 95.9 501.8 95.9 505.6 C 95.9 506.2 102.9 507.3 103.9 508.3 C 105.4 509.8 104.6 513 106.6 515 C 107.9 516.3 107.8 518.5 108.4 520.8 C 109.1 523.5 111.2 524.7 112.4 527 C 113.9 529.9 113.2 530.2 116 531.7 C 116.4 530 116.7 528.7 116.9 528.2 C 118.7 523.7 124.1 510.4 124.1 506.8 C 124.1 503.2 119.6 498.8 125 498.8 C 130.4 498.8 134.8 499.7 139.3 499.7 C 143.8 499.7 144.7 501.5 148.2 504.2 C 151.8 506.9 156.2 506 161.6 506.9 C 167 507.8 170.5 515.8 175.9 519.4 C 181.3 523 183.9 522.1 189.3 522.1 C 194.7 522.1 201.8 519.4 206.3 517.6 C 210.8 515.8 212.6 517.6 217 517.6 C 221.5 517.6 225 513.1 219.7 511.3 C 214.3 509.5 219.7 505.9 223.3 505 C 225 504.6 229.2 503.4 233.2 502.2 C 231.8 499.4 231.3 496.9 231.3 496.9 C 231.3 496.9 238.4 493.4 242.9 492.5 C 247.4 491.6 245.6 481.8 245.6 481.8 C 245.6 481.8 249.2 480 253.6 480 C 254.7 480 255.7 480.8 256.7 481.9 C 256.5 478.2 256.3 475.4 256.3 474.6 C 256.3 471 262.6 471 267 474.6 C 271.5 478.2 273.3 464.8 273.3 464.8 L 265.3 463.9 C 265.3 463.9 263.5 462.1 260 462.1 C 256.4 462.1 259.1 456.7 252.8 455.8 C 246.5 454.9 250.1 448.7 254.6 449.5 C 259.1 450.4 259.1 448.6 260.9 442.3 C 262.7 436.1 267.1 435.2 262.7 435.2 C 258.2 435.2 257.4 423.6 252 423.6 C 246.6 423.6 244 413.8 239.5 418.2 C 235 422.7 229.7 419.1 227.9 424.5 C 226.1 429.9 226.1 429 221.7 428.1 C 217.2 427.2 212.8 435.2 212.8 440.6 C 212.8 446 207.4 441.5 203.9 445.1 C 200.3 448.7 196.8 446 191.4 446 C 186 446 185.1 434.4 181.6 430.8 C 178 427.2 173.6 413 170 413 C 166.5 412.9 157.5 405.7 152.8 402.5Z');
    Obj.attr({ 'href': '3245', 'cursor': 'pointer', 'title': 'کرمانشاه', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 283 368.1 C 282 368.2 281.1 369.6 277.6 370.1 C 271.4 371 281.2 376.3 283.9 379.9 C 286.6 383.5 278.5 381.7 274.1 383.5 C 269.6 385.3 269.6 386.2 268.7 380.8 C 267.8 375.4 269.6 376.3 264.2 381.7 C 258.8 387.1 270.5 400.5 270.5 400.5 C 270.5 400.5 275 409.4 280.3 413 C 285.7 416.6 286.6 421 283 426.4 C 279.4 431.8 277.6 431.8 275 428.2 C 272.3 424.6 271.4 430.9 267 430.9 C 265.8 430.9 263 431.9 259.6 433.3 C 260.4 434.5 261.4 435.4 262.6 435.4 C 267.1 435.4 262.6 436.3 260.8 442.5 C 259 448.7 259 450.5 254.5 449.7 C 250 448.8 246.5 455.1 252.7 456 C 258.9 456.9 256.3 462.3 259.9 462.3 C 263.5 462.3 265.2 464.1 265.2 464.1 L 273.2 465 C 273.2 465 271.4 478.4 266.9 474.8 C 262.4 471.2 256.2 471.2 256.2 474.8 C 256.2 475.7 256.4 478.5 256.6 482.1 C 259.5 485.6 262.2 492.7 266.9 492.7 C 273.1 492.7 275.8 499.8 281.2 501.6 C 286.6 503.4 290.1 505.2 298.2 505.2 C 306.3 505.2 307.1 502.5 310.7 503.4 C 314.3 504.3 317.8 500.7 321.4 506.1 C 324.4 510.6 330 511.3 332.2 516.4 C 335.6 513.4 338.3 510.9 339.3 509.7 C 342.9 505.2 343.8 498.1 343.8 498.1 C 343.8 498.1 342 488.3 339.3 484.7 C 336.6 481.1 335.8 481.1 336.6 474.9 C 337.5 468.7 337.5 471.3 333 467.8 C 328.5 464.2 326.8 462.5 333 462.5 C 339.2 462.5 341 465.2 341 461.6 C 341 458 340.1 454.5 344.6 451.8 C 349.1 449.1 341.9 439.3 346.4 439.3 C 350.9 439.3 358.9 441.1 362.5 441.1 C 366.1 441.1 361.6 442.9 366.1 449.1 C 370.6 455.3 367 458.9 371.4 458 C 375.9 457.1 376.8 453.5 375.9 449.1 C 375 444.6 375 439.3 369.6 435.7 C 364.2 432.1 362.5 434.8 358 431.2 C 353.5 427.6 356.2 425.8 359.8 426.7 C 363.4 427.6 368.7 424.9 367 421.4 C 365.2 417.8 361.6 421.4 361.6 414.3 C 361.6 407.2 361.6 407.1 366.1 408.9 C 370.6 410.7 373.2 408.9 370.6 404.4 C 368 400 369.6 400 373 400 C 372 399.4 371.1 399.1 370.6 399.1 C 366.1 399.1 354.5 394.6 349.2 394.6 C 343.8 394.6 342.1 394.6 343 390.1 C 343.9 385.6 342.1 381.2 335.8 381.2 C 329.6 381.2 330.5 380.3 328.7 383.9 C 326.9 387.5 328.7 387.5 321.5 387.5 C 314.4 387.5 311.7 387.5 309.9 383 C 308.1 378.5 304.5 383 300.1 383 C 295.6 383 291.2 376.8 286.7 371.4 C 284.5 368.6 283.8 368 283 368.1Z');
    Obj.attr({ 'href': '3231', 'cursor': 'pointer', 'title': 'همدان', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 360.6 293.3 C 353.5 293.3 352.5 298.7 347.2 300.5 C 341.8 302.3 339.2 305 343.6 310.3 C 348.1 315.7 350.8 316.5 358.8 320.1 C 366.8 323.7 371.3 329.9 369.5 333.5 C 367.7 337.1 370.4 343.3 370.4 343.3 L 367.7 348.7 C 367.7 348.7 366.8 348.7 365 353.2 C 363.2 357.7 366.8 361.2 360.5 357.6 C 354.3 354 347.1 363 342.7 360.3 C 338.2 357.6 340 355 334.7 355 C 329.3 355 332.9 363 329.3 363 C 325.7 363 324.8 363 326.6 371 C 327.1 373.2 328.2 377 329.5 381.4 C 330.2 380.4 331.1 380.8 335.5 380.8 C 341.7 380.8 343.5 385.2 342.7 389.7 C 341.8 394.2 343.6 394.2 348.9 394.2 C 354.3 394.2 365.9 398.7 370.3 398.7 C 370.9 398.7 371.7 399 372.7 399.6 C 372.8 399.6 372.9 399.6 373 399.6 C 376.6 399.6 378.3 399.6 378.3 399.6 C 378.3 399.6 381 400.5 382.8 396 C 384.6 391.5 403.3 389.8 409.6 387.1 C 413.8 385.3 418.2 384 420.8 383.3 C 420.8 383.3 420.8 383.3 420.8 383.3 C 420.3 379.5 420 373.2 423.1 372 C 427.6 370.2 438.3 372 441.9 363.1 C 445.5 354.2 452.6 353.3 449 347.9 C 445.4 342.5 438.3 344.3 436.5 338.1 C 434.9 332.6 435.4 331.9 439.8 331.8 C 440.4 331.8 441.1 331.8 441.9 331.8 C 446.9 331.8 457.5 335.7 465.2 337.4 C 463.2 332.6 461.2 327.6 458 326.5 C 452.6 324.7 434.8 309.5 434.8 309.5 C 434.8 309.5 433 307.3 432.6 305.5 C 429.6 305.2 427 305 425.8 305 C 419.6 305 398.1 313 393.7 313 C 389.2 313 389.3 310.3 384.8 310.3 C 380.3 310.3 373.2 305.8 369.6 302.3 C 365.9 298.6 367.7 293.3 360.6 293.3 L 360.6 293.3Z');
    Obj.attr({ 'href': '3237', 'cursor': 'pointer', 'title': 'قزوین', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 156.2 307.8 C 155.1 307.8 154 308.2 152.6 309.4 C 148.1 313 151.7 321 146.3 321.9 C 140.9 322.8 138.3 321 132 322.8 C 125.7 324.6 125.7 329 125.7 329 C 125.7 329 124.8 326.3 120.3 336.2 C 119.5 337.9 118.8 340.1 118 342.6 C 119.3 343.5 119.9 345 120.7 346.6 C 121.4 347.9 124.9 349 126 351.9 C 127.4 355.5 126.8 357 130.4 359.4 C 131 359.8 131.6 360 132.2 360.3 C 135.3 361.8 137.9 361 140.2 359.8 C 140.7 359.5 148.9 358.7 150 358.9 C 154.5 360 154.4 356.2 155.8 361.6 C 156.2 363.2 165.6 361.3 166.9 361.6 C 170.4 362.3 164.4 365.4 163.8 366 C 161 368.8 162.3 368 158.5 369.1 C 152.6 370.9 154.9 368.7 150.5 370.9 C 147.6 372.3 144.9 370.9 146.9 374.9 C 148.3 377.7 145.6 379.8 143.8 380.2 C 141.7 380.7 143.6 384.1 143.8 385.1 C 143.2 388.8 146.2 390.3 146.9 393.1 C 147.7 396.4 145 396.3 149.1 398 C 151.8 399.1 153.5 396.9 153.5 401.1 C 153.5 402 153.1 402 152.6 402.5 C 157.3 405.7 166.3 412.9 169.4 412.9 C 173 412.9 177.4 427.2 181 430.7 C 184.6 434.3 185.5 445.9 190.8 445.9 C 196.2 445.9 199.7 448.6 203.3 445 C 206.9 441.4 212.2 445.9 212.2 440.5 C 212.2 435.1 216.7 427.1 221.1 428 C 225.6 428.9 225.5 429.8 227.3 424.4 C 229.1 419 234.5 422.6 238.9 418.1 C 243.4 413.6 246 423.5 251.4 423.5 C 255.3 423.5 256.8 429.7 259.1 433 C 262.5 431.6 265.3 430.6 266.5 430.6 C 271 430.6 271.9 424.3 274.5 427.9 C 277.2 431.5 279 431.5 282.5 426.1 C 286.1 420.7 285.2 416.3 279.8 412.7 C 274.4 409.1 270 400.2 270 400.2 C 270 400.2 258.4 386.8 263.7 381.4 C 269.1 376 267.3 375.2 268.2 380.5 C 269.1 385.9 269.1 385 273.6 383.2 C 278.1 381.4 286.1 383.2 283.4 379.6 C 280.7 376 270.9 370.7 277.1 369.8 C 278.5 369.6 279.5 369.3 280.2 368.9 C 277.9 367.2 275.7 365.3 272.6 361.7 C 267.2 355.5 271.7 357.2 273.5 350.1 C 275.3 343 275.3 333.1 271.7 327.8 C 268.1 322.4 262.8 317.1 258.3 317.1 C 253.8 317.1 249.4 312.6 244.9 312.6 C 240.4 312.6 241.3 308.2 235.1 309.9 C 228.9 311.6 229.8 324.2 224.4 322.4 C 219 320.6 209.2 323.3 204.7 316.1 C 200.2 309 202 309 191.3 311.6 C 180.6 314.3 169 310.7 163.6 309.8 C 160.5 309.6 158.5 307.7 156.2 307.8Z');
    Obj.attr({ 'href': '3249', 'cursor': 'pointer', 'title': 'کردستان', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 264.8 260.8 C 264.3 260.8 263.7 260.9 262.6 261.1 C 262.5 261.1 262.5 261.1 262.4 261.1 C 257 262 245.5 273.6 243.7 273.6 C 243.7 273.6 235.6 276.3 230.3 286.1 C 224.9 295.9 233 297.7 235.6 307.5 C 235.8 308.1 236.1 309.2 236.4 310 C 241.8 308.8 241.2 312.8 245.5 312.8 C 250 312.8 254.4 317.3 258.9 317.3 C 263.4 317.3 268.7 322.7 272.3 328 C 275.9 333.4 275.9 343.2 274.1 350.3 C 272.3 357.4 267.8 355.6 273.2 361.9 C 276.3 365.5 278.5 367.3 280.8 369.1 C 281.9 368.6 282.5 368.1 283.1 368 C 283.9 367.9 284.6 368.5 286.6 370.9 C 291.1 376.3 295.5 382.5 300 382.5 C 304.5 382.5 308 378 309.8 382.5 C 311.6 387 314.3 387 321.4 387 C 328.5 387 326.8 387 328.6 383.4 C 329.1 382.4 329.4 381.7 329.7 381.3 C 328.4 377 327.2 373.1 326.8 370.9 C 326.6 369.9 326.4 369 326.3 368.2 C 326.2 367.4 326 366.8 326 366.2 C 326 365.9 326 365.7 326 365.4 C 325.9 362.8 327.1 362.8 329.6 362.8 C 333.2 362.8 329.6 354.8 335 354.8 C 340.4 354.8 338.6 357.5 343 360.1 C 347.5 362.8 354.6 353.8 360.8 357.4 C 367 361 363.5 357.4 365.3 353 C 367.1 348.5 368 348.5 368 348.5 L 370.7 343.1 C 370.7 343.1 368 336.9 369.8 333.3 C 371.6 329.7 367.1 323.5 359.1 319.9 C 351.1 316.3 348.4 315.4 343.9 310.1 C 339.4 304.7 342.1 302.1 347.5 300.3 C 352.9 298.5 353.8 293.1 360.9 293.1 C 361.1 293.1 361.3 293.1 361.5 293.1 C 361.4 292.7 361.3 292.4 361.2 292 C 361 291.4 360.8 290.7 360.6 290.1 C 360.6 290 360.5 289.9 360.5 289.8 C 360.4 289.4 360.2 288.9 360 288.5 C 359.9 288.2 359.7 287.8 359.5 287.4 C 359.3 287 359.1 286.5 358.8 286.1 C 358.7 285.9 358.6 285.8 358.5 285.6 C 358.3 285.3 358.2 285 358 284.6 C 357.7 284.1 357.4 283.5 357 283 C 356.7 282.5 356.4 282 356 281.5 C 355.9 281.4 355.9 281.3 355.8 281.2 C 355.5 280.8 355.2 280.4 354.9 280 C 354.7 279.8 354.5 279.5 354.3 279.2 C 353.8 278.5 353.2 277.8 352.6 277 C 350.1 273.9 348.4 271.4 347 269.5 C 346 268.1 345.2 267 344.5 266.1 C 344 265.5 343.6 265.1 343.2 264.8 C 343 264.6 342.8 264.5 342.6 264.4 C 342.4 264.3 342.2 264.2 342 264.1 C 341.8 264 341.6 264 341.4 263.9 C 341.4 263.9 341.4 263.9 341.4 263.9 C 340.8 263.8 340.1 263.9 339.2 264.2 C 338.9 264.3 338.6 264.4 338.3 264.5 C 338 264.6 337.6 264.7 337.3 264.8 C 336.7 264.9 336.2 264.9 335.7 264.8 C 335.5 264.8 335.3 264.7 335 264.6 C 335 264.6 335 264.6 335 264.6 C 334.8 264.5 334.6 264.5 334.4 264.4 C 333.8 264.1 333.3 263.8 332.9 263.5 C 332.9 263.5 332.9 263.5 332.9 263.5 C 332.5 263.2 332.1 263 331.8 262.9 C 331.5 262.8 331.2 262.9 330.9 263.3 C 330.9 263.3 330.9 263.3 330.9 263.3 C 330.7 263.6 330.5 263.9 330.3 264.5 C 329.2 267.3 329.1 269.7 328.1 270.3 C 328 270.4 327.9 270.4 327.8 270.4 C 327.2 270.5 326.3 270.1 324.9 269 C 322.7 267.2 321.8 266.3 320.7 265.9 C 320.7 265.9 320.7 265.9 320.7 265.9 C 320.2 265.7 319.5 265.6 318.7 265.5 C 318.7 265.5 318.7 265.5 318.7 265.5 C 317.8 265.4 316.7 265.4 315.2 265.4 C 314.4 265.4 313.8 265.3 313.4 265.2 C 313.2 265.2 313 265.1 312.8 265 C 312.6 264.9 312.5 264.8 312.4 264.7 C 312.3 264.6 312.2 264.5 312.2 264.4 C 312.2 264.4 312.2 264.4 312.2 264.4 C 312.1 264.3 312.1 264.2 312 264.1 C 311.7 263 312.5 261.4 308 260.8 C 307.1 260.7 306.4 260.7 305.8 260.7 C 305.5 260.7 305.2 260.7 305 260.8 C 305 260.8 305 260.8 305 260.8 C 304.8 260.8 304.6 260.9 304.4 261 C 304 261.2 303.8 261.4 303.6 261.6 C 303.4 261.8 303.2 262.1 303.1 262.4 C 302.9 262.8 302.7 263.3 302.5 263.7 C 302.4 263.8 302.3 264 302.2 264.1 C 302.1 264.2 302 264.4 301.9 264.5 C 301.8 264.6 301.6 264.7 301.4 264.8 C 301.2 264.9 301 265 300.8 265.1 C 300.6 265.2 300.3 265.3 300 265.3 C 298.9 265.5 297.7 265.6 296.6 265.7 C 294.4 265.8 292.3 265.6 291.1 265.4 C 290.5 265.3 290.2 265.3 290.2 265.3 C 290.2 265.3 290 265 289.5 264.6 C 289.4 264.5 289.2 264.4 289.1 264.2 C 289 264.1 288.9 264 288.8 263.9 C 288.8 263.9 288.8 263.9 288.8 263.9 C 288.5 263.6 288.2 263.3 287.8 263.1 C 286.2 262 284 260.9 281.3 260.9 C 280.8 260.9 280.2 260.9 279.7 261 C 279.2 261 278.7 261.1 278.3 261.1 C 274.8 261.5 272.3 262.5 268.8 261.8 C 268 261.6 267.6 261.5 267.2 261.3 C 267.2 261.3 267.2 261.3 267.2 261.3 C 267.1 261.3 267.1 261.3 267.1 261.2 C 267 261.1 266.8 261.1 266.7 261 C 266.5 260.9 266.4 260.8 266.3 260.7 C 266.2 260.7 266.2 260.6 266.1 260.6 C 266.1 260.6 266.1 260.6 266 260.6 C 265.9 260.6 265.9 260.5 265.8 260.5 C 265.8 260.5 265.8 260.5 265.7 260.5 C 265.6 260.5 265.5 260.5 265.4 260.5 C 265.2 260.8 265 260.8 264.8 260.8 L 264.8 260.8Z');
    Obj.attr({ 'href': '3248', 'cursor': 'pointer', 'title': 'زنجان', 'fill': '#0290d9', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 341.9 160.7 C 339.5 160.7 336.6 161 334.5 162.6 C 331.8 164.6 330 165.1 327.5 163.4 C 327 165 326.7 166.4 326.6 167.4 C 325.7 177.2 332.9 167.4 332 176.3 C 331.1 185.2 323.1 188.8 324 195.1 C 324.9 201.3 321.3 201.3 321.3 210.3 C 321.3 219.3 331.1 238.9 335.6 244.2 C 340.1 249.6 340.1 250.5 339.2 256.7 C 339 258.4 338.7 261.3 338.5 264.7 C 343.6 263 342.9 264.9 352.6 277.3 C 359.3 285.9 360.2 289.4 361.2 293.4 C 367.6 293.7 366 298.8 369.5 302.3 C 373.1 305.9 380.2 310.3 384.7 310.3 C 389.2 310.3 389.1 313 393.6 313 C 398.1 313 419.5 305 425.7 305 C 426.9 305 429.4 305.2 432.5 305.5 C 432.3 304.5 432.5 303.6 433.8 303.2 C 437.4 302.3 436.5 298.8 437.4 294.3 C 438.3 289.8 437.4 289.8 442.7 288 C 443.7 287.7 446.1 285.9 449.5 283.4 C 449.4 283.3 449.4 283.3 449.3 283.2 C 445.2 279.4 442.3 277.8 437.8 275 C 431.8 271.4 430.1 268.7 427.4 263.3 C 425.9 260.2 426.1 258 425.9 255.8 C 425.7 253.7 427.6 251.7 424.3 249.3 C 423.4 248.6 416.5 247.3 413.6 246.1 C 412.2 245.5 410 244.6 409.4 244.1 C 408.8 243.6 408.3 242.3 407.1 242.1 C 404.6 241.7 404.8 242.7 403.1 243.1 C 401.4 243.5 397.9 243.3 396.8 243 C 391.4 241.7 387.2 241.7 381.1 240.2 C 377.5 239.3 373.1 238.4 369.3 236.7 C 365.4 235 362.2 232.5 359.1 230.9 C 356.5 229.6 352.8 226.8 351.1 224.4 C 349.4 222 348.8 219.2 346.9 217.4 C 344.2 214.7 345.2 207.6 344.2 203.8 C 343.6 201.2 343.5 198.4 343.5 195.7 C 343.4 193 343.3 190.5 343.3 187.8 C 343.3 183.1 343.3 178.3 343.3 173.6 C 343.3 169.6 343.4 163.8 344 160.7 C 343.4 160.8 342.7 160.7 341.9 160.7 L 341.9 160.7Z');
    Obj.attr({ 'href': '3234', 'cursor': 'pointer', 'title': 'گیلان', 'fill': '#89cb2b', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 291.3 57.1 C 290.7 57.1 290 57.3 289.2 57.8 C 287 58.9 288.2 59.1 283.9 58.2 C 282 57.8 279.1 61.6 278.6 62.2 C 276.5 64.3 276.7 64.6 273.7 65.3 C 271 66 268.3 67.1 266.6 68.4 C 263.1 71 264.2 72.8 259.1 72.8 C 255.5 72.8 254.1 72.9 250.7 74.6 C 249.5 75.2 247.6 78.2 247.6 79.5 C 247.6 80.5 242.8 82.2 242.7 82.2 C 240.4 82.7 237.6 84.9 235.6 85.7 C 233.5 86.6 232.9 88.5 232 90.2 C 231.8 90.6 231.6 90.9 231.3 91.3 C 232.3 93.5 233.2 95.6 233.9 96.9 C 237.5 104 235.7 104.1 235.7 110.3 C 235.7 116.5 234.8 119.2 234.8 122.8 C 234.8 126.4 239.3 121.9 242.8 118.4 C 246.4 114.8 250.8 107.7 257.1 105.9 C 263.4 104.1 256.2 113.9 258.9 119.3 C 261.6 124.7 258.9 122.9 256.2 127.3 C 253.5 131.8 255.3 131.8 250.9 133.6 C 246.4 135.4 250.9 137.2 255.4 137.2 C 259.9 137.2 253.6 139.9 258.1 143.5 C 262.6 147.1 257.2 146.2 255.4 150.7 C 253.6 155.2 243.8 164.1 243.8 164.1 C 243.8 164.1 250.9 171.2 259 175.7 C 267.1 180.2 269.7 171.2 272.4 175.7 C 275.1 180.2 279.6 190 279.6 198 C 279.6 206 280.5 204.3 285.9 204.3 C 291.3 204.3 290.4 204.3 290.4 210.6 C 290.4 216.9 285.9 222.2 286.8 230.2 C 287.7 238.2 297.5 239.1 298.4 247.2 C 299.3 255 299.3 252.8 305 261.5 C 305.7 261.3 306.7 261.3 308.2 261.4 C 315.3 262.3 309.1 265.9 315.3 265.9 C 321.5 265.9 320.6 265.9 325.1 269.5 C 329.6 273.1 328.7 269.5 330.5 265 C 332.3 260.5 333.2 266.8 338.5 265 C 338.6 265 338.6 265 338.7 264.9 C 338.9 261.6 339.1 258.6 339.4 256.9 C 340.3 250.7 340.3 249.8 335.8 244.4 C 331.3 239 321.5 219.4 321.5 210.5 C 321.5 201.6 325.1 201.6 324.2 195.3 C 323.3 189.1 331.3 185.5 332.2 176.5 C 333.1 167.6 325.9 177.4 326.8 167.6 C 326.9 166.6 327.3 165.2 327.7 163.6 C 327.5 163.5 327.3 163.4 327.1 163.2 C 325.1 161.7 323.7 159.4 322.2 157.9 C 320.7 156.4 319.1 153.9 317.8 152.1 C 316.5 150.4 316.1 147 315.6 145 C 315.1 143 310.8 146.5 309.4 145 C 307.5 143.1 304.9 142.8 303.6 140.1 C 302 136.9 301.8 136.4 297.8 133.4 C 295 131.3 293.2 129.2 290.7 126.7 C 289.4 125.4 293.7 121.6 294.3 120.9 C 297 117.3 295.9 116.9 301.4 116.9 C 304.2 116.9 306.7 116.3 309.4 115.6 C 312.7 114.8 312.4 111.9 310.7 110.3 C 311 110.2 306.5 103.2 304.9 102.8 C 302.8 102.3 301.2 98.2 300 97 C 297 94 300.5 91.2 302.2 89.4 C 303.5 88.1 307.6 88 309.3 87.2 C 312.3 85.7 314.6 84.5 313.7 81 C 313 78 309 74.9 306.6 72.6 C 303.5 69.5 300.7 66.7 298.2 63.3 C 297.1 61.9 295.6 60.7 294.6 59.7 C 293.2 57.8 292.3 57.1 291.3 57.1Z M 335.3 162.1 C 335 162.2 334.8 162.4 334.5 162.6 C 334.7 162.4 335 162.2 335.3 162.1Z M 331.3 164.3 C 331.1 164.3 331 164.4 330.8 164.4 C 331 164.4 331.1 164.4 331.3 164.3Z M 339.6 264.3 C 339.5 264.3 339.3 264.4 339.2 264.4 C 339.3 264.4 339.5 264.3 339.6 264.3Z');
    Obj.attr({ 'Text': 'اردبیل', 'href': '3251', 'cursor': 'pointer', 'title': 'اردبیل', 'fill': '#920101', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 231.2 91.3 C 230.2 92.7 228.7 93.9 227.5 95.1 C 226.2 96.4 221.3 95.1 219.5 96 C 217 97.2 215.2 99 213.3 100.5 C 212.3 101.2 212.2 105.6 211.1 106.7 C 208.5 109.3 207.2 109 205.8 112 C 205.3 112.9 200.3 115.9 199.1 116.4 C 197.5 117.2 194.2 117.3 192.4 117.3 C 188.6 117.3 185.5 112.7 182.2 116 C 180.3 117.9 177.8 119.1 175.1 119.1 C 171.5 119.1 169 116.8 165.8 116 C 163.6 115.5 161.4 115.6 159.1 115.6 C 156.5 115.6 154.3 113.4 152.4 112 C 150.2 110.4 147.8 109.9 145.7 108.9 C 143.8 107.9 140.5 108.4 138.2 108.4 C 137.2 108.4 134.6 105.8 132.7 104.4 C 129.9 107.5 127.6 110.1 126.7 111.2 C 121.3 118.3 122.3 120.1 124 128.2 C 125.7 136.3 124 133.6 121.3 138 C 118.6 142.5 107 150.5 105.2 154.9 C 103.4 159.3 113.2 166.5 113.2 171.9 C 113.2 177.3 112.3 198.7 114.1 204 C 115.9 209.3 119.4 213.8 119.4 219.2 C 119.4 224.6 119.4 229.9 121.2 235.3 C 123 240.7 131.9 243.3 137.3 245.1 C 142.7 246.9 148 254 156.1 256.7 C 164.2 259.4 166.8 267.4 171.3 271.9 C 175.8 276.4 172.2 271 177.6 271.9 C 183 272.8 180.3 264.8 185.6 263.9 C 191 263 189.2 266.6 193.6 262.1 C 198.1 257.6 195.4 265.7 200.8 265.7 C 206.2 265.7 204.4 269.3 207.9 273.7 C 211.5 278.2 204.3 282.6 207 288.9 C 209.7 295.1 213.2 290.7 220.4 290.7 C 221.7 290.7 224.6 291.1 228.4 291.7 C 228.5 290.2 229 288.4 230.2 286.2 C 235.6 276.4 243.6 273.7 243.6 273.7 C 243.6 273.7 255.3 261.2 262.5 261.2 C 262.5 261.2 262.5 261.2 262.5 261.2 C 266.6 260.5 265.2 261 266.9 261.7 C 270.8 262.1 272.4 262 276.6 261.3 C 277.1 261.2 277.6 261.3 278.1 261.5 C 279 261.4 280 261.3 281.1 261.3 C 286.5 261.3 290 265.8 290 265.8 C 290 265.8 295.3 266.7 299.8 265.8 C 303.3 265.1 301.9 262.2 304.6 261.4 C 299 252.7 298.9 254.8 298 247.1 C 297.1 239.1 287.3 238.2 286.4 230.1 C 285.5 222 290 216.7 290 210.5 C 290 204.3 290.9 204.2 285.5 204.2 C 280.1 204.2 279.2 206 279.2 197.9 C 279.2 189.8 274.7 180 272 175.6 C 269.3 171.1 266.7 180.1 258.6 175.6 C 250.5 171.1 243.4 164 243.4 164 C 243.4 164 253.2 155.1 255 150.6 C 256.8 146.1 262.1 147 257.7 143.4 C 253.2 139.8 259.5 137.1 255 137.1 C 250.5 137.1 246.1 135.3 250.5 133.5 C 255 131.7 253.2 131.7 255.8 127.2 C 258.5 122.7 261.2 124.5 258.5 119.2 C 255.8 113.8 263 104 256.7 105.8 C 250.4 107.6 246 114.7 242.4 118.3 C 238.8 121.9 234.4 126.3 234.4 122.7 C 234.4 119.1 235.3 116.5 235.3 110.2 C 235.3 103.9 237.1 103.9 233.5 96.8 C 233.1 95.6 232.2 93.6 231.2 91.3 L 231.2 91.3Z M 306 261 C 305.9 261 305.8 261 305.7 261 C 306 261 306.3 261 306.7 261 C 306.4 261.1 306.2 261 306 261Z');
    Obj.attr({ 'href': '3227', 'cursor': 'pointer', 'title': 'آذربایجان شرقی', 'fill': '#920101', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    Obj = paper.path('M 872.6 433 C 869.4 433.1 866.4 433.6 863.9 435.3 C 858.2 439.1 843.7 449.2 839.9 454.9 C 836.1 460.6 820.4 463.1 814.7 465 C 814.1 465.2 813.1 465.2 811.8 465.1 C 804.1 477.3 797.4 488.6 795.1 491.5 C 790.1 497.8 763.6 494 763.6 494 C 763.6 494 763.6 494.2 763.6 494.3 C 763.6 494.5 763.5 495.6 763.4 497.6 C 763.4 498 763.4 497.9 763.4 498.4 C 763.3 500.8 763.1 503.9 763 507.8 C 763 508.9 762.9 510 762.9 511.2 C 762.9 512.3 762.8 513.4 762.8 514.6 C 762.8 514.8 762.8 514.9 762.8 515 C 762.7 517.3 762.6 519.7 762.6 522.3 C 762.6 522.4 762.6 522.5 762.6 522.6 C 762.5 525.2 762.5 527.9 762.5 530.7 C 762.5 533.6 762.4 536.5 762.4 539.5 C 762.4 563.5 752.3 566 752.3 566 C 752.3 566 754.3 567.2 754.6 567.4 C 753.6 567.7 752.8 568 752.4 568.2 C 747 570.9 744.4 567.3 738.1 574.5 C 734.2 579 732 586.6 729.7 593.4 C 729.5 594.1 729.2 594.8 729 595.4 C 727.8 598.7 726.5 601.7 724.8 604 C 720.1 610.2 703.1 613 693 614.3 C 691.5 614.5 690.2 614.6 689.1 614.7 C 688 614.8 686.9 615.1 685.8 615.4 C 685.2 615.6 684.7 615.8 684.1 616 C 676.9 619 669.6 626 658.7 628.9 C 645.3 632.5 638.2 627.1 630.1 626.2 C 623.1 625.4 612 627.4 606.9 630.9 C 606.5 631.1 606.2 631.4 605.9 631.7 C 605.6 632 605.3 632.2 605.1 632.5 C 601.5 637 600.6 635.2 595.3 638.8 C 589.9 642.4 596.2 656.7 594.4 667.4 C 593.1 675.4 590.7 687 589.6 695.7 C 589.3 697.9 589.1 699.9 589 701.6 C 589 702.2 589 702.7 589 703.2 C 589 705.4 589.3 709.1 589.7 713.2 C 592.3 712.9 594.8 713 597.2 713.4 C 597.5 713.4 597.8 713.5 598.1 713.5 C 599.9 713.8 601.7 714.2 603.3 714.7 C 604 714.9 604.7 715.2 605.4 715.4 C 605.4 715.4 605.5 715.4 605.5 715.4 C 606.6 715.8 607.6 716.2 608.6 716.6 C 614.8 719.3 623.8 732.7 630 746.9 C 636.2 761.2 647.9 792.4 656.8 799.6 C 665.7 806.7 670.2 819.2 670.2 827.3 C 670.2 835.4 672 843.4 680.9 845.1 C 686.9 846.3 692.1 849.5 695.7 853.4 C 700 852.2 703.5 851.3 705 851.3 C 705.7 851.3 706.3 851.3 706.9 851.3 C 707.5 851.3 708.2 851.3 708.7 851.2 C 712 850.9 713.9 849.7 711.2 845 C 711.1 844.8 711 844.6 710.9 844.4 C 708.3 838.2 714.7 825.3 712.7 818.6 C 712.6 818.4 712.6 818.2 712.5 817.9 C 712.4 817.7 712.3 817.5 712.2 817.3 C 708.6 811.1 705.9 818.2 705.9 809.3 C 705.9 800.4 701.4 800.4 698.7 786.1 C 696 771.8 701.4 773.6 705 765.6 C 708.6 757.6 709.5 758.4 714.8 760.2 C 716.1 760.6 717.9 760.8 719.9 760.8 C 720.4 760.8 720.9 760.8 721.4 760.7 C 727.2 760.3 734.8 758.5 741.6 756.6 C 751.4 753.9 767.5 758.4 772.9 756.6 C 778.3 754.8 784.5 755.7 784.5 745 C 784.5 737.3 782.7 725.4 785.9 717.1 C 786 716.7 786.2 716.4 786.3 716 C 787.6 713.2 789.6 710.8 792.5 709.3 C 804.1 703.1 819.3 699.5 826.4 694.1 C 833.5 688.7 834.4 686.1 841.6 682.5 C 844.8 680.9 847.8 678.2 850.1 675.8 C 864.4 682.8 895.1 694.7 926 706.5 L 925.9 706.2 C 926.9 702.7 928.2 699.5 928.9 697.3 C 933.3 684 930.8 685.9 933.9 680.3 C 937 674.7 947.8 663.3 950.3 657.6 C 952.8 651.9 942.1 641.8 942.1 634.9 C 942.1 628 930.1 611.6 924.4 611.6 C 918.7 611.6 920 604 916.8 595.8 C 913.6 587.6 917.4 577.5 911.1 571.8 C 904.8 566.1 911.1 556 913 552.9 C 914.9 549.7 907.9 546.6 904.2 549.7 C 900.4 552.9 889.7 554.1 884 552.2 C 878.3 550.3 874.5 537.1 868.8 533.9 C 863.1 530.7 865 524.4 864.4 517.5 C 863.8 510.6 868.2 508 873.9 495.4 C 879.6 482.8 873.9 484.7 877.1 480.2 C 880.3 475.7 881.5 469.5 889.7 468.9 C 897.9 468.3 895.4 459.4 898.5 450 C 901.6 440.6 894.1 434.8 885.9 434.2 C 881.4 433.7 876.8 432.9 872.6 433 L 872.6 433Z');
    Obj.attr({ 'href': '3240', 'cursor': 'pointer', 'title': 'یزد', 'fill': '#faf000', 'stroke': '#ffffff', 'stroke-width': '2', 'stroke-opacity': '1' });
    var st = paper.setFinish();
    st.hover(over, out);
    st.click(click);
    var translate = 't' + (-1 * st.getBBox().x) + ',' + (-1 * st.getBBox().y);
    st.transform(translate);
    st.transform('s0.38,0.38,0,0');
}
function chkRandomCheck(obj) {
    var ch = $(obj);
    if (ch[0].checked) {
        $(".inputFrom").addClass("inputDisable").attr("disabled", true).val("");
        $('.inputRandom').removeClass("inputDisable").attr("disabled", false);
    }
    else {
        $(".inputFrom").removeClass("inputDisable").attr("disabled", false);
        $(".inputRandom").addClass("inputDisable").attr("disabled", true).val("");
    }
}

function DetectAllCheckBox() {
    $('#ctl00_ContentPlaceHolder1_treeViewDiv input:checked').each(function () {
       // 
        var parent = this.parentElement;
        var txt = parent.innerText;
        if (parent.innerHTML.indexOf("NotExpand") != -1) {
            var count = $(parent).find('input[type=hidden][name="NotExpand"]').val();
            globalNumCount += parseInt(count)
            // alert(count);
        }
        var i = 1;
    });
}

//*******************************StateSent***********************************************


//*************************Momen

var mode = 1;

function SetMode(event) {
    if (e == null)
        e = window.event;
    var code = e.which || e.charCode || e.keyCode;
    if (code == 123) {
        if (mode == 0)
            mode = 1;
        else
            mode = 0;

        window.event.returnValue = false;
        return;
    }
    window.event.returnValue = true;
}
function SetChar(obj, e, code) {
    var key = String.fromCharCode(code);
    obj.value += key;
}



function IChangeToFarsi(obj, e) {

    if (e == null)
        e = window.event;

    var code = e.which || e.charCode || e.keyCode;

    var key = String.fromCharCode(code);

    if (code > 127)
        return;

    if (window.event == null) {
        if (mode == 1) {
            switch (key) {
                case 'H': ISetChar(obj, e, 1570); break;

                case 'h': ISetChar(obj, e, 1575); break;
                case 'f':
                case 'F': ISetChar(obj, e, 1576); break;
                case '`': ISetChar(obj, e, 1662); break;
                case 'j':
                case 'J': ISetChar(obj, e, 1578); break;
                case 'e':
                case 'E': ISetChar(obj, e, 1579); break;
                case '[': ISetChar(obj, e, 1580); break;
                case ']': ISetChar(obj, e, 1670); break;
                case 'p':
                case 'P': ISetChar(obj, e, 1581); break;
                case 'o':
                case 'O': ISetChar(obj, e, 1582); break;
                case 'n':
                case 'N': ISetChar(obj, e, 1583); break;
                case 'b':
                case 'B': ISetChar(obj, e, 1584); break;
                case 'v':
                case 'V': ISetChar(obj, e, 1585); break;
                case 'c':
                case 'C': ISetChar(obj, e, 1586); break;
                case '\\': ISetChar(obj, e, 1688); break;
                case 's':
                case 'S': ISetChar(obj, e, 1587); break;
                case 'a':
                case 'A': ISetChar(obj, e, 1588); break;
                case 'w':
                case 'W': ISetChar(obj, e, 1589); break;
                case 'q':
                case 'Q': ISetChar(obj, e, 1590); break;
                case 'x':
                case 'X': ISetChar(obj, e, 1591); break;
                case 'z':
                case 'Z': ISetChar(obj, e, 1592); break;
                case 'u':
                case 'U': ISetChar(obj, e, 1593); break;
                case 'y':
                case 'Y': ISetChar(obj, e, 1594); break;
                case 't':
                case 'T': ISetChar(obj, e, 1601); break;
                case 'r':
                case 'R': ISetChar(obj, e, 1602); break;
                case ';': ISetChar(obj, e, 1603); break;
                case '\'': ISetChar(obj, e, 1711); break;
                case 'g':
                case 'G': ISetChar(obj, e, 1604); break;
                case 'l': ISetChar(obj, e, 1605); break;
                case 'k': ISetChar(obj, e, 1606); break;
                case 'K': ISetChar(obj, e, 0161); break;
                case ',': ISetChar(obj, e, 1608); break;
                case 'i':
                case 'I': ISetChar(obj, e, 1607); break;
                case 'd': ISetChar(obj, e, 1610); break;
                case 'D': ISetChar(obj, e, 1609); break;
                case 'm':
                case 'M': ISetChar(obj, e, 1574); break;
                case 'L': ISetChar(obj, e, 1548); break;

            }
        }
        if (e.preventDefault)
            e.preventDefault();
        e.returnValue = false;

    }


    else {
        if (mode == 1) {
            switch (key) {
                case 'H': window.event.keyCode = 1570; break;

                case 'h': window.event.keyCode = 1575; break;
                case 'f':
                case 'F': window.event.keyCode = 1576; break;
                case '`': window.event.keyCode = 1662; break;
                case 'j':
                case 'J': window.event.keyCode = 1578; break;
                case 'e':
                case 'E': window.event.keyCode = 1579; break;
                case '[': window.event.keyCode = 1580; break;
                case ']': window.event.keyCode = 1670; break;
                case 'p':
                case 'P': window.event.keyCode = 1581; break;
                case 'o':
                case 'O': window.event.keyCode = 1582; break;
                case 'n':
                case 'N': window.event.keyCode = 1583; break;
                case 'b':
                case 'B': window.event.keyCode = 1584; break;
                case 'v':
                case 'V': window.event.keyCode = 1585; break;
                case 'c':
                case 'C': window.event.keyCode = 1586; break;
                case '\\': window.event.keyCode = 1688; break;
                case 's':
                case 'S': window.event.keyCode = 1587; break;
                case 'a':
                case 'A': window.event.keyCode = 1588; break;
                case 'w':
                case 'W': window.event.keyCode = 1589; break;
                case 'q':
                case 'Q': window.event.keyCode = 1590; break;
                case 'x':
                case 'X': window.event.keyCode = 1591; break;
                case 'z':
                case 'Z': window.event.keyCode = 1592; break;
                case 'u':
                case 'U': window.event.keyCode = 1593; break;
                case 'y':
                case 'Y': window.event.keyCode = 1594; break;
                case 't':
                case 'T': window.event.keyCode = 1601; break;
                case 'r':
                case 'R': window.event.keyCode = 1602; break;
                case ';': window.event.keyCode = 1603; break;
                case '\'': window.event.keyCode = 1711; break;
                case 'g':
                case 'G': window.event.keyCode = 1604; break;
                case 'l': window.event.keyCode = 1605; break;
                case 'k': window.event.keyCode = 1606; break;
                case 'K': window.event.keyCode = 0161; break;
                case ',': window.event.keyCode = 1608; break;
                case 'i':
                case 'I': window.event.keyCode = 1607; break;
                case 'd': window.event.keyCode = 1610; break;
                case 'D': window.event.keyCode = 1609; break;
                case 'm':
                case 'M': window.event.keyCode = 1574; break;
                case 'L': window.event.keyCode = 1548; break;

            }


        }
        window.event.returnValue = true;
    }
}


function IChangeNumbersToFarsi(obj, e) {


    if (e == null)
        e = window.event;

    var code = e.which || e.charCode || e.keyCode;
    var key = String.fromCharCode(code);

    if (code > 127)
        return;
    if (window.event == null) {

        if (mode == 1) {
            switch (key) {
                case '.': ISetChar(obj, e, 1632); break;
                case '0': ISetChar(obj, e, 1776); break;
                case '1': ISetChar(obj, e, 1777); break;
                case '2': ISetChar(obj, e, 1778); break;
                case '3': ISetChar(obj, e, 1779); break;
                case '4': ISetChar(obj, e, 1780); break;
                case '5': ISetChar(obj, e, 1781); break;
                case '6': ISetChar(obj, e, 1782); break;
                case '7': ISetChar(obj, e, 1783); break;
                case '8': ISetChar(obj, e, 1784); break;
                case '9': ISetChar(obj, e, 1785); break;
            }
        }

        if (e.preventDefault)
            e.preventDefault();
        e.returnValue = false;
    }
    else {
        if (mode == 1) {
            switch (key) {
                case '.': window.event.keyCode = 1632; break;
                case '0': window.event.keyCode = 1776; break;
                case '1': window.event.keyCode = 1777; break;
                case '2': window.event.keyCode = 1778; break;
                case '3': window.event.keyCode = 1779; break;
                case '4': window.event.keyCode = 1780; break;
                case '5': window.event.keyCode = 1781; break;
                case '6': window.event.keyCode = 1782; break;
                case '7': window.event.keyCode = 1783; break;
                case '8': window.event.keyCode = 1784; break;
                case '9': window.event.keyCode = 1785; break;
            }
        }

        window.event.returnValue = true;
    }
}



//*************************Momen