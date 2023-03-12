import type { Address } from "$lib/types/address";

export const ConvertRegisterDataToJson = (formData: FormData) => {
    const formDataObj: Record<string, string | Array<Address>> = {};

    const keys = formData.keys()
    const addresses: Array<Address> = []
    for (let key of keys) {
        if (key.includes('[')) {
            const values = formData.getAll(key)
            // let addr: Address = ;
            key = key.replace('[]', '')
            let idx = 0
            for(const value of values){
                //fix ts error
                if(!addresses[idx]){
                    addresses.push({} as Address);
                }
                const addr = addresses[idx]
                if (typeof value === "string") {
                    addr[key] = value;
                } else if (typeof value === "number") {
                    addr[key] = +value;
                }
                idx++
            }
        } else {
            const value = formData.get(key)
            formDataObj[key] = value as string
        }
    }
    formDataObj.addresses = addresses


    return formDataObj
}
