import { API_ROUTE } from "$env/static/private";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies, fetch, locals }) => {
    const fd = await request.formData()
    
    const dataToSend = {
        email: fd.get('email'),
        password: fd.get('password')
    }
    const url = `${API_ROUTE}authentication/login`;
    const response = await fetch(url, {
        method: "POST",
        mode: 'cors',
        body: JSON.stringify(dataToSend),
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        credentials: "same-origin",

    });
    const res = await response.json()
    if (res.id) {
        const plusOneMonth = new Date(Date.now() + 30 * 24 * 60 * 60 * 1000);
        cookies.set('token', res.token, { httpOnly: true, path: "/", expires: plusOneMonth })
        cookies.set('refreshToken', res.refreshToken, { httpOnly: true, path: "/", expires: plusOneMonth })
        const userData = UserDataFromJwt(res.token!)
        locals.user = userData
    }
    
    return json(res)
}