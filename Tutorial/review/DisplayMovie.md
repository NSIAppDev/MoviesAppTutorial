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
  export default {
	name: 'app',
	components: {
		DisplayMovie,
	},
	data() {
		return {
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
		}
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
			</div>
		</div>
	</div>
</template>
<script type="text/javascript">
	export default {
		props: ['movie'],
	};
</script>
<style lang="scss"></style>
```
Your output should look like:
![DisplayMovie](../images/FirstScreen.jpg?raw=true)