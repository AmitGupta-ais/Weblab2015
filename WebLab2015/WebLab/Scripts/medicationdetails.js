
function fillminuteOO() {
    for (i = 1; i < 50; i++) {
   
        var uctxtt1 = "Uc_MedicationandInfu" + i + "_txtt1";
        var uctxtt2 = "Uc_MedicationandInfu" + i + "_txtt2";
        var uctxtt3 = "Uc_MedicationandInfu" + i + "_txtt3";
        var uctxtt4 = "Uc_MedicationandInfu" + i + "_txtt4";
        var gy = document.getElementById(uctxtt1).value;
        if (gy.length>0) {
           var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxtt1).value = ft[0] + ":00"; 
            }

        }

        var gy = document.getElementById(uctxtt2).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxtt2).value = ft[0] + ":00";
            }

        }

        var gy = document.getElementById(uctxtt3).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxtt3).value = ft[0] + ":00";
            }

        }
        
        var gy = document.getElementById(uctxtt4).value;
        if (gy.length > 0) {
            var ft = gy.split(":");
            if (ft[1] == "__") {
                document.getElementById(uctxtt4).value = ft[0] + ":00";
            }

        }



       
       
    }
}

function currenttime() {
    var cdate = new Date()
    var ctime = cdate.getHours() + ":" + cdate.getMinutes();
   
    var nlength = 0;

    for (i = 1; i < 50; i++) {
  
      var uctxtn1 ="Uc_MedicationandInfu" + i + "_txtn1";
      var uctxtn2 = "Uc_MedicationandInfu" + i + "_txtn2";
      var uctxtn3 = "Uc_MedicationandInfu" + i + "_txtn3";
      var uctxtn4 = "Uc_MedicationandInfu" + i + "_txtn4";

      var uctxtt1 = "Uc_MedicationandInfu" + i + "_txtt1";
      var uctxtt2 = "Uc_MedicationandInfu" + i + "_txtt2";
      var uctxtt3 = "Uc_MedicationandInfu" + i + "_txtt3";
      var uctxtt4 = "Uc_MedicationandInfu" + i + "_txtt4";

      if (document.getElementById(uctxtn1).value != "") {
       if(document.getElementById(uctxtt1).value =="")
          {
              document.getElementById(uctxtt1).value = ctime;
          } 

      }

      if (document.getElementById(uctxtn2).value != "") {
          if (document.getElementById(uctxtt2).value == "") {

              document.getElementById(uctxtt2).value = ctime;
          }
      }
      if (document.getElementById(uctxtn3).value != "") {
          if (document.getElementById(uctxtt3).value == "") {

              document.getElementById(uctxtt3).value = ctime;
          }
      }
      if (document.getElementById(uctxtn4).value != "") {
          if (document.getElementById(uctxtt4).value == "") {
             document.getElementById(uctxtt4).value = ctime;
          }
      }
         
  }


} 