function Customer(id, name, surname, phoneNumber, address, version) {
    var self = this;
    self.id = id;
    self.name = name;
    self.surname = surname;
    self.phoneNumber = phoneNumber;
    self.address = address;
    self.version = version;
}

function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.listVisible = window.ko.observable(true);
    self.insertOrUpdateVisible = window.ko.observable(false);

    self.customers = window.ko.observableArray([]);

    self.testClick = function () {
        alert("ala ma kota");
        ajaxGet("customers/?top=100&skip=0",
            function (data) {
                alert("ok");
                var x = data.items;
                var items = [];
                $.each(x, function (i, item) {
                    items.push(new Customer(item.id, item.name, item.surname, item.phoneNumber, item.address, item.version));
                });
                self.customers(items);
            }),
            function () {
                alert("error");
            }
    }

    function ajaxGet(endpoint, doneFn, failFn) {
        $.ajax({
            method: "GET",
            url: self.baseUrl + endpoint,
            cache: false,
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
    window.ko.applyBindings(vm);
})