/// <reference path="_reference.js" />
var games = (function () {

    var directions = [
        { dx: 0, dy: -1 },
        { dx: +1, dy: 0 },
        { dx: 0, dy: 1 },
        { dx: -1, dy: 0 }
    ];

    function Game(renderer) {
        this.renderer = renderer;
        this.wall = new snakes.getWall(1, 1, 300);
        //ne trqbva da se hardcodvat
        this.snake = new snakes.get(250, 250, 15);
        this.food = [];
        window.addEventListener("keydown", function (e) {
            var e = e || window.event;
            var currentDirection = theSnake.direction;
            if (currentDirection % 2 != e.keyCode % 2) {
                if (e.keyCode == 40) { theSnake.changeDirection(2); }
                else if (e.keyCode == 39) { theSnake.changeDirection(1); }
                else if (e.keyCode == 38) { theSnake.changeDirection(0); }
                else if (e.keyCode == 37) { theSnake.changeDirection(3); }
            }
        }, false);
        setInterval(dynFood, 5000);
    }

    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    };

    //TODO with requestAnimationFrame
    function dynFood() {
        Math.random
        var food = snakes.getFood(getRandomInt(11, 489), getRandomInt(11, 489), 10);
        theFood.push(food);
    }

    function checkCoord(x, y) {
        var sposition = theSnake.parts[0].getPosition();
        if (sposition.x < x && x < sposition.x + 30 && sposition.y < y && y < sposition.y + 30) {
            return true;
        }
        return false;
    }

    function checkExitCoord(x, y) {
        {
            var sposition = theSnake.parts[0].getPosition();
            var direction = theSnake.direction;
            var dx = directions[direction].dx;
            var dy = directions[direction].dy;
            var newHeadPosition = {
                x: sposition.x + 30 * dx,
                y: sposition.y + 30 * dy,
            };

            if (newHeadPosition.x < x && x < newHeadPosition.x + 30 && newHeadPosition.y < y && y < newHeadPosition.y + 30) {
                return true;
            }
            return false;
        }
    }

    function checkCoordInd(position, size) {
        var result = false;
        if (checkCoord(position.x, position.y)) { result = true; }
        else if (checkCoord(position.x + size, position.y)) { result = true; }
        else if (checkCoord(position.x, position.y + size)) { result = true; }
        else if (checkCoord(position.x + size, position.y + size)) { result = true; }
        return result;
    }

    function checkExitCoordInd(position, size) {
        var result = false;
        if (checkExitCoord(position.x, position.y)) { result = true; }
        else if (checkExitCoord(position.x + size, position.y)) { result = true; }
        else if (checkExitCoord(position.x, position.y + size)) { result = true; }
        else if (checkExitCoord(position.x + size, position.y + size)) { result = true; }
        return result;
    }

    function checkExit() {
        var wallp = theWall.elements;
        var snkp = theSnake.parts;
        for (var i = 0; i < wallp.length; i++) {
            var fposition = wallp[i].getPosition();
            if (checkCoordInd(fposition, 10)) {
                window.alert("GameOver");
                return;
            }
        }
        for (var i = 3; i < snkp.length; i++) {
            var fposition = snkp[i].getPosition();
            if (checkCoordInd(fposition, 15)) {
                window.alert("GameOver");
                return;
            }
        }
    }
    function checkBite() {
        var sposition = theSnake.parts[0].getPosition();
        for (var i = 0; i < theFood.length; i++) {
            var fposition = theFood[i].getPosition();
            if (checkExitCoordInd(fposition, 10)) {
                var newPart = snakes.getSnakePart(100, 100, 15);
                theSnake.parts.push(newPart);
                theFood.splice(i, 1);
                return true;
            }
        }
    }

    function animationFrame() {
        var bite = checkBite();
        checkExit();
        theSnake.move();
        theRenderer.clear();
        theRenderer.draw(theWall);
        for (var i = 0; i < theFood.length; i++) {
            theRenderer.draw(theFood[i]);
        }
        if (bite) {
            theRenderer.drawOpenSnake(theRenderer.canvas, theSnake);
        }
        else { theRenderer.draw(theSnake); }
        setTimeout(animationFrame, 400);
    }
    var theSnake;
    var theRenderer;
    var theFood;
    var theWall;
    Game.prototype = {
        start: function () {
            theSnake = this.snake;
            theRenderer = this.renderer;
            theFood = this.food;
            theWall = this.wall;
            requestAnimationFrame(animationFrame);
        },
        stop: function () {
            setTimeout(animationFrame, 15000);
        }

    }
    return {
        get: function (renderer) {
            return new Game(renderer);
        }
    };
}());