# Review
Contents of `App.vue`:
```html
<template>
  <div>
    <div class="row">
      <display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id"></display-movie>
    </div>
  </div>
</template>
<script>
  import DisplayMovie from './components/DisplayMovie'  
  import axios from 'axios'
  export default {
	name: 'app',
	components: {
		DisplayMovie,
	},
	data() {
		return {
			movies: [],
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
    },
    mounted() {
		this.getMovies();
    },
  }
</script>
<style lang="scss">
</style>
```
Your output should look like:
![DisplayMovie](../images/FirstScreen.jpg?raw=true)