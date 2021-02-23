var _dataTablesLocalizer;

function SetDataTablesLocalizer(localizer) {
    _dataTablesLocalizer = localizer;
}

function InitDataTableLanguage() {
    var language = {
        infoFiltered: " ",
        info: " ",
        //"info": "Showing _START_ to _END_ of _TOTAL_ entries",
        //info: "Showing page _PAGE_ of _PAGES_",
        processing: _dataTablesLocalizer["Loading"] + "...",
        zeroRecords: _dataTablesLocalizer["ZeroRecords"],
        infoEmpty: _dataTablesLocalizer["ZeroRecords"],
        paginate: {
            previous: "<",
            next: ">",
            first: "<<",
            last: ">>",
        }
    };

    return language;
}