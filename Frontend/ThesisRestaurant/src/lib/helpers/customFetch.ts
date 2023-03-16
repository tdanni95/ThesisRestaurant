import { API_ROUTE } from "$env/static/private";
import type { CustomFetchOptions } from "$lib/types/customFetchOptions";
import {  error, redirect, type Cookies } from "@sveltejs/kit";
import { RefreshToken } from "./refreshToken";

export async function customFetch(url: string, cookies: Cookies, options: CustomFetchOptions = {}): Promise<Response> {
    const accessToken = cookies.get('token')
    const refreshToken = cookies.get('refreshToken')

    if (accessToken) {
        options.headers = {
            ...options.headers,
            Authorization: `Bearer ${accessToken}`,
        };
    }

    const response = await fetch(url, options)
    
    if (response.status == 401) {
        if (!refreshToken) {
            throw redirect(307, '/login')
        }
        const res = await RefreshToken(refreshToken, cookies)
        if (res.id) {
            return await customFetch(url, cookies, options)
        } else {
            throw redirect(307, "/login")
        }
    }
    else if(response.status == 403){
        throw error(403, "Unathorized")
    }

    return response;
}
