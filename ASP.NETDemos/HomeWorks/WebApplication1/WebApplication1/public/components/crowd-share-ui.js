define(['jquery'], function() {
	'use strict';
	
	function createLogInPage(container) {
		$(container)
			.empty()
			.append($('<div id="login-form">')
						.append('Username:<input type="text" id="username-field"><br />')
						.append('Password:<input type="password" id="password-field"><br />')
						.append('<button id="login-button">Login</button>')
			);
	}
	
	function createLogOutPage(container) {
		$(container)
			.empty()
			.append($('<div>')
						.append('<button id="logout-button">Logout</button>')
			);
	}
	
	function createRegisterPage(container) {
		$(container)
			.empty()
			.append($('<div id="register-form">')
						.append('Username:<input type="text" id="username-reg-field"><br />')
						.append('Password:<input type="password" id="password-reg-field"><br />')
						.append('<button id="submit-button">Submit</button>')
			);
	}
	
	function createPostsPage(container, posts) {
		var $postsContainer = $('<ul>'),
			user,
			i;
		
		for (i = 0; i < posts.length; i += 1) {
			user = posts[i].user;
			$postsContainer.append(
				$('<li>Title: ' + posts[i].title + ',Text: ' + posts[i].body + 'Post Date: ' + posts[i].postDate + 
							'Username: ' + user.username + '</li>')
			)
		}
		
		$(container)
			.empty()
			.append($('<div id="posts-container">')
						.append($postsContainer)
			);	
	}
	
	function createCreatePostPage(container) {
		$(container)
			.empty()
			.append($('<div id="post-form">')
						.append('Title:<input type="text" id="title-post"><br />')
						.append('Body:<input type="password" id="body-post"><br />')
						.append('<button id="post-button">Post</button>')
			);
	}
	
	return {
		createLogInPage: createLogInPage,
		createLogOutPage: createLogOutPage,
		createRegisterPage: createRegisterPage,
		createPostsPage: createPostsPage,
		createCreatePostPage: createCreatePostPage
	};	
});