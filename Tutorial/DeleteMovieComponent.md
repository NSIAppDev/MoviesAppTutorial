# Delete a Movie
We get get data, add data and edit data.  Our next step is to remove data.
This step doesn't actually use a component, just a series of methods and hooks.

## Creating a Hook
Similar to the edit component, we will call our methods from the movie template, so again we have to add our hook using `v-on`.
On the `<display-movie>` tag, add in the new attribute of `v-on:delete-movie="deleteMovie"`, giving you the line
```html
<display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id" v-on:edit-movie="editMovie" v-on:delete-movie="deleteMovie"></display-movie>
```

## Adding methods
Next, add this method into the `methods` area:
```javascript
deleteMovie(id) {
	axios({
		method: 'DELETE', 'url': '/api/movies/' + id
	}).then(result => {
		this.getMovies();
	}, error => {
		console.error(error);
	});
},
```

## Adding button
Finally, we need add the button and the local functions to the `DisplayMovie.vue` file.
Add out button below the previous button
```html
<button class="btn btn-secondary" v-on:click="deleteMovie(movie.id)">Delete</button>
```
Add an additional method
```javascript
deleteMovie(id) {
	this.$emit('delete-movie', id);
},
```

## Review
Compare your files against the [expected results](review/DeleteMovie.md).