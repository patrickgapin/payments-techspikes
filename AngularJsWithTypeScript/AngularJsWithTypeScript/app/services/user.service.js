var app;
(function (app) {
    var Services;
    (function (Services) {
        'use strict';
        var UserService = (function () {
            function UserService($http) {
                this.$http = $http;
            }
            UserService.prototype.getUsers = function () {
                return this.$http.get('/api/users')
                    .then(function (response) {
                    return response.data;
                });
            };
            UserService.$inject = ['$http'];
            return UserService;
        }());
        angular
            .module('app.services')
            .service('app.services.UserService', UserService);
    })(Services = app.Services || (app.Services = {}));
})(app || (app = {}));
//# sourceMappingURL=user.service.js.map