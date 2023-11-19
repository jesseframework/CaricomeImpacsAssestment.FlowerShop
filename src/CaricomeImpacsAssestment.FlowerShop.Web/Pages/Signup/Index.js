$(document).ready(function () {
    var baseUrl = `${window.location.protocol}//${window.location.host}`;

    //ToDo: Lookiup addresstype id from server
    var adt = "3a0eefdd-0124-b1aa-4f8d-35f15c43b419";
    var sdt = "3a0eefdd-47bb-7748-beec-ba3f3f20f0c3";
    var setContactId = "";
    var currencyId = "";

    var setBillingAddressId = "";
    var setShippingAddressId = "";
 
    
    var sameAddressRadio = document.getElementById('sameAddress');
   
    var $form = $('#registrationForm'); 
    $form.validate({
        rules: {
            userName: {
                required: true,
                minlength: 3
            },
            emailAddress: {
                required: true,
                email: true
            },
            phone: {
                required: true,
                digits: true,
                minlength: 10,
                maxlength: 15
            },
            password: {
                required: true,
                minlength: 6
            },
            confirmpassword: {
                required: true,
                minlength: 6,
                equalTo: '#password'
            },
            fullName: {
                required: true,
                customSpaceValidation: true
            },
            countryId: {
                required: true 
            },
            postalCode: {
                required: function () {                    
                    var selectedCountry = $('#countryId').val();
                    return selectedCountry === 'US' || selectedCountry === 'CA';
                }
            },           
            shippingStreet: {
                required: function () {
                    return $('#shippingAddressSection').is(':visible');
                }
            },
            shippingCity: {
                required: function () {
                    return $('#shippingAddressSection').is(':visible');
                }
            },
            
            billingStreet: {
                required: function () {
                    return $('#billingAddressSection').is(':visible');
                }
            },
            billingCity: {
                required: function () {
                    return $('#billingAddressSection').is(':visible');
                }
            },
           
        },
        messages: {
            fullName: {
                required: 'Full name must contain a space.'
            },
            countryId: {
                required: 'Please select a country.'
            },
            postalCode: {
                required: 'Postal code is required for United States and Canada only.'
            },
           
        },
        errorElement: 'span',
        errorClass: 'error',
        validClass: 'valid',
        highlight: function (element, errorClass, validClass) {
            $(element).addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass);
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },
        submitHandler: function (form) {
            registerUser(); 
        }
    });

    
    $.validator.addMethod('customSpaceValidation', function (value, element) {
        return value.trim().indexOf(' ') !== -1;
    }, 'Full name must contain a space.');

    function registerUser() {

        return new Promise((resolve, reject) => {
            var userName = $('#userName').val();
            var emailAddress = $('#emailAddress').val();
            var password = $('#password').val();
            abp.appName = 'CIMPACTFlowerShop';


            abp.ajax({
                url: baseUrl + '/api/account/register',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    userName: userName,
                    emailAddress: emailAddress,
                    password: password,
                    appName: abp.appName
                }),
                success: function (response) {
                    resolve(response);

                },
                error: function (e) {
                    abp.notify.error('An error occurred: ' + e.statusText);
                }
            });
        });
        
    }
    
    //function saveAddress() {

    //    return new Promise((resolve, reject) => {

    //        var shippingStreet = $('#shippingStreet').val();
    //        var shippingCity = $('#shippingCity').val();
    //        var shippingState = $('#shippingState').val();
    //        var shippingPostalCode = $('#shippingPostalCode').val();
    //        var shippingCountryId = $('#shippingCountryId').val();

    //        var billingStreet = $('#billingStreet').val();
    //        var billingCity = $('#billingCity').val();
    //        var billingState = $('#billingState').val();
    //        var billingPostalCode = $('#billingPostalCode').val();
    //        var billingCountryId = $('#billingCountryId').val();

    //        if (sameAddressRadio.checked) {

    //            shippingStreet = billingStreet;
    //            shippingCity = billingCity;
    //            shippingState = billingState;
    //            shippingPostalCode = billingPostalCode;
    //            shippingCountryId = billingCountryId;

    //        }

    //        abp.ajax({
    //            url: baseUrl + '/api/app/address',
    //            type: 'POST',
    //            contentType: 'application/json',
    //            data: JSON.stringify({
    //                street: shippingStreet,
    //                city: shippingCity,
    //                state: shippingState,
    //                postalCode: shippingPostalCode,
    //                countryId: shippingCountryId,
    //                addressTypeId: sdt
    //            }),
    //            success: function (shippingResponse) {
    //                setShippingAddressId = shippingResponse.Id;
    //                abp.ajax({
    //                    url: baseUrl + '/api/app/address',
    //                    type: 'POST',
    //                    contentType: 'application/json',
    //                    data: JSON.stringify({
    //                        street: billingStreet,
    //                        city: billingCity,
    //                        state: billingState,
    //                        postalCode: billingPostalCode,
    //                        countryId: billingCountryId,
    //                        addressTypeId: adt
    //                    }),
    //                    success: function (billingResponse) {
    //                        setBillingAddressId = billingResponse.Id;
    //                        resolve(response);

    //                    },
    //                    error: function (billingError) {
    //                        abp.notify.error('An error occurred while saving account: ' + billingError.statusText);
    //                    }
    //                });
    //            },
    //            error: function (shippingError) {
    //                abp.notify.error('An error occurred while saving shipping address: ' + shippingError.statusText);
    //            }
    //        });
    //    });
    //}

    function saveShippingAddress() {
        return new Promise((resolve, reject) => {
            var shippingStreet = $('#shippingStreet').val();
            var shippingCity = $('#shippingCity').val();
            var shippingState = $('#shippingState').val();
            var shippingPostalCode = $('#shippingPostalCode').val();
            var shippingCountryId = $('#shippingCountryId').val();

            abp.ajax({
                url: baseUrl + '/api/app/address',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    street: shippingStreet,
                    city: shippingCity,
                    state: shippingState,
                    postalCode: shippingPostalCode,
                    countryId: shippingCountryId,
                    addressTypeId: sdt
                }),
                success: function (response) {
                    setShippingAddressId = response.id;
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while saving shipping address: ' + error.statusText);
                }
            });
        });
    }

    function saveBillingAddress() {
        return new Promise((resolve, reject) => {
            var billingStreet = $('#billingStreet').val();
            var billingCity = $('#billingCity').val();
            var billingState = $('#billingState').val();
            var billingPostalCode = $('#billingPostalCode').val();
            var billingCountryId = $('#billingCountryId').val();

            abp.ajax({
                url: baseUrl + '/api/app/address',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    street: billingStreet,
                    city: billingCity,
                    state: billingState,
                    postalCode: billingPostalCode,
                    countryId: billingCountryId,
                    addressTypeId: adt
                }),
                success: function (response) {
                    setBillingAddressId = response.id;
                    resolve(response);
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while saving billing address: ' + error.statusText);
                }
            });
        });
    }

    function saveContact() {

        return new Promise((resolve, reject) => {
            var fullName = $('#fullName').val();
            var email = $('#emailAddress').val();
            var phoneNumber = $('#phone').val();


            var nameParts = fullName.split(' ');
            var firstName = nameParts[0] || '';
            var lastName = nameParts.slice(1).join(' ') || '';

            var contactData = {
                firstName: firstName,
                lastName: lastName,
                email: email,
                phoneNumber: phoneNumber
            };

            abp.ajax({
                url: baseUrl + '/api/app/contact',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(contactData),
                success: function (response) {
                    setContactId = response.id;
                    resolve(response);
                },
                error: function (error) {
                    abp.notify.error('An error occurred while saving the contact: ' + error.statusText);
                }
            });

        });
    }

    function saveCustomerAccount() {
        return new Promise((resolve, reject) => {
            var uniqueRandomNumber = generateUniqueRandomNumber();
            var fullName = $('#fullName').val();
            var email = $('#emailAddress').val();
            var billingCountryId = $('#billingCountryId').val();
            var shippingCountryId = $('#shippingCountryId').val();
            var currencyId = $('#currencyId').val();

            var customerAccountData = {
                name: fullName,
                email: email,
                userId: "3a0efa3a-7ec4-b591-06f5-6407fa6922ca",
                customerNo: uniqueRandomNumber.toString(),
                contactId: setContactId,
                billingAddressId: setBillingAddressId,
                shippingAddressId: setShippingAddressId,
                countryId: billingCountryId,
                currencyId: currencyId
            };
            debugger
            abp.ajax({
                url: baseUrl + '/api/app/customer-account',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(customerAccountData),
                success: function (response) {
                    abp.notify.success('Customer account created successfully.');
                },
                error: function (error) {
                    abp.notify.error('An error occurred while creating the customer account: ' + error.statusText);
                }
            });
        });

    }

    function saveAddress() {
        if (sameAddressRadio.checked) {
            return saveBillingAddress().then(response => {                
                $('#shippingStreet').val($('#billingStreet').val());
                $('#shippingCity').val($('#billingCity').val());
                $('#shippingState').val($('#billingState').val());
                $('#shippingPostalCode').val($('#billingPostalCode').val());
                $('#shippingCountryId').val($('#billingCountryId').val());
                return saveShippingAddress();
            });
        } else {
            return saveShippingAddress().then(saveBillingAddress);
        }
    }

    $('#registerUserBtn').click(function (e) {
        e.preventDefault();

        registerUser().then(() => {            
            return saveAddress();        
        }).then(() => { 
            return saveContact();
        }).then(() => {            
            return saveCustomerAccount();
        }).then(() => {            
            abp.notify.success('Your account was created successfully.');
        }).catch((error) => {            
            abp.notify.error('An error occurred: ' + error.statusText);
        });
    });
  
});

function generateUniqueRandomNumber() {    
    var timestamp = new Date().getTime();   
    var randomComponent = Math.floor(Math.random() * 100000);    
    var uniqueNumber = timestamp + randomComponent;
    return uniqueNumber;
}

document.addEventListener('DOMContentLoaded', function () {
    var sameAddressRadio = document.getElementById('sameAddress');
    var differentAddressRadio = document.getElementById('differentAddress');
    var shippingInfoSection = document.getElementById('shippingInfo');

    sameAddressRadio.addEventListener('change', function () {
        if (this.checked) {
            shippingInfoSection.style.display = 'none';
        }
    });

    differentAddressRadio.addEventListener('change', function () {
        if (this.checked) {
            shippingInfoSection.style.display = 'block';
        }
    });
});


