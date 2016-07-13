"use strict";

binLookupApp.service("binDetailService", function ($http)
{
    var getBinDetails = function(binSearch)
    {
      return  $http({
                url: "http://172.24.21.75/api/bindetails",
                method: "Post",
                data: JSON.stringify(binSearch),
                headers: {
                    "Content-Type": "application/json;charset=UTF-8"
                }
            })
            .then(function(response)
            {
                return response.data;
            });
    };

    // Get Request

    //var getUserDetails = function(userName, password)
    //{
    //    return $http({
    //        url: "http://172.24.21.75/api/bindetails/" + userName + "/" + password,
    //        method: "Get",
    //        headers: {
    //            "Content-Type": "application/json;charset=UTF-8"
    //        }
    //    })
    //        .then(function (response) {
    //            return response.data;
    //        });
    //}

    // Calling to api methods at a time and combine the results.


    //var demo = function(demoDetail)
    //{
    //    var res;
    //    var url = "";
    //    return $http({
    //        url: url,
    //        method: "Post",
    //        data: JSON.stringify(binSearch),
    //        headers: {
    //            "Content-Type": "application/json;charset=UTF-8"
    //        }
    //    })
    //         .then(function (response)
    //        {
    //             res = response.data;
    //            return $http({
    //                url: "http://172.24.21.75/api/bindetails",
    //                method: "Post",
    //                data: JSON.stringify(binSearch),
    //                headers: {
    //                    "Content-Type": "application/json;charset=UTF-8"
    //                }
    //            });
    //         })
    //    .then(function (response)
    //        {
    //        res.someInfo = response.data;
    //            return res;
    //        });
    //};

    return {
        getBinDetails: getBinDetails
    };
});