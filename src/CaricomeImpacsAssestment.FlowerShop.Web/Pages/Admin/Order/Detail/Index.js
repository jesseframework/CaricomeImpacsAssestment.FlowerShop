$(document).ready(function () {
    var baseUrl = `${window.location.protocol}//${window.location.host}`;

    function deleteCustomerAccount(button) {
        return new Promise((resolve, reject) => {
            var id = button.attr('id').split('_')[1];
            var url = `${baseUrl}/api/app/customer-account/${encodeURIComponent(id)}`;

            abp.ajax({
                url: url,
                type: 'DELETE',
                contentType: 'application/json',
                success: function (response) {
                    resolve(response);
                    
                },
                error: function (error) {
                    reject(error);
                    abp.notify.error('An error occurred while deleting the customer account: ' + error.statusText);
                }
            });
        });
    }

    $('.delete-cust').click(function (e) {
        e.preventDefault();
        var button = $(this);
        abp.message.confirm('Are you sure to delete customer?')
            .then(function (confirmed) {
                if (confirmed) {
                    abp.ui.block({ busy: true });
                    deleteCustomerAccount(button).then(() => {
                        abp.ui.unblock();
                        abp.notify.success('Account was deleted successfully.');
                        window.location.reload(); 
                    }).catch((error) => {
                        abp.notify.error('An error occurred: ' + error.statusText);
                    });
                }
            });
    });
});
