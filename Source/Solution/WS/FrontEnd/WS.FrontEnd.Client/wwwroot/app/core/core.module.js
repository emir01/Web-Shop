(function () {
    "use strict";

    angular.module("ws.core", [
        // angular common 
        "LocalStorageModule",
        "ngAnimate",
        "ngResource",
        "ui.router",

        // app common
        "blocks.authorization",
        "blocks.entities",
        "blocks.logger",
        "blocks.utilities",

        // third party modules:
        "angularTrix",
        "bcherny/formatAsCurrency",
        "darthwade.dwLoading",
        "focus-if",
        "ng-currency",
        "ngFileUpload",
        "ngSanitize",
        "ui-rangeSlider",
        "ui.bootstrap",
        "ui.grid",
        "ui.grid.selection",
        "ui.grid.pagination",
        "ui.select"
    ]);
})();