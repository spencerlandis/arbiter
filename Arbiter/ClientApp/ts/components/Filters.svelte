<script lang="ts">
	import Select from 'svelte-select';
	import {GetDataFeeds, GetSports} from '../data/Odds'
	import type {Option} from '../data/models/Option';
	import {onMount} from 'svelte';
	import {feedId, sportId} from '../data/Store';
	import Icon from 'svelte-awesome';
	import { refresh } from 'svelte-awesome/icons';

	let feeds: Option[] = [];
	let loadingFeeds = false;
	let sports: Option[] = [];
	let loadingSports = false;

	feedId.subscribe(async (event) => {
		loadingSports = true;
		sportId.set(null);
		sports = [];
		if(event)
			sports = await GetSports(event.value)
		loadingSports = false;
	});

	onMount(async () => {
		loadingFeeds = true;
		feeds = await GetDataFeeds();
		loadingFeeds = false;
	});
</script>

<div class="filters">
	<div class="filter-select">
		{#if loadingFeeds}
			<Icon data={refresh} spin/>
		{:else}
			<Select items={feeds} 
				bind:selectedValue={$feedId}
				placeholder='Select Odds Feed...'/>
		{/if}
	</div>

	<div class="filter-select">
		{#if loadingSports}
			<Icon data={refresh} spin/>
		{:else}
			<Select items={sports} 
				bind:selectedValue={$sportId}
				placeholder='Select Sport...'/>
		{/if}
	</div>
</div>

<style type="text/scss">
	.filters {
		display: flex;
		flex-wrap: wrap;

		.filter-select {
			min-width: 300px;
			flex: 1 0 auto;
			margin-left: 1em;
		}
	}
</style>
