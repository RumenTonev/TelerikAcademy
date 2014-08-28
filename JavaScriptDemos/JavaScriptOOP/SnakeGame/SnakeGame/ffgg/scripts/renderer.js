/// <reference path="_reference.js" />
var renderers = (function () {
    //fo private use
    var drawSnake = function (canvas, snake) {
        drawFirstSnakePart(canvas, snake.parts[0], snake.direction);
        for (var i = 1; i < snake.parts.length; i++) {
            drawSnakePart(canvas, snake.parts[i]);

        }
       
    };
    var drawOpenSnake = function (canvas, snake) {
        drawFirstSnakeOpenPart(canvas, snake.parts[0], snake.direction);
        for (var i = 1; i < snake.parts.length; i++) {
            drawSnakePart(canvas, snake.parts[i]);

        }
        
    };
    var drawSnakePart = function (canvas,part) {
        var ctx = canvas.getContext("2d");
        var position = part.getPosition();
        ctx.fillStyle = "orange";
        
        ctx.fillRect(position.x, position.y, part.size, part.size);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y, part.size, part.size);
    };
    var drawFirstSnakePart = function (canvas, part,direction) {
        var ctx = canvas.getContext("2d");
        var position = part.getPosition();
        var chX = 0;
        var chY = 0;
        var curPS = 2 * part.size;
        ctx.fillStyle = "red";
        if (direction == 1) {
            chY = -part.size / 2;

        }
        else if (direction == 0) {
            chX = -part.size / 2;
            chY = -part.size;
        }
        else if (direction == 2) {
            chX = -part.size / 2;

        }
        else if (direction == 3) {
            chX = -part.size;
            chY = -part.size / 2;
        }
        ctx.fillRect(position.x+chX, position.y+chY, curPS, curPS);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x + chX, position.y + chY, curPS, curPS);
        var centerx, centery, endx, endy;
        centerx = position.x + chX + (curPS / 2);
        centery = position.y + chY + (curPS / 2);
        if (direction == 1) {
            endx = centerx + (curPS / 2);
            endy = centery;
        }
        else if (direction == 0) {
            endx = centerx ;
            endy = centery - (curPS / 2);
        }
        else if (direction == 2) {
            endx = centerx ;
            endy = centery+ (curPS / 2);
        }
        else if (direction == 3) {
            endx = centerx - (curPS / 2);
            endy = centery ;
        }
        ctx.beginPath();
        ctx.moveTo(centerx, centery);
        ctx.lineTo(endx, endy);
        ctx.stroke();
        ctx.fill();            
    };
    var drawFirstSnakeOpenPart = function (canvas, part, direction) {
        var ctx = canvas.getContext("2d");
        var position = part.getPosition();
        var chX = 0;
        var chY = 0;
        var curPS = 2 * part.size;
        ctx.fillStyle = "red";
        if (direction == 1) {
            chY = -part.size/2;
           
        }
        else if (direction == 0) {
            chX = -part.size / 2;
            chY = -part.size ;
        }
        else if (direction == 2) {
            chX = -part.size / 2;
            
        }
        else if (direction == 3) {
            chX = -part.size ;
            chY = -part.size / 2;
        }        
        ctx.strokeStyle = "black";       
        var centerx, centery, endx, endy;
        var startx = position.x + chX;
        var starty = position.y + chY;
        var rigthx = startx + curPS;
        var rigthy = starty;
        var oppositex = rigthx;
        var oppositey = starty + curPS;
        var downx = startx;
        var downy = starty + curPS;

        centerx = position.x + chX + (curPS / 2);
        centery = position.y + chY + (curPS / 2);
        ctx.beginPath();
        ctx.moveTo(startx, starty);
        if (direction == 1) {
            ctx.lineTo(rigthx, rigthy);
            ctx.lineTo(centerx, centery);
            ctx.lineTo(oppositex, oppositey);
            
            ctx.lineTo(downx, downy);
            ctx.lineTo(startx, starty);
           
        }
        else if (direction == 0) {
            ctx.lineTo(centerx, centery);
            ctx.lineTo(rigthx, rigthy);
           
            ctx.lineTo(oppositex, oppositey);

            ctx.lineTo(downx, downy);
            ctx.lineTo(startx, starty);
        }
        else if (direction == 2) {
            
            ctx.lineTo(rigthx, rigthy);

            ctx.lineTo(oppositex, oppositey);
            ctx.lineTo(centerx, centery);
            ctx.lineTo(downx, downy);
            ctx.lineTo(startx, starty);
        }
        else if (direction == 3) {
            ctx.lineTo(rigthx, rigthy);

            ctx.lineTo(oppositex, oppositey);
           
            ctx.lineTo(downx, downy);
            ctx.lineTo(centerx, centery);
            ctx.lineTo(startx, starty);
        }
       
        
        ctx.stroke();
        ctx.fill();

    };
    var drawFood = function (canvas,food) {
        var ctx = canvas.getContext("2d");
        var position = food.getPosition();
        ctx.fillStyle = "green";
        ctx.fillRect(position.x, position.y, food.size, food.size);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y,food.size, food.size);
    };
    var drawWallPart = function (canvas, wallPart) {
        var ctx = canvas.getContext("2d");
        var position = wallPart.getPosition();
        ctx.fillStyle = "yellow";
        ctx.fillRect(position.x, position.y, wallPart.size, wallPart.size);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y, wallPart.size, wallPart.size);
    };
    var drawWall = function (canvas, wall) {
        var elements = wall.getElements(canvas);
        for (var i = 0; i < elements.length; i++) {
            drawWallPart(canvas, elements[i]);

        }
       
    };
    function CanvasRenderer(selector) {
        if (selector instanceof HTMLCanvasElement) {
            this.canvas = selector;
        }
        else if (typeof selector === "String" || typeof selector === "string") {
            this.canvas = document.querySelector(selector);
        }

    }

    ///pravim taka za da skriem metodite za risuvane na otdelnite elemneti
    CanvasRenderer.prototype = {
        draw: function (obj) {
            if (obj instanceof snakes.SnakeType) {
                drawSnake(this.canvas,obj);
            }
            else if (obj instanceof snakes.SnakePartType) {
                drawSnakePart(this.canvas, obj);
            }
            else if (obj instanceof snakes.FoodType) {
                drawFood(this.canvas, obj);
            }
            else if (obj instanceof snakes.WallType) {
                drawWall(this.canvas, obj);
            }
            else if (obj instanceof snakes.FirstSnakePartType) {
                drawFirstSnakePart(this.canvas, obj,direction);
            }
            else if (obj instanceof snakes.WallPartType) {
                drawWallPart(this.canvas, obj);
            }
        },
        drawOpenSnake: function (canvas, snake) {
        
            for (var i = 1; i < snake.parts.length; i++) {
                drawSnakePart(canvas, snake.parts[i]);

            }
            drawFirstSnakeOpenPart(canvas, snake.parts[0],snake.direction);
        },
        clear: function () {
            var ctx = this.canvas.getContext("2d");
            var w = this.canvas.width;
            var h = this.canvas.height;
            ctx.clearRect(0, 0, w, h);
    }
    }
    //drug variant
   // function DOMRenderer() {

    //}
    return {
        getCanvas: function (selector) {
            return new CanvasRenderer(selector);
        },
        
       // getDOM:function(){
          //  return new DOMRenderer();            
   // }
    };
}());