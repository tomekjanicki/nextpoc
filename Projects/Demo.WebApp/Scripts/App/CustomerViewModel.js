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
}


function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.listVisible = window.ko.observable(true);
    self.insertOrUpdateVisible = window.ko.observable(false);

    self.customers = window.ko.observableArray([]);

    self.customer = window.ko.observable(null);

    self.initCustomers = function () {
        ajaxGetOrDelete("GET", "customers/?top=1000&skip=0",
            function (data) {
                var itemList = data.items;
                var items = [];
                $.each(itemList, function (i, item) {
                    items.push(new Customer(item.id, item.name, item.surname, item.phoneNumber, item.address, item.version));
                });
                self.customers(items);
            }),
            function () {
                alert("error");
            }
    }

    self.deleteCustomer = function(customer) {
        ajaxGetOrDelete("DELETE", "customers/" + customer.id + "?version=" + customer.version,
            function() {
                self.customers.remove(customer);
            },
            function() {
                alert("error");
            });
    }

    self.backToList = function() {
        self.listVisible(true);
        self.insertOrUpdateVisible(false);
        self.customer(null);
    }

    self.addCustomer = function() {
        var observableCustomer = new ObservableCustomer(0, "", "", "", "", "");
        self.customer(observableCustomer);
        self.listVisible(false);
        self.insertOrUpdateVisible(true);        
    }

    self.editCustomer = function(customer) {
        ajaxGetOrDelete("GET", "customers/" + customer.id,
            function (data) {
                var observableCustomer = new ObservableCustomer(data.id, data.name, data.surname, data.phoneNumber, data.address, data.version);
                self.customer(observableCustomer);
                self.listVisible(false);
                self.insertOrUpdateVisible(true);
            },
            function () {
                alert("error");
            });
    }

    self.insertOrUpdateCustomer = function () {
        var version = self.customer().version();
        if (version === "") {
            var postData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '" }';
            ajaxPostOrPut("POST",
                "customers/",
                postData,
                function () {
                    self.listVisible(true);
                    self.insertOrUpdateVisible(false);
                    self.initCustomers();
                    self.customer(null);
                },
                function () {
                    alert("error");
                });            
        } else {
            var putData = '{ "name": "' + self.customer().name() + '", "surname": "' + self.customer().surname() + '", "phoneNumber": "' + self.customer().phoneNumber() + '", "address": "' + self.customer().address() + '", "version": "' + self.customer().version() + '" }';
            ajaxPostOrPut("PUT",
                "customers/" + self.customer().id(),
                putData,
                function () {
                    self.listVisible(true);
                    self.insertOrUpdateVisible(false);
                    self.initCustomers();
                    self.customer(null);
                },
                function () {
                    alert("error");
                });            
        }

    }

    function ajaxGetOrDelete(method, endpoint, doneFn, failFn) {
        $.ajax({
            method: method,
            url: self.baseUrl + endpoint,
            cache: false,
            contentType: "application/json; charset=UTF-8"
        }).done(function (result) {
            doneFn(result);
        }).fail(function () {
            failFn();
        });
    }

    function ajaxPostOrPut(method, endpoint, data, doneFn, failFn) {
        $.ajax({
            method: method,
            cache: false,
            url: self.baseUrl + endpoint,
            data: data,
            contentType: "application/json; charset=UTF-8"
        }).done(function (result) {
            doneFn(result);
        }).fail(function () {
            failFn();
        });
    }
}

$(function () {
    var vm = new CustomerViewModel("http://localhost:2776/");
    vm.initCustomers();
    window.ko.applyBindings(vm);
})