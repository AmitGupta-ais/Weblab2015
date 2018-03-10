function aiskeycode() {

    if (window.event.keyCode == 13) window.event.keyCode = 9; if (window.event.keyCode == 113) document.getElementById('<%= btn_Save.ClientID %>').click(); if (window.event.keyCode == 27) document.getElementById('btn_cancel').click(); ;
}
