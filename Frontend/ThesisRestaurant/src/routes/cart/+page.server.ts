import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { UserData } from "$lib/types/userData";
import type { PageServerLoad } from "./$types";
import cartStore from "$lib/stores/cartStore";
import { get } from 'svelte/store';

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:cart');  
  
    const userDataResponse = await customFetch(`${API_ROUTE}user/${locals.user?.id}`, cookies);
    const userData = await userDataResponse.json();
  
    return {
      userData: userData as UserData,
      user: locals.user
    };
  };