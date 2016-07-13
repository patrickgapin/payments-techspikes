"use strict";

binLookupApp.controller("specificBinController", function ($scope, binDetailService, $location, $routeParams)
{
    $scope.binParam = $routeParams.binNumber;  // from route in app.js 

    var onBinComplete = function(data)
    {
        $scope.specificDetails = data;
    };
    var onError = function()
    {
        $scope.error = "Could Not fetch the details";
    };
    var search = function()
    {
        var binSearch =
        {
            "BinNumber": $scope.binParam
        };
        binDetailService.getBinDetails(binSearch)
            .then(onBinComplete, onError);
    };

    search();
});