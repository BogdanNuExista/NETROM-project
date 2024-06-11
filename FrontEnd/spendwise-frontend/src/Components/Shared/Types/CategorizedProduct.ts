import { ScannedProduct } from "./ScannedProduct";

export type CategorizedProduct = {
    id : number;
    name : string;
    products : ScannedProduct[];
}