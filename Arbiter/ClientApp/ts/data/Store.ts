import { writable } from 'svelte/store';
import type { Option } from './models/Option';

export const feedId = writable<Option>(null);
export const sportId = writable<Option>(null);