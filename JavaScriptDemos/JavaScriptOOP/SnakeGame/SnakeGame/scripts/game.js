/// <reference path="_reference.js" />
var games = (function () {

   

    function seedFood() {
        theFood.addPart(theFood.size);
    }

    function checkWallOrBodyPartInSnakeHeadArea(x, y) {
        var snakeHeadPosition = theSnake.head().getPosition();
        var snakeHeadSize = theSnake.head().getSize();
        if (snakeHeadPosition.x < x && x < snakeHeadPosition.x + snakeHeadSize && snakeHeadPosition.y < y && y < snakeHeadPosition.y + snakeHeadSize) {
            return true;
        }
        return false;
    }
 
    //check in next position in order to open snake mouth
    function checkFoodInSnakeHeadArea(x, y) {
        {
            var currentHeadPosition = theSnake.head().getPosition();
            var currentHeadSize = theSnake.head().getSize();
            var directions = theSnake.getDirections();
            var direction = theSnake.getDirection();
            var dx = directions[direction].dx;
            var dy = directions[direction].dy;
            var newHeadPosition = {
                x: currentHeadPosition.x + currentHeadSize * dx,
                y: currentHeadPosition.y + currentHeadSize * dy,
            };

            if (newHeadPosition.x < x && x < newHeadPosition.x + currentHeadSize && newHeadPosition.y < y && y < newHeadPosition.y + currentHeadSize) {
                return true;
            }
            return false;
        }
    }

    function checkWallAndSnakeBodyPosition(position, size) {
        var result = false;
        if (checkWallOrBodyPartInSnakeHeadArea(position.x, position.y)) { result = true; }
        else if (checkWallOrBodyPartInSnakeHeadArea(position.x + size, position.y)) { result = true; }
        else if (checkWallOrBodyPartInSnakeHeadArea(position.x, position.y + size)) { result = true; }
        else if (checkWallOrBodyPartInSnakeHeadArea(position.x + size, position.y + size)) { result = true; }
        return result;
    }

    function checkFoodPosition(position, size) {
        var result = false;
        if (checkFoodInSnakeHeadArea(position.x, position.y)) { result = true; }
        else if (checkFoodInSnakeHeadArea(position.x + size, position.y)) { result = true; }
        else if (checkFoodInSnakeHeadArea(position.x, position.y + size)) { result = true; }
        else if (checkFoodInSnakeHeadArea(position.x + size, position.y + size)) { result = true; }
        return result;
    }

    function sortByScore(st1, st2) {
        return st2._score - st1._score;
    }

    function setEndTableGame(){
        var name = window.prompt("GameOver", "anonymous");
        var arr = localStorage.getArray("GameTable");
        if (typeof name === 'undefined') { name = "anonymous"; }
        var arrayItem = new user.getUser(name, sessionStorage.eatenFood);
        arr.push(arrayItem);
        arr.sort(sortByScore);
        localStorage.setItem("GameTable", JSON.stringify(arr));
        window.location.href = ("finalePage.html");
       
    }

    function checkForEnd() {
        var snakePartSize = theSnake.getPartSize();
        var wallPartSize = theWall.getSize();
        for (var i = 0; i < theWall.elements.length; i++) {
            var wallPartPosition = theWall.elements[i].getPosition();
            if (checkWallAndSnakeBodyPosition(wallPartPosition, wallPartSize)) {
                
                setEndTableGame.call(this);
                return;
            }
        }
        for (var i = 3; i < theSnake.parts.length; i++) {
            var snakePartPosition = theSnake.parts[i].getPosition();
            if (checkWallAndSnakeBodyPosition(snakePartPosition, snakePartSize*2)) {
                setEndTableGame.call(this);
                return;
            }
        }
    }

    function checkForFood() {
        var size = theFood.size;
        for (var i = 0; i < theFood.parts.length; i++) {
            var foodPartPosition = theFood.parts[i].getPosition();
            if (checkFoodPosition(foodPartPosition, size)) {
                theSnake.addPart(theSnake.getPartSize());
                theSnake.head().IsMouthOpen = true;
                theFood.parts.splice(i, 1);
              sessionStorage.eatenFood= parseInt(sessionStorage.eatenFood) + 100;
            }
        }
    }

    function animationFrame() {
        
       checkForFood.call(this);
        checkForEnd();
        theSnake.move();
        theRenderer.clear();
        theRenderer.draw(theWall);
        theRenderer.draw(theFood);       
        theRenderer.draw(theSnake);
        theSnake.head().IsMouthOpen = false;
        if (theGame.state === "running") {
            setTimeout(animationFrame, 400);
        }
        
    }
    var theSnake;
    var theRenderer;
    var theFood;
    var theWall;


    function Game(renderer, wallStartX, wallStartY, wallBrickSize, snakeStartX, snakeStartY, snakeSize, snakePartSize, snakeFoodSize) {
        this.renderer = renderer;
        this.wall = new snakes.getWall(wallStartX, wallStartY, wallBrickSize);
        this.snake = new snakes.get(snakeStartX, snakeStartY, snakeSize, snakePartSize);
        this.food = new snakes.getFood(snakeFoodSize);
        this.state = "stopped";
        this.user = new user.getUser("anonymous", 0);
    }

    Game.prototype = {
        start: function () {
            theGame = this;
            theSnake = this.snake;
            theRenderer = this.renderer;
            theFood = this.food;
            theUser = this.user;          
            setInterval(seedFood, 5000);
            sessionStorage.eatenFood = 0;
            window.addEventListener("keydown", function (e) {
                var e = e || window.event;
                var currentDirection = theSnake.getDirection();
                if (currentDirection % 2 != e.keyCode % 2) {
                    if (e.keyCode == 40) { theSnake.changeDirection(2); }
                    else if (e.keyCode == 39) { theSnake.changeDirection(1); }
                    else if (e.keyCode == 38) { theSnake.changeDirection(0); }
                    else if (e.keyCode == 37) { theSnake.changeDirection(3); }
                }
            }, false);
            theWall = this.wall;
            requestAnimationFrame(animationFrame);
            this.state = "running";
        },
        resume:function(){
            theGame.state = "running";
            requestAnimationFrame(animationFrame);
        },
        stop: function () {
            theGame.state = "stopped";
        }

    }
    return {
        get: function (renderer, wallStartX, wallStartY, wallBrickSize, snakeStartX, snakeStartY,snakeSize,snakePartSize, snakeFoodSize) {
            return new Game(renderer, wallStartX, wallStartY, wallBrickSize, snakeStartX, snakeStartY,snakeSize, snakePartSize, snakeFoodSize);
        }
    };
}());