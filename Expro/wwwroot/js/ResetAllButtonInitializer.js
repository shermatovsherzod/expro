function InitResetAllButton() {
    $(".filterForm .resetBtn").click(function () {
        var formSelectItems = $(".filterForm select");
        for (var i = 0; i < formSelectItems.length; i++) {
            $(formSelectItems[i]).val("").trigger("change", [false]);
        }

        table.draw();
    });
}