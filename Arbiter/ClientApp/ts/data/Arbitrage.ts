import axios from 'axios';
import type { Opportunity } from './models/Opportunity'; 

export async function GetOpportunities(dataFeedId: number, sportId: number): Promise<Opportunity[]> {
    const response = await axios.get<Opportunity[]>(`/api/arbitrage/feed/${dataFeedId}/sport/${sportId}`);
    return response.data;
}