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

        $(this._alertPanelSelector).addClass("alert-success").html(message).show();
    };

    alertBox.prototype.showErrorMessage = function (message) {
        this.resetAlertStyle();

        $(this._alertPanelSelector).addClass("alert-danger").html(message).show();
    };

    alertBox.prototype.showErrorMessageWithActivityId = function (message, activityid) {
        this.resetAlertStyle();

        // TODO: Localize string.
        $(this._alertPanelSelector).addClass("alert-danger").html(message + '<div class="alert-activityId">Activity Id: ' + activityid + '</div>').show();
    };

    alertBox.prototype.showRefreshPageMessage = function (message) {
        this.resetAlertStyle();

        // TODO: Localize string.
        $(this._alertPanelSelector).addClass("alert-danger").html(message + ' <a href=\"javascript:history.go(0);\">Refresh the page.</a>').show();
    };

    alertBox.prototype.showInfoMessage = function (message) {
        this.resetAlertStyle();

        $(this._alertPanelSelector).addClass("alert-info").html(message).show();
    };

    alertBox.prototype.showWarningMessage = function (message) {
        this.resetAlertStyle();

        $(this._alertPanelSelector).addClass("alert-warning").html(message).show();
    };

    alertBox.prototype.hide = function () {
        $(this._alertPanelSelector).html('').hide();
    };

    alertBox.prototype.resetAlertStyle = function () {
        $(this._alertPanelSelector).removeClass('alert-danger alert-success alert-info alert-warning');
    };
    return alertBox;
})();
;
//# sourceMappingURL=alertBox.js.map
