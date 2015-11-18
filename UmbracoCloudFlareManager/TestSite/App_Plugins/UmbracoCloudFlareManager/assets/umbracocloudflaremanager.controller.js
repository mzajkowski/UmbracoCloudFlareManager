function ucfmDashboardController($scope, $routeParams, umbracoCloudFlareManagerResource, notificationsService, dialogService, entityResource) {

    $scope.init = function() {
        umbracoCloudFlareManagerResource
            .getSettings()
            .then(function (resp) {
                $scope.settings = resp.data;

                $scope.setHomepage();
            });
    }

    $scope.save = function (settings) {
        umbracoCloudFlareManagerResource.saveSettings(settings).then(function (response) {
            $scope.cloudFlareSettingsForm.$dirty = false;
            notificationsService.success("Success", "Settings have been saved.");
        });
    };

    $scope.testConnection = function() {
        umbracoCloudFlareManagerResource.testConnection().then(function(response) {
            if (response.data.Key == true) {
                notificationsService.success("Success", "Connection was successful. " + response.data.Value);
            } else {
                notificationsService.error("Error", response.data.Value);
            }
        });
    };

    $scope.setHomepage = function() {
        var val = parseInt($scope.settings.Homepage);

        if (!isNaN(val) && angular.isNumber(val) && val > 0) {
            entityResource.getById(val, "Document").then(function (item) {
                $scope.selectedHomepage = item;
            });
        }
    }

    $scope.pickHomepage = function () {
        dialogService.contentPicker({ callback: itemPicked });

        function itemPicked(pickedItem) {
            $scope.selectedHomepage = pickedItem;
            $scope.settings.Homepage = pickedItem.id;
        }
    };

    $scope.clearHomepage = function () {
        $scope.selectedHomepage = undefined;
        $scope.settings.Homepage = undefined;
    };

    $scope.urls = "";
    $scope.purgeUrls = function (urls) {
        umbracoCloudFlareManagerResource.purgeUrls(urls.split('\n')).then(function (response) {
            console.log(response);

            $scope.cloudFlarePurgerForm.$dirty = false;
            notificationsService.success("Success", "Urls have been purged.");
        });
    }

    $scope.init();

};

angular.module("umbraco").controller("UCFM.DashboardController", ucfmDashboardController);