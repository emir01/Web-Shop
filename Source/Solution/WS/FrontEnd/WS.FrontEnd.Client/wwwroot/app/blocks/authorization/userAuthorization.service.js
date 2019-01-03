(function () {
    "use strict";

    angular
        .module("blocks.authorization")
        .factory("UserAuthorization", userAuthorizationService);

    userAuthorizationService.$inject = ["common"];

    function userAuthorizationService(common) {
        var localStorageAuthorizationKey = "authorizationData";

        var authentication = {
            isAuth: false,
            userName: ""
        };

        var factory = {};

        factory.Authentication = authentication;
        factory.Register = register;
        factory.Login = login;
        factory.Logout = logOut;
        factory.FillAuthData = fillAuthData;
        factory.GetAuthData = getAuthData;
        factory.IsAuthorized = isAuthorized;

        return factory;

        function fillAuthData() {
            var fromStorage = common.localStorage.get(localStorageAuthorizationKey);

            if (fromStorage) {
                authentication.data = fromStorage;
                authentication.isAuth = true;
                authentication.userName = fromStorage.userName;
            }
        }

        function getAuthData() {
            var fromStorage = common.localStorage.get(localStorageAuthorizationKey);
            return fromStorage;
        }

        function isAuthorized() {
            fillAuthData();

            return authentication.isAuth && tokenIsNotExpired(authentication.data);

            function tokenIsNotExpired(data) {
                var now = common.moment();
                var createdAt = common.moment(data.created);

                var diff = now.diff(createdAt, "seconds");
                var expires = data.expires;

                if (diff > expires) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        function register(registration) {
            logOut();

            return common.$http.post(common.restConfig.account_base, registration).then(successRegister);

            function successRegister(response) {
                return response;
            }
        }

        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = common.$q.defer();

            common.$http.post(common.restConfig.tokenUrl, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(successLogin, errorLogin);

            function successLogin(response) {
                common.localStorage.set(localStorageAuthorizationKey, buildLoginObject(loginData, response));

                authentication.isAuth = true;
                authentication.userName = loginData.userName;

                deferred.resolve(response);

                function buildLoginObject(loginData, response) {
                    return { token: response.data.access_token, userName: loginData.userName, created: new Date(), expires: response.data.expires_in };
                }
            }

            function errorLogin(response) {
                logOut();
                deferred.reject(response.data.error_description);
            }

            return deferred.promise;
        }

        function logOut() {
            common.localStorage.remove(localStorageAuthorizationKey);

            authentication.isAuth = false;
            authentication.userName = "";
        }
    }
})();