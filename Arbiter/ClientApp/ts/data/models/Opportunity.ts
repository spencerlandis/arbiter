export interface Opportunity {
    sport: number;
    dataFeedId: number;
    startTime: string;
    homeTeam: string;
    awayTeam: string;
    bets: Bet[];
}

export interface Bet {
    outcome: OutcomeId;
    site: string;
    outcomeOdds: number;
    lastUpdated: string;
}

export enum OutcomeId {
    HomeWin = 1,
    AwayWin = 2,
    Tie = 3
}