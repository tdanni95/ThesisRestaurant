import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import { url } from "inspector";
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies, fetch, locals, url }) => {
    const fetchUrl = buildUrl(url)
    const data = await request.json()
    const response = await customFetch(fetchUrl, cookies, {
        method: 'POST', body: JSON.stringify(data), headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    const res = await response.json()

    // const response = await customFetch();
    return json(res)
}

export const PUT: RequestHandler = async ({ request, cookies, fetch, locals, url}) => {
    const fetchUrl = buildUrl(url)
    const data = await request.json()
    const response = await customFetch(fetchUrl, cookies, {
        method: 'PUT', body: JSON.stringify(data), headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    const res = await response.json()

    // const response = await customFetch();
    return json(res)
}


export const DELETE: RequestHandler = async ({ request, cookies, fetch, locals, url }) => {
    const fetchUrl = buildUrl(url)
    const response = await customFetch(fetchUrl, cookies, {
        method: 'DELETE', headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    const res = await response.json()

    // const response = await customFetch();
    return json(res)
}

const buildUrl = (url: URL) => {
    const type = url.searchParams.get('type')
    let id = url.searchParams.get('id')

    let fetchUrl = `${API_ROUTE}${type}`
    if (id) {
        fetchUrl += `/${id}`
    }
    return fetchUrl
}