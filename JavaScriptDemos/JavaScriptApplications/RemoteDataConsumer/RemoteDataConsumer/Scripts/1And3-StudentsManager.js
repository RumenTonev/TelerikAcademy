/// <reference path="jquery-2.1.1.min.js" />

define(['q', 'jquery-2.1.1.min'], function (Q) {
    var StudentsManager = (function () {
        getJSON = function (url, header) {
            var deferred = Q.defer();

            $.ajax({
                url: url,
                contentType: header.contentType,
                type: 'GET',
                accept: header.accept,
                success: function (responseData) {
                    deferred.resolve(responseData);
                },
                error: function (errorData) {
                    deferred.reject(errorData);
                }
            });

            return deferred.promise;
        }

        postJSON = function (url, header, data) {
            var deferred = Q.defer();

            if (data) {
                data = JSON.stringify(data)
            }

            $.ajax({
                url: url,
                data: data,
                type: 'POST',
                contentType: header.contentType,
                accept: header.accept,
                success: function (responseData) {
                    deferred.resolve(responseData);
                },
                error: function (errorData) {
                    deferred.reject(errorData);
                }
            });
            
            return deferred.promise;
        }

        deleteStudent = function (url, id) {
            var deferred = Q.defer();

            $.ajax({
                url: url + '/' + id,
                type: 'POST',
                success: function (responseData) {
                    deferred.resolve(responseData);
                },
                error: function (errorData) {
                    deferred.reject(errorData);
                },
                timeout: 5000,
                data: { _method: 'delete' }
            });
            
            return deferred.promise;
        }

        return {
            getJSON: getJSON,
            postJSON: postJSON,
            deleteStudent: deleteStudent
        };
    }());

    return StudentsManager;
});