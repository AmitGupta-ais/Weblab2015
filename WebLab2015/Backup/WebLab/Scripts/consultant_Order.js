function currenttime() {
   // var cdate = new Date()
    //var ctime = cdate.getHours() + ":" + cdate.getMinutes();

    var cdate = new Date()
    var hh = cdate.getHours();
    var mm = cdate.getMinutes();
    if (hh < 10) {
        hh = "0" + hh;
    }
    if (mm < 10) {
        mm = "0" + mm;
    }
   var ctime = hh + ":" + mm;
    
    var nlength = 0;
    for (i = 1; i < 50; i++) {
        var uctxt1 = "consultant_order_UC" + i + "_Txt1";
        var uctxt2 = "consultant_order_UC" + i + "_Txt2";
        if (document.getElementById(uctxt2).value != "") {
            if (document.getElementById(uctxt1).value == "") {
                document.getElementById(uctxt1).value = ctime;
            }
        }
      }

 } 