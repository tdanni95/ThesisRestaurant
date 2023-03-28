import { redirect } from "@sveltejs/kit"
import type { PageLoad } from "./$types"

export const load: PageLoad = async ({ parent, fetch }) => {
    const { user } = await parent()

    console.log("USER", user);
    
    
    if (user) throw redirect(307, "/")
}