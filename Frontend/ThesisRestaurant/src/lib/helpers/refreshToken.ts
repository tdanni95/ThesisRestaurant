import { API_ROUTE } from "$env/static/private"
import { redirect, type Cookies } from "@sveltejs/kit"

export async function RefreshToken(token: string, cookies: Cookies) {
    const profileRes = await fetch(`${API_ROUTE}authentication/refreshtoken`, { method: "POST" })
    const res = await profileRes.json()
    if (res.id) {
        const plusOneMonth = new Date(Date.now() + 30 * 24 * 60 * 60 * 1000);
        cookies.set('token', res.token, { httpOnly: true, path: "/" })
        cookies.set('refreshToken', res.refreshToken, { httpOnly: true, path: "/", expires: plusOneMonth })
    } else {
        throw redirect(307, "/login")
    }
    return res
}