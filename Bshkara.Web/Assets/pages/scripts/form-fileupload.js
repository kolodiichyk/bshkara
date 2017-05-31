var FormFileUpload = function() {
    return {
        //main function to initiate the module
        init: function() {

            var jqXHRData;

            $("#file-upload").fileupload({
                url: "/File/UploadFile",
                dataType: "json",
                add: function(e, data) {
                    jqXHRData = data;
                },
                done: function(event, data) {
                    if (data.result.isUploaded) {
                        $("#uploaded-file-path").val(data.result.filePath);
                        $("#uploaded-file-path-label").val(data.result.filePath);

  
                        var extension = "unknown";
                        var file = data.result.filePath;
                        extension = file.substr((file.lastIndexOf(".") + 1));
                        switch (extension) {
                        case "jpg":
                        case "png":
                        case "gif":
                            break;
                        case "zip":
                        case "rar":
                        case "7z":
                            alert("was zip rar");
                            break;
                        case "pdf":
                            break;
                        }

                        $("#uploaded-file-format").val(extension);
                        $("#uploaded-file-type-label").val(type);
                        $("#uploaded-file-format-label").val(extension);
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