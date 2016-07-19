var mode = 1;

function SetMode(event) {
    if (e == null)
        e = window.event;
    var code = e.which || e.charCode || e.keyCode;
    if (code == 123)
	{
		if (mode == 0)
			mode = 1;
		else
			mode = 0;
	
		window.event.returnValue = false;
		return;
	}
	window.event.returnValue = true;
}
function SetChar(obj,e,code) {
    var key = String.fromCharCode(code);
                  obj.value += key;
}



function ChangeToFarsi(obj, e) {

    if (e == null)
        e = window.event;

    var code = e.which || e.charCode || e.keyCode;

    var key = String.fromCharCode(code);
    if (e.keyCode==8) {
        return true;
    }
    if (code > 127)
        return;

    if (window.event == null) {
    
        if (mode == 1) {
            switch (key) {
                case 'H': SetChar(obj, e, 1570); break;

                case 'h': SetChar(obj, e, 1575); break;
                case 'f':
                case 'F': SetChar(obj, e, 1576); break;
                case '`': SetChar(obj, e, 1662); break;
                case 'j':
                case 'J': SetChar(obj, e, 1578); break;
                case 'e':
                case 'E': SetChar(obj, e, 1579); break;
                case '[': SetChar(obj, e, 1580); break;
                case ']': SetChar(obj, e, 1670); break;
                case 'p':
                case 'P': SetChar(obj, e, 1581); break;
                case 'o':
                case 'O': SetChar(obj, e, 1582); break;
                case 'n':
                case 'N': SetChar(obj, e, 1583); break;
                case 'b':
                case 'B': SetChar(obj, e, 1584); break;
                case 'v':
                case 'V': SetChar(obj, e, 1585); break;
                case 'c':
                case 'C': SetChar(obj, e, 1586); break;
                case '\\': SetChar(obj, e, 1688); break;
                case 's':
                case 'S': SetChar(obj, e, 1587); break;
                case 'a':
                case 'A': SetChar(obj, e, 1588); break;
                case 'w':
                case 'W': SetChar(obj, e, 1589); break;
                case 'q':
                case 'Q': SetChar(obj, e, 1590); break;
                case 'x':
                case 'X': SetChar(obj, e, 1591); break;
                case 'z':
                case 'Z': SetChar(obj, e, 1592); break;
                case 'u':
                case 'U': SetChar(obj, e, 1593); break;
                case 'y':
                case 'Y': SetChar(obj, e, 1594); break;
                case 't':
                case 'T': SetChar(obj, e, 1601); break;
                case 'r':
                case 'R': SetChar(obj, e, 1602); break;
                case ';': SetChar(obj, e, 1603); break;
                case '\'': SetChar(obj, e, 1711); break;
                case 'g':
                case 'G': SetChar(obj, e, 1604); break;
                case 'l': SetChar(obj, e, 1605); break;
                case 'k': SetChar(obj, e, 1606); break;
                case 'K': SetChar(obj, e, 0161); break;
                case ',': SetChar(obj, e, 1608); break;
                case 'i':
                case 'I': SetChar(obj, e, 1607); break;
                case 'd': SetChar(obj, e, 1610); break;
                case 'D': SetChar(obj, e, 1609); break;
                case 'm':
                case 'M': SetChar(obj, e, 1574); break;
                case 'L': SetChar(obj, e, 1548); break;
                case '': SetChar(obj, e, 8); break;
                case ' ': SetChar(obj, e, 32); break;
//                case 'L': SetChar(obj, e, 1548); break;
//                case 'L': SetChar(obj, e, 1548); break;

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
                case '': SetChar(obj, e, 8); break;
                case ' ': SetChar(obj, e, 32); break;
            }


        }
        window.event.returnValue = true;
    } 
}


function ChangeNumbersToFarsi(obj, e) {

  
    if (e == null)
        e = window.event;

    var code = e.which || e.charCode || e.keyCode;
    var key = String.fromCharCode(code);
    if (e.keyCode == 8) {
        return true;
    }
    if (code > 127)
        return;
    if (window.event == null) {
    
        if (mode == 1) {
            switch (key) {
                case '.': SetChar(obj, e, 1632); break;
                case '0': SetChar(obj, e, 1776); break;
                case '1': SetChar(obj, e,  1777); break;
                case '2':SetChar(obj, e,  1778); break;
                case '3':SetChar(obj, e,  1779); break;
                case '4':SetChar(obj, e, 1780); break;
                case '5': SetChar(obj, e, 1781); break;
                case '6':SetChar(obj, e, 1782); break;
                case '7':SetChar(obj, e,  1783); break;
                case '8':SetChar(obj, e, 1784); break;
                case '9': SetChar(obj, e,  1785); break;
            }
        }

        if (e.preventDefault)
            e.preventDefault();
        e.returnValue = false;
    }
    else {
        if (mode == 1) {
            switch (key) {
                case '.':  window.event.keyCode= 1632; break;
                case '0': window.event.keyCode= 1776; break;
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

