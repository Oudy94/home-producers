function OnClickIncreaseQuantity() {
    var quantityInput = document.querySelector('.form-control-quantity');
    quantityInput.value = parseInt(quantityInput.value) + 1;
};

function OnClickDecreaseQuantity() {
    var quantityInput = document.querySelector('.form-control-quantity');
    var newValue = parseInt(quantityInput.value) - 1;
    if (newValue > 0) {
        quantityInput.value = newValue;
    }
};

function OnClickBuyNow() {
    var dataToSend = {
        ProductId: @Model.Product.Id,
        Quantity: document.querySelector('.form-control-quantity').value
    };

    fetch('/Product?handler=AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => {
            if (response.ok) {
                window.location.href = '/Cart';
            } else {
                console.error('Failed to process the request:', response.statusText);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
};

function OnClickAddToCart() {
    var dataToSend = {
        ProductId: @Model.Product.Id,
        Quantity: document.querySelector('.form-control-quantity').value
    };

    fetch('/Product?handler=AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => response.text())
        .then(data => {
            UpdateCartBadge()
            alert("Product added to cart.");
        })
        .catch(error => {
            console.error('Error:', error);
        });
};
