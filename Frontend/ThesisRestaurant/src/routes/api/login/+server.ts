import { API_ROUTE } from "$env/static/private";
import { error, json } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies, fetch }) => {
    const fd = await request.formData()
    console.log(fd);

    const dataToSend = {
        email: fd.get('email'),
        password: fd.get('password')
    }
    console.log(dataToSend);
    const url = `${API_ROUTE}authentication/login`;
    const response = await fetch(url, {
        method: "POST",
        mode: 'cors',
        body: JSON.stringify(dataToSend),
        headers: {
            // 'Access-Control-Allow-Origin': 'https://localhost',
            Accept: "application/json",
            "Content-Type": "application/json",
        },
        credentials: "same-origin",

    });
    const res = await response.json()
    if (res.id) {
        // console.log(res.token);
        cookies.set('token', res.token, { httpOnly: true, path: "/" })
        cookies.set('refreshToken', res.refreshToken, { httpOnly: true, path: "/" })
    }
    return json(res)
}