(function () {
    angular
        .module('app.users')
        .config(config);
    config.$inject = [
        '$routeProvider'
    ];
    function config($routeProvider) {
        $routeProvider
            .when('/users', {
            templateUrl: 'app/users/users.html',
            controller: 'app.users.UserController',
            controllerAs: 'vm'
        });
    }
})();
//# sourceMappingURL=users.routes.js.map