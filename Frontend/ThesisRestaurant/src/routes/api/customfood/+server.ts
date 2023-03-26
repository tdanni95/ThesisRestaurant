import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import { url } from "inspector";
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies }) => {
    const token = cookies.get('token')
    const ud = UserDataFromJwt(token!)
    const fetchUrl = `${API_ROUTE}customfood/${ud.id}`
    const data = await request.json()

    console.log(fetchUrl);
    


    const response = await customFetch(fetchUrl, cookies, {
        method: 'POST', body: JSON.stringify(data), headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })

    console.log(response);
    

    const res = await response.json()

    // const response = await customFetch();
    return json(res)
}
