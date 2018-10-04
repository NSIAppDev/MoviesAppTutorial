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
		AddMovie,
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

Contents of `AddMovie.vue`:
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
Your output should look like:
![AddMovie](../images/AddMovieButton.jpg?raw=true)
When you click the `Add Movie` button you should see:
![AddMovie Modal](../images/AddMovieModal.jpg?raw=true)
After entering details and clicking `Add Movie`, you should see:
![AddMovie Details](../images/AddMovieDetails.jpg?raw=true)
