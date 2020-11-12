$(document).ready(function () {
    $("form#purchaseForm").submit(function () {
        if (!confirm("Вы уверены, что хотите купить?"))
            return false;

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