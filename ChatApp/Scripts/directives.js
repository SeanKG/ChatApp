'use strict';

angular.module('app.directives', [])

    .directive('appVersion', ['version', function (version) {

        return {
            //template: '<div>' + version + '</div>',
            template: version,
            restrict: "AE"
        }

        //return function (scope, elm, attrs) {
        //    elm.text(version);
        //    attrs.restrict = "AE";
        //};
    }]);