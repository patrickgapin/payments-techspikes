module app.Services {
    'use strict';

    export interface IUserService {
        getUsers(): ng.IPromise<IUser>;
    }

    export interface IUser {
        username: string
    }

    
    class UserService {
        static $inject = ['$http'];
        constructor(private $http: ng.IHttpService) {
        }

        getUsers(): ng.IPromise<IUser> {
            return this.$http.get('/api/users')
                .then((response: ng.IHttpPromiseCallbackArg<IUser>): IUser => {
                    return response.data;
                });
        }
    }

    angular
        .module('app.services')
        .service('app.services.UserService', UserService);
}