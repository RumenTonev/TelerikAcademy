define(['jquery'], function() {
	'use strict';
	
	// Visible functions
	function logInUser(baseUrl, user) {
		return postRequest(baseUrl, user, 'auth');
	}
	
	function registerUser(baseUrl, user) {
		return postRequest(baseUrl, user, 'user');
	}
	
	function logOutUser(baseUrl, user, key) {
		return $.ajax({
			url: baseUrl + 'user',
			type: 'PUT',
			headers: {
				'X-SessionKey': key
			},
			success: function(data) {
				console.log(data);
			},
			error: function(err) {
				console.log(err);
			}
		});
	}
	
	function getAllPost(baseUrl) {
		return workWithPosts(baseUrl, 'post', 'GET');
	}
	
	function sendPost(baseUrl, data, key) {
		return $.ajax({
			url: baseUrl + 'user',
			type: 'POST',
			data: data,
			headers: {
				'X-SessionKey': key
			},
			success: function(data) {
				console.log(data);
			},
			error: function(err) {
				console.log(err);
			}
		});
	}
	
	// Hidden helper functions
	function workWithPosts(baseUrl, addedUrl, method) {
		return $.ajax({
			url: baseUrl + addedUrl,
			type: method,
			success: function(data) {
				console.log(data);
			},
			error: function(err) {
				console.log(err);
			}
		});
	
	}
	
	function postRequest(baseUrl, user, addedUrl) {
		return $.ajax({
			url: baseUrl + addedUrl,
			type: 'POST',
			data: user,
			success: function(data) {
				console.log(data);
			},
			error: function(err) {
				console.log(err);
			}
		});
	}
	
	return {
		logInUser: logInUser,
		logOutUser: logOutUser,
		registerUser: registerUser,
		getAllPost: getAllPost,
		sendPost: sendPost
	};
});