

function fillminOOLT1() {
    for (i = 1; i < 25; i++) {

         var uctxttime1 = "UC_leftpanel" + i + "_txtTime1";
         var gy = document.getElementById(uctxttime1).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxttime1).value = ft[0] + ":00";
            }

        }
    }
}

function fillminOOLT2() {
    for (i = 1; i < 25; i++) {

        var uctxttime2 = "UC_leftpanel" + i + "_txttime2";
        var gy = document.getElementById(uctxttime2).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxttime2).value = ft[0] + ":00";
            }

        }
    }
}

function fillminOORT1() {
    for (i = 1; i < 25; i++) {

        var uctxttime1 = "UC_rightpanel" + i + "_txtTime1";
        var gy = document.getElementById(uctxttime1).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxttime1).value = ft[0] + ":00";
            }

        }
    }
}








function updateleftpanel() {
   currenttime1();
   currenttime2();
        var totalleft = 0;
            var tleft = document.getElementById("UC_leftpanel1_txtInfuQty").value;
            totalleft  = totalleft*1 +tleft*1 ;
            tleft = document.getElementById("UC_leftpanel1_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel2_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel2_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel3_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel3_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel4_txtInfuQty").value;
//            if (isNaN(tleft)) {
//            }
                totalleft = totalleft * 1 + tleft * 1;

            
            tleft = document.getElementById("UC_leftpanel4_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel5_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel5_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel6_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel6_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel7_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel7_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel8_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel8_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

//            var FF = document.getElementById("UC_leftpanel9_txtInfuQty");
//            if (FF != undefined) {
//            }
             var tleft = document.getElementById("UC_leftpanel9_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            
           
            tleft = document.getElementById("UC_leftpanel9_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel10_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel10_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel11_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel11_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel12_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel12_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel13_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel13_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel14_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel14_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel15_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel15_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel16_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel16_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel17_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel17_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel18_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel18_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel19_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel19_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel20_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel20_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel21_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel21_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel22_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel22_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel23_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel23_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;


            var tleft = document.getElementById("UC_leftpanel24_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel24_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            var tleft = document.getElementById("UC_leftpanel25_txtInfuQty").value;
            totalleft = totalleft * 1 + tleft * 1;
            tleft = document.getElementById("UC_leftpanel25_txtMediQty").value;
            totalleft = totalleft * 1 + tleft * 1;

            document.getElementById("lbltotalin").innerText = totalleft.toString();
            


        }


   
  function updaterightpanel() {
      currentRtime1();
        var totalright = 0;
       
            var tright = document.getElementById("UC_rightpanel1_txtUrine").value;
            totalright  = totalright*1 +tright*1 ;

            var tright = document.getElementById("UC_rightpanel2_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           


            var tright = document.getElementById("UC_rightpanel3_txtUrine").value;
            totalright = totalright * 1 + tright * 1;

            var tright = document.getElementById("UC_rightpanel4_txtUrine").value;
            totalright = totalright * 1 + tright * 1;


            var tright = document.getElementById("UC_rightpanel5_txtUrine").value;
            totalright = totalright * 1 + tright * 1;

            var tright = document.getElementById("UC_rightpanel6_txtUrine").value;
            totalright = totalright * 1 + tright * 1;


            var tright = document.getElementById("UC_rightpanel7_txtUrine").value;
            totalright = totalright * 1 + tright * 1;

            var tright = document.getElementById("UC_rightpanel8_txtUrine").value;
            totalright = totalright * 1 + tright * 1;


            var tright = document.getElementById("UC_rightpanel9_txtUrine").value;
            totalright = totalright * 1 + tright * 1;

            var tright = document.getElementById("UC_rightpanel10_txtUrine").value;
            totalright = totalright * 1 + tright * 1;


            var tright = document.getElementById("UC_rightpanel11_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel12_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel13_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel14_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel15_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel16_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel17_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel18_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel19_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel20_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel21_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel22_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel23_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           

            var tright = document.getElementById("UC_rightpanel24_txtUrine").value;
            totalright = totalright * 1 + tright * 1;
           
            var tright = document.getElementById("UC_rightpanel25_txtUrine").value;
            totalright = totalright * 1 + tright * 1;

            document.getElementById("lbltotalout").innerText = totalright.toString();
             
            
 
        }




        function currenttime1() {
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
            for (i = 1; i < 25; i++) {
                var uctxt1 = "UC_leftpanel" + i + "_txtInfusion";
                var uctxt2 = "UC_leftpanel" + i + "_txtTime1";
                var uctxt3 = "UC_leftpanel" + i + "_txtInfuQty";
                

                if (document.getElementById(uctxt1).value != "" || document.getElementById(uctxt3).value!="") {
                    if (document.getElementById(uctxt2).value == "") {
                        document.getElementById(uctxt2).value = ctime;
                    }
                }
            }

        }

        function numbervalidate(e) {
            
            
            
            //var cd = window.event.keyChar;

            if ((e.keyCode >= 48) && (e.keyCode <= 57)) {
                return true;
            }
            else {

                return false;
                
            }
        
        
        }
        function currenttime2() {
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
            //var ctime = cdate.getHours() + ":" + cdate.getMinutes();
            for (i = 1; i < 25; i++) {
                var uctxt1 = "UC_leftpanel" + i + "_txtmediName";
                var uctxt2 = "UC_leftpanel" + i + "_txttime2";
                var uctxt3 = "UC_leftpanel" + i + "_txtMediQty";

                if (document.getElementById(uctxt1).value != "" || document.getElementById(uctxt3).value != "") {
                    if (document.getElementById(uctxt2).value == "") {
                        document.getElementById(uctxt2).value = ctime;
                    }
                }
            }

        }


        function currentRtime1() {
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
            //var ctime = cdate.getHours() + ":" + cdate.getMinutes();
            for (i = 1; i < 25; i++) {
                var uctxt1 = "UC_rightpanel" + i + "_txtUrine";
                var uctxt2 = "UC_rightpanel" + i + "_txtStomuchColor";
                var uctxt3 = "UC_rightpanel" + i + "_txtContentQuentity";
                var uctxt4 = "UC_rightpanel" + i + "_txtstoolAmt";
                var uctxt5 = "UC_rightpanel" + i + "_txtStoolRemarks";

                var uctxt6 = "UC_rightpanel" + i + "_txtTime1";

                if (document.getElementById(uctxt1).value != "" || document.getElementById(uctxt2).value != "" || document.getElementById(uctxt3).value != "" || document.getElementById(uctxt4).value != "" || document.getElementById(uctxt5).value != "") {
                    if (document.getElementById(uctxt6).value == "") {
                        document.getElementById(uctxt6).value = ctime;
                    }
                }
            }

        }