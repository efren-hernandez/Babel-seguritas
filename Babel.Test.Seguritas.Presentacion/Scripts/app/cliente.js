var ViewModelCliente = function () {
    var self = this;
    self.clientes = ko.observableArray();
    self.error = ko.observable();
    self.detalle = ko.observable();
    self.nuevoCliente = {
        Nombre: ko.observable()
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

    var uriCliente = '/api/cliente/';

    function getClientes() {
        ajaxHelper(uriCliente, 'GET').done(function (data) {
            self.clientes(data);
            $('#dataTableCliente').DataTable();
        });
    }

    self.getCliente = function (item) {
        ajaxHelper(uriCliente + item.Id, 'GET').done(function (data) {
            self.detalle(data);
        });
    }

    self.addCliente = function (formElement) {
        var cliente = {
            Nombre: self.nuevoCliente.Nombre()
        };

        ajaxHelper(uriCliente, 'POST', cliente).done(function (item) {
            self.nuevoCliente.Nombre('');
            window.location.reload();
        });
    }

    self.popDeleteCliente = function (item) {
        itemEliminar = item;
        self.mensajeEliminar("¿Desea eliminar al cliente " + item.Nombre + "?");
        $('#clienteModal').modal('show');
    }

    self.deleteCliente = function () {
        ajaxHelper(uriCliente + itemEliminar.Id, 'DELETE').done(function (data) {
            itemEliminar = null;
            window.location.reload();
        });
    }

    getClientes();
};

ko.applyBindings(new ViewModelCliente());