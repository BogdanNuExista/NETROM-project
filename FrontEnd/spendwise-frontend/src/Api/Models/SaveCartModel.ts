import { CategorizedProductModel } from "./CategorizedProductModel";

export type SaveCartModel = {
  date: Date;
  categoryProducts: CategorizedProductModel[];
};