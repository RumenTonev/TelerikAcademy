(function() {
	'use strict';
	
	require.config({
		paths: {
			'jquery': '../js/jquery-2.1.1.min',
			'sammy': '../../node_modules/shimney-sammy/main',
			'_': '../../node_modules/underscore/underscore',
			'ui': 'crowd-share-ui',
			'controller': 'crowd-share-controller',
			'data': 'crowd-share-data'
		}
	});
	
	require(['jquery', 'controller'], function($, controller) {
		
		controller.run();
	});
}());