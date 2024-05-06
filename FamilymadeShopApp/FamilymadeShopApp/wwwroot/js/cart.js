var isAuthenticated = @User.Identity.IsAuthenticated ? true : false;
var isVIP = false;

function OnClickIncreaseQuantity(productId) {
    var quantityInput = document.querySelector(`.form-control-quantity-${productId}`);
    quantityInput.value = parseInt(quantityInput.value) + 1;
    OnQuantityChanged(quantityInput.value, productId)
};

function OnClickDecreaseQuantity(productId) {
    var quantityInput = document.querySelector(`.form-control-quantity-${productId}`);
    var newValue = parseInt(quantityInput.value) - 1;
    if (newValue > 0) {
        quantityInput.value = newValue;
        OnQuantityChanged(quantityInput.value, productId)
    }
};

function OnQuantityChanged(quantity, productId) {
    var dataToSend = {
        ProductId: productId,
        Quantity: quantity
    };

    fetch('/Cart?handler=ChangeQuantity', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => response.text())
        .then(data => {
            var parsedData = JSON.parse(data);
            UpdateCart(parsedData.cartItems);
            UpdateSummary(parsedData.cartItems);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function OnClickRemoveProduct(productId) {
    var dataToSend = {
        ProductId: productId,
    };

    fetch('/Cart?handler=RemoveProduct', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        body: JSON.stringify(dataToSend)
    })
        .then(response => response.text())
        .then(data => {
            var productCountElement = document.querySelector(`.cart-count-items`);
            var parsedData = JSON.parse(data);
            productCountElement.textContent = `Cart - ${parsedData.cartItems.length} items`;
            UpdateCart(parsedData.cartItems);
            UpdateSummary(parsedData.cartItems);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function UpdateCart(cartItems) {
    var cartElement = document.querySelector('.cart-items');

    cartElement.innerHTML = '';

    cartItems.forEach((cartItem, index) => {
        var cartItemHtml = `
                <div class="row">
                    <div class="product-${cartItem.productId}">
                        <div class="col-lg-3 col-md-12 mb-4 mb-lg-0">
                            <div class="bg-image hover-overlay hover-zoom ripple rounded" data-mdb-ripple-color="light">
                                <img src="https://via.placeholder.com/650x500" class="w-100" />
                                <a href="/Product?id=${cartItem.productId}">
                                    <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                                </a>
                            </div>
                        </div>

                        <div class="col-lg-5 col-md-6 mb-4 mb-lg-0">
                            <p><strong>${cartItem.name}</strong></p>

                            <button type="button" class="btn btn-danger btn-sm me-1 mb-2" onclick="OnClickRemoveProduct(${cartItem.productId})" data-mdb-toggle="tooltip" title="Remove item">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>

                        <div class="col-lg-4 col-md-6 mb-4 mb-lg-0">
                            <div class="">
                                <div class="input-group mb-3">
                                    <button class="btn btn-white border border-secondary px-3" type="button" onclick="OnClickDecreaseQuantity(${cartItem.productId})">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                            <input class="form-control text-center border border-secondary form-control-quantity-${cartItem.productId}"
                                        value="${cartItem.quantity}"
                                        onchange="OnQuantityChanged(this.value, ${cartItem.productId})" />
                                    <button class="btn btn-white border border-secondary px-3" type="button" onclick="OnClickIncreaseQuantity(${cartItem.productId})">
                                        <i class="fas fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <p class="text-md-center">
                                <strong class="product-total-price">€${cartItem.price.toFixed(2)} x ${cartItem.quantity} = €${(cartItem.price * cartItem.quantity).toFixed(2)}</strong>
                            </p>
                        </div>
                    </div>
                </div>`;

        cartElement.innerHTML += cartItemHtml;

        if (index !== cartItems.length - 1) {
            cartElement.innerHTML += '<hr class="my-4" />';
        }
    });
}

function UpdateSummary(cartItems) {
    var cartSummaryElement = document.querySelector('.cart-summary');

    var totalPrice = cartItems.reduce((total, cartItem) => total + (cartItem.price * cartItem.quantity), 0);

    var summaryHtml = `
            <ul class="list-group list-group-flush">
                <li class="list-group-item d-flex justify-content-between align-items-center border-0 px-0 pb-0">
                    Products
                    <span>€${totalPrice.toFixed(2)}</span>
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center px-0">
                    Shipping
                    ${isAuthenticated ? (isVIP ? '<span>Free</span>' : '<span>€10.00</span>') : '<span>€10.00</span>'}
                </li>
                <li class="list-group-item d-flex justify-content-between align-items-center border-0 px-0 mb-3">
                    <div>
                        <strong>Total amount</strong>
                    </div>
                    <span>${isAuthenticated ? (isVIP ? `<strong>€${totalPrice.toFixed(2)}</strong>` : `<strong>€${(totalPrice + 10).toFixed(2)}</strong>`) : `<strong>€${(totalPrice + 10).toFixed(2)}</strong>`}</span>
                </li>
            </ul>

            <button type="button" class="btn btn-primary btn-lg btn-block">
                Go to checkout
            </button>
        `;

    cartSummaryElement.innerHTML = summaryHtml;
}
