import { ScannedProductModel } from "./ScannedProductModel"

export type CategorizedProductModel = {
    id : number;
    name : string;
    products : ScannedProductModel[];
}