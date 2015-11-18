function umbracoCloudFlareManagerResource($http) {

    var apiRoot = "backoffice/UmbracoCloudFlareManager/Dashboard/";

    return {
        getSettings: function () {
            return $http.get(apiRoot + "GetSettings");
        },

        saveSettings: function (settings) {
            return $http.post(apiRoot + "SaveSettings", settings);
        },

        testConnection: function () {
            return $http.get(apiRoot + "GetConnection");
        },

        purgeUrls: function (urls) {
            return $http.post(apiRoot + "PurgeUrls", urls);
        }
    };

}

angular.module('umbraco.resources').factory('umbracoCloudFlareManagerResource', umbracoCloudFlareManagerResource);