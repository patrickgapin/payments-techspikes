var app;
(function (app) {
    var Users;
    (function (Users) {
        'use strict';
        var UserController = (function () {
            function UserController(userService) {
                var vm = this;
                userService.getUsers()
                    .then(function (user) {
                    console.log(user);
                    vm.users = user;
                });
            }
            UserController.$inject = [
                'app.services.UserService'
            ];
            return UserController;
        }());
        angular
            .module('app.users')
            .controller('app.users.UserController', UserController);
    })(Users = app.Users || (app.Users = {}));
})(app || (app = {}));
//# sourceMappingURL=users.controller.js.map