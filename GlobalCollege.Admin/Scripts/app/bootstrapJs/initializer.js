/// <reference path="initializerDataSource.js" />
if (!window.appInit) {
    window.appInit = {};
}


appInit.applicationInitializer = (function ($) {
    var $body = $('body'),
        dataSource = appInit.initializerDataSource,

        init = function () {
            $body.on('click', ".modal button[data-dismiss='modal']", closeModal);
            $body.find('.datepicker').datepicker({
                autoclose: true
            });

            $body.find('.futuredatepicker').datepicker({
                autoclose: true
            });

            $body.find('.backdatepicker').datepicker({
                autoclose: true,
                endDate: '+0d'

            });

            $body.find('.timepicker').timepicker({});

            $(document).ajaxStop(function () {
                $('select').selectpicker('refresh');
            });

            $body.find('#irm-workspace #main-workspace').on('click', 'form .unlock', function () {

                $('select').selectpicker('refresh');
            });

            $body.find('#irm-workspace #main-workspace').on('click', 'form .download-select', updateSelect);
            $body.find('#irm-workspace #main-workspace').on('change', 'form select', detectSelectChange);

            getbackdatepicker();

        }, updateSelect = function (event) {

            event.preventDefault();

            var _fieldName = $(this).attr('data-entityname');

            var _entityName = $body.find('#irm-workspace #main-workspace form #' + _fieldName).attr('entityname');


            var _parameter = $body.find('#irm-workspace #main-workspace form #' + _fieldName).attr('parameter');


            var ParameterList = {
                EntityName: _entityName,
                FieldName: _fieldName,
                Parameters: []
            };

            $.each(splitAndTrim(_parameter), function (index, value) {

                var parameter = appendModelPrefix(value, '');

                var _fieldValue = $body.find('#irm-workspace #main-workspace form [name="' + parameter + '"]').val();

                var _parameterValue = {
                    Name: parameter,
                    Value: _fieldValue
                };

                ParameterList.Parameters.push(_parameterValue);

            });

            showloader();

            dataSource.getSelectedList(ParameterList).done(function (response) {

                $body.find('#irm-workspace #main-workspace form #' + _fieldName).empty();

                $.each(response[0].SelectValues, function (index, value) {

                    $body.find('#irm-workspace #main-workspace form #' + _fieldName).append($('<option>', {
                        value: value.Value,
                        text: value.Title
                    }));



                });

                $body.find('#irm-workspace #main-workspace form #' + _fieldName).trigger("chosen:updated");



                if (response[0].SelectValues.length > 0) {
                    $('select').selectpicker('refresh');
                }

                hideloader();

            }).fail(function (response) {

            });


        }, splitAndTrim = function (value) {

            return value.replace(/^\s+|\s+$/g, "").split(/\s*,\s*/g);


        }, appendModelPrefix = function (value, prefix) {
            if (value.indexOf("*.") === 0) {
                value = value.replace("*.", prefix);
            }
            return value;
        },
        showloader = function () {

            $body.find('#loader-wrapper').show();
        },
        hideloader = function () {
            $body.find('#loader-wrapper').hide();



            $body.find('.datepicker').datepicker({
                autoclose: true
            });


            $body.find('.futuredatepicker').datepicker({
                autoclose: true
            });

            $body.find('.backdatepicker').datepicker({
                autoclose: true,
                endDate: '+0d'

            });


            getbackdatepicker();



            $body.find('.timepicker').timepicker({});

            $('[data-toggle="tooltip"]').tooltip(); 


        },
        closeModal = function (event) {
            event.preventDefault();
            var $modalId = $(this).closest('.modal').attr('id');
            $('#' + $modalId).modal('hide');

        }, searchRecords = function (event) {
            event.preventDefault();

            var id = $(event).attr('data-id');

            //var value = $(event).val();

            console.log($(this));

            //$body.find('#irm-workspace #main-workspace form #' + id).append($('<option>', {
            //    value: $(event).val(),
            //    text: $(event).val()
            //}));

            //$('select').selectpicker('refresh');


        }, detectSelectChange = function (event) {
            event.preventDefault();
            var name = $(this).attr('name');

            $.each($body.find('#irm-workspace #main-workspace form select'), function (index, value) {

                var parameter = $(value).attr('parameter');

                if (parameter != null) {

                    var parameterList = parameter.split(',');

                    $.each(parameterList, function (index, formParameter) {

                        if (name === formParameter.replace("*.", "")) {

                            $(value).empty();

                        }

                    });

                }

            });

            $('select').selectpicker('refresh');
        }, getbackdatepicker = function () {

            $.each($body.find('.backdatepicker'), function (index, value) {
                var _date = $(this).val();
                if (_date) {
                    $('#' + $(this).attr('name')).datepicker({ dateFormat: "yy-mm-dd" }).datepicker("setDate", new Date(_date));
                }
            });

            $.each($body.find('.futuredatepicker'), function (index, value) {
                var _date = $(this).val();
                if (_date) {
                    $('#' + $(this).attr('name')).datepicker({ dateFormat: "yy-mm-dd" }).datepicker("setDate", new Date(_date));
                }
            });
        };

    return {
        init: init,
        showloader: showloader,
        hideloader: hideloader
    }

}(jQuery));

jQuery(appInit.applicationInitializer.init);