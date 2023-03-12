import { API_ROUTE } from "$env/static/private";
import { ConvertRegisterDataToJson } from "$lib/helpers/formdata";
import { error, json } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies }) => {
    const fd = await request.formData()
    const dataToSend = ConvertRegisterDataToJson(fd)
    console.log(dataToSend);
    
    const url = `${API_ROUTE}authentication/register`;
    const response = await fetch(url, {
        method: "POST",
        body: JSON.stringify(dataToSend),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    });
    const res = await response.json()
    return json(res)
}