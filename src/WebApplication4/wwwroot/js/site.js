function UpdateQuantity(id,qty) {
    $.ajax({
        type: "POST",
        url: 'CartItems/Update/',
        data: { id: id, qty: qty },
        success: function (result) {
            location.reload();
        }
    });
}
