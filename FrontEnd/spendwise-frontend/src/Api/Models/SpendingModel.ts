import { CategorySpendingModel } from "./CategorySpendingModel";

export type SpendingModel = {
  total_sum: number;
  categories: CategorySpendingModel[];
};