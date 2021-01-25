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
                    var url = '/' + documentType + '/Details/' + full.id;

                    //html += '<div class="col-12">';
                    html += '<div class="card card-bordered card-hover-shadow mb-5">';
                    html += '   <div class="card-body">';
                    html += '       <div class="d-sm-flex">';
                    html += '           <div class="media-body">';
                    html += '               <div class="row">';
                    html += '                   <div class="col col-md-8">';
                    html += '                       <h3 class="mb-0">';
                    html += '                           <a class="text-dark" href="' + url + '" target="_blank">' + full.title + '</a>';
                    html += '                       </h3>';
                    html += '                       <div class="d-none d-sm-inline-block">';
                    html += '                           <h6 class="mb-0">';
                    html += '                               <p class="text-muted">';
                    if (full.contentType == documentContentTypeFile) {
                        html += '                               <i class="fa fa-file" aria-hidden="true"></i> Есть вложенный файл';
                    }
                    else {
                        html += full.text;
                    }
                    html += '                               </p>';
                    html += '                           </h6>';
                    html += '                       </div>';
                    html += '                       <div class="d-none d-sm-block">';
                    for (var i = 0; i < full.lawAreas.length; i++) {
                        html += '                       <span class="badge badge-primary">' + full.lawAreas[i].name + '</span>';
                    }
                    html += '                       </div>';
                    html += '                   </div>';
                    html += '                   <div class="col-12 col-md mt-3 mt-md-2">';
                    if (full.priceType == documentPriceTypePaid)
                        html += '                   <span class="badge badge-soft-info">Цена: ' + full.priceStr + ' сум</span>';
                    html += '                   </div>';
                    html += '               </div>';
                    html += '           </div>';
                    html += '       </div>';
                    html += '   </div>';
                    html += '   <div class="card-footer">';
                    html += '       <ul class="list-inline list-separator small text-body">';
                    html += '           <li class="list-inline-item">Автор: ' + full.author.fullName + '</li>';
                    html += '           <li class="list-inline-item">Дата публикации: ' + full.datePublished + '</li>';
                    html += '       </ul>';
                    html += '   </div>';
                    html += '</div>';
                    //html += '</div>';

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