if (window.appInit) {
    window.appInit = {};
}



appInit.initializerDataSource = (function ($) {

    var getSelectedList = function (ParameterList) {


        var $d = $.Deferred();
        $.ajax({
            type: "POST",
            url: '/Service/GetSelectValues',
            data: JSON.stringify(ParameterList),
            contentType: "application/json;",
            dataType: "json"

        }).done(function (response) {

            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });


        return $d.promise();




    };
    return {
        getSelectedList: getSelectedList

    };
}(jQuery));
