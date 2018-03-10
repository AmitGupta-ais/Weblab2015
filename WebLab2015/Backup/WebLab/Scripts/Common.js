function SetAutoComplete(one,URL) {
    debugger;
    $(function () {
        $("#" + one.id).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'Code': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },

                    success: function (data) {
                        response($.map(data.d, function (item) {
                            var v = item.ID
                            return {
                                value: item.Code,
                                result: item.Code,
                                label: item.Name
                            }
                        }
                  ))
                    },
                    error: function (XMLHttpRequest, callStatus, errorThrown) {
                    }
                });
            },
            open: function (event, ui) { insideauto = true; },
            close: function (event, ui) {
                insideauto = false;
                if ($("*:focus").attr("id") == $(this).attr("id")) {
                }
                else {
                    $(this).focus();
                }

                var DisplayValue = document.getElementById("tempHdn").value.trim();
                if (DisplayValue.length > 0) {
                    $("#" + one.id).value = DisplayValue;
                    document.getElementById("tempHdn").value = '';
                    window.event.keyCode = 9;
                }
            },
            select: function (event, ui) {
                $("#" + one.id).text(ui.item.result);
                document.getElementById("tempHdn").value = ui.item.result;
            },
            minLength: 2,
            max: 20
        });
    });
}


function myConverttoDouble(objval) {
    var retval;
    retval = 0;

    if (isNaN(objval)) {
    } else {
        if (objval != null) {
            if (objval.length == 0) {
                return retval;
            }
        }
        retval = parseFloat(objval);
    }
    return retval;
}