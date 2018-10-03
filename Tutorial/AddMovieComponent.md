# Add a Movie
Now that our code is getting data from our api, we need a way to add new data, using those same APIs.
We will do this with a new component.

## Writing the Component
Right click on the components directory, select `Add` then `New File`, type `AddMovie.vue` and press enter.
Add this code:
```html
<template>
	<transition name="addModal">
		<div class="modal modal-mask" style="display: block">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h3 class="modal-title">
							Add Movie
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
						<button type="button" class="btn btn-primary" v-on:click="saveMovieAdd">Add Movie</button>
						<button type="button" class="btn btn-secondary" data-dismiss="modal" v-on:click="closeAddModal">Cancel</button>
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
			closeAddModal() {
				this.$emit('close-add-modal');
			},
			saveMovieAdd() {
				this.showError = false;
				if (this.movie.title.length > 0 && this.movie.year > 0 && this.movie.director.length > 0 && this.movie.description.length > 0) {
					this.$emit('save-movie-add');
				} else {
					this.showError = true;
				}
			},
		},
	};
</script>

<style></style>
```
Like our other component, this one consists of an HTML template, a form and buttons.  We again set the `props` to `movie`.  We also setup a couple of methods `closeAddModal()` and `saveMovieAdd()`.  

In `closeAddModal()` we use the `$emit` to call a method in our parent file `App.vue`. 

In `saveMovieAdd()` we do some error checking, and if the data is valid, we again use the `$emit` to call a method in our parent file.

We will setup those parent methods in the next section.

## Registering the Component
Back in `App.vue`, we need to update our template to use our new component. Add this line below the `<display-movie>` tag
```html
<add-movie v-if="showAddModal" v-bind:movie="movieToAdd" v-on:close-add-modal="closeAddModal" v-on:save-movie-add="saveMovieAdd"></add-movie>
```

We need to register the component with an `import` command.
In the `script` section, add this line
```javascript
import AddMovie from './components/AddMovie',
```
We also need to update the `component` section to register `AddMovie`
```javascript
components: {
	DisplayMovie,
	AddMovie
},
```

## Modifying initial state "data()"
The component needs to have some objects and values initialized on load, so we enter the `data()` element.  You will need to add
```javascript
movieToAdd: {
	title: '',
	year: '',
	director: '',
	description: ''
},
showAddModal: false
```
## Adding methods
The component needs some methods added to `App.vue`.  These methods open and close the modal, and communicate with the API. Add these functions into the `methods` section:
```javascript
openAddModal() {
	this.showAddModal = true;
},
closeAddModal() {
	this.showAddModal = false;
	this.movieToAdd = {
		title: '',
		year: '',
		director: '',
		description: ''
	};
},
saveMovieAdd() {
	axios({
		method: 'POST', 'url': '/api/movies', 'data': this.movieToAdd
	}).then(result => {
		this.getMovies();
		this.closeAddModal();
	}, error => {
		console.error(error);
	});
},
```
## Adding buttons
Finally, we need to add an HTML button to interact with the component. This should be added above the row div for the movie.
```html
<div class="row">
	<div class="col mt-3">
		<button class="btn btn-primary" v-on:click="openAddModal">Add Movie</button>
	</div>
</div>
```

Your `App.vue` file should now look like this:
```html
<template>
  <div>
		<div class="row">
			<div class="col mt-3">
				<button class="btn btn-primary" v-on:click="openAddModal">Add Movie</button>
			</div>
		</div>
    <div class="row">
      <display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id"></display-movie>
      <add-movie v-if="showAddModal" v-bind:movie="movieToAdd" v-on:close-add-modal="closeAddModal" v-on:save-movie-add="saveMovieAdd"></add-movie>
    </div>
  </div>
</template>
<script>
  import DisplayMovie from './components/DisplayMovie'
  import axios from 'axios'
  import AddMovie from './components/AddMovie'
	export default {
		name: 'app',
		components: {
			DisplayMovie,
			AddMovie
		},
		data() {
			return {
				movies: [],
				movieToAdd: {
					title: '',
					year: '',
					director: '',
					description: ''
				},
				showAddModal: false
			}
		},
		methods: {
			getMovies() {
				axios({
					method: 'GET', 'url': '/api/movies'
				}).then(result => {
					this.movies = result.data;
				}, error => {
					console.error(error);
				});
			},
			openAddModal() {
				this.showAddModal = true;
			},
			closeAddModal() {
				this.showAddModal = false;
				this.movieToAdd = {
					title: '',
					year: '',
					director: '',
					description: ''
				};
			},
			saveMovieAdd() {
				axios({
					method: 'POST', 'url': '/api/movies', 'data': this.movieToAdd
				}).then(result => {
					this.getMovies();
					this.closeAddModal();
				}, error => {
					console.error(error);
				});
			},
		},
		mounted() {
			this.getMovies();
		},
	}
</script>
<style lang="scss">
</style>
```