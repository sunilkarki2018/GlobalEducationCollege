if (!window.app) {
    window.app = {};
}



app.commonServiceDataSource = (function () {

    var previewDocument = function (id) {

        var $d = $.Deferred();

        $.ajax({
            type: "GET",
            url: "/Service/GetDocumentPreview",
            data: { Id: id }

        }).done(function (response) {

            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });

        return $d.promise();

    };

    return {
        previewDocument: previewDocument
    }



}(jQuery))