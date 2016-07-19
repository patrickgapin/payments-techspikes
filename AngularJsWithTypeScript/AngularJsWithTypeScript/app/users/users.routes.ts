((): void => {

    angular
        .module('app.users')
        .config(config);

    config.$inject = [
        '$routeProvider'
    ];
    function config(
        $routeProvider: ng.route.IRouteProvider) {

        $routeProvider
            .when('/users',
            {
                templateUrl: 'app/users/users.html',
                controller: 'app.users.UserController',
                controllerAs: 'vm'
            });
    }
})();