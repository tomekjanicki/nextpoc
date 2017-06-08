function CustomerViewModel(baseUrl) {
    var self = this;

    self.baseUrl = baseUrl;

    self.testClick = function() {
        alert("ala ma kota");
    }
}

$(function () {
    var vm = new CustomerViewModel("http://localhost:2776/");
    window.ko.applyBindings(vm);
})