var app;
(function (app) {
    var Layout;
    (function (Layout) {
        'use strict';
        var NavigationController = (function () {
            function NavigationController() {
                this.heading = 'Welcome to my page!';
            }
            return NavigationController;
        }());
        angular
            .module('app.layout')
            .controller('app.layout.NavigationController', NavigationController);
    })(Layout = app.Layout || (app.Layout = {}));
})(app || (app = {}));
//# sourceMappingURL=navigation.controller.js.map