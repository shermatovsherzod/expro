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
                    return '' + full.amountStr + ' сум';
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
    var html = "<div class='alert ";
    if (full.status.id == withdrawRequestStatusPending)
        html += "alert-secondary'>";
    else if (full.status.id == withdrawRequestStatusCompleted)
        html += "alert-success'>";

    html += full.status.name;

    if (full.status.id == withdrawRequestStatusCompleted)
        html += "<br> " + full.dateCompleted;

    html += "</div>";

    return html;
}