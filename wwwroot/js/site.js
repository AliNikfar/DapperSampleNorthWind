function toggleAllCheckBox(el)
{
    if ($(el).is(':checked')) {
        $('table tbody input[type="checkbox"]').attr('checked', 'checked');
    }
    else {
        $('table tbody input[type="checkbox"]').removeAttr('checked');
    }

}

function DeleteSelectedRow() {
    let ids = []
    let rows = $('table tbody input[type="checkbox"]:checked');
    rows.each(function () { ids.push($(this).attr("id")) });
    //console.log(rows);
    //console.log(ids);
    $.post("/Product/BulkDelete", { ids: ids }, function () {
        alert("remove sucsess");
        location.reload();
    })
}


