(function () {
    'use strict';

    angular
        .module('ws.core')
        .factory("common", common);

    common.$inject = [
        "$rootScope",
        "$state",
        "$q",
        "$http",
        "$uibModal",
        "$loading",
        "localStorageService",
        "logger",
        "lodash",
        "moment",
        "rest_config",
        "defaults",
        "events",
        "commands",
        "keys"
    ];

    function common(
        $rootScope,
        $state,
        $q,
        $http,
        $uibModal,
        $loading,
        localStorageService,
        logger,
        lodash,
        moment,
        rest_config,
        defaults,
        events,
        commands,
        keys
        ) {

        var factory = {};

        factory.$rootScope = $rootScope;
        factory.$state = $state;
        factory.$http = $http;

        factory.$uibModal = $uibModal;

        factory.$loading = $loading;

        factory.lodash = lodash;

        factory.moment = moment;

        factory.logger = logger;

        factory.restConfig = rest_config;

        factory.defaults = defaults;

        factory.events = events;
        factory.commands = commands;

        factory.keys = keys;

        factory.localStorage = localStorageService;

        factory.$q = $q;

        return factory;
    }
})();
