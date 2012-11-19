/*global $, jQuery, ko:false, document:false, confirm: false*/
var checkPIN, showMessage, checkFormData, CompanyViewModel, loadPageCompanies;
checkPIN = function (oib) {
    "use strict";
    var a, b, i, kontrolni;
    oib = oib.toString();
    if (oib.length !== 11) {
        return false;
    }
    b = parseInt(oib, 10);
    if (isNaN(b)) {
        return false;
    }
    a = 10;
    for (i = 0; i < 10; i += 1) {
        a = a + parseInt(oib.substr(i, 1), 10);
        a = a % 10;
        if (a === 0) {
            a = 10;
        }
        a *= 2;
        a = a % 11;
    }
    kontrolni = 11 - a;
    if (kontrolni === 10) {
        kontrolni = 0;
    }
    return kontrolni === parseInt(oib.substr(10, 1), 10);
};
showMessage = function (message, dom) {
    "use strict";
    $(dom + " p").html(message);
    $(dom).show().delay(4000).fadeOut();
};

checkFormData = function (name, oib, error) {
    "use strict";
    if (name === "" || name === null) {
        showMessage("Empty Name", error);
        return false;
    }
    if (!checkPIN(oib)) {
        showMessage("Invalid PIN", error);
        return false;
    }
    return true;
};

loadPageCompanies = function (companies, baseUri) {
    "use strict";
    var pageSize, pageIndex, url;
    pageSize = $('#pageSize').val();
    pageIndex = parseInt($('#pageIndex').val(), 10) - 1;
    url = baseUri + "?$top=" + pageSize + '&$skip=' + (pageIndex * pageSize) + '&$orderby=Name';
    $.getJSON(url, function (data) { companies(data); });
};

CompanyViewModel = function () {
    "use strict";
    var self, baseUri;
    self = this;
    self.companies = ko.observableArray();
    self.chosencompany = ko.observable();
    baseUri = 'api/companies';
    self.create = function () {
        var name, PIN, data;
        name = $("#company #Name").val();
        PIN = $.trim($("#company #PIN").val());
        if (checkFormData(name, PIN, "#msgError")) {
            data = { Id: $("company #Id").val(), Name: name, PIN: PIN, Description: $("#company #Description").val(), Contact: $("#company #Contact").val() };
            $.ajax({
                type: "POST",
                url: baseUri,
                data: data,
                dataType: "json",
                success: function (o) {
                    self.companies.unshift(o);
                    $('#company').get(0).reset();
                    showMessage("Company was successfully created!", "#success");
                },
                error: function () {
                    showMessage("An error occured while saving changes! Check if PIN already exists in database", "#error");
                }
            });
        }
    };
    self.update = function () {
        var PIN, name, data;
        PIN = $.trim($("#edit_company #PIN").val());
        name = $("#edit_company #Name").val();
        if (checkFormData(name, PIN, "#msgErrorEdit")) {
            data = { Id: $("#edit_company #Id").val(), Name: name, PIN: PIN, Description: $("#edit_company #Description").val(), Contact: $("#edit_company #Contact").val() };
            $.ajax({
                type: "PUT",
                url: baseUri + '/' + data.Id,
                data: data,
                success: function () {
                    loadPageCompanies(self.companies, baseUri);
                    $("#edit_dialog").modal("hide");
                    showMessage("Company was successfully updated!", "#success");
                },
                error: function () {
                    loadPageCompanies(self.companies, baseUri);
                    $("#edit_dialog").modal("hide");
                    showMessage("An error occured while saving changes! Check if PIN already exists or entry deleted from database!", "#error");
                }
            });
        }
    };
    self.remove = function (company) {
        var r = confirm('Are you sure you want to delete this?', 'Delete company?');
        if (r === true) {
            $.ajax({
                type: "DELETE",
                url: baseUri + '/' + company.Id,
                success: function () {
                    self.companies.remove(company);
                    showMessage("Company was successfully deleted!", "#success");
                },
                error: function () {
                    loadPageCompanies(self.companies, baseUri);
                    showMessage("An error occured while saving changes! Company is very likely already deleted", "#error");
                }
            });
        }
    };
    self.edit = function (company) {
        $("#edit_dialog").modal("show");
        $("#edit_company #Id").val(company.Id);
        $("#edit_company #Name").val(company.Name);
        $("#edit_company #PIN").val(company.PIN);
        $("#edit_company #Contact").val(company.Contact);
        $("#edit_company #Description").val(company.Description);
    };
    self.getPage = function () {
        loadPageCompanies(self.companies, baseUri);
    };
    self.search = function () {
        var query, url;
        query = $.trim($('#searchQuery').val());
        if (query === "") {
            loadPageCompanies(self.companies, baseUri);
        } else {
            url = baseUri + "?$filter=(substringof(tolower('" + query + "'), tolower(Name)) eq true or substringof(trim('" + query + "'), trim(PIN)) eq true)";
            $.getJSON(url, function (data) { self.companies(data); });
        }
    };
    loadPageCompanies(self.companies, baseUri);
};

$(document).ready(function () {
    "use strict";
    ko.applyBindings(new CompanyViewModel());
    $("#msgError").hide();
    $("#msgErrorEdit").hide();
    $("#error").hide();
    $("#success").hide();
});