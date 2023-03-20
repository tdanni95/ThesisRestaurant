import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect, type Cookies } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const PUT: RequestHandler = async ({ request, cookies, url }) => {
    const fd = await request.json()
    const jwt = cookies.get('token')
    const userData = UserDataFromJwt(jwt!)
    const action = url.searchParams.get("action");

    if (action === "modify") {
        const dataToSend = {
            Id: userData.id,
            FirstName: fd.firstName,
            LastName: fd.lastName,
            Email: fd.email,
            PhoneNumber: fd.phoneNumber,
        }
        const response = await modifyUserData(dataToSend, cookies)
        const res = await response.json()
        return json(res)
    } else {        
        fd.Id = userData.id
        const response = await modifyUserPassword(fd, cookies)
        //nocontent
        if(response.status === 204){
            return json({msg: 'success'})
        }
        const res = await response.json()
        console.log(res);
        
        return json(res)
    }
}

async function modifyUserData(dataToSend: {}, cookies: Cookies): Promise<Response> {
    const postUrl = `${API_ROUTE}user`;
    const response = await customFetch(postUrl, cookies, {
        method: "PUT",
        body: JSON.stringify(dataToSend),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    });
    return response
}

async function modifyUserPassword(dataToSend: {}, cookies: Cookies): Promise<Response> {
    const postUrl = `${API_ROUTE}user/password`;
    const response = await customFetch(postUrl, cookies, {
        method: "PUT",
        body: JSON.stringify(dataToSend),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    });
    return response
}