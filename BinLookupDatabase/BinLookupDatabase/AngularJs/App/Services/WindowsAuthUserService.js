"use strict";

binLookupApp.service("windowsAuthService", function ($http, $window) {
    var user;

    function login() {

        if (user) {
            return $q.when(user); 
        }

        var url = '/api/WindowsAuthentication';
        var defer = $q.defer();

        $http.get(url).then(function (result) {
            user = {
                username: result.data.UserName,
                apiKey: result.data.ApiKey
            };

            addUserToStorage();
            defer.resolve(user);
        });

        return defer.promise;
    }

    function addUserToStorage() {
        $window.sessionStorage['userInfo'] = JSON.stringify(user);
    }

    function getUser() {
        return user;
    }

    function init() {
        if ($window.sessionStorage['userInfo']) {
            user = JSON.parse($window.sessionStorage['userInfo']);
        }
    }

    init();

    return {
        user: user,
        init: init,
        addUserToStorage: addUserToStorage,
        login: login,
        getUser: getUser
    };
});