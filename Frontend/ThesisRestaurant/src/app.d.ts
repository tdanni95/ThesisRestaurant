// See https://kit.svelte.dev/docs/types#app

import type { UserData } from "$lib/types/userData";

// for information about these interfaces
declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			user?: UserData
		}
		// interface PageData {}
		// interface Platform {}
	}
}

export {};
