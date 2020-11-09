var documentType = "";
var documentContentTypeFile = 0;
var documentPriceTypePaid = 0;

function SetVariables(
    documentTypeName,
    documentContentTypesEnumFile,
    documentPriceTypesEnumPaid)
{
    documentType = documentTypeName;
    documentContentTypeFile = documentContentTypesEnumFile;
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
            url: "/" + documentType + "/Search",
            type: "POST",
            //contentType: "application/json",
            datatype: "json",
            data: function (d) {
                d.priceType = $("#priceTypeDD").val();

                var lawAreasCheckboxValues = new Array();
                $.each($("input[name='lawAreasCheckbox']:checked"), function () {
                    lawAreasCheckboxValues.push(Number($(this).val()));
                });
                d.lawAreas = lawAreasCheckboxValues;
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
                    var html = '';
                    html += '<div class="card" style="margin-top: 10px;">';
                    html += '   <div class="card-body">';
                    html += '       <h3><a href="/' + documentType + '/Details/' + full.id + '">' + full.title + '</a></h3>';

                    if (full.contentType == documentContentTypeFile)
                        html += '   <p>Есть вложенный файл</p>';
                    //else
                    //    html += '   <p>' + full.text + '</p>';

                    html += '       <p>Дата публикации: ' + full.datePublished + '</p>';
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

    $("#statusDD, #priceTypeDD, input[name=lawAreasCheckbox]").change(function () {
        table.draw();
    });
});