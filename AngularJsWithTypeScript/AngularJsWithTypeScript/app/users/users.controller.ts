module app.Users {
    'use strict';


    class UserController {
        users: Services.IUser;

        static $inject = [
            'app.services.UserService'
        ];

        constructor(userService: Services.IUserService) {
            var vm = this;
            userService.getUsers()
                .then((user: Services.IUser): void => {
                    console.log(user);
                    vm.users = user;
                });
        }
    }

    angular
        .module('app.users')
        .controller('app.users.UserController', UserController);
}