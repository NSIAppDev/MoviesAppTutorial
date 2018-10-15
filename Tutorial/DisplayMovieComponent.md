# Creating Your First Component
A component is a reusable vue instance.  A component consists of a HTML (template), JavaScript and CSS.  An HTML Template is a section of HTML that can be used in place of static HTML on a page.  It can be used in a loop, or called dynamically.  The JavaScript section contains relevant functions or properties of the component.  The CSS section can contain related styles. For the purposes of this tutorial, we are not using component CSS, but rely on the SASS file.

## Setup
Before we begin, it is a good idea to be organized, and therefor, we will create a folder for our components.  Components should be name with two words, usually an action and an object. In our case `DisplayMovie`.  Components are files that end in `.vue`.
Open the file structure of the application and create a new folder called `components` under the `App` folder.

![Create Components Directory](images/CreateComponentsDir.jpg)

## Writing the Component
Now that we have our directory, go ahead and create a new file called `DisplayMovie.vue`.
Right click on the components directory, select `Add` then `New File`, type `DisplayMovie.vue` and press enter.
Your file should automatically open.

Paste in this code and save
```html
<template>
</template>
<script type="text/javascript">
	export default {};
</script>
<style lang="scss"></style>
```
This is default code for an empty template.

Next, open the file `App.vue`
You will notice that this also contains a template section:
```html
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
```
This is the code that is displaying your movie.  The `v-for` and `v-bind` commands are what create the loop.  When we change this code to a component, we will alter this setup.

Back in `DisplayMovie.vue` we will paste in our html that was inside the row div (`<div class="row">...</div>)`
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
```
You will notice that we removed the `v-for` and `v-bind` commands.  To rewire the data, we add a `props` property in our `export` and define it as our `movie` object.
Update the JavaScript to this:
```javascript
<script type="text/javascript">
	export default {
		props: ['movie'],
	};
</script>
```

Your final file should now look like this:
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
## Registering the Component
Back in `App.vue`, we need to update our template to use our new component.  The html we removed will be replaced with:
```html
<display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id"></display-movie>
```
giving us a template section that now looks like this:
```html
<template>
	<div>
		<div class="row">
			<display-movie v-for="movie in movies" v-bind:movie="movie" v-bind:key="movie.id"></display-movie>
		</div>
	</div>
</template>
```
We have re-introduced our `v-for` and `v-bind`, but have also extended it by using the object `movie`.  
You may notice that the component is called with `display-movie` even though we saved the file as `DisplayMovie.vue` and registered it as `DisplayMovie`.
The reason for this is because `Vue` components are saved in PascalCase, but HTML is acted upon in kebab-case.  So `DisplayMovie` becomes `display-movie`.

Before we can run the code, we need to register the component with an `import` command 
In the `script` section, add this line
```javascript
import DisplayMovie from './components/DisplayMovie'
```
We also need to add a new section in our export to list the component
```javascript
components: {
	DisplayMovie,
},
```
## Review
Compare your files against the [expected results](review/DisplayMovie.md).
