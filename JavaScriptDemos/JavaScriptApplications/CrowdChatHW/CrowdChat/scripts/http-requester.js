define(['libs/jquery-2.1.1'], function () {
    var HttpRequester = (function () {
        var makeHttpRequest = function (url, type, data, success, error) {
            if (data) {
                data = JSON.stringify(data);
            }
            else {
                data = '';
            }

            $.ajax({
                url: url,
                type: type,
                data: data,
                contentType: 'application/json',
                timeout: 5000,
                success: success,
                error: error
            });
        }

        var getJSON = function (url, success, error) {
            makeHttpRequest(url, 'GET', '', success, error);
        }

        var postJSON = function (url, data, success, error) {
            makeHttpRequest(url, 'POST', data, success, error);
        }

        return {
            getJSON: getJSON,
            postJSON: postJSON
        }
    }());

    return HttpRequester;
});