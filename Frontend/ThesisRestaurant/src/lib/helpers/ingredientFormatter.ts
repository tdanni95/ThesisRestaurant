import type { Ingredient } from "$lib/types/classData";

export function ingredientFormatter (ingredients: Array<Ingredient>) {
    let types: { [name: string]: Array<string> } = {};
    if (!ingredients) return "";
    ingredients.sort((a, b) => {
        if (a.ingredientType.name === b.ingredientType.name) {
            return a.name.localeCompare(b.name);
        }
        return a.ingredientType.name > b.ingredientType.name ? 1 : -1;
    });
    ingredients.forEach((ingredient) => {
        const typeName = ingredient.ingredientType.name;
        if (!types[typeName]) {
            types[typeName] = [] as Array<string>;
        }
        types[typeName].push(ingredient.name);
    });
    let retStr: string = "";
    for (const type in types) {
        retStr += `<p><strong>${type}:</strong> ${types[type].join(", ")}<br></p>`;
    }
    return retStr;
};