function BindRegionPickers(regionInputSelect, cityInputSelect) {
    $(regionInputSelect).change(function () {
        var value = $(this).val();
        if (value == undefined || value == "") {
            $(cityInputSelect).html("");
            $(cityInputSelect).trigger("change", [false]);

            return;
        }

        LoadCitiesByRegion(value, cityInputSelect);
    });
}

function LoadCitiesByRegion(regionID, cityInputSelect) {

    $.ajax({
        type: "GET",
        url: "/City/GetByRegionIDAsSelectList",
        data: {
            regionID: regionID,
            selected: null
        },
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (data) {
            $(cityInputSelect).empty().append($("<option>").text(" ").val(""));
            $.each(data,
                function (i, item) {
                    $(cityInputSelect)
                        .append("<option value='" + item.value + "'>" + item.text + "</option>");

                });
            $(cityInputSelect).trigger("change", [false]);;
        },
        error: function (data) {
            console.log(data);
        }
    });
}