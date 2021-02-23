var area = "";
var withdrawRequestStatusPending = "";
var withdrawRequestStatusCompleted = "";

function SetVariables(
    areaName,
    withdrawRequestStatusesEnumPending,
    withdrawRequestStatusesEnumCompleted)
{
    area = areaName;
    withdrawRequestStatusPending = withdrawRequestStatusesEnumPending;
    withdrawRequestStatusCompleted = withdrawRequestStatusesEnumCompleted;
}

var table;

$(document).ready(function () {
    table = $("table#list").DataTable({
        searching: false,
        ordering: false,
        processing: true,
        serverSide: true,
        lengthChange: false,
        scrollX: true,
        lengthChange: false,
        language: InitDataTableLanguage(),
        ajax:
        {
            url: "/" + area + "/WithdrawRequest/Search",
            type: "POST",
            //contentType: "application/json",
            datatype: "json",
            data: function (d) {
                d.statusID = $("#statusDD").val();
                //d.priceType = $("#priceTypeDD").val();
            },
            //complete: function () {
            //    StartLoadingImagesAsync();
            //}
        },
        //columnDefs: [
        //    {
        //        "targets": [2],
        //        "visible": false,
        //    },
        //    //{
        //    //    "targets": [2],
        //    //    "visible": false
        //    //}
        //],
        columns: [
            {
                "data": 0, //"name": "statusId", "autoWidth": true,
                "render": function (data, type, full, meta) {
                    return '' + full.id + '';
                }
            },
            {
                "data": 1, //"name": "statusId", "autoWidth": true,
                "render": function (data, type, full, meta) {
                    return '' + full.amountStr;
                }
            },
            {
                "data": 2, //"name": "statusId", "autoWidth": true,
                "render": function (data, type, full, meta) {
                    return '' + full.dateCreated + '';
                }
            },

            {
                "data": 3, //"name": "statusId", "autoWidth": true,
                "render": function (data, type, full, meta) {
                    return '' + InsertStatusAlert(full) + '';
                }
            },
        ]
    });

    table
        .on('preDraw', function () {
            $("#tableBox .spinner").show();
        })
        .on('draw.dt', function () {
            $("#tableBox .spinner").hide();
            //console.log('Redraw took at: ' + (new Date().getTime() - startTime) + 'mS');
        });

    $("#statusDD, #priceTypeDD").change(function () {
        table.draw();
    });
});

function InsertStatusAlert(full) {
    var badgeClass = "";
    if (full.status.id == withdrawRequestStatusPending)
        badgeClass = "-secondary";
    else if (full.status.id == withdrawRequestStatusCompleted)
        badgeClass = "-success";


    var html = '<span class="badge badge-soft' + badgeClass + ' mr-2">';
    html += full.status.name;
    html += '</span>';

    return html;
}

function InitHSPlugins() {
    // INITIALIZATION OF SELECT2
    // =======================================================
    $('.js-custom-select').each(function () {
        var select2 = $.HSCore.components.HSSelect2.init($(this));
    });


    //// INITIALIZATION OF ION RANGE SLIDER
    //// =======================================================
    //$('.js-ion-range-slider').each(function () {
    //    var ionRangeSlider = $.HSCore.components.HSIonRangeSlider.init($(this));
    //});
}