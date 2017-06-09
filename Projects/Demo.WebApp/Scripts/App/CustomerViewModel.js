function Customer(id, name, surname, phoneNumber, address, version) {
    var self = this;
    self.id = id;
    self.name = name;
    self.surname = surname;
    self.phoneNumber = phoneNumber;
    self.address = address;
    self.version = version;
}

function ObservableCustomer(id, name, surname, phoneNumber, address, version) {
    var self = this;
    self.id = window.ko.observable(id);
    self.name = window.ko.observable(name);
    self.surname = window.ko.observable(surname);
    self.phoneNumber = window.ko.observable(phoneNumber);
    self.address = window.ko.observable(address);
    self.version = window.ko.observable(version);
    self.nameValidationText = window.ko.observable("");
    self.surnameValidationText = window.ko.observable("");
    self.phoneNumberValidationText = window.ko.observable("");
    self.addressValidationText = window.ko.observable("");
}


function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.editVisible = window.ko.observable(false);

    self.customers = window.ko.observableArray([]);

    self.customer = window.ko.observable(null);

    self.initCustomers = function () {
        ajax("GET", "customers/?top=1000&skip=0", null,
            function (data) {
                var itemList = data.items;
                var items = [];
                $.each(itemList, function (i, item) {
                    items.push(new Customer(item.id, item.name, item.surname, item.phoneNumber, item.address, item.version));
                });
                self.customers(items);
            }),
            function (data) {
                alert(data.responseText);
            }
    }

    self.deleteCustomer = function (customer) {
        ajax("DELETE", "customers/" + customer.id + "?version=" + customer.version, null,
            function () {
                self.customers.remove(customer);
            },
            function (data) {
                alert(data.responseText);
            });
    }

    self.backToList = function () {
        self.editVisible(false);
        self.customer(null);
    }

    self.addCustomer = function () {
        var observableCustomer = new ObservableCustomer(0, "", "", "", "", "");
        self.customer(observableCustomer);
        self.editVisible(true);
    }

    self.editCustomer = function (customer) {
        ajax("GET", "customers/" + customer.id, null,
            function (data) {
                var observableCustomer = new ObservableCustomer(data.id, data.name, data.surname, data.phoneNumber, data.address, data.version);
                self.customer(observableCustomer);
                self.editVisible(true);
            },
            function (data) {
                alert(data.responseText);
            });
    }

    self.insertOrUpdateCustomer = function () {
        var version = self.customer().version();
        var postOrPutData;
        var method;
        var url;
        var customerIsValid = validateCustomer();
        if (customerIsValid) {
            if (version === "") {
                method = "POST";
                postOrPutData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '" }';
                url = "customers/";
            } else {
                method = "PUT";
                postOrPutData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '", "version": "' + self.customer().version() + '" }';
                url = "customers/" + self.customer().id();
            }
            ajax(method,
                url,
                postOrPutData,
                function () {
                    self.editVisible(false);
                    self.initCustomers();
                    self.customer(null);
                },
                function (data) {
                    alert(data.responseText);
                });
            
        }
    }

    function validateCustomer() {
        var valid = true;
        var customer = self.customer(); 
        if (customer.name() !== "") {
            if (customer.name().length >= 50) {
                customer.nameValidationText("");
            } else {
                customer.nameValidationText("Name cannot be longer than 50 chars");
                valid = false;
            }
        } else {
            customer.nameValidationText("Name is required");
            valid = false;
        }
        return valid;
    }

    function ajax(method, endpoint, data, doneFn, failFn) {
        $.ajax({
            method: method,
            cache: false,
            url: self.baseUrl + endpoint,
            data: data,
            contentType: "application/json; charset=UTF-8"
        }).done(function (result) {
            doneFn(result);
        }).fail(function (result) {
            failFn(result);
        });
    }
}

$(function () {
    var vm = new CustomerViewModel("http://localhost:2776/");
    vm.initCustomers();
    window.ko.applyBindings(vm);
})