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
var _localizer;

function SetVariables(
    documentTypeName,
    documentStatusesEnum,
    documentPriceTypesEnumPaid,
    localizer)
{
    documentType = documentTypeName;
    documentStatuses = documentStatusesEnum;
    documentPriceTypePaid = documentPriceTypesEnumPaid;
    _localizer = localizer;
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
                    var url = '/Admin/' + documentType + '/Details/' + full.id;

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
                    /*html += '                       <div class="d-none d-sm-inline-block">';
                    html += '                           <h6 class="mb-0">';
                    html += '                               <p class="text-dark">';
                    if (full.contentType == '@((int)DocumentContentTypesEnum.file)') {
                        html += '                               <i class="fa fa-file" aria-hidden="true"></i> Есть вложенный файл';
                    }
                    else {
                        html += full.text;
                    }
                    html += '                               </p>';
                    html += '                           </h6>';
                    html += '                       </div>';*/
                    html += '                       <div class="d-none d-sm-block">';
                    for (var i = 0; i < full.lawAreas.length; i++) {
                        html += '                       <span class="badge badge-primary">' + full.lawAreas[i].name + '</span>';
                    }
                    html += '                       </div>';
                    html += '                   </div>';
                    html += '                   <div class="col-12 col-md mt-3 mt-md-2">';
                    if (full.priceType == documentPriceTypePaid)
                        html += '                   <span class="badge badge-soft-info mr-2">' + _localizer["Price"] + ': ' + full.priceStr + ' ' + _localizer["soum"] + '</span>';
                    html += InsertStatusAlert(full);
                    html += '                   </div>';
                    html += '               </div>';
                    html += '           </div>';
                    html += '       </div>';
                    html += '   </div>';
                    html += '   <div class="card-footer">';
                    html += '       <ul class="list-inline list-separator small text-body">';
                    html += '           <li class="list-inline-item">' + _localizer["Author"] + ': ' + full.author.fullName + '</li>';
                    html += '           <li class="list-inline-item">' + _localizer["DateModified"] + ': ' + full.dateModified + '</li>';
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

    $("#statusDD, #priceTypeDD").change(function () {
        table.draw();
    });
});

function InsertStatusAlert(full) {
    var badgeClass = "";
    if (full.status.id == documentStatuses.waitingForApproval)
        badgeClass = "-warning";
    else if (full.status.id == documentStatuses.approved)
        badgeClass = "-success";
    else if (full.status.id == documentStatuses.rejected)
        badgeClass = "-danger";


    var html = '<span class="badge badge-soft' + badgeClass + ' mr-2">';
    html += '   <span class="legend-indicator bg' + badgeClass + '"></span>' + full.status.name;
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