$(document).ready(function () {
    $("form#purchaseForm").submit(function () {
        ShowFormSpinner();
        DisableFormElements();
    });
});

function ShowFormSpinner() {
    $("#purchaseForm .spinner").show();
}

function DisableFormElements() {
    $("form#purchaseForm input").attr("readonly", "readonly");
    $("form#purchaseForm button").attr("disabled", "disabled");
}