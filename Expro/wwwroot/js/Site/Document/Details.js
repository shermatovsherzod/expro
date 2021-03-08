var _documentType = "";
var _localizer;

function SetVariables(localizer, documentType) {
    _localizer = localizer;
    _documentType = documentType;
}

$(document).ready(function () {
    $("form#purchaseForm").submit(function () {
        if (!confirm(_localizer["AreYouSureYouWantToPurchase"] + "?"))
            return false;

        ShowFormSpinner();
        DisableFormElements();
    });

    InitCKEDITORForView();
});

function ShowFormSpinner() {
    $("#purchaseForm .spinner").show();
}

function DisableFormElements() {
    $("form#purchaseForm input").attr("readonly", "readonly");
    $("form#purchaseForm button").attr("disabled", "disabled");
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

var _likeType;

function SetLikeType(likeType) {
    _likeType = likeType;
}

function Like(objectID, isLike) {
    var isPositive = true;
    if (isLike == false)
        isPositive = false;

    var likeData = {
        ObjectID: objectID,
        IsPositive: isPositive,
        LikeType: _likeType
    };

    $.ajax({
        url: "/Like/Save",
        data: JSON.stringify(likeData),
        processData: false,
        contentType: "application/json; charset=utf-8",
        type: "POST",
        //dataType: "json",
        beforeSend: function () {
            //$(".errorMessage, .successMessage").hide().text("");
            $("#likesInfoBox .spinner").show();
            $("#likesInfoBox a").attr("disabled", "disabled");
        },
        success: function (data) {
            //$("#moneyDistributionModal .successMessage")
            //    .text(data.successMessage)
            //    .show();

            location.reload();
            //alert("File uploaded!");
        },
        error: function (data) {
            $("#likesInfoBox .spinner").hide();
            $("#likesInfoBox a").removeAttr("disabled");
        },
        complete: function () {
            //$("#" + spinnerTagID).hide();
            //$(input).removeAttr("disabled");
        }
    });
}

function Dislike(objectID) {
    Like(objectID, false);
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

function Reject(id) {
    var confirmationText = _localizer["AreYouSureYouWantToReject"] + "?";

    if (!confirm(confirmationText))
        return;

    $.ajax({
        type: "POST",
        url: "/Admin/" + _documentType + "/Reject/" + id,
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
            $(".adminActions .spinner").show();
        },
        success: function (data) {
            //alert("success");
            //alert(data);
            location.replace("/" + _documentType);
        },
        error: function (data) {
            alert("Ajax error (status code = " + data.status + "): " + data.statusText);
            $(".adminActions .spinner").hide();
        },
        complete: function (data) {
            //alert("completed");
            //$(".btn").removeAttr("disabled");
        }
    });
}