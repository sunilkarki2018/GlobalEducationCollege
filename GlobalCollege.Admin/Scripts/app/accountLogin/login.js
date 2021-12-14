/// <reference path="../../jquery.validate.js" />
if (!window.app) {
    window.app = {};
}



app.login = (function () {

    var mainBody = $('.container');


    init = function () {

        initValidation();


    },
    initValidation = function () {

        $.validator.addMethod(
       "regex",
       function (value, element, regexp) {
           var re = new RegExp(regexp);
           return this.optional(element) || re.test(value);
       },
       "Password should be 1 lowercase 1 uppercase 1 numeric 1 special character & 8 length long."
   );


        mainBody.find("#loginForm").validate({
            rules: {
                Email: {
                    required: true,
                    email: true
                },
                Password: {
                    required: true,
                    regex: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})"

                }
            },
            messages: {
                Email: "Please enter a valid email address",
                Password: {
                    required: "Please provide a password",
                    minlength: 8

                }
            }, errorPlacement: function (error, element) {
                // Add the `help-block` class to the error element
                error.addClass("help-block");

                // Add `has-feedback` class to the parent div.form-group
                // in order to add icons to inputs
                element.parents(".col-sm-5").addClass("has-feedback");

                if (element.prop("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else {
                    error.insertAfter(element);
                }

                // Add the span element, if doesn't exists, and apply the icon classes to it.
                if (!element.next("span")[0]) {
                    $("<span class='glyphicon glyphicon-remove form-control-feedback right'> </span>").insertAfter(element);
                }
            },
            success: function (label, element) {
                // Add the span element, if doesn't exists, and apply the icon classes to it.
                if (!$(element).next("span")[0]) {
                    $("<span class='glyphicon glyphicon-ok form-control-feedback right'> </span>").insertAfter($(element));
                }
            },
            highlight: function (element, errorClass, validClass) {
                $(element).parents(".formField").addClass("has-error").removeClass("has-success");
                $(element).next("span").addClass("glyphicon-remove right").removeClass("glyphicon-ok");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".formField").addClass("has-success").removeClass("has-error");
                $(element).next("span").addClass("glyphicon-ok right").removeClass("glyphicon-remove");
            }
        });

    };
    return {
        init: init
    };

}(jQuery));

jQuery(app.login.init);

