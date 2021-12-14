if (!window.app) {
    window.app = {};
}

app.applicationBootstraper = (function () {

    var mainBody = $('#main-workspace'),
        dataSource = app.dataSourceBootstrap,
        initializer = appInit.applicationInitializer,
        editor = null,
        init = function () {

            mainBody.on('click', 'form#searchForm button#searchRecords', SearchRecords);
            mainBody.on('click', '#RecordsContent ul.recordSize li a', resizeContentRecords);
            mainBody.on('click', '#RecordsContent ul#paginationUL li a', paginatedContentRecords);
            mainBody.on('click', '.scopeChange', changeScope);
            mainBody.on('click', 'form#mainForm .saveRecords', confirmation);
            mainBody.on('click', 'form#mainForm .unlock', unlockRecords);
            mainBody.on('submit', 'form#mainForm', submitForm);
            mainBody.on('change', 'form#mainForm :input', onChangeFormValues);
            mainBody.on('click', '#addNewChildRecord', addNewChildRecord);
            mainBody.on('click', '#backtoSummary', backtoSummary);
            mainBody.on('click', '.previewChildDetails', previewChildDetails);
            if (document.querySelector('.text-area-detail') !== null) {
                editor = new FroalaEditor(".text-area-detail");


            }
            mainBody.find("#UploadFile").on('change', uploadFile);

        },
        changeScope = function (event) {
            try {
                event.preventDefault();

                if (!getParentId()) {

                    $.dialog({
                        title: 'Warning',
                        content: 'Please save record for current tab to proceed for next tab.',
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                } else {

                    var url = $(this).attr('href');

                    if ($(this).attr('data-scope') === 'parent') {
                        url = window.location;
                    }


                    mainBody.find('#childTabInformation li').removeClass('active');
                    $(this).closest('li').addClass('active');

                    initializer.showloader();

                    dataSource.accessLink(url, getParentId()).done(function (response) {

                        mainBody.find('#currentTabInformation').html(response);

                        var currForm = mainBody.find('form#mainForm');

                        if (currForm.length > 0) {
                            currForm.removeData("validator");
                            currForm.removeData("unobtrusiveValidation");
                            $.validator.unobtrusive.parse(currForm);
                        }

                        if (document.querySelector('.text-area-detail') !== null) {
                            
                            editor = new FroalaEditor(".text-area-detail");
                        }

                        $('select').selectpicker('refresh');

                        initializer.hideloader();

                    }).fail(function (response) {

                        $.dialog({
                            title: 'Warning !',
                            content: response.ResponseView === undefined ? response : response.ResponseView,
                            animation: 'scale',
                            columnClass: 'medium',
                            closeAnimation: 'scale',
                            backgroundDismiss: true,
                        });

                        initializer.hideloader();

                    });

                }


            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        backtoSummary = function (event) {
            try {
                event.preventDefault();


                var url = $(this).attr('href');

                initializer.showloader();

                dataSource.accessLink(url, getParentId()).done(function (response) {

                    mainBody.find('#currentTabInformation').html(response);

                    initializer.hideloader();

                }).fail(function (response) {

                    $.dialog({
                        title: 'Warning !',
                        content: response.ResponseView === undefined ? response : response.ResponseView,
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                    initializer.hideloader();

                });



            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        previewChildDetails = function (event) {
            try {
                event.preventDefault();

                var url = $(this).attr('href');

                initializer.showloader();

                dataSource.previewChildDetails(url).done(function (response) {

                    mainBody.find('#currentTabInformation').html(response);

                    if (document.querySelector('.text-area-detail') !== null) {
                        
                        editor = new FroalaEditor(".text-area-detail");
                    }

                    $('select').selectpicker('refresh');
                    initializer.hideloader();

                }).fail(function (response) {

                    $.dialog({
                        title: 'Warning !',
                        content: response.ResponseView === undefined ? response : response.ResponseView,
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                    initializer.hideloader();

                });

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        confirmation = function (event) {
            try {
                event.preventDefault();

                var CurrentAction = $(this).attr('data-currentaction');

                $.confirm({
                    title: 'Confirm!',
                    content: 'Do you really want to submit the form!',
                    buttons: {
                        Yes: function () {

                            mainBody.find('form#mainForm input#CurrentAction').val(CurrentAction);
                            if (mainBody.find('form#mainForm').valid()) {
                                mainBody.find('form#mainForm').submit();
                            }
                        },
                        cancel: function () {
                            $.alert('Canceled!');
                        }
                    }
                });

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        unlockRecords = function (event) {
            try {
                event.preventDefault();
                console.log(this);

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        submitForm = function (event) {
            try {
                event.preventDefault();


                var $form = $(this);

                initializer.showloader();

                dataSource.submitForm($form).done(function (response) {

                    if (response.IsSuccess === true) {

                        if (mainBody.find('.nav.nav-pills.nav-stacked li.active a').attr('data-scope') === 'parent') {
                            mainBody.find('#ParentId').val(response.Id);

                            var path = window.location.pathname;

                            if (path.includes('Create')) {

                                path = window.location.pathname.replace('Create', 'Details');
                                path = path + '?Id=' + response.Id;
                                window.history.pushState({ urlPath: path }, "", path);
                            }
                        }

                        $.dialog({
                            title: 'Successful Response',
                            content: response.ResponseView === undefined ? response : response.ResponseView,
                            animation: 'scale',
                            columnClass: 'medium',
                            closeAnimation: 'scale',
                            backgroundDismiss: true,
                        });

                        $('body').find('#loader-wrapper').hide();

                        initializer.hideloader();

                    }
                    else {

                        $.dialog({
                            title: 'Warning !',
                            content: response.ResponseView === undefined ? response : response.ResponseView,
                            animation: 'scale',
                            columnClass: 'medium',
                            closeAnimation: 'scale',
                            backgroundDismiss: true,
                        });

                        mainBody.find('form#mainForm :button').attr('disabled', false);
                        $form.find(':input').prop("disabled", false);

                        initializer.hideloader();
                    }

                }).fail(function (response) {

                    $.dialog({
                        title: 'Warning !',
                        content: response.ResponseView === undefined ? response : response.ResponseView,
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                    mainBody.find('form#mainForm :button').attr('disabled', false);
                    $form.find(':input').prop("disabled", false);

                    initializer.hideloader();

                });


            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        SearchRecords = function (event) {
            try {
                event.preventDefault();

                var $form = mainBody.find('form#searchForm');

                initializer.showloader();

                dataSource.searchRecords($form).done(function (response) {

                    mainBody.find('#RecordsContent').html(response);
                    initializer.hideloader();


                }).fail(function (response) {

                    $.dialog({
                        title: 'Warning !',
                        content: response.ResponseView === undefined ? response : response.ResponseView,
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                    initializer.hideloader();

                });

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }
        },
        resizeContentRecords = function (event) {
            try {
                event.preventDefault();

                var activeSize = $(this).attr('data-pagesize');
                mainBody.find('form#searchForm input#PageSize').val(activeSize);
                mainBody.find('form#searchForm input#PageNumber').val(1);

                mainBody.find('form#searchForm button#searchRecords').click();

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        paginatedContentRecords = function (event) {
            try {
                event.preventDefault();

                var activePage = $(this).attr('data-page');
                mainBody.find('form#searchForm input#PageNumber').val(activePage);
                mainBody.find('form#searchForm button#searchRecords').click();


            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        onChangeFormValues = function (event) {
            try {
                event.preventDefault();

                if (mainBody.find('form#mainForm').valid()) {
                    mainBody.find('form#mainForm button.saveRecords').attr('disabled', false);
                }

            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }

        },
        addNewChildRecord = function (event) {
            try {
                event.preventDefault();

                if (!getParentId()) {

                    $.dialog({
                        title: 'Warning',
                        content: 'Please save record for current tab to proceed for next tab.',
                        animation: 'scale',
                        columnClass: 'medium',
                        closeAnimation: 'scale',
                        backgroundDismiss: true,
                    });

                } else {

                    var url = $(this).attr('href');

                    initializer.showloader();

                    dataSource.accessLink(url, getParentId()).done(function (response) {

                        mainBody.find('#currentTabInformation').html(response);

                        if (document.querySelector('.text-area-detail') !== null) {
                            
                            editor = new FroalaEditor(".text-area-detail");
                        }

                        $('select').selectpicker('refresh');
                        initializer.hideloader();

                    }).fail(function (response) {

                        $.dialog({
                            title: 'Warning !',
                            content: response.ResponseView === undefined ? response : response.ResponseView,
                            animation: 'scale',
                            columnClass: 'medium',
                            closeAnimation: 'scale',
                            backgroundDismiss: true,
                        });

                        initializer.hideloader();

                    });

                }


            } catch (err) {

                $.alert({
                    title: 'Alert!',
                    content: err.message,
                });

            }


        },
        getParentId = function () {

            return mainBody.find('input#ParentId').val();

        },
        uploadFile = function (e) {

            if (e.target.files.length > 0) {

                var fileName = e.target.files[0].name;
                var extension = fileName.split('.').pop();
                var FilePath = fileName;
                mainBody.find('form #FileName').val(fileName);
                mainBody.find('form #Extension').val(extension);
                mainBody.find('form #FilePath').val(FilePath);
            }
        };

    return {
        init: init
    };


}(jQuery));

jQuery(app.applicationBootstraper.init());

