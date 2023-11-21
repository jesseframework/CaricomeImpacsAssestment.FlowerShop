
$(document).ready(function () {
    var baseUrl = `${window.location.protocol}//${window.location.host}`;
    function saveShippingAddress(updatecartbutton) {
        return new Promise((resolve, reject) => {
            var shippingStreet = $('#ShipIdStreet').val();
            var shippingCity = $('#ShipIdCity').val();
            var shippingState = $('#ShipIdState').val();
            var shippingPostalCode = $('#ShipIdPostalCode').val();
            var shippingCountryId = $('#ShippingCountryId').val();
            var shippingId = $('#IdshippingId').val();
            var id = $('#shipping-address-id').val();
            var url = `${baseUrl}/api/app/address/${encodeURIComponent(id)}`;
            debugger
            abp.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify({
                    street: shippingStreet,
                    city: shippingCity,
                    state: shippingState,
                    postalCode: shippingPostalCode,
                    countryId: shippingCountryId,
                    addressTypeId: shippingId
                    
                }),
                success: function (response) {
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while saving shipping address: ' + error.statusText);
                }
            });
        });
    }

    function saveBillingAddress(updatecartbutton) {
        return new Promise((resolve, reject) => {
            debugger
            var billingStreet = $('#IdStreet').val();
            var billingCity = $('#IdCity').val();
            var billingState = $('#IdState').val();
            var billingPostalCode = $('#IdPostalCode').val();
            var billingCountryId = $('#billingCountryId').val();
            var billingId = $('#IdbillingId').val();
            var id = $('#billione-address-id').val();
            var url = `${baseUrl}/api/app/address/${encodeURIComponent(id)}`;

            abp.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify({
                    street: billingStreet,
                    city: billingCity,
                    state: billingState,
                    postalCode: billingPostalCode,
                    countryId: billingCountryId,
                    addressTypeId: billingId
                }),
                success: function (response) {
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while saving billing address: ' + error.statusText);
                }
            });
        });
    }

    function saveContact(updatecartbutton) {

        return new Promise((resolve, reject) => {
            debugger
            var firstName = $('#IdFirstName').val();
            var lastName = $('#IdLastName').val();
            var email = $('#IdEmail').val();
            var phoneNumber = $('#IdPhoneNumber').val();
            var id = $('#contact-id').val();
            var url = `${baseUrl}/api/app/contact/${encodeURIComponent(id)}`;

            var contactData = {
                firstName: firstName,
                lastName: lastName,
                email: email,
                phoneNumber: phoneNumber
            };

            abp.ajax({
                url: url,
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(contactData),
                success: function (response) {
                    resolve(response);
                },
                error: function (error) {
                    abp.notify.error('An error occurred while saving the contact: ' + error.statusText);
                }
            });

        });
    }

    function deleteCustomerAccount() {
        return new Promise((resolve, reject) => {
            var id = $('#customer-account-id').val();
            var url = `${baseUrl}/api/app/customer-account/${encodeURIComponent(id)}`;

            abp.ajax({
                url: url,
                type: 'DELETE',
                contentType: 'application/json',
                success: function (response) {
                    resolve(response);
                    abp.notify.success('Customer account deleted successfully.');
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while deleting the customer account: ' + error.statusText);
                }
            });
        });
    }

    $('.save-cust').click(function (e) {
        e.preventDefault();
        abp.ui.block({ busy: true });
        saveShippingAddress($(this)).then(() => {
            return saveBillingAddress($(this));
        }).then(() => {
            return saveContact($(this));
        }).then(() => {
            abp.ui.unblock();
            abp.notify.success('Account was update successfully.');
        }).catch((error) => {
            abp.notify.error('An error occurred: ' + error.statusText);
        });
    });
});