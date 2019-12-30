var ViewModelCobertura = function () {
    var self = this;
    self.coberturas = ko.observableArray();
    self.error = ko.observable();
    self.detalle = ko.observable();
    self.nuevaCobertura = {
        Descripcion: ko.observable()
    }
    self.mensajeEliminar = ko.observable();

    var itemEliminar;

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

    var uriCobertura = '/api/cobertura/';

    function getCoberturas() {
        ajaxHelper(uriCobertura, 'GET').done(function (data) {
            self.coberturas(data);
            $('#dataTableCobertura').DataTable();
        });
    }

    self.getCobertura = function (item) {
        ajaxHelper(uriCobertura + item.Id, 'GET').done(function (data) {
            self.detalle(data);
        });
    }

    self.addCobertura = function (formElement) {
        var cobertura = {
            Descripcion: self.nuevaCobertura.Descripcion()
        };

        ajaxHelper(uriCobertura, 'POST', cobertura).done(function (item) {
            self.nuevaCobertura.Descripcion('');
            window.location.reload();
        });
    }

    self.popDeleteCobertura = function (item) {
        itemEliminar = item;
        self.mensajeEliminar("¿Desea eliminar la cobertura " + item.Descripcion + "?");
        $('#coberturaModal').modal('show');
    }

    self.deleteCobertura = function () {
        ajaxHelper(uriCobertura + itemEliminar.Id, 'DELETE').done(function (data) {
            itemEliminar = null;
            window.location.reload();
        });
    }

    getCoberturas();
};

ko.applyBindings(new ViewModelCobertura());