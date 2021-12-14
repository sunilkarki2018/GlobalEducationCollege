if (!window.app) {
    window.app = {};
}

app.cascadingdropdownDataSource = (function () {

    var GetCascadingDropdownList = function (dropdownList) {

        var $d = $.Deferred();

        $.ajax({
            type: "POST",
            url: "/Service/GetCascadingDropdownList",
            data: JSON.stringify(dropdownList),
            contentType: "application/json; charset=utf-8",
            dataType: "json"

        }).done(function (response) {

            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });

        return $d.promise();

    };

    return {
        GetCascadingDropdownList: GetCascadingDropdownList
    };

}(jQuery));