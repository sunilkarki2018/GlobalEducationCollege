if (!window.app) {
    window.app = {};
}


app.cascadingdropdown = (function () {

    var mainBody = $('#main-workspace'),
        dataSource = app.cascadingdropdownDataSource,
        init = function () {
            $('.CascadingDropdownParameter').on('change', CascadingDropdownInformation);

        },
        CascadingDropdownInformation = function (event) {

            event.preventDefault();

            var FieldName = $(this).attr('name');


            var model = {
                dropdownList: []
            };



            $.each($("select[data-dropdownparameter *='" + FieldName + "']"), function (index, selectInformation) {

                var DropdownInformation = {
                    ColumnName: $(selectInformation).attr('name'),
                    EntityName: $(selectInformation).attr('data-entityname'),
                    Parameters: []
                };

                var htmlparameters = $(selectInformation).attr('data-dropdownparameter').split(',');

                $.each(htmlparameters, function (paraindex, parameterInformation) {

                    var DropdownParameter = {

                        ParameterName: parameterInformation,
                        ParameterValue: $('select[id=' + parameterInformation + ']').val()

                    };

                    DropdownInformation.Parameters.push(DropdownParameter);

                });

                model.dropdownList.push(DropdownInformation);
            });

            console.log(JSON.stringify(model));

            dataSource.GetCascadingDropdownList(model).done(function (response) {

                var $body = $('#main-workspace');

                $.each(response.dropdownInformationList, function (index, dropdown) {

                    $body.find('form #' + dropdown.ColumnName).empty();

                    $body.find('form #' + dropdown.ColumnName).append($('<option>', {
                        value: null,
                        text: '--Select--'
                    }));

                    $.each(dropdown.selectListItems, function (index, value) {

                        $body.find('form #' + dropdown.ColumnName).append($('<option>', {
                            value: value.Value,
                            text: value.Text
                        }));
                    });

                    $body.find('form #' + dropdown.ColumnName).trigger("chosen:updated");

                });

                $('select').selectpicker('refresh');


            }).fail(function (response) {

                $.dialog({
                    title: 'Failure Response',
                    content: response.ResponseView,
                    animation: 'scale',
                    columnClass: 'medium',
                    closeAnimation: 'scale',
                    backgroundDismiss: true,
                });

            });


        };

    return {
        init: init
    };


}(jQuery));

jQuery(app.cascadingdropdown.init());