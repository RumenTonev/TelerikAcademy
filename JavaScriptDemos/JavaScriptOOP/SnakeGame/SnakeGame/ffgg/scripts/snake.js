/// <reference path="_reference.js" />
var snakes = (function () {
    //tova moge i v constructora na Snake
    var snakePartSize = 15;

    var directions = [
        { dx: 0, dy: -1 },
        { dx: +1, dy: 0 },
        { dx: 0, dy: 1 },
        { dx: -1, dy: 0 }
    ];
    //s cel nasledqvane na koordinatite ot vsi4ki-ponege sa ob6ti,Snake moge i bez 
    function GameObject(x, y,size) {
        this.x = x;
        this.y = y;
        this.size = size;//bez zna4enie 4e se realizira po razli4en na4in
    }
    GameObject.prototype = {
        getPosition: function () {
            return {
                x: this.x,
                y: this.y
            };
        },
        getSize: function () {
            return this.size;

        }
    };
   
    function SnakePart(x, y, size) {
        //vikame star6ia konstruktor
        GameObject.call(this, x, y, size);
    }
    //sledva6tite 2 reda sa za nasledqvane
    
    SnakePart.prototype = new GameObject();
    SnakePart.prototype.constructor = SnakePart;
    //nasledqvaneto e predi popylvaneto na prototipa
    SnakePart.prototype.changePosition = function (x, y) {
        this.x = x;
        this.y = y;
    }

    function FirstSnakePart(x, y, size) {
        SnakePart.call(this, x, y, size);
    }
    FirstSnakePart.prototype = new SnakePart();
    FirstSnakePart.prototype.constructor = FirstSnakePart;
    FirstSnakePart.prototype.changePosition = function (x, y) {
        this.x = x;
        this.y = y;
    }

    function Snake(x, y, size) {
        GameObject.call(this, x, y, size);
        var part = null,
            partX,
            par2,
            partY;

        this.parts = [];
        this.direction = 1;       
        part = new FirstSnakePart(x, y, snakePartSize);
        this.parts.push(part);      
        for (var i = 1; i < size; i++) {
            partX = x  - i * snakePartSize;
            partY=y;
            part = new SnakePart(partX, partY, snakePartSize);
            this.parts.push(part);
        }
    }
    Snake.prototype = new GameObject();
    Snake.prototype.constructor = Snake;
    Snake.prototype.head=function(){
        return this.parts[0];
    }
    Snake.prototype.changeDirection = function (newDirection) {
        
        this.direction = newDirection;
    }
    Snake.prototype.move = function () {
        var x, y;
        for (var i = this.parts.length - 1; i >= 1; i--) {
            var position = this.parts[i - 1].getPosition();
            this.parts[i].changePosition(position.x,position.y);

        }
        var head = this.head();
        var dx = directions[this.direction].dx;
        var dy = directions[this.direction].dy;
        var headPosition = head.getPosition();
        var newHeadPosition = {
            x: headPosition.x + head.size * dx,
            y: headPosition.y + head.size * dy,
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
        
    }
    Wall.prototype = new GameObject();
    Wall.prototype.constructor = Wall;
    Wall.prototype.getElements = function (canvas) {      
        //tavan
        for (var i = this.x; i <= canvas.width - 11; i = i + 10) {
            var part = new WallPart(i, this.y, 10);
            this.elements.push(part);
        }
        //pod
        for (var i = this.x; i <= canvas.width - 11; i = i + 10) {
            var part = new WallPart(i, canvas.width-11, 10);
            this.elements.push(part);
        }
        //lqvo
        for (var i = this.y+10; i <=canvas.height - 21; i = i + 10) {
            var part = new WallPart(this.x, i, 10);
            this.elements.push(part);
        }
        //dqsno
        for (var i = this.y+10; i <= this.x + canvas.height - 21; i = i + 10) {
            var part = new WallPart( canvas.width-11, i, 10);
            this.elements.push(part);
        }

        return this.elements;
    }
    function Food(x, y, size) {
        GameObject.call(this, x, y, size);
    }
    Food.prototype = new GameObject();
    Food.prototype.constructor = Food;
    return {        
        get: function (x,y,size) {
            return new Snake(x,y,size);
        },
        getFood: function (x, y, size) {
            return new Food(x, y, size);
        },
        getSnakePart: function (x, y, size) {
            return new SnakePart(x, y, size);
        },
        getWall: function (x, y, size) {
            return new Wall(x, y, size);
        },
        getWallPart: function (x, y, size) {
            return new WallPart(x, y, size);
        },
        SnakeType: Snake,//za da ne se instanciira a samo da se polzva tipa
        SnakePartType: SnakePart,
        FirstSnakePartType:FirstSnakePart,
        FoodType: Food,
        WallType: Wall,
        WallPartType:WallPart
    };
}());
//dva ekvivalentni na4ina-zakomentiraniq i tozi pravqt edno i sy6to
//var theSnake = new snakes.Snake();
