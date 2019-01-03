(function() {
    "use strict";

    angular
        .module("blocks.entities")
        .factory("StateDirtyCheckingService", stateDirtyCheckingService);
	
    function stateDirtyCheckingService() {
    	var factory = {
    	    getObjectClone: getObjectClone,
            applyChanges: applyChanges
    	};

    	return factory;

    	function getObjectClone(source) {
	        return angular.copy(source);
    	}

    	function applyChanges(changesDestination, changesSource) {
    	    return angular.extend(changesDestination, changesSource);
    	}
	}
})();