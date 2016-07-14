"use strict";

binLookupApp.service("binAdminService", function($http)
{
    var getBinDetails = function (binSearch) {
        return $http({
            url: "http://172.24.21.75/api/bindetails",
            method: "Post",
            data: JSON.stringify(binSearch),
            headers: {
                "Content-Type": "application/json;charset=UTF-8"
            }
        })
              .then(function (response) {
                  return response.data;
              });
    };
    return {
        getBinDetails: getBinDetails
    };
});