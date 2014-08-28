/// <reference path="_reference.js" />
var user = (function () {
    function User(name, score) {
        this._name = name;
        this._score = score;

    }

    return {
        getUser: function (name, score) {
            return new User(name, score);
        },

    };
}());

