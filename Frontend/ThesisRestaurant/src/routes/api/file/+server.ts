import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import { url } from "inspector";
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies, url }) => {
    const data = await request.formData()
    const id = url.searchParams.get('id')
    
    const fetchUrl = `${API_ROUTE}food/file/${id}`

    const response = await customFetch(fetchUrl, cookies, {
        method: 'POST', body: data,
    })

    if(response.ok){
        return json({msg: "success"})
    }
    

    const res = await response.json()

    // const response = await customFetch();
    return json(res)
}
