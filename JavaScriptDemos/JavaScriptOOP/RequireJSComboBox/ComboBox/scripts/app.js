(function () {
    'use strict';
    require.config({
       paths: {
            "jquery": "libs/jquery/jquery-2.1.1",
            "handlebars": 'libs/handlebars/handlebars-v1.3.0',
            "controls": 'controls',
            "comboBox": "comboBox/comboBox",
        }
    });

    require(["jquery", "controls"], function ($, controls) {
        var people = [
            { id: 1, name: "Nakov", age: 31, avatarUrl: "images/firstTr.jpg" },
            { id: 2, name: "Niki", age: 29, avatarUrl: "images/secondTr.jpg" },
            { id: 3, name: "Ivo", age: 20, avatarUrl: "images/thirdTr.jpg" },
            { id: 4, name: "Goro",age: 33, avatarUrl: "images/fourthTr.jpg" }
        ];

        var comboBox = controls.ComboBox(people),
            template = $("#person-template").html(),
            comboBoxHtml = comboBox.render(template);
        $('body').append(comboBoxHtml);
    });
}());