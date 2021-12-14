/// <reference path="../../jquery-1.10.2.js" />
if (!window.app) {
    window.app = {};
}


app._validationDataSource = (function () {

    var getValidationRule = function (Name) {

        $validationDeferred = $.Deferred();

        var _storeresponse = localStorage.getItem(Name);
       
        jQuery.ajax({
            url: '/api/CommonService/GetValidationInformationAsync',
            method: 'GET',
            data: { _name: Name, _currentaction: 1 }
        }).done(function (response) {

            localStorage.setItem(Name, JSON.stringify(response));

            $validationDeferred.resolve(response);

        }).fail(function () {
            $validationDeferred.reject(response);
        });        

        return $validationDeferred.promise();

    };

    return {
        getValidationRule: getValidationRule
    };

}(jQuery));