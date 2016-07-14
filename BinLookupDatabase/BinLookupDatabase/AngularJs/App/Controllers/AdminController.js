"use strict";

binLookupApp.controller("binAdminController", function ($scope, binAdminService)
{
    $scope.isClicked = false;
    $scope.isReadOnly = true;
    $scope.binClicked = function()
    {
        $scope.isClicked = $scope.isClicked ? false : true;
    }
    var onBinComplete = function (data) {
        $scope.details = data[0];
        $scope.binNumber = data[0].BinNumber;
        $scope.declarationRatio = data[0].DeclarationRatio;
        $scope.updatedDateTime = data[0].UpdatedDateTime;
        $scope.userComments = data[0].UserComments;
        $scope.gamingFriendly = data[0].GamingFriendly;
        $scope.error = "";
        $scope.productsDataSource = [
        { text: $scope.gamingFriendly, value: "1" },
        { text: "hostile", value: "2" }
        ];
    };
    $scope.productsDataSource = [
        { text: "demo", value: "1" }
    ];
    
    var onError = function (response) {
        $scope.details = "";
        if (response.data === "")
        {
            $scope.error = response.statusText;
        }
        else if (response.status === -1)
        {
            $scope.error = "Something Went Wrong";
        }
        else
        {
            $scope.error = response.data.message;
        }
       
    };
    $scope.searchBin = function()
    {
        var binSearch =
            {
                BinNumber: $scope.binNumber
            };
        binAdminService.getBinDetails(binSearch)
            .then(onBinComplete, onError);
    }
});