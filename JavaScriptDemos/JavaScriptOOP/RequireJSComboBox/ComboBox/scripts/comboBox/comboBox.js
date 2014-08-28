define(['jquery', 'handlebars'], function ($) {
    'use strict';

    var ComboBox;

    function appendOnClick(element, $container) {
        element.on('click', function () {
            var $this = $(this);
            if ($this.hasClass('visible')) {
                $this.addClass('selected');
                $container.find('.visible').removeClass('visible').hide();
                $container.find('.selected').show();
            }
            else {
                $this.removeClass('selected');
                $container.find('.person-item').addClass('visible').show();
            }
        });
    }

    ComboBox = (function () {
        function ComboBox(data) {
            this._data = data;
        }

    ComboBox.prototype.render = function (template) {
            var templateHtml = Handlebars.compile(template),
                $container = $('<div>').addClass('combo-box');
            for (var i = 0; i < this._data.length; i++) {
                $container.append(templateHtml(this._data[i]));
            }
            $container.find('.person-item').hide().first().addClass('selected').show();
            appendOnClick.call(this, $container.find('.person-item'), $container);
            return $container;
        };
        return ComboBox;
    }());
    return ComboBox;
});