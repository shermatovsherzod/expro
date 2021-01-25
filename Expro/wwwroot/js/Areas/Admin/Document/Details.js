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

function InitQuill() {
    // INITIALIZATION OF QUILLJS EDITOR
    // =======================================================
    var options = {
        //debug: 'info',
        //modules: {
        //    toolbar: '#toolbar'
        //},
        //placeholder: 'Compose an epic...',
        readOnly: true,
        //theme: 'snow'
    };
    var quill = $.HSCore.components.HSQuill.init('.js-quill', options);

    return quill;
}