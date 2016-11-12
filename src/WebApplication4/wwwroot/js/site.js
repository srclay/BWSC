function UpdateQuantity(id,qty) {
    $.ajax({
        type: "POST",
        url: 'CartItems/Update/',
        data: { id: id , qty: qty},
    })
}
