if (!window.app) {
    window.app = {};
}


app.commonService = (function () {

    var mainBody = $('#main-workspace'),
        dataSource = app.commonServiceDataSource,
        init = function () {
            mainBody.on('click', '.filePreview', previewDocument);
          
        },
        previewDocument = function (event) {

            event.preventDefault();

            var id = $(this).attr('data-id');

            var width = window.innerWidth * 0.66;

            var height = width * window.innerHeight / window.innerWidth;

            window.open(this.href, 'newwindow', 'width=' + width + ', height=' + height + ', top=' + ((window.innerHeight - height) / 2) + ', left=' + ((window.innerWidth - width) / 2));


        };

    return {
        init: init
    }


}(jQuery))

jQuery(app.commonService.init());