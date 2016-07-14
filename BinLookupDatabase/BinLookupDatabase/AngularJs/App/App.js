"use strict";

var binLookupApp = angular
    .module("binLookupApp", ["ngRoute", "kendo.directives"])
    .config(function($routeProvider)
    {
        $routeProvider.when("/",
        {
            templateUrl: "AngularJs/App/Templates/BinDetail.html",
            controller: "binDetailController"
        });
        $routeProvider.when("/binDetail",
        {
            templateUrl: "AngularJs/App/Templates/BinDetail.html",
            controller: "binDetailController"
        });
        $routeProvider.when("/binDetail/:binNumber",
        {
            templateUrl: "AngularJs/App/Templates/SpecificBin.html",
            controller: "specificBinController"
        });

        $routeProvider.when("/admin",
        {
            templateUrl: "AngularJs/App/Templates/Admin.html",
            controller: "binAdminController"
        });

        $routeProvider.otherwise({ redirectTo: "/binDetail" });
    });