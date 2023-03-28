import { API_ROUTE, NODE_TLS_REJECT_UNAUTHORIZED } from "$env/static/private";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { redirect } from "@sveltejs/kit";
import type { LayoutServerLoad } from "./$types";
//only for dev purposes
process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = NODE_TLS_REJECT_UNAUTHORIZED

export const load: LayoutServerLoad = async ({ cookies, fetch, url, locals }) => {
    if (locals.user && locals.user.role !== "Admin") {
        throw redirect(307, "/")
    }
    return {
        user: locals.user
    }
}