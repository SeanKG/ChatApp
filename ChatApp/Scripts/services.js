'use strict';


(function(){

    // Demonstrate how to register services
    // In this case it is a simple value service.
    var app = angular.module('app.services', ["SignalR"]);

    app.value('version', '0.1');

    app.factory('ChatHub', ['$rootScope', 'Hub', function ($rootScope, Hub) {
        var ChatHub = this;

        ChatHub.count = 0;
        ChatHub.messages = [];

        var hub = new Hub('ChatHub', {
            //Client methods
            'hello': function (message) {
                ChatHub.messages.push(message);
                $rootScope.$apply();
            },
        },
            //Server method stubs for ease of access
            ['Hello']
        );

        ChatHub.Hello = function () {
            hub.Hello("Hello"); //Calling a server method
        };


        return ChatHub;

    }]);

})();