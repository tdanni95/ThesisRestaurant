import type { UserData } from "$lib/types/userData";

export function UserDataFromJwt(accesToken: string): UserData {
    let user: UserData = {} as UserData;
    // locals
    try {
        let tokenData = JSON.parse(Buffer.from(accesToken!.split('.')[1], 'base64').toString());
        for (const key in tokenData) {
            if (key.indexOf('role') > 0) {
                user.role = tokenData[key];
                break;
            }
        }
        user.firstName = tokenData.family_name;
        user.lastName = tokenData.given_name;
        user.id = tokenData.sub;
        return user;
    } catch (error) {
        return {} as UserData
    }
}