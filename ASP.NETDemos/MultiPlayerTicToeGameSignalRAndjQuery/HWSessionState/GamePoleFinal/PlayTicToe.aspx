<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayTicToe.aspx.cs" Inherits="HWSessionState.GamePoleFinal.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/jquery-1.10.2.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script src="../Scripts/jquery.signalR-2.0.3.js"></script>

    <script src="../signalr/hubs"></script>
    <script src="../Scripts/ModalWindow.js"></script>

    <script src="../Scripts/ChatHub.js"></script>
    <h2>Chat</h2>
    <div class="container">

        <input type="image" src="fuc1.jpg" width="100" height="100" id="0" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="1" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="2" value="Send" />
        <br />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="3" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="4" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="5" value="Send" />
        <br />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="6" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="7" value="Send" />
        <input type="image" src="fuc1.jpg" width="100" height="100" id="8" value="Send" />

        <input type="hidden" id="displayname" />

    </div>
    <div id="dialog" title="Basic dialog">
        <p>Wait for another player's turn till 'ok' button is enabled</p>
    </div>
    <script>

        var groupname = GetUrlValue('id');
        var pictureLetter = GetUrlValue('Text');
        var newSource = "~/NorthWindForm.img?Text=".concat(pictureLetter);
        var successValuesArray = GetArraySuccessValues();
        var currentPermutationHolder = new Array();
        var checkForWinner = true;
        var modalWindow = new Modal();
        var messageTie = "We have a tie";
        var name = '<%=this.User.Identity.Name%>';
    var messageWinner = "Winnner is " + name;
    var chat = $.connection.chatHub;
    var clickedIdsForPlayer = [];
    $(function () {
        modalWindow.disableOkButton();
        //vzimame kliknata snimka promenqme ia i go razpra6tame do vsi4ki klienti sys server metoda sens
        chat.client.changePicOnClickandMarkAsSuch = function (id, newSource) {
            var r = "#".concat(id);
            $(r).attr("src", newSource);
            $(r).addClass("clicked");
        };
        ////END

        ////pri pobeda razpratme syob6tenie do vsi4ki s metoda Win
        chat.client.weHaveWinner = function (message) {
            window.alert(message);
            if (!window.location.origin) {
                window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
            }
            window.location.replace(window.location.origin.concat("/Default"));
        };
        ////END

        ////enable button ok on new turn
        chat.client.enableButtonOnNewTurn = function () {
            modalWindow.enableOkButton();
            $("#button-ok").button("enable");
        };
        //END

        // Start the connection.
        $.connection.hub.start().done(function () {
            //klienta vliza v grupa na servera s ime ID-to
            chat.server.joinGroup(groupname);
            //deklarirame click funkciata na vsi`ki snimki
            $("[type='image']").click(function (event) {
                var self = this;
                if (!$(this).hasClass("clicked")) {
                    GameLoop(self);
                }
                return false;
            });
        });
    });
    // This optional function html-encodes messages for display in the page.
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }
    function GetUrlValue(VarSearch) {
        var SearchString = window.location.search.substring(1);
        var VariableArray = SearchString.split('&');
        for (var i = 0; i < VariableArray.length; i++) {
            var KeyValuePair = VariableArray[i].split('=');
            if (KeyValuePair[0] == VarSearch) {
                return KeyValuePair[1];
            }
        }
    }

    function GameLoop(self) {
        //check after clicked for a player are more than 2
        if (clickedIdsForPlayer.length >= 2) {
            currentPermutationHolder.push(self.id);
            for (var i = 0; i <= clickedIdsForPlayer.length - 1; i++) {
                currentPermutationHolder.push(clickedIdsForPlayer[i]);
                for (var k = i + 1; k <= clickedIdsForPlayer.length - 1; k++) {
                    currentPermutationHolder.push(clickedIdsForPlayer[k]);
                    var currentPermutationHolderDeepCopySorted = currentPermutationHolder.slice(0);
                    currentPermutationHolderDeepCopySorted.sort();
                    for (var m = 0; m <= 7; m++) {
                        checkForWinner = true;
                        for (var l = 0; l <= 2; l++) {
                            if (currentPermutationHolderDeepCopySorted[l] != successValuesArray[m][l]) {
                                checkForWinner = false; break;
                            }
                        }
                        if (checkForWinner == true) {
                            chat.server.sendDb(groupname, name)
                            chat.server.win(groupname, messageWinner, event.target.id, newSource);
                            return false;
                        }
                    } currentPermutationHolder.pop();
                } currentPermutationHolder.pop();
            } currentPermutationHolder.pop();
        }
        //o6te ne sme pushnali teku6ttoto
        if (clickedIdsForPlayer.length == 4) {
            chat.server.sendDb(groupname, "tie");
            chat.server.win(groupname, messageTie, event.target.id, newSource); return false;
        }
        clickedIdsForPlayer.push(self.id);
        chat.server.send(event.target.id, groupname, newSource);
        modalWindow.setModalVisible();
        modalWindow.disableOkButton();
        modalWindow.openModal();
    }
    function GetArraySuccessValues() {

        var successValuesArray = new Array(8);
        successValuesArray[0] = new Array(3);

        successValuesArray[0][0] = "0";

        successValuesArray[0][1] = "1";
        successValuesArray[0][2] = "2";
        successValuesArray[1] = new Array(3);

        successValuesArray[1][0] = "3";

        successValuesArray[1][1] = "4";
        successValuesArray[1][2] = "5";
        successValuesArray[2] = new Array(3);

        successValuesArray[2][0] = "6";

        successValuesArray[2][1] = "7";
        successValuesArray[2][2] = "8";
        successValuesArray[3] = new Array(3);
        successValuesArray[3][0] = "0";

        successValuesArray[3][1] = "3";
        successValuesArray[3][2] = "6";
        successValuesArray[4] = new Array(3);

        successValuesArray[4][0] = "1";

        successValuesArray[4][1] = "4";
        successValuesArray[4][2] = "7";
        successValuesArray[5] = new Array(3);

        successValuesArray[5][0] = "2";

        successValuesArray[5][1] = "5";
        successValuesArray[5][2] = "8";
        successValuesArray[6] = new Array(3);
        successValuesArray[6][0] = "0";

        successValuesArray[6][1] = "4";
        successValuesArray[6][2] = "8";
        successValuesArray[7] = new Array(3);

        successValuesArray[7][0] = "2";

        successValuesArray[7][1] = "4";
        successValuesArray[7][2] = "6";
        return successValuesArray;
    }
    </script>
</asp:Content>
