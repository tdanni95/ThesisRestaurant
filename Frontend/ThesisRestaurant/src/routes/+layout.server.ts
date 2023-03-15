import { API_ROUTE, NODE_TLS_REJECT_UNAUTHORIZED } from "$env/static/private";
import { redirect } from "@sveltejs/kit";
import type { LayoutServerLoad } from "./$types";
//only for dev purposes
process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = NODE_TLS_REJECT_UNAUTHORIZED

export const load: LayoutServerLoad = async ({ cookies, fetch, url }) => {
    const accesToken = cookies.get('token')
    const refreshToken = cookies.get('refreshToken')


    if (!accesToken) {
        const profileRes = await fetch(`${API_ROUTE}authentication/refreshtoken`, { method: "POST" })
        const res = await profileRes.json()
        if (res.id) {
            // console.log(res.token);
            cookies.set('token', res.token, { httpOnly: true, path: "/" })
            cookies.set('refreshToken', res.refreshToken, { httpOnly: true, path: "/" })
        }else{
            throw redirect(307, "/login")
        }
        return
    }

    let valami = JSON.parse(Buffer.from(accesToken.split('.')[1], 'base64').toString());
    for(const key in valami){
        if(key.indexOf('role') > 0){
            //AppUser
            //Employee
            //Admin
            console.log(valami[key]);
        }
    }
    // console.log(valami);

    //TODO
}