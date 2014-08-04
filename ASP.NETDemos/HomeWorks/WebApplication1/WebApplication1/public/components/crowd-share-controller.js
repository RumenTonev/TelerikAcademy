define(['jquery', 'sammy', 'ui', 'data', '_'], function($, sammy, ui, data, _) {
	'use strict';
	var URL = 'http://localhost:3000/';
	
	// Hidden helper functions
	function isUsernameCorrect(name) {
		return (typeof name === 'string' || name instanceof String)
				&& 6 <= name.length && name.length <= 40;
	}
	
	function createUser(username, password) {
		var	authoCode;
						
		if (isUsernameCorrect(username) && password) {
			authoCode = username + password;
			return {
				username: username,
				authCode: "73b24dd0bfd8858fb9734ee51aa65c05261199cc"
			};
		}
	}
	
	function setOneHourCookie(username) {
		var date = new Date();
		date.setTime(date.getTime() + (60 * 60 * 1000));
		document.cookie = 'username=' + username + ';expires=' + date.toGMTString();
	}
	
	function getUsernameFromCookie() {
		var cookies = document.cookie,
			temp = cookies.split(";"),
			username = (temp[0].split("="))[1];
			
		return username;
	}
	
	function removeSession() {		
		// From stackoverflow
		var cookies = document.cookie.split(";");		
		
		for (var i = 0; i < cookies.length; ++i)
		{
			var myCookie = cookies[i];
			var pos = myCookie.indexOf("=");
			var name = pos > -1 ? myCookie.substr(0, pos) : myCookie;
			document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
		}
		
		sessionStorage.clear();
	}
	
	// Visible values
	var controller = sammy('#content-wrapper', function() {
		
		this.get('#/login', function() {
			var $container = this.$element();
			
			ui.createLogInPage($container);
			
			$('#login-button').on('click', function() {
				var username = $('#username-field').val(),
					password = $('#password-field').val(),
					user = createUser(username, password);
								
				if (user) {				
					data.logInUser(URL, user)
						.then(function(data) {
							sessionStorage.setItem(data.username, data.sessionKey);
							setOneHourCookie(data.username);
						});
				}
			});
		});
		
		this.get('#/register', function() {
			var $container = this.$element();
			
			ui.createRegisterPage($container);
			
			$('#submit-button').on('click', function() {
				var username = $('#username-reg-field').val(),
					password = $('#password-reg-field').val(),
					user = createUser(username, password);
				
				if (user) {
					data.registerUser(URL, user);
				}
			});
		});
		
		this.get('#/posts', function() {	
			var $container = this.$element();
						
			data.getAllPost(URL)
				.then(function(data) {
					if (data) {
						ui.createPostsPage($container, data);
					} else {
						ui.createPostsPage($container, []);
					}					
					
				});
		});
		
		this.get('#/createPost', function() {
			var $container = this.$element(),
				sessionKey,
				user,
				title,
				body,
				postInfo;
			
			ui.createCreatePostPage($container);
			
			$('#post-button').on('click', function() {
				user = getUsernameFromCookie();
				sessionKey = sessionStorage[user];
				title = $('#title-post').val();
				body = $('#body-post').val();
				
				console.log(title);
				postInfo = {
					title: title,
					body: body
				};
				
				if (user && sessionKey) {
					data.sendPost(URL, postInfo,sessionKey)
						.then(function(data) {
							console.log(data);
						});		
				}	
			});
		});
		
		this.get('#/logout', function() {
			var $container = this.$element(),
				user;
			
			ui.createLogOutPage($container);
			
			$('#logout-button').on('click', function() {
				user = getUsernameFromCookie();
				
				if (user) {
					data.logOutUser(URL, user, sessionStorage[user])
						.then(function() {							
								removeSession();							
						});		
				}					
			});
		});
		
	});
	
	return controller;
});