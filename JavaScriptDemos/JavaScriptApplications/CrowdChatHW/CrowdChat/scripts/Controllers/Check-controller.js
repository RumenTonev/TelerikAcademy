define(function () {
    var CheckController = (function () {
        function isUserLoggedIn() {
            var name = localStorage.getItem('username');

            if (name == null || name == "") {
                return false;
            }

            return true;
        }

        function isInputValid(input) {
            if (input !== null && input !== '') {
                return true;
            }
            
            return false;
        }

        return {
            isUserLoggedIn: isUserLoggedIn(),
            isInputValid: isInputValid
        }
    }());

    return CheckController;
})