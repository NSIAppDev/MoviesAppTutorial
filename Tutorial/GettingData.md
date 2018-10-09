# Getting Data
Our code is currently using a static list of movies.  This is good for our test, but a poor setup for an application.
In `App.vue` we are defining the movies as:
```javascript
movies: [
	{
		title: 'Avengers: Infinity War',
		year: '2018',
		director: 'Anthony Russo',
		description: 'The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe.'
	},
	{
		title: 'The Last Jedi',
		year: '2017',
		director: 'Rian Johnson',
		description: 'Rey develops her newly discovered abilities with the guidance of Luke Skywalker, who is unsettled by the strength of her powers. Meanwhile, the Resistance prepares for battle with the First Order.'
	}
]
```

Let's change this to get data from a database (storage).

## Adding Axios
We will use a library called Axios to make the calls to our APIs.  These APIs were setup automatically by Visual Studio.
The first step is to import the Axios library.  In `App.vue`, we will modify the `script` section to add another import.
```javascript
import axios from 'axios'
```

Next, we need to add methods that will get the movies from our API.  We need to add a new section in our `script` for `methods`.  This should be added after the `components` and `data()` section
 ```javascript
methods: {
	getMovies() {
		axios({
			method: 'GET', 'url': '/api/movies'
		}).then(result => {
			this.movies = result.data;
		}, error => {
			console.error(error);
		});
	}
},
```
When called, this method performs a GET command to `/api/movies` and returns the result.

In order to run this command, we will a new section that will run when the page is loaded
```javascript
mounted() {
	this.getMovies();
},
```
## Replacing Seed data 
Now that we are able to retrieve information from the API, we will replace our seed data with an empty array.
However, I would suggest you keep the values close by, as you may want to use them to repopulate your database.
You will need to set `movies` to an empty array, so that your `data()` call is
```javascript
data() {
	return {
		movies: [],
	}
},
```

When you run the application, no movies will be listed (your database is empty), however if you turn on your debugging tools, you can see a network request being made to `/api/movies`. 
The request should return an empty JSON array.

## Review
Compare your files against the [expected results](review/GettingData.md).