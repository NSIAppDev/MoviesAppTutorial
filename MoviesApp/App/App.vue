<template>
	<div>
		<div class="row">
			<div class="col-sm-4" v-for="movie in movies" v-bind:key="movie.id">
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
		</div>
	</div>
</template>
<script>
	export default {
		name: 'app',
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
			},
		}
	}
</script>
<style lang="scss">
</style>
