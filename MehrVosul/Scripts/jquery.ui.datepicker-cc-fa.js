// Mahdi Hasheminezhad. email: hasheminezhad at gmail dot com (http://hasheminezhad.com)
jQuery(function($){
//	$.datepicker.regional['fa'] = {
//		calendar: JalaliDate,
//		closeText: 'بستن',
//		prevText: 'قبل',
//		nextText: 'بعد',
//		currentText: 'امروز',
//		monthNames: ['فروردین','اردیبهشت','خرداد','تیر','مرداد','شهریور','مهر','آبان','آذر','دی','بهمن','اسفند'],
//		monthNamesShort: ['فروردین','اردیبهشت','خرداد','تیر','مرداد','شهریور','مهر','آبان','آذر','دی','بهمن','اسفند'],
//		dayNames: ['یکشنبه', 'دوشنبه', 'سه شنبه', 'چهارشنبه', 'پنجشنبه', 'جمعه', 'شنبه'],
//		dayNamesShort: ['یک', 'دو', 'سه', 'چهار', 'پنج', 'جمعه', 'شنبه'],
//		dayNamesMin: ['ی','د','س','چ','پ','ج','ش'],
//		weekHeader: 'ه',
//		dateFormat: 'dd/mm/yy',
//		firstDay: 6,
//		isRTL: true,
//		showMonthAfterYear: false,
//		yearSuffix: '',
//		calculateWeek: function(date) {
//			var checkDate = new JalaliDate(date.getFullYear(), date.getMonth(), date.getDate() + (date.getDay() || 7) - 3);
//			return Math.floor(Math.round((checkDate.getTime() - new JalaliDate(checkDate.getFullYear(), 0, 1).getTime()) / 86400000) / 7) + 1;
    //		}};
    jQuery(function (a) { a.datepicker.regional.fa = { calendar: JalaliDate, closeText: "بستن", prevText: "", nextText: "", currentText: "امروز", monthNames: ["فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند"], monthNamesShort: ["فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند"], dayNames: ["يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"], dayNamesShort: ["يک", "دو", "سه", "چهار", "پنج", "جمعه", "شنبه"], dayNamesMin: ["ي", "د", "س", "چ", "پ", "ج", "ش"], weekHeader: "ه", dateFormat: "dd/mm/yy", firstDay: 6, isRTL: true, showMonthAfterYear: false, yearSuffix: "", calculateWeek: function (b) { var c = new JalaliDate(b.getFullYear(), b.getMonth(), b.getDate() + (b.getDay() || 7) - 3); return Math.floor(Math.round((c.getTime() - new JalaliDate(c.getFullYear(), 0, 1).getTime()) / 86400000) / 7) + 1 } }; a.datepicker.setDefaults(a.datepicker.regional.fa) }); function JalaliDate(i, h, f) { var d; var a; if (!isNaN(parseInt(i)) && !isNaN(parseInt(h)) && !isNaN(parseInt(f))) { var c = j([parseInt(i, 10), parseInt(h, 10), parseInt(f, 10)]); e(new Date(c[0], c[1], c[2])) } else { e(i) } function j(l) { var k = 0; if (l[1] < 0) { k = leap_persian(l[0] - 1) ? 30 : 29; l[1]++ } var g = jd_to_gregorian(persian_to_jd(l[0], l[1] + 1, l[2]) - k); g[1]--; return g } function b(k) { var g = jd_to_persian(gregorian_to_jd(k[0], k[1] + 1, k[2])); g[1]--; return g } function e(g) { if (g && g.getGregorianDate) { g = g.getGregorianDate() } d = new Date(g); d.setHours(d.getHours() > 12 ? d.getHours() + 2 : 0); if (!d || d == "Invalid Date" || isNaN(d || !d.getDate())) { d = new Date() } a = b([d.getFullYear(), d.getMonth(), d.getDate()]); return this } this.getGregorianDate = function () { return d }; this.setFullDate = e; this.setMonth = function (l) { a[1] = l; var k = j(a); d = new Date(k[0], k[1], k[2]); a = b([k[0], k[1], k[2]]) }; this.setDate = function (l) { a[2] = l; var k = j(a); d = new Date(k[0], k[1], k[2]); a = b([k[0], k[1], k[2]]) }; this.getFullYear = function () { return a[0] }; this.getMonth = function () { return a[1] }; this.getDate = function () { return a[2] }; this.toString = function () { return a.join(",").toString() }; this.getDay = function () { return d.getDay() }; this.getHours = function () { return d.getHours() }; this.getMinutes = function () { return d.getMinutes() }; this.getSeconds = function () { return d.getSeconds() }; this.getTime = function () { return d.getTime() }; this.getTimeZoneOffset = function () { return d.getTimeZoneOffset() }; this.getYear = function () { return a[0] % 100 }; this.setHours = function (g) { d.setHours(g) }; this.setMinutes = function (g) { d.setMinutes(g) }; this.setSeconds = function (g) { d.setSeconds(g) }; this.setMilliseconds = function (g) { d.setMilliseconds(g) } };
//	$.datepicker.setDefaults($.datepicker.regional['fa']);
});

function JalaliDate(p0, p1, p2) {
    var gregorianDate;
    var jalaliDate;

	if (!isNaN(parseInt(p0)) && !isNaN(parseInt(p1)) && !isNaN(parseInt(p2))) {
        var g = jalali_to_gregorian([parseInt(p0, 10), parseInt(p1, 10), parseInt(p2, 10)]);
		setFullDate(new Date(g[0], g[1], g[2]));
    } else {
        setFullDate(p0);
    }

    function jalali_to_gregorian(d) {
		var adjustDay = 0;
		if(d[1]<0){
			adjustDay = leap_persian(d[0]-1)? 30: 29;
			d[1]++;
		}
        var gregorian = jd_to_gregorian(persian_to_jd(d[0], d[1] + 1, d[2])-adjustDay);
        gregorian[1]--;
        return gregorian;
    }

    function gregorian_to_jalali(d) {
        var jalali = jd_to_persian(gregorian_to_jd(d[0], d[1] + 1, d[2]));
        jalali[1]--;
        return jalali;
    }

    function setFullDate(date) {
        if (date && date.getGregorianDate) date = date.getGregorianDate();
        gregorianDate = new Date(date);
		gregorianDate.setHours(gregorianDate.getHours() > 12 ? gregorianDate.getHours() + 2 : 0)
        if (!gregorianDate || gregorianDate == 'Invalid Date' || isNaN(gregorianDate || !gregorianDate.getDate())) {
            gregorianDate = new Date();
        }
        jalaliDate = gregorian_to_jalali([
            gregorianDate.getFullYear(),
            gregorianDate.getMonth(),
            gregorianDate.getDate()]);
        return this;
    }

    this.getGregorianDate = function() { return gregorianDate; }

    this.setFullDate = setFullDate;

	this.setMonth = function(e) {
		jalaliDate[1] = e;
        var g = jalali_to_gregorian(jalaliDate);
        gregorianDate = new Date(g[0], g[1], g[2]);
        jalaliDate = gregorian_to_jalali([g[0], g[1], g[2]]);
	}

    this.setDate = function(e) {
        jalaliDate[2] = e;
        var g = jalali_to_gregorian(jalaliDate);
        gregorianDate = new Date(g[0], g[1], g[2]);
        jalaliDate = gregorian_to_jalali([g[0], g[1], g[2]]);
    };

    this.getFullYear = function() { return jalaliDate[0]; };
    this.getMonth = function() { return jalaliDate[1]; };
    this.getDate = function() { return jalaliDate[2]; };
    this.toString = function() { return jalaliDate.join(',').toString(); };
    this.getDay = function() { return gregorianDate.getDay(); };
    this.getHours = function() { return gregorianDate.getHours(); };
    this.getMinutes = function() { return gregorianDate.getMinutes(); };
    this.getSeconds = function() { return gregorianDate.getSeconds(); };
    this.getTime = function() { return gregorianDate.getTime(); };
    this.getTimeZoneOffset = function() { return gregorianDate.getTimeZoneOffset(); };
    this.getYear = function() { return jalaliDate[0] % 100; };

    this.setHours = function(e) { gregorianDate.setHours(e) };
    this.setMinutes = function(e) { gregorianDate.setMinutes(e) };
    this.setSeconds = function(e) { gregorianDate.setSeconds(e) };
    this.setMilliseconds = function(e) { gregorianDate.setMilliseconds(e) };
}
