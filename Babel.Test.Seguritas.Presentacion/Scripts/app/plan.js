var ViewModelPlan = function () {
    var self = this;
    self.planes = ko.observableArray();
    self.error = ko.observable();
    self.detalle = ko.observable();
    self.nuevoPlan = {
        Descripcion: ko.observable(),
        Cliente: ko.observable()
    }
    self.mensajeEliminar = ko.observable();

    self.clientes = ko.observableArray();

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

    var uriPlan = '/api/plan/';
    var uriCliente = '/api/cliente/';

    function getPlanes() {
        ajaxHelper(uriPlan, 'GET').done(function (data) {
            self.planes(data);
            $('#dataTablePlan').DataTable();
        });
    }

    function getClientes() {
        ajaxHelper(uriCliente, 'GET').done(function (data) {
            self.clientes(data);
        });
    }

    self.getPlan = function (item) {
        ajaxHelper(uriPlan + item.Id, 'GET').done(function (data) {
            self.detalle(data);
        });
    }

    self.addPlan = function (formElement) {
        var plan = {
            Descripcion: self.nuevoPlan.Descripcion(),
            ClienteId: self.nuevoPlan.Cliente().Id
        };

        ajaxHelper(uriPlan, 'POST', plan).done(function (item) {
            self.nuevoPlan.Descripcion('');
            window.location.reload();
        });
    }

    self.popDeletePlan = function (item) {
        itemEliminar = item;
        self.mensajeEliminar("¿Desea eliminar el plan " + item.Descripcion + "?");
        $('#planModal').modal('show');
    }

    self.deletePlan = function () {
        ajaxHelper(uriPlan + itemEliminar.Id, 'DELETE').done(function (data) {
            itemEliminar = null;
            window.location.reload();
        });
    }

    getPlanes();
    getClientes();
};

ko.applyBindings(new ViewModelPlan());