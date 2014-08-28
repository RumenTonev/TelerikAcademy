define(['Controllers/Check-controller', 'http-requester'], function (CheckController, HttpRequester) {
    var MainController = (function () {
        var url = 'http://crowd-chat.herokuapp.com/posts';

        var $container = $('#container');

        var lastMessages = 0;
        var messagesAfterUpdate = 0;
        var interval;

        function animateScrolling() {
            $("#container-for-messages").animate({ scrollTop: $("#container-for-messages").prop("scrollHeight") }, 1500);
        }

        function updateWholeUi() {
            var chatbox = $('#container-for-messages');

            var list;

            HttpRequester.getJSON(url, function (data) {
                messagesAfterUpdate = data.length;

                if (messagesAfterUpdate > lastMessages) {
                    for (var i = lastMessages; i < messagesAfterUpdate; i++) {
                        list =
                            '<li>' +
                            '<span>' + data[i].text + '</span>' +
                            '<div>By: ' + data[i].by + '</div>' +
                            '</li>'
                        chatbox.append(list);
                    }

                    animateScrolling();

                    lastMessages = messagesAfterUpdate;
                }
            })
        }

        function loadUI() {
            if (CheckController.isUserLoggedIn) {
                loadGameUI();
            }
            else {
                loadLoginFormUi();
            }

            attachEventHandlers();
        }

        function updateMessageBoxUi() {
            var chatbox = $('#container-for-messages');
            var message = $('#field-for-message').val();
            var username = localStorage.getItem('username');

            var list =
                '<li>' +
                '<span>' + message + '</span>' +
                '<div>By: ' + username + '</div>' +
                '</li>'
            chatbox.append(list);

            animateScrolling();

            $('#field-for-message').val('');
            $('#field-for-message').focus();
        }

        function loadGameUI() {
            var userNickname = localStorage.getItem('username');

            $('#container').load('Partials/loggedIn.html', function () {
                $('#nickname').append(userNickname);
            });

            lastMessages = 0;
            interval = setInterval(updateWholeUi, 500);
        }

        function loadLoginFormUi() {
            clearInterval(interval);

            $('#container-for-nickname-and-logout').remove();
            $('#chatbox').remove();

            $container.load('Partials/login.html');
        }

        function attachEventHandlers() {
            $container.on('click', '#logout-button', function () {
                localStorage.removeItem('username');
                loadLoginFormUi();
            });

            $container.on('click', '#ready-with-nickname', function () {
                var username = $('#input-nickname').val();

                if (CheckController.isInputValid(username)) {
                    username.trim();
                    localStorage.setItem('username', username);
                    loadGameUI();
                }
                else {
                    $('#input-nickname').val('Enter a valid username');
                    $('#input-nickname').focus();
                }
            });

            $container.on('click', '#send', function () {
                var text = $('#field-for-message').val();

                if (CheckController.isInputValid(text)) {
                    text.trim();
                    var userName = localStorage.getItem('username');

                    var data = {
                        user: userName,
                        text: text
                    }

                    HttpRequester.postJSON(url, data, function () {
                        lastMessages++;
                        updateMessageBoxUi();
                    }, function () {
                        $('#field-for-message').val('Error occurred');
                        $('#field-for-message').focus();
                    });
                }
                else {
                    $('#field-for-message').val('Enter a valid message');
                    $('#field-for-message').focus();
                }
            });
        }

        return {
            loadUI: loadUI
        }
    }());

    return MainController;
})