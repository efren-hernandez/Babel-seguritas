var ViewModelCliente = function () {
    var self = this;
    self.clientes = ko.observableArray();
    self.error = ko.observable();
    self.errorActualizar = ko.observable();
    self.detalle = ko.observable();
    self.nuevoCliente = {
        Nombre: ko.observable()
    }
    self.actualizaCliente = {
        Nombre: ko.observable()
    }
    self.mensajeEliminar = ko.observable();
    self.mensajeActualizar = ko.observable();

    var itemEliminar;
    var itemActualizar = {
        Id: 0,
        Nombre: ''
    }

    function ajaxHelper(uri, method, data) {
        self.error('');
        self.errorActualizar('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.responseJSON != null) {
                if (method == 'PUT') {
                    self.errorActualizar(jqXHR.responseJSON.Message);
                }
                else {
                    self.error(jqXHR.responseJSON.Message);
                }
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

    self.popActualizarCliente = function (item) {
        itemActualizar = item;
        self.mensajeActualizar("Se va a actualizar al cliente " + item.Nombre);
        self.actualizaCliente.Nombre(item.Nombre);
        $('#clienteModalActualizar').modal('show');
    }

    self.actualizarCliente = function (item) {
        var cliente = {
            Id: itemActualizar.Id,
            Nombre: self.actualizaCliente.Nombre()
        }

        ajaxHelper(uriCliente + itemActualizar.Id, 'PUT', cliente).done(function (item) {
            self.actualizaCliente.Nombre('');
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