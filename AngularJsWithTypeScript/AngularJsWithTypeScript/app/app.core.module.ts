((): void => {
    'use strict';
    
    angular
        .module('app.core', [
            /*
             * Angular Modules
             */
            'ngRoute',
            'ngSanitize',
            'ngCookies',
            'ngResource'
            /*
             * 3rd Party Modules
             */
        ]);
})();