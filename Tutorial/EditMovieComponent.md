# Edit a Movie
We are retrieving data from our API and adding new data as well.  Now we will edit those same records.
We will do this with a new component.

## Writing the Component
Right click on the components directory, select `Add` then `New File`, type `EditMovie.vue` and press enter.
Add this code:
```html
<template>
	<transition name="editModal">
		<div class="modal modal-mask" style="display: block">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h3 class="modal-title">
							Edit Movie
						</h3>
					</div>
					<div class="modal-body">
						<div class="alert alert-danger" v-show="showError">
							Please fill out all form fields.
						</div>
						<div class="form-group">
							<label>Title</label>
							<input type="text" class="form-control" v-model="movie.title" />
						</div>
						<div class="form-group">
							<label>Year</label>
							<input type="text" class="form-control" v-model="movie.year" />
						</div>
						<div class="form-group">
							<label>Director</label>
							<input type="text" class="form-control" v-model="movie.director" />
						</div>
						<div class="form-group">
							<label>Description</label>
							<textarea class="form-control" rows="3" v-model="movie.description"></textarea>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-primary" v-on:click="saveMovieEdit">Save Changes</button>
						<button type="button" class="btn btn-secondary" data-dismiss="modal" v-on:click="closeEditModal">Cancel</button>
					</div>
				</div>
			</div>
		</div>
	</transition>
</template>

<script>
	export default {
		props: ['movie'],
		data() {
			return {
				showError: false
			}
		},
		methods: {
			closeEditModal() {
				this.$emit('close-edit-modal');
			},
			saveMovieEdit() {
				this.showError = false;
				if (this.movie.title.length > 0 && this.movie.year > 0 && this.movie.director.length > 0 && this.movie.description.length > 0) {
					this.$emit('save-movie-edit');
				} else {
					this.showError = true;
				}
			},
		},
	};
</script>

<style></style>
```
Like our add component, this one consists of an HTML template, a form and buttons.  Again, we also setup a couple of methods: `closeEditModal()` and `saveMovieEdit()`.  
These methods are nearly identical to the Add methods, with the only different being that they call different parent methods.

## Registering the Component
Back in `App.vue`, we need to update our template to use our new component. Add this line below the `<display-movie>` tag
```html
<edit-movie v-if="showEditModal" v-bind:movie="movieToEdit" v-on:close-edit-modal="closeEditModal" v-on:save-movie-edit="saveMovieEdit"></edit-movie>
```

Register the component in the `import` and `components` areas
```javascript
import EditMovie from './components/EditMovie'
```
and
```javascript
components: {
	DisplayMovie,
	AddMovie,
	EditMovie
},
```

## Modifying initial state "data()"
Just like the add component, we need to setup the initial state.  On the `data()` element you will need to add
```javascript
movieToEdit: {
	title: '',
	year: '',
	director: '',
	description: ''
},
showEditModal: false,
```
## Adding methods
The component needs some methods to control the modal, and communicate with the API. Add these functions into the `methods` section:
```javascript
editMovie(id) {
	axios({
		method: 'GET', 'url': '/api/movies/' + id
	}).then(result => {
		this.movieToEdit = result.data;
	}, error => {
		console.error(error);
	});
	this.showEditModal = true;
},
closeEditModal() {
	this.showEditModal = false;
	this.movieToEdit = {
		title: '',
		year: '',
		director: '',
		description: ''
	};
},
saveMovieEdit() {
	axios({
		method: 'PUT', 'url': '/api/movies/' + this.movieToEdit.id, 'data': this.movieToEdit
	}).then(result => {
		this.getMovies();
		this.closeEditModal();
	}, error => {
		console.error(error);
	});
},
```
## Creating a Hook (v-on)
Unlike the add component, the edit component will not be called directly, but will be called from the movie display template.  We therefor have to hook into the movie template by using the `v-on`.
On the `<display-movie>` tag, add in the new attribute of `v-on:edit-movie="editMovie"`, giving you the line
```html
<display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id" v-on:edit-movie="editMovie"></display-movie>
```
## Adding buttons
Finally, we need to add an HTML button to interact with the component. This will be done in the `DisplayMovie.vue` file.
After the `</p>` tag, add:
```html
<button class="btn btn-primary" v-on:click="editMovie(movie.id)">Edit</button>
```
We will also need to add in method into this component.  In our `script` area, add a `methods` section:
```javascript
methods: {
	editMovie(id) {
		this.$emit('edit-movie', id);
	},
},
```
Notice that the button and the method pass the `id` of the movie.  This will be used to save the record using the API.
## Review
Compare your files against the [expected results](review/EditMovie.md).