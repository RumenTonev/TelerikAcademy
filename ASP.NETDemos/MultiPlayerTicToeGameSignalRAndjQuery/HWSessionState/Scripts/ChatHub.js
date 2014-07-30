var Game = (function () {

    function Game(){
        
    }

    Game.startNewGame = function (chat) {
       // var chat = $.connection.chatHub;
        //var chat = new ChatHub();
        //var chat = chatObject.getConnection();
        // chat.startConnection;
       // var tempArray = [];
        // Create a function that the hub can call back to display messages.


        //vzimame kliknata snimka promenqme ia i go razpra6tame do vsi4ki klienti sys server metoda sens
        chat.client.changePicOnClickandMarkAsSuch = function (id, newSource) {
            var r = "#".concat(id);
            $(r).attr("src", newSource);
            $(r).addClass("clicked");
        };
        //END

        //pri pobeda razpratme syob6tenie do vsi4ki s metoda Win
        chat.client.weHaveWinner = function (message) {
            window.alert(message);
            if (!window.location.origin) {
                window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
            }
            window.location.replace(window.location.origin.concat("/Default"));
        };
        //END

        //enable button ok on new turn
        chat.client.enableButtonOnNewTurn = function () {
            modalWindow.enableOkButton();
            $("#button-ok").button("enable");

        };
    };

  








    return Game;
}());

//return only a reference to the function

