module app.Layout {
    'use strict';

    interface INavigationScope {
        heading: string;
    }

	class NavigationController implements INavigationScope {
        heading = 'Welcome to my page!';
	}

    angular
        .module('app.layout')
        .controller('app.layout.NavigationController', NavigationController);
}