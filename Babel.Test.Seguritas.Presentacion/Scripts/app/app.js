var ViewModelApp = function () {
    var self = this;
    self.clientes = ko.observableArray();
    self.planes = ko.observableArray();
    self.coberturas = ko.observableArray();
    self.error = ko.observable();

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.responseJSON != null) {
                self.error(jqXHR.responseJSON.Message);
            }
        });
    }

    function getTotales() {
        ajaxHelper('/api/App/', 'GET').done(function (data) {
            self.clientes(data.TotalClientes);
            self.planes(data.TotalPlanes);
            self.coberturas(data.TotalCoberturas);
        });
    }

    getTotales();
};

ko.applyBindings(new ViewModelApp());