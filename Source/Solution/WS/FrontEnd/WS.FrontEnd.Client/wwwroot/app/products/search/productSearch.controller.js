(function () {
    "use strict";

    angular.module("ws.products")
		.controller("ProductSearchController", ProductSearchController);

    ProductSearchController.$inject = ["$scope", "common", "services", "categories", "manufacturers", "tagTypes"];

    function ProductSearchController($scope, common, services, categories, manufacturers, tagTypes) {
        var vm = this;

        vm.products = [];
        vm.categories = categories;
        vm.manufacturers = manufacturers;
        vm.tagTypes = tagTypes;

        vm.searchParams = services.data.products.getDefaultSearchParameters();
        vm.sortOptions = services.data.products.sortOptions;

        vm.searchConfig = {
            minPrice: 0,
            maxPrice: 500000
        }

        vm.searching = false;
        vm.search = _search;
        vm.clearSearch = _clearSearch;
        vm.priceRangeChange = _priceRangeChange;

        vm.selectedCategory = null;
        vm.selectedManufacturer = null;

        vm.tagFilters = [
        {
            tagType: null,
            filterValue: ""
        }];

        vm.addTagFilter = _addTagFitler;
        vm.removeTagFilter = _removeTagFilter;

        activate();

        function activate() {
            $scope.$on(common.events.keydown, _handleGlobalKeydown);
            $scope.$on(common.commands.product.delete, _handleDeleteProductCommand);

            var searchParamsFromUrl = common.$state.params.params;
            if (services.utils.data.DataIsDefinedAndNotNull(searchParamsFromUrl)) {
                vm.searchParams = searchParamsFromUrl;
                _mapSearchParamsToFilters();
            }

            _setupFilterWatches();

            return _queryProducts();
        }

        function _setupFilterWatches() {
            $scope.$watch(function () { return vm.searchParams.onSale }, function (newValue, oldValue) {
                if (!angular.equals(newValue, oldValue, true)) {
                    _handleFilterPropertyChange("onSale", newValue);
                }
            });

            $scope.$watch(function () { return vm.searchParams.sort }, function (newValue, oldValue) {
                if (!angular.equals(newValue, oldValue, true)) {
                    _handleFilterPropertyChange("sort", newValue);
                }
            });

            $scope.$watch(function () { return vm.selectedCategory }, function (newValue, oldValue) {
                if (!angular.equals(newValue, oldValue, true)) {
                    _handleFilterPropertyChange("categoryId", newValue, "Id");
                }
            });

            $scope.$watch(function () { return vm.selectedManufacturer }, function (newValue, oldValue) {
                if (!angular.equals(newValue, oldValue, true)) {
                    _handleFilterPropertyChange("manufacturerId", newValue, "Id");
                }
            });
        }

        function _handleFilterPropertyChange(searchParamPropertyName, newFilter, newFilterPropertyName) {
            if (newFilter) {
                var filter = newFilter;

                if (newFilterPropertyName) {
                    filter = newFilter[newFilterPropertyName];
                }

                vm.searchParams[searchParamPropertyName] = filter;
            } else {
                vm.searchParams[searchParamPropertyName] = null;
            }

            _search();
        }

        function _handleGlobalKeydown(event, data) {
            if (data === common.keys.escape && common.$state.current.name === "products.search") {
                _clearSearch();
                _search();
            }
        }

        function _handleDeleteProductCommand(event, data) {
            services.data.products.deleteProduct(data.Id).then(function(data) {
                var productInArray = common.lodash.find(vm.products, function (pr) {
                    return data.Id === pr.Id;
                });

                if (productInArray) {
                    var index = vm.products.indexOf(productInArray);
                    vm.products.splice(index, 1);
                }
            });
        }

        function _addTagFitler() {
            vm.tagFilters.push({
                tagType: null,
                filterValue: ""
            });
        }

        function _removeTagFilter(filter) {
            if (onlyOneTagFilterLeft()) {
                var tagFilterWasValid = _tagFilterIsValid(filter);

                vm.tagFilters[0] = { tagType: null, filterValue: "" }
                if (tagFilterWasValid) {
                    _search();
                }
            } else {
                var index = vm.tagFilters.indexOf(filter);
                vm.tagFilters.splice(index, 1);
            }

            function onlyOneTagFilterLeft() {
                return vm.tagFilters.length === 1;
            }
        }

        function _clearSearch() {
            vm.searchParams = services.data.products.getDefaultSearchParameters();

            vm.selectedCategory = null;
            vm.selectedManufacturer = null;

            vm.tagFilters = [{ tagType: null, filterValue: "" }];

            _search();
        }

        function _priceRangeChange() {
            _search();
        }

        function _search() {
            if (!vm.searching) {
                _mapFiltersToSearchParams();

                common.$state.go("products.search", { params: vm.searchParams }, { reload: true });
            }
        }

        function _queryProducts() {
            vm.products = [];
            vm.searching = true;
            common.$loading.start("view");
            return services.data.products.QueryProducts(vm.searchParams).then(function (data) {
                common.$loading.finish("view");
                vm.searching = false;
                vm.products = data;
            });
        }

        /*
         * Maps filters to search parameters. Used to map tag type filters
         * to parameters usable in the search back end. 
         * 
         * Basically maps {{tagTypeObject}, filterValue} to {tagTypeId, filterValue}
         */

        function _mapFiltersToSearchParams() {
            vm.searchParams.tagFilters =
                common.lodash(vm.tagFilters)
                    .filter(function (tagFilter) {
                        // only get the tag type filters that have a tag type property selected
                        // and the filter value is defined and not an empty string
                        return _tagFilterIsValid(tagFilter);
                    })
                    // then map those to {tagTypeId, filterValue} objects on the search params
                    .map(function (filterObject) {
                        return {
                            tagTypeId: filterObject.tagType.Id,
                            filterValue: filterObject.filterValue
                        };
                    }).value();
        }

        /*
         * Because we are using Query Parameters to store the filters we will also need to map
         * data from vm.searchParams (after restoring it from the Query String) to any 
         * special filters used on the UI. 
         * 
         * In the current case it is only applicable for the vm.tagFilters
         */

        function _mapSearchParamsToFilters() {
            var restoredTagFilters = common.lodash(vm.searchParams.tagFilters)
            .map(function (tagFilter) {
                return {
                    tagType: {
                        Id: tagFilter.tagTypeId,
                        Name: common.lodash.find(vm.tagTypes, function (tagType) { return tagType.Id === tagFilter.tagTypeId }).Name
                    },
                    filterValue: tagFilter.filterValue
                };
            }).value();

            if (restoredTagFilters && restoredTagFilters.length > 0) {
                vm.tagFilters = restoredTagFilters;
            } else {
                vm.tagFilters = [{ tagType: null, filterValue: "" }];
            }

            if (vm.searchParams.categoryId) {
                vm.selectedCategory = common.lodash.find(vm.categories, function (category) { return category.Id === vm.searchParams.categoryId });
            }

            if (vm.searchParams.manufacturerId) {
                vm.selectedManufacturer = common.lodash.find(vm.manufacturers, function (manu) { return manu.Id === vm.searchParams.manufacturerId });
            }
        }

        function _tagFilterIsValid(tagFilter) {
            return services.utils.data.DataIsDefinedAndNotNull(tagFilter.tagType)
                            && services.utils.data.StringIsDefinedAndNotEmpty(tagFilter.filterValue);
        }
    }
})();
