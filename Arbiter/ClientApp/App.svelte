<script lang="ts">
	import Filters from './ts/components/Filters.svelte';
	import ResultsView from './ts/components/ResultsView.svelte';
</script>

<main>
	<h1>Arbiter</h1>
	<Filters />
	<ResultsView />
</main>

<style type="text/scss">
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		color: #0011ff;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>
