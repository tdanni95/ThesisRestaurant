import { redirect } from "@sveltejs/kit";
export const load = ({ data, url }) => {
    if(data && data.user){
        return{
            user: data.user
        }
    }
}