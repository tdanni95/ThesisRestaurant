import { RefreshToken } from "$lib/helpers/refreshToken";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import type { Handle, HandleFetch, HandleServerError } from "@sveltejs/kit";
import { sequence } from "@sveltejs/kit/hooks";

export const handle: Handle = async ({ event, resolve }) => {
    const { locals, cookies, url } = event
    const accesToken = cookies.get('token')

    if (!accesToken) {
        locals.user = undefined
    } else if (!url.pathname.startsWith('/api') && accesToken) {
        locals.user = accesToken ? UserDataFromJwt(accesToken) : undefined
    }
    const response = await resolve(event)


    return response
}

export const handleHooks = sequence(handle)

export const handleFetch: HandleFetch = async ({ request, fetch }) => {
    return fetch(request)
}

export const handleError: HandleServerError = async ({ error, event }) => {
    console.log("error", error);
}