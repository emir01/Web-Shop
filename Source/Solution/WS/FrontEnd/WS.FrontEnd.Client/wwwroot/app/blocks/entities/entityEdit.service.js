(function () {
    "use strict";

    angular
        .module("blocks.entities")
        .factory("EntityEditService", entityEditService);

    entityEditService.$inject = ["StateDirtyCheckingService"];

    function entityEditService(stateDirtyCheckingService) {

        var entityState = {
            NoChanges: 1,
            Added: 2,
            Modified: 3,
            Deleted: 4
        };

        var currentEntity = {};

        var originalEntity = {};

        var newEntityFlag = false;

        var temporaryIdCounter = 0;

        var factory = {
            // open up the entity state variables
            entityState: function () {
                return entityState;
            }(),

            // Tell the edit service we are working with a new entity
            setNewEntity: setNewEntity,

            // Tell the edit service we are working with a existing entity
            // We are editing a entity.
            setEditEntity: setEditEntity,

            // apply the changes made to the current entity
            // to the original entity - propagating to ui and client code
            applyEntityEdit: applyEntityEdit,

            // undo the changes applie
            undoChanges: undoChanges,

            // flag returning if we are working with a new/editing entity
            isNewEntity: isNewEntity,

            // get the current entity object
            // which could be either the original or a changed entitiy
            getEntity: getEntity,

            // return a temporary id to be used on the client side for
            // entities
            getEntityTemporaryId: getEntityTemporaryId,

            StateSetAdded: stateSetAdded,
            StateSetModified: stateSetModified,
            StateSetDeleted: stateSetDeleted
        }

        return factory;

        //////////////

        function setNewEntity() {
            currentEntity = {};
            originalEntity = {};
            newEntityFlag = true;
        }

        function setEditEntity(entity) {
            originalEntity = stateDirtyCheckingService.getObjectClone(entity);

            currentEntity = entity;

            newEntityFlag = false;

            return currentEntity;
        }

        function undoChanges() {
            // set the current entitiy to a new clone of the original entity
            // client code using this original entity will have the changes propragetd
            //to ui respectfully
            currentEntity = stateDirtyCheckingService.getObjectClone(originalEntity);
            return currentEntity;
        }

        function applyEntityEdit() {
            stateDirtyCheckingService.applyChanges(originalEntity, currentEntity);
            return originalEntity;
        }

        function isNewEntity() {
            return newEntityFlag;
        }

        function getEntity() {
            return currentEntity;
        }

        function getEntityTemporaryId() {
            temporaryIdCounter = temporaryIdCounter + 1;
            return "TmpId" + temporaryIdCounter;
        }

        ////// STATE OPERATIONS

        function stateSetAdded(entity) {
            entity.State = entityState.Added;
            return entity;
        }

        function stateSetModified(entity) {
            entity.State = entityState.Modified;
            return entity;
        }

        function stateSetDeleted(entity) {
            entity.State = entityState.Deleted;
            return entity;
        }
    };
})();