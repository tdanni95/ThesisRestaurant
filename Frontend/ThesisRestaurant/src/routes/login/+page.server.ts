import { fail, redirect, type Actions } from "@sveltejs/kit";

export const actions: Actions = {
    logout: async ({ cookies, url, locals }) => {
        cookies.delete('token', { path: '/' })
        cookies.delete('refreshToken', {path: '/'})
        locals.user = undefined
        throw redirect(303,  '/login')
    }
}