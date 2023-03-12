import { API_ROUTE } from "$env/static/private";
import { ConvertRegisterDataToJson as Convert } from "$lib/helpers/formdata";
import type { Actions } from "@sveltejs/kit"

export const actions: Actions = {
    register: async ({ request }) => {
        console.log("here");
        
        const form = await request.formData()

        const dataToSend = Convert(form)

        const result = await fetch(`${API_ROUTE}:7068/foodtype/`)
        console.log(result);
    
    }
}