<script lang="ts">
	import type {Opportunity} from '../data/models/Opportunity';
	import {feedId, sportId} from '../data/Store';
	import Icon from 'svelte-awesome';
	import { refresh } from 'svelte-awesome/icons';
    import type { Option } from '../data/models/Option';
    import { GetOpportunities } from '../data/Arbitrage';
    import OpportunityView from './OpportunityView.svelte'

    let loading = false;
    let validOptions = false;
    let selectedFeed: Option;
    let selectedSport: Option;
    let opportunities: Opportunity[]= [];

    async function selectionChanged() {
        loading = true;
        opportunities =[];
        validOptions = !!(selectedFeed && selectedSport);
        if(validOptions)
            opportunities = await GetOpportunities(selectedFeed.value, selectedSport.value);
        loading = false;
    }
    
    feedId.subscribe(async value => {
        selectedFeed = value;
        selectedSport = null;
        await selectionChanged();
    });

    sportId.subscribe(async value => {
        selectedSport = value;
        await selectionChanged();
    });
</script>

<div class="results">
    {#if loading}
        <Icon data={refresh} spin/>
    {:else if !validOptions}
        <span>Select a feed and sport to get started.</span>
    {:else if opportunities.length === 0}
        <span>No opportunities available on {selectedFeed.label} for {selectedSport.label}</span>
    {:else}
        {#each opportunities as opportunity}
            <OpportunityView opportunity={opportunity} />
        {/each}
    {/if}
</div>

<style type="text/scss">
    .results {
        padding: 1em;
        display: flex;
        flex-wrap: wrap;
    }
</style>
