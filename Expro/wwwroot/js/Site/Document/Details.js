$(document).ready(function () {
    $("form#purchaseForm").submit(function () {
        if (!confirm("Вы уверены, что хотите купить?"))
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