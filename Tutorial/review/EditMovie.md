## Review
Contents of `App.vue`:
```html
<template>
	<div>
		<div class="row">
			<div class="col mt-3">
				<button class="btn btn-primary" v-on:click="openAddModal">Add Movie</button>
			</div>
		</div>
		<div class="row">
			<display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id" v-on:edit-movie="editMovie"></display-movie>
			<add-movie v-if="showAddModal" v-bind:movie="movieToAdd" v-on:close-add-modal="closeAddModal" v-on:save-movie-add="saveMovieAdd"></add-movie>
			<edit-movie v-if="showEditModal" v-bind:movie="movieToEdit" v-on:close-edit-modal="closeEditModal" v-on:save-movie-edit="saveMovieEdit"></edit-movie>
		</div>
	</div>
</template>
<script>
  import DisplayMovie from './components/DisplayMovie'  
  import axios from 'axios'
  import AddMovie from './components/AddMovie'
  import EditMovie from './components/EditMovie'
  export default {
	name: 'app',
	components: {
		DisplayMovie,
		AddMovie,
		EditMovie
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
			showAddModal: false,
			movieToEdit: {
				title: '',
				year: '',
				director: '',
				description: ''
			},
			showEditModal: false,
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
    },
    mounted() {
		this.getMovies();
    },
  }
</script>
<style lang="scss">
</style>

```
Contents of `DisplayMovie.vue`:
```html
<template>
	<div class="col-sm-4">
		<div class="card mt-3">
			<div class="card-body">
				<h4 class="card-title">{{movie.title}}</h4>
				<h6 class="card-subtitle mb-2 text-muted">{{movie.year}} - {{movie.director}}</h6>
				<p class="card-text">
					{{movie.description}}
				</p>
				<button class="btn btn-primary" v-on:click="editMovie(movie.id)">Edit</button>
			</div>
		</div>
	</div>
</template>
<script type="text/javascript">
	export default {
		props: ['movie'],
		methods: {
			  editMovie(id) {
				  this.$emit('edit-movie', id);
			  },
		},
	};
</script>
<style lang="scss"></style>
```
Contents of `EditMovie.vue`:
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

Your output should look like:

![EditMovie](../images/EditMovieButton.JPG?raw=true)

When you click the `Edit` button you should see:

![EditMovie Modal](../images/EditMovieModal.JPG?raw=true)