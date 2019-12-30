var ViewModelPlan = function () {
    var self = this;
    self.planes = ko.observableArray();
    self.coberturas = ko.observableArray();
    self.planCoberturas = ko.observableArray();

    self.error = ko.observable();
    self.detalle = ko.observable();
    self.nuevoPlanCobertura = {
        Plan: ko.observable(),
        Cobertura: ko.observable()
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

    var uriPlan = '/api/plan/';
    var uriCobertura = '/api/cobertura/';
    var uriPlanCobertura = '/api/planCobertura/';

    function getPlanCoberturas() {
        ajaxHelper(uriPlanCobertura, 'GET').done(function (data) {
            self.planCoberturas(data);
            $('#dataTablePlanCobertura').DataTable();
        });
    }

    function getPlanes() {
        ajaxHelper(uriPlan, 'GET').done(function (data) {
            self.planes(data);
        });
    }

    function getCoberturas() {
        ajaxHelper(uriCobertura, 'GET').done(function (data) {
            self.coberturas(data);
        });
    }

    self.getPlanCobertura = function (item) {
        ajaxHelper(uriPlanCobertura + item.Id, 'GET').done(function (data) {
            self.detalle(data);
        });
    }

    self.addPlanCobertura = function (formElement) {
        var planCobertura = {
            PlanId: self.nuevoPlanCobertura.Plan().Id,
            CoberturaId: self.nuevoPlanCobertura.Cobertura().Id
        };

        ajaxHelper(uriPlanCobertura, 'POST', planCobertura).done(function (item) {
            self.nuevoPlanCobertura.Plan('');
            self.nuevoPlanCobertura.Cobertura('');
            window.location.reload();
        });
    }

    self.popDeletePlanCobertura = function (item) {
        itemEliminar = item;
        self.mensajeEliminar("¿Desea eliminar la relación entre plan y cobertura?");
        $('#planCoberturaModal').modal('show');
    }

    self.deletePlanCobertura = function () {
        var planCobertura = {
            PlanId: itemEliminar.PlanId,
            CoberturaId: itemEliminar.CoberturaId
        };

        ajaxHelper(uriPlanCobertura + 'DEL', 'POST', planCobertura).done(function (data) {
            itemEliminar = null;
            window.location.reload();
        });
    }

    getPlanCoberturas();
    getPlanes();
    getCoberturas();
};

ko.applyBindings(new ViewModelPlan());