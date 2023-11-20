$(function () {
    //var l = abp.localization.getResource('CustomerAccount');

    var dataTable = $('#CustomerTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(caricomeImpacsAssestment.flowerShop.customer.customerAccount.getManagmentCustomer),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: ('FullName'),
                    data: "contact",
                    render: function (data) {
                        return data.firstName + ' ' + data.lastName;
                    }
                },
                {
                    title: ('Email'),
                    data: "contact.email"
                },
                {
                    title: ('Phone'),
                    data: "contact.phoneNumber"
                },
                {
                    title: ('Address'),
                    data: "address",
                    render: function (data) {
                        return data.street + ', ' + data.city + ', ' + data.state + ', ' + data.postalCode;
                    }
                },
                {
                    title: ('AddressType'),
                    data: "addressType.type"
                },
                {
                    title: ('CreationTime'),
                    data: "contact.creationTime",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'Admin/Customer/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCustomerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});


