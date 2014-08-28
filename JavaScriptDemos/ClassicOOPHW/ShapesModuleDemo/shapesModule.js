
function Field(canvasWidth, canvasHeight, contextType, contextLineWidth) {
    var Shapes = (function () {

        var Shape = (function () {
            function Shape(x, y) {
                this._x = x;
                this._y = y;
            }
            /*parent class Shape initializing current field-creates canvas field and return context,I called it in every child.
            Module does not return Shape,thus making it kind of "abstract in C#"-can not be instantiated*/
            //with every draw() implementation we achieve polymorphisym(kind of).
            Shape.prototype = {
                draw: function () {
                    var theCanvas = document.createElement("canvas");
                    theCanvas.width = canvasWidth;
                    theCanvas.height = canvasHeight;
                    theCanvas.style.borderColor = "black";
                    theCanvas.style.borderWidth = "2px";
                    theCanvas.setAttribute("id", "the-canvas");
                    document.body.appendChild(theCanvas);
                    var context = theCanvas.getContext(contextType);
                    context.lineWidth = contextLineWidth;
                    return context;
                }
            };
            return Shape;
        }());

        var Rectangular = (function () {
            function Rectangular(x, y, width, height) {
                Shape.call(this, x, y);
                this._width = width;
                this._height = height;
            }

            Rectangular.prototype = new Shape();

            Rectangular.prototype.draw = function () {
                var context = Shape.prototype.draw.call(this);
                context.strokeRect(this._x, this._y, this._width, this._height);
            };
            return Rectangular;
        }());

        var Circle = (function () {
            function Circle(x, y, r) {
                Shape.call(this, x, y);
                this._radius = r;
            }

            Circle.prototype = new Shape();

            Circle.prototype.draw = function () {
                var context = Shape.prototype.draw.call(this);
                context.beginPath();
                context.arc(this._x, this._x, this._radius, 0, 2 * Math.PI);
                context.fill();
                context.stroke();
            };
            return Circle;
        }());

        var Line = (function () {
            function Line(x, y, xEnd, yEnd) {
                Shape.call(this, x, y);
                this._xEnd = xEnd;
                this._yEnd = yEnd;
            }

            Line.prototype = new Shape();

            Line.prototype.draw = function () {
                var context = Shape.prototype.draw.call(this);
                context.beginPath();
                context.moveTo(this._x, this._y);
                context.lineTo(this._xEnd, this._yEnd);
                context.stroke();
            };

            return Line;
        }());

        return {
            Line: Line,
            Rectangular: Rectangular,
            Circle: Circle,
        };
    }());

    return Shapes;
}