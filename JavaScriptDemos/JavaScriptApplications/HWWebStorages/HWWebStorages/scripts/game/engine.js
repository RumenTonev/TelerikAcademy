define(['game/game', 'game/storage', 'game/ui', 'lib/jquery'], function (game, storage, ui) {
    var engine = (function () {
        function run() {
            var generatedNumber = game.generateNumber(),
                tries = 0,
                button = $('#check');
          
            button.click(function () {
                var input = ui.getGivenNumber(),
                    currentResult,
                    nick,
                    topScores;

              

                if ((game.validateNumber(input))) {
                    currentResult = game.getCurrentResult(generatedNumber, input);
                    ui.showCurrentResult(currentResult);
                    tries++;

                    //if the number is guessed
                    if (currentResult.ram === 4) {
                        ui.finalMessage(tries);
                        nick = ui.getNickname();
                        storage.addScore(nick, tries);
                        topScores = storage.getScores();
                        ui.fillHighScores(topScores);
                        $('#highScores').css('display', 'block');
                        playAgain();
                    }

                }
            });

            function playAgain() {
                var again = $('<button>').attr('id', 'playAgain').text('Play again');
                $('#wrapper').append(again);
                again.click(function () {
                    location.reload();
                });
            };

        }
        return {
            run: run
        }
    })();

    return engine;
})