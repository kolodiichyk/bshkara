var FormFileUpload = function() {
    return {
        //main function to initiate the module
        init: function() {

            var jqXHRData;

            $("#file-upload").fileupload({
                url: "/File/UploadFile",
                dataType: "json",
                autoUpload: true,
                maxFileSize: 5000000,
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
                add: function(e, data) {
                    jqXHRData = data;
                },
                done: function(event, data) {
                    if (data.result.isUploaded) {
                        $("#uploaded-image-path").val(data.result.filePath);
                        $("#uploaded-image").attr("src", data.result.filePath + "?t=" + new Date().getTime());
                    } else {
                    }
                },
                fail: function(event, data) {
                    if (data.files[0].error) {
                        alert(data.files[0].error);
                    }
                }
            });

            $("#file-upload").change(function(ev) {
                if (jqXHRData) {
                    jqXHRData.submit();
                }
            });

        }
    };

}();

jQuery(document).ready(function() {
    FormFileUpload.init();
});