if (!window.app) {
    window.app = {};
}



app.dataSourceBootstrap = (function ($) {

    var accessLink = function (url, parentId) {


        var $d = $.Deferred();
        $.ajax({
            type: "GET",
            url: url,
            data: { ParentPrimaryRecordId: parentId, Id: parentId }

        }).done(function (response) {

            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });


        return $d.promise();




    }, submitForm = function ($form) {

        var $d = $.Deferred();

        $form.find(':input').prop("disabled", false);

        if ($form.find("input[type='file']").length > 0 && $form.find("input[type='file']").get(0).files.length > 0) {

            var fileData = new FormData($form[0]);

            $.each($form.find("input[type='file']"), function (index, control) {

                var fileUpload = $(control).get(0);
                var files = fileUpload.files;
                fileData.append(files[index].name, files[index]);

            });

            $form.find(':input').prop("disabled", true);
            $form.find("button[type='submit']").prop('disabled', true);
            var url = getUrl($form.attr('action'), $form.find('#CurrentAction').val());

            $.ajax({
                type: "POST",
                url: url,
                data: fileData,
                contentType: false,
                processData: false
            }).done(function (response) {

                if (response.IsSuccess === false) {
                    $form.find(':input').prop("disabled", false);
                }
                $d.resolve(response);

            }).fail(function (response) {

                $d.reject(response);
            });

            return $d.promise();

        } else {

            $form.find(':input').prop("disabled", false);

            var formdata = $form.serialize();

            $form.find(':input').prop("disabled", true);
            $form.find("button[type='submit']").prop('disabled', true);
            var formurl = getUrl($form.attr('action'), $form.find('#CurrentAction').val());

            $.ajax({
                type: "POST",
                url: formurl,
                data: formdata
            }).done(function (response) {

                if (response.IsSuccess === false) {
                    $form.find(':input').prop("disabled", false);
                }
                $d.resolve(response);

            }).fail(function (response) {

                $d.reject(response);
            });

            return $d.promise();
        }


    }, accessChildLink = function (url, parentId) {


        var $d = $.Deferred();

        $.ajax({
            type: "GET",
            url: url,
            data: { ParentPrimaryRecordId: parentId }

        }).done(function (response) {

            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });

        return $d.promise();




    }, getUrl = function (url, CurrentAction) {

        switch (CurrentAction) {

            case 'Authorise':
                url = url.replace('Update', 'Authorise');
                return url;
            case 'Revert':
                url = url.replace('Update', 'Revert');
                return url;
            case 'Discard':
                url = url.replace('Update', 'Discard');
                return url;
            case 'Delete':
                url = url.replace('Update', 'Delete');
                return url;
            default:
                return url;


        }

    }, searchRecords = function ($form) {

        var $d = $.Deferred();

        var data = $form.serialize();

        var url = $form.attr('action');

        $.ajax({
            type: "POST",
            url: url,
            data: data
        }).done(function (response) {

            if (response.IsSuccess === false) {
                $form.find(':input').prop("disabled", false);
            }
            $d.resolve(response);

        }).fail(function (response) {

            $d.reject(response);
        });

        return $d.promise();

    },
        previewChildDetails = function (url) {


            var $d = $.Deferred();

            $.ajax({
                type: "GET",
                url: url

            }).done(function (response) {

                $d.resolve(response);

            }).fail(function (response) {

                $d.reject(response);
            });

            return $d.promise();




        };
    return {
        accessLink: accessLink,
        submitForm: submitForm,
        accessChildLink: accessChildLink,
        previewChildDetails: previewChildDetails,
        searchRecords: searchRecords

    };
}(jQuery));
