<script lang="ts">
	import type {Opportunity} from '../data/models/Opportunity';
	import {OutcomeId} from '../data/models/Opportunity';
    export let opportunity: Opportunity;

    let desiredWin = 100;
    $: stakeTotal = opportunity.bets.reduce((total, current) => total + 1.0 / current.outcomeOdds, 0);
    $: rateOfReturn = Math.round((1.0 - stakeTotal) / stakeTotal * 10_000) / 100;
</script>

<div class="result">
    <div>
        <span class="teams">{opportunity.awayTeam} @ {opportunity.homeTeam}</span>
        <span class="roi">{rateOfReturn}%</span>
        <input bind:value={desiredWin}>
        {#each opportunity.bets as bet}
            <span class="bet">${Math.round((desiredWin / bet.outcomeOdds) * 100) / 100} 
                on {bet.outcome === OutcomeId.Tie ? "Tie" : (bet.outcome === OutcomeId.HomeWin ? opportunity.homeTeam : opportunity.awayTeam)}
                at {bet.site}</span>
        {/each}
    </div>
</div>

<style type="text/scss">
    .result {
        flex: 1, 0 auto;
        min-width: 300px;
        margin: 1em;
        padding: 1em;
        border: .1em solid blue;
        border-radius: 5px;

        span {
            display: block;

            &:not(:first-child){
				margin-top: 1em;
			}
        }
    }
</style>
