import { redirect } from "@sveltejs/kit"
import type { PageLoad } from "./$types"

export const load: PageLoad = async ({ parent, fetch }) => {
    const { user } = await parent()

    if (user) throw redirect(307, "/")
}