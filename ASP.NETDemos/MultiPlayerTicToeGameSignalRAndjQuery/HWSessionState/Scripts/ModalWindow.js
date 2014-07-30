 var Modal = (function () {
         function Modal() {
            var modal= document.createElement("div");
            modal.setAttribute("id", "dialog");
            $("#dialog").insertAfter("#container");
            $("#dialog").dialog({
                autoOpen: false,
                show: {
                    effect: "blind",
                    duration: 1000
                },
                hide: {
                    effect: "explode",
                    duration: 1000
                },
                buttons: [
                  {
                      id: "button-ok",
                      text: "OK",
                      click: function () {
                          $(this).dialog("close");
                      }
                  }
                ],
                dialogClass: "no-close",
            });
        };
       
        //END-dotuk deklarirame popup windowa
        //on sturt-up go disable
        //$("#button-ok").button("disable");


        Modal.prototype.disableOkButton = function () {
            $("#button-ok").button("disable");
        };
        Modal.prototype.enableOkButton = function () {
            $("#button-ok").button("enable");
        };
        Modal.prototype.setModalVisible = function () {
            $("#dialog").dialog({ modal: true, height: 500, width: 700 });
        };
        Modal.prototype.openModal = function () {
            $("#dialog").dialog("open");
        };

        return Modal;
    }());

    //return only a reference to the function
 
