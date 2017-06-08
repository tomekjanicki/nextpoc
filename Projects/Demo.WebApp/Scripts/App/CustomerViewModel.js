function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.testClick = function () {
        alert("ala ma kota");
        ajaxGet("customers/1",
            function (data) {
                alert(data);
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