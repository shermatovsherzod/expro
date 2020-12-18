var documentType = "";

function SetDocumentType(name) {
    documentType = name;
}

function Approve(id) {
    ResponseToDocument(id, "Approve");
}

function Reject(id) {
    ResponseToDocument(id, "Reject");
}

function ResponseToDocument(id, action) {
    var confirmationText = "";
    if (action == "Approve")
        confirmationText = "Вы уверены, что хотите подтвердить?";
    else
        confirmationText = "Вы уверены, что хотите отменить?";

    if (!confirm(confirmationText))
        return;

    $.ajax({
        type: "POST",
        url: "/Admin/" + documentType + "/" + action + "/" + id,
        //data: {
        //    id: requestID
        //},
        //data: JSON.stringify({
        //    id: requestID
        //}),
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        beforeSend: function (data) {
            $(".btn").attr("disabled", "disabled");
            ShowFormSpinner();
        },
        success: function (data) {
            //alert("success");
            //alert(data);
            location.reload();
        },
        error: function (data) {
            alert("Ajax error (status code = " + data.status + "): " + data.statusText);
        },
        complete: function (data) {
            //alert("completed");
            //$(".btn").removeAttr("disabled");
        }
    });
}

function ShowFormSpinner() {
    $(".spinner").show();
}

function InitCKEDITORForView() {
    var configViewMode =
    {
        extraPlugins: 'autogrow',
        autoGrow_minHeight: 1,
        autoGrow_maxHeight: 100000,
        autoGrow_onStartup: true,
        readOnly: true
    }

    $('.editorViewMode').each(function (e) {
        CKEDITOR.replace(this.id, configViewMode);
    });
}