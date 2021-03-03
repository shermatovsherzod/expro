var _lawAreasWithParent;
var _selectedParentLawArea;
var _selectedChildLawAreas;

function BindLawAreaPickers(
    parentLawAreaInputSelect,
    childLawAreasInputSelect,
    lawAreasWithParent,
    selectedParentLawArea,
    selectedChildLawAreas) {
    if (lawAreasWithParent == undefined || lawAreasWithParent.length == 0)
        return;

    _lawAreasWithParent = lawAreasWithParent;
    _selectedParentLawArea = selectedParentLawArea;
    _selectedChildLawAreas = selectedChildLawAreas;

    $(parentLawAreaInputSelect).change(function () {
        var value = $(this).val();
        if (value == undefined || value == "")
        {
            $(childLawAreasInputSelect).html("");
            $(childLawAreasInputSelect).trigger("change", [false]);
            _selectedChildLawAreas = undefined;

            return;
        }

        _selectedParentLawArea = "" + value;

        var childItems = _lawAreasWithParent.filter(IsChildLawArea);
        var html = "";
        for (var i = 0; i < childItems.length; i++) {
            var selected = "";
            if (_selectedChildLawAreas != undefined
                && _selectedChildLawAreas.includes(Number(childItems[i].value))) {
                selected = "selected=selected";
            }

            html += "<option value='" + childItems[i].value + "' " + selected + ">" + childItems[i].text + "</option>";
        }

        $(childLawAreasInputSelect).html(html);
        $(childLawAreasInputSelect).trigger("change", [false]);
        _selectedChildLawAreas = undefined;
    });

    if (selectedParentLawArea != undefined || selectedParentLawArea != "") {
        $(parentLawAreaInputSelect).change();
    }
        
}

function IsChildLawArea(item) {
    return item.parentValue == _selectedParentLawArea;
}