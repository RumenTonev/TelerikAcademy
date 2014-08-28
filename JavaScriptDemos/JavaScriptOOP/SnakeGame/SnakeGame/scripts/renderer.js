/// <reference path="_reference.js" />
var renderers = (function () {
    //for private use
    var drawSnake = function (canvas, snake) {
        for (var i = 1; i < snake.parts.length; i++) {
            drawSnakePart(canvas, snake.parts[i]);
        }
        drawSnakeHead(canvas, snake.head(), snake.getDirection());

    };

    var drawSnakePart = function (canvas, part) {
        var ctx = canvas.getContext("2d");
        var position = part.getPosition();
        ctx.fillStyle = "orange";
        var currentSize = part.getSize();
        ctx.fillRect(position.x, position.y, currentSize, currentSize);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y, currentSize, currentSize);
    };

    var drawSnakeHead = function (canvas, part, direction) {
        var ctx = canvas.getContext("2d");
        var position = part.getPosition();
        var xAdjastmentValue = 0;
        var yAdjastmentValue = 0;
        var headActualSize = part.getSize();
        ctx.fillStyle = "red";
        if (direction == 1) {
            yAdjastmentValue = -headActualSize / 4;

        }
        else if (direction == 0) {
            xAdjastmentValue = -headActualSize / 4;
            yAdjastmentValue = -headActualSize / 2;
        }
        else if (direction == 2) {
            xAdjastmentValue = -headActualSize / 4;

        }
        else if (direction == 3) {
            xAdjastmentValue = -headActualSize / 2;
            yAdjastmentValue = -headActualSize / 4;
        }
        ctx.strokeStyle = "black";
        var centerX = position.x + xAdjastmentValue + (headActualSize / 2);
        var centerY = position.y + yAdjastmentValue + (headActualSize / 2);
        if (part.IsMouthOpen == false) {

            drawClosedMouth();
        }
        else if (part.IsMouthOpen = true) {
            drawOpenMouth();
        }
        ctx.stroke();
        ctx.fill();

        function drawClosedMouth() {
            ctx.fillRect(position.x + xAdjastmentValue, position.y + yAdjastmentValue, headActualSize, headActualSize);
            ctx.strokeRect(position.x + xAdjastmentValue, position.y + yAdjastmentValue, headActualSize, headActualSize);
            var endX, endY;
            if (direction == 1) {
                endX = centerX + (headActualSize / 2);
                endY = centerY;
            }
            else if (direction == 0) {
                endX = centerX;
                endY = centerY - (headActualSize / 2);
            }
            else if (direction == 2) {
                endX = centerX;
                endY = centerY + (headActualSize / 2);
            }
            else if (direction == 3) {
                endX = centerX - (headActualSize / 2);
                endY = centerY;
            }
            ctx.beginPath();
            ctx.moveTo(centerX, centerY);
            ctx.lineTo(endX, endY);
        }
        function drawOpenMouth() {
            var newPosition = function (x, y) {
                this.x = x; this.y = y;
            };
            var startX = position.x + xAdjastmentValue;
            var startY = position.y + yAdjastmentValue;

            var arr = [];
            arr.push(new newPosition(startX + headActualSize, startY));
            arr.push(new newPosition(startX + headActualSize, startY + headActualSize));
            arr.push(new newPosition(startX, startY + headActualSize));
            ctx.beginPath();
            ctx.moveTo(startX, startY);

            for (var i = 0; i < arr.length;) {
                if (i == direction && direction < 3) {
                    ctx.lineTo(centerX, centerY);
                }

                ctx.lineTo(arr[i].x, arr[i].y);
                i++;
                if (i == direction && direction == 3) {
                    ctx.lineTo(centerX, centerY);
                }

            }

            ctx.lineTo(startX, startY);
        }
    };

    var drawFoodPart = function (canvas, foodPart) {
        var ctx = canvas.getContext("2d");
        var position = foodPart.getPosition();
        var size = foodPart.getSize();
        ctx.fillStyle = "green";
        ctx.fillRect(position.x, position.y, size, size);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y, size, size);
    };
    var drawFood = function (canvas, food) {
        for (var i = 0; i < food.parts.length; i++) {
            drawFoodPart(canvas, food.parts[i]);
        }
    };


    var drawWallPart = function (canvas, wallPart) {
        var ctx = canvas.getContext("2d");
        var position = wallPart.getPosition();
        var size = wallPart.getSize();
        ctx.fillStyle = "yellow";
        ctx.fillRect(position.x, position.y, size, size);
        ctx.strokeStyle = "black";
        ctx.strokeRect(position.x, position.y, size, size);
    };
    var drawWall = function (canvas, wall) {
        for (var i = 0; i < wall.elements.length; i++) {
            drawWallPart(canvas, wall.elements[i]);
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

    ///for better abstraction - hiding concrete drawing methods
    CanvasRenderer.prototype = {
        draw: function (obj) {
            if (obj instanceof snakes.SnakeType) {
                drawSnake(this.canvas, obj);
            }
            else if (obj instanceof snakes.SnakePartType) {
                drawSnakePart(this.canvas, obj);
            }
            else if (obj instanceof snakes.FoodType) {
                drawFood(this.canvas, obj);
            }
            else if (obj instanceof snakes.FoodPartType) {
                drawFoodPart(this.canvas, obj);
            }
            else if (obj instanceof snakes.WallType) {
                drawWall(this.canvas, obj);
            }
            else if (obj instanceof snakes.SnakeHeadType) {
                drawSnakeHead(this.canvas, obj, direction);
            }
            else if (obj instanceof snakes.WallPartType) {
                drawWallPart(this.canvas, obj);
            }
        },

        clear: function () {
            var ctx = this.canvas.getContext("2d");
            var w = this.canvas.width;
            var h = this.canvas.height;
            ctx.clearRect(0, 0, w, h);
        }
    }

    return {
        getCanvas: function (selector) {
            return new CanvasRenderer(selector);
        },


    };
}());