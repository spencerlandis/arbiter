import axios from 'axios';
import type { Option } from './models/Option'; 

export async function GetDataFeeds(): Promise<Option[]> {
    const response = await axios.get<Option[]>('/api/odds/feeds');
    return response.data;
}

export async function GetSports(feedId: number): Promise<Option[]> {
    const response = await axios.get<Option[]>(`/api/odds/feed/${feedId}/sports`);
    return response.data;
}