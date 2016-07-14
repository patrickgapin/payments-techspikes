"use strict";

binLookupApp.controller("binDetailController", function($scope, binDetailService, $interval, $anchorScroll, $location, $routeParams)
{
    $scope.onClick1 = function () {
        alert("Primary button click");
    };
    $scope.onClick2 = function () {
        alert("Second button click");
    };
    var decrement = function()
    {
        $scope.countdown -= 1;
        if ($scope.countdown < 1)
        {
            $scope.search($scope.binNumber);
        }
    };
    var countInterval = null;
    var start = function()
    {
        countInterval = $interval(decrement, 1000, $scope.countdown);
    };
    $scope.getUserDetails = function()
    {
        $scope.pageTitle = "Angular Demo";
        $scope.countdown = 5;
        // start();
    };
    var onBinComplete = function(data)
    {
        $scope.details = data;
        $scope.error = "";
        //$location.hash("binDetails");
        //$anchorScroll();
    };
    var onError = function(response)
    {
        $scope.details = "";
        if (response.data === "") {
            $scope.error = response.statusText;
        }
        else {
            $scope.error = response.data.message;
        }
    };
    //$scope.binParam = $routeParams.binNumber;  // from route in app.js
    //$location.path("/binDetail/" + binParam);  Through this we can move to another path which is mention in app.js like on click of search we can move to another html.
    
    $scope.search = function()
    {
        if (($scope.binNumber === "" || $scope.binNumber === undefined) && ($scope.bankName === "" || $scope.bankName === undefined)
            && ($scope.countryName === "" || $scope.countryName === undefined) & ($scope.currencyId === "" || $scope.currencyId === undefined))
        {
            $scope.error = "Input Parameters are missing.";
        }
        else
        {
            var binSearch =
            {
                BinNumber: $scope.binNumber,
                CardIssuingbank: $scope.bankName,
                CardIssuingCountryName: $scope.countryName,
                CurrencyId: $scope.currencyId
            };
            binDetailService.getBinDetails(binSearch)
                .then(onBinComplete, onError);
            if (countInterval)
            {
                $interval.cancel(countInterval);
            }
        }
    };
});