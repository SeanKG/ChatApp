﻿'use strict';


(function(){

    // Demonstrate how to register services
    // In this case it is a simple value service.
    var app = angular.module('app.services', ["SignalR", 'ngCookies']);

    app.value('version', '0.1');

    app.factory('ChatHub', ['$rootScope', 'Hub', '$cookies', function ($rootScope, Hub, $cookies) {
        var ChatHub = this;

        var count = ChatHub.count = 0;
        var messages = ChatHub.messages = [];
        var users = ChatHub.users = [];
                
        var UserName = ChatHub.UserName = $cookies.UserName
            || 'Guest' + Math.floor(1000 + Math.random() * 9000);

        var ConnectionID = ChatHub.ConnectionID  = null;

        var hub = new Hub('ChatHub', {
            //Client methods
            newMessage: function (message) {
                ChatHub.messages.push(message);
                $rootScope.$apply();
            },

            NewConnectionID: function (id) {
                ChatHub.users.push({id:id});
                $rootScope.$apply();
            },

            SetConnectionID: function (id) {
                ConnectionID = id;
                $rootScope.$apply();
            },

            SetUserName: function (name, connectionID) {
                angular.forEach(users, function (user, index) {
                    if (user.id == connectionID) {
                        user.name = name;
                    }
                });
            }
        },
            //Server method stubs for ease of access
            ['newMessage', 'SetUserName']
        );

        hub.promise.done(function () {
            hub.SetUserName(UserName);
        });

        ChatHub.newMessage  = function (message) {
            hub.newMessage(message, UserName); //Calling a server method
        };


        return ChatHub;

    }]);

})();