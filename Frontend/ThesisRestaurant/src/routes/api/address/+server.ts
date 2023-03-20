import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const PUT: RequestHandler = async ({ request, cookies }) => {
    const fd = await request.json()
    const jwt = cookies.get('token')
    const userData = UserDataFromJwt(jwt!)



    // "address/{userId:int}/{addressId:int}"
    const url = `${API_ROUTE}user/address/${userData.id}/${fd.id}`;

    const response = await customFetch(url, cookies, {
        method: "PUT",
        body: JSON.stringify(fd),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    });
    const res = await response.json()

    return json(res)
}

export const POST: RequestHandler = async ({ request, cookies }) => {
    const fd = await request.json()
    const jwt = cookies.get('token')
    const userData = UserDataFromJwt(jwt!)
    // int ZipCode, string City, string Street, string HouseNumber
    const url = `${API_ROUTE}user/address/${userData.id}`
    const response = await customFetch(url, cookies, {
        method: "POST",
        body: JSON.stringify(fd),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    const res = await response.json()
    return json(res)
}

export const DELETE : RequestHandler = async ({request,cookies}) => {
    const fd = await request.json()
    const jwt = cookies.get('token')
    const userData = UserDataFromJwt(jwt!)
    // const url = `${API_ROUTE}user/address/${userData.id}/${fd.id}`
    const url = `${API_ROUTE}user/address/${userData.id}/${fd.id}`
    const response = await customFetch(url, cookies, {
        method: "DELETE",
        body: JSON.stringify(fd),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    if(response.status === 204){
        return json({msg: 'success'})
    }
    const res = await response.json()
    return json(res)
}