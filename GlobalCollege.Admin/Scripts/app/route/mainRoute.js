/// <reference path="routeDataSource.js" />

if (!window.app) {
    window.app = {};
}

app.route = (function () {
    var _mainSideSection = $('.main-sidebar'),
         dataSource = app.routeDataSource;

    init = function () {

        _mainSideSection.find('.sidebar-menu .treeview-menu').on('click', 'li .route', routeURL);


    },
    routeURL = function (event) {
        event.preventDefault();

        var url = $(this).attr('href');
        var lookupInfo = $(this).attr('data-lookup');
        var module = $(this).attr('data-module');

        $.when(dataSource.routeURL(url), dataSource.lookupData(lookupInfo, module)).done(function (responseView, responseLookupInfo) {

            $('.main_container').find('#view_section').html(responseView);



            $.each(responseLookupInfo, function (index, value) {

                console.log(value);

                $("#" + value.fieldName).append($("<option />").val(value.value).text(value.text));

            });



            $('.select2').select2();


        }).fail(function (responseCreate, responseValidation) {

        });




    };

    return {
        init: init
    };


}(jQuery));


jQuery(app.route.init);
