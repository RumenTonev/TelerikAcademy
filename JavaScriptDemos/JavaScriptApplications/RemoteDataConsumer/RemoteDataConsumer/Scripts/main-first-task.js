/// <reference path="jquery-2.1.1.min.js" />
(function () {
    require(['1And3-StudentsManager', 'q'], function (requestModule, Q) {
        var url = 'http://localhost:3000/students';
        var header = {
            contentType: 'application/json',
            accept: 'application/json'
        }

        var counter = 0;

        $('#add-student').on('click', function () {
            counter++;

            var student = {
                name: 'Pesho#' + counter,
                grade: (Math.random() * 12 + 1) | 0
            };

            requestModule.postJSON(url, header, student)
                .then(function (data) {
                    $('#show-message-after-adding-student').html('Successfully added a new student!');
                }, function (err) {
                    $('#show-message-after-adding-student').html('Adding a student caused an error: ' + err);
                });
        });

        $('#get-all-students').on('click', function () {
            requestModule.getJSON(url, header)
                .then(function (data) {
                    var listForStudents = $('<ul>');
                    for (var i = 0; i < data.count; i++) {
                        listForStudents.append($('<li>').html('id: ' + data.students[i].id + ' name: ' + data.students[i].name + ' grade: ' + data.students[i].grade));
                    }

                    $('#layer-for-students').html(listForStudents);
                }, function (err) {
                    $('#show-message-after-adding-student').html('Adding a student caused an error: ' + err);
                });
        });
        
        $('#delete-student').on('click', function () {
            var id = $('#get-id').val();
            requestModule.deleteStudent(url, id)//gets the url and id of the student which must be deleted
                .then(function () {
                    $('#show-message-after-deleting-student').html('Student has been deleted!');
                }, function (err) {
                    $('#show-message-after-adding-student').html('Adding a student caused an error: ' + err);
                });
        });
    });
}());