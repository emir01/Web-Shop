/* global toastr:false, moment:false */
(function () {
    'use strict';

    angular
        .module('ws.core')
        .constant('toastr', toastr)
        .constant("lodash", _)
        .constant("moment", moment)
        .constant("underscore", _);
})();
