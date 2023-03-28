import { fail, redirect, type Actions } from "@sveltejs/kit";

export const actions: Actions = {
    logout: async ({ cookies, url, locals }) => {
        console.log("futok?");
        
        cookies.delete('token', {path: '/', httpOnly: true})
        cookies.delete('refreshToken', {path: '/', httpOnly: true})
        locals.user = undefined
        throw redirect(303,  '/login')
    }
}