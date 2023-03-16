import { API_ROUTE } from "$env/static/private";
import { ConvertRegisterDataToJson } from "$lib/helpers/formdata";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import type { RequestHandler } from "./$types"

export const POST: RequestHandler = async ({ request, cookies, locals }) => {
    const fd = await request.formData()
    const dataToSend = ConvertRegisterDataToJson(fd)

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
    if (res.id) {
        const plusOneMonth = new Date(Date.now() + 30 * 24 * 60 * 60 * 1000);
        cookies.set('token', res.token, { httpOnly: true, path: "/" })
        cookies.set('refreshToken', res.refreshToken, { httpOnly: true, path: "/", expires: plusOneMonth })

        const userData = UserDataFromJwt(res.token!)
        locals.user = userData
    }
    return json(res)
}