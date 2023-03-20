// See https://kit.svelte.dev/docs/types#app

import type { Cart } from "$lib/types/cart";
import type { UserData } from "$lib/types/userData";

// for information about these interfaces
declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			user?: UserData;
			cart?: Cart;
		}
		// interface PageData {}
		// interface Platform {}
	}
}

export { };
