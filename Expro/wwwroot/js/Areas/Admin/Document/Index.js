var documentType = "";
var documentStatuses =
{
    waitingForApproval: 0,
    approved: 0,
    rejected: 0
};
//var documentStatusWaitingForApproval = 0;
//var documentStatusApproved = 0;
//var documentStatusRejected = 0;

var documentPriceTypePaid = 0;

function SetVariables(
    documentTypeName,
    documentStatusesEnum,
    documentPriceTypesEnumPaid)
{
    documentType = documentTypeName;
    documentStatuses = documentStatusesEnum;
    documentPriceTypePaid = documentPriceTypesEnumPaid;
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
            url: "/Admin/" + documentType + "/Search",
            type: "POST",
            //contentType: "application/json",
            datatype: "json",
            data: function (d) {
                d.statusID = $("#statusDD").val();
                d.priceType = $("#priceTypeDD").val();
            },
            //complete: function () {
            //    StartLoadingImagesAsync();
            //}
        },
        columns: [
            {
                "data": 0, //"name": "statusId", "autoWidth": true,
                "render": function (data, type, full, meta) {
                    var html = '';
                    html += '<div class="card" style="margin-top: 10px;">';
                    html += '   <div class="card-body">';
                    html += '       <h3><a href="/Admin/' + documentType + '/Details/' + full.id + '">' + full.title + '</a></h3>';

                    html += InsertStatusAlert(full);

                    //if (full.contentType == documentPriceTypePaid)
                    //    html += '   <p>Есть вложенный файл</p>';
                    //else
                    //    html += '   <p>' + full.text + '</p>';

                    html += '       <p>Дата изменения: ' + full.dateModified + '</p>';
                    html += '       <p>Автор: <a href="#">' + full.author.fullName + '</a></p>';
                    html += '       <p>';
                    for (var i = 0; i < full.lawAreas.length; i++) {
                        html += '       <span class="badge badge-primary">' + full.lawAreas[i].name + '</span>';
                    }
                    html += '       </p>';
                    //console.log(full.title);
                    //console.log(full.priceType);
                    if (full.priceType == documentPriceTypePaid)
                        html += '   <p>Цена: <b class="text-danger">' + full.priceStr + ' сум</b></p>';
                    html += '        <a href="/Admin/' + documentType + '/Details/' + full.id + '" class="btn btn-outline-primary" target="_blank">Открыть</a>';
                    html += '   </div>';
                    html += '</div>';
                    return html;
                }
            }
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
    //if (full.status.id == "@((int)DocumentStatusesEnum.WaitingForApproval)")
    if (full.status.id == documentStatuses.waitingForApproval)
        html += "alert-warning'>";
    else if (full.status.id == documentStatuses.approved)
        html += "alert-success'>";
    else if (full.status.id == documentStatuses.rejected)
        html += "alert-danger'>";

    html += full.status.name;
    html += "</div>";

    return html;
}