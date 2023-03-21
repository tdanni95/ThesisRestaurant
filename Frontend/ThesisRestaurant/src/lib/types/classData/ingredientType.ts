export interface IngredientType {
    [key: string]: string | number;
    id: number;
    minOption: number;
    maxOption: number;
    name: string;
}