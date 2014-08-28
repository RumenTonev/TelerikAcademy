/// <reference path="_reference.js" />
var snakes = (function () {
    //tova moge i v constructora na Snake
   // var snakePartSize = 15;
    var canvas = document.getElementById("snake-canvas");
   
    //s cel nasledqvane na koordinatite ot vsi4ki-ponege sa ob6ti,Snake moge i bez 
    function GameObject(x, y,size) {
        this._x = x;
        this._y = y;
        this._size = size;//bez zna4enie 4e se realizira po razli4en na4in
    }
    GameObject.prototype = {
        getPosition: function () {
            return {
                x: this._x,
                y: this._y
            };
        },
        getSize: function () {
            return this._size;

        }
    };
   
    function SnakePart(x, y, size) {      
        GameObject.call(this, x, y, size);
    }   
    
    SnakePart.prototype = new GameObject();
    SnakePart.prototype.constructor = SnakePart;
    
    SnakePart.prototype.changePosition = function (x, y) {
        this._x = x;
        this._y = y;
    }

    function SnakeHead(x, y, size) {
        SnakePart.call(this, x, y, size);
        this.IsMouthOpen = false;
       
    }
    SnakeHead.prototype = new SnakePart();
    SnakeHead.prototype.constructor = SnakeHead;
  
    function Snake(x, y, size,snakePartSize) {
        GameObject.call(this, x, y, size);
        var part = null,
            partX,
            par2,
            partY;
        this._snakePartSize = snakePartSize;
        this.parts = [];
        this._directions = [
       { dx: 0, dy: -1 },
       { dx: +1, dy: 0 },
       { dx: 0, dy: 1 },
       { dx: -1, dy: 0 }
        ];
        this._direction = 1;
        part = new SnakeHead(x, y, 2*this._snakePartSize);
        this.parts.push(part);      
        for (var i = 1; i < size; i++) {
            partX = x  - i * this._snakePartSize;
            partY=y;
            part = new SnakePart(partX, partY, this._snakePartSize);
            this.parts.push(part);
        }
    }
    Snake.prototype = new GameObject();
    Snake.prototype.constructor = Snake;
    Snake.prototype.getDirections = function () {
        return this._directions;
    }
    Snake.prototype.head=function(){
        return this.parts[0];
    }
    Snake.prototype.getHeadSize = function () {
        return this._snakePartSize*2;
    }
    Snake.prototype.getPartSize = function () {
        return this._snakePartSize;
    }
    Snake.prototype.changeDirection = function (newDirection) {
        
        this._direction = newDirection;
    }
    Snake.prototype.getDirection = function () {

        return this._direction;
    }
    Snake.prototype.addPart= function (size) {
        var newPart = new SnakePart(100, 100, size);
        this.parts.push(newPart);
    }
    Snake.prototype.move = function () {
        var x, y;
        for (var i = this.parts.length - 1; i >= 1; i--) {
            var position = this.parts[i - 1].getPosition();
            this.parts[i].changePosition(position.x,position.y);

        }
        var head = this.head();
        var dx = this._directions[this._direction].dx;
        var dy = this._directions[this._direction].dy;
        var headPosition = head.getPosition();
        var newHeadPosition = {
            x: headPosition.x + this._snakePartSize * dx,
            y: headPosition.y + this._snakePartSize * dy,
        };
        head.changePosition(newHeadPosition.x,newHeadPosition.y);
    }

    function WallPart(x,y,size) {
        GameObject.call(this, x, y, size);
    }
    WallPart.prototype = new GameObject();
    WallPart.prototype.constructor = WallPart;

    function Wall(x, y, size) {
        GameObject.call(this, x, y, size);
        this.elements = [];
        seedWallElements(this.elements, canvas,x,y,size);
    }
    Wall.prototype = new GameObject();
    Wall.prototype.constructor = Wall;
    //private use
    function seedWallElements(container, canvas,xstart,ystart,size) {
        //up
        for (var i = xstart; i <= canvas.width -( size+1); i = i + size) {
            var part = new WallPart(i, ystart, size);
            container.push(part);
        }
        //down
        for (var i = xstart; i <= canvas.width - (size + 1) ; i = i + size) {
            var part = new WallPart(i, canvas.width - (size + 1), size);
            container.push(part);
        }
        //left
        for (var i = ystart + size; i <= canvas.height - (2 * size + 1) ; i = i + size) {
            var part = new WallPart(xstart, i, size);
            container.push(part);
        }
        //right
        for (var i = ystart + size; i <= xstart + canvas.height - (2 * size + 1) ; i = i + size) {
            var part = new WallPart(canvas.width - (size+1), i, size);
            container.push(part);
        }      
    } 

    function Food(size) {
        this.parts = [];
        this.size = size;
    }
   
    Food.prototype.addPart = function (size) {
        var foodPart = new FoodPart(size);
        this.parts.push(foodPart);
    }

    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    };

    //TODO with requestAnimationFrame


    function FoodPart(size) {
        this._x = getRandomInt(size + 1, canvas.offsetWidth - 2 * size + 1);
        this._y = getRandomInt(size + 1, canvas.offsetWidth - 2 * size + 1);
        GameObject.call(this, this._x, this._y, size);
       
    }
    FoodPart.prototype = new GameObject();
    FoodPart.prototype.constructor = FoodPart;

    return {        
        get: function (x,y,size,partSize) {
            return new Snake(x,y,size,partSize);
        },
        getFood: function (size) {
            return new Food(size);
        },
        
        getWall: function (x, y, size) {
            return new Wall(x, y, size);
        },
      
        SnakeType: Snake,//only for type using
        SnakePartType: SnakePart,
        SnakeHeadType:SnakeHead,
        FoodType: Food,
        FoodPartType:FoodPart,
        WallType: Wall,
        WallPartType:WallPart
    };
}());

