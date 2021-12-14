if (!window.app) {
    window.app = {};
}


app.routeDataSource = (function () {

    routeURL = function (url) {

        $routeURLDeferred = $.Deferred();

        jQuery.ajax({
            url: url,
            method: 'GET'
        }).done(function (response) {

            $routeURLDeferred.resolve(response);

        }).fail(function (response) {
            $routeURLDeferred.reject(response);
        });

        return $routeURLDeferred.promise();

    }, lookupData = function (lookupInfo, module) {

        $lookupInfoDeferred = $.Deferred();

        var _storeresponse = localStorage.getItem(lookupInfo+'-lookup');

        if (!_storeresponse) {
            jQuery.ajax({
                url: '/api/CommonService/GetLookupValues',
                method: 'GET',
                data: { _lookupinfo: lookupInfo, _module: module }
            }).done(function (response) {

                localStorage.setItem(lookupInfo+'-lookup', JSON.stringify(response));

                $lookupInfoDeferred.resolve(response);

            }).fail(function (response) {
                $lookupInfoDeferred.reject(response);
            });
        } else {

            var response = JSON.parse(_storeresponse);
            $lookupInfoDeferred.resolve(response);

        }

        return $lookupInfoDeferred.promise();

    };




    return {
        routeURL: routeURL,
        lookupData: lookupData
    };

}(jQuery));

