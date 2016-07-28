var alertBox = (function () {
    function alertBox(alertPanelSelector) {
        /// <summary>Create a new alert box that controls the specified alert panel.</summary>
        /// <param name="alertPanelSelector">A jQuery-compatible selector that uniquely identifies the alert panel's root element.</param>
        if (!alertPanelSelector)
            throw Error("Must supply a valid alertPanelSelector for the alert-box element.");

        this._alertPanelSelector = alertPanelSelector;
    }
    alertBox.prototype.showSuccessMessage = function (message) {
        this.resetAlertStyle();

        $(this._alertPanelSelector).find('span#message').html(message);
        $(this._alertPanelSelector).addClass("alert-success").show();
    };

    alertBox.prototype.showErrorMessage = function (message) {
        this.resetAlertStyle();
        $(this._alertPanelSelector).find('span#message').html(message);
        $(this._alertPanelSelector).addClass("alert-danger").show();
    };

    alertBox.prototype.showRefreshPageMessage = function (message) {
        this.resetAlertStyle();

        // TODO: Localize string.
        $(this._alertPanelSelector).find('span#message').html(message + ' <a href=\"javascript:history.go(0);\">Refresh the page.</a>');
        $(this._alertPanelSelector).addClass("alert-danger").show();
    };

    alertBox.prototype.showInfoMessage = function (message) {
        this.resetAlertStyle();
        $(this._alertPanelSelector).find('span#message').html(message);
        $(this._alertPanelSelector).addClass("alert-info").show();
    };

    alertBox.prototype.showWarningMessage = function (message) {
        this.resetAlertStyle();
        $(this._alertPanelSelector).find('span#message').html(message);
        $(this._alertPanelSelector).addClass("alert-warning").show();
    };

    alertBox.prototype.hide = function () {
        $(this._alertPanelSelector).find('span#message').html('');
        $(this._alertPanelSelector).hide();
    };

    alertBox.prototype.resetAlertStyle = function () {
        $(this._alertPanelSelector).removeClass('alert-danger alert-success alert-info alert-warning');
    };
    return alertBox;
})();
;
//# sourceMappingURL=alertBox.js.map
