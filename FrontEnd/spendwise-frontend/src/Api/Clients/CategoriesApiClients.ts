import { SpendWiseClient } from "../Base/BaseApiClient";
import { CategoryModel } from "../Models/CategoryModel";
import { CategorySpendingModel } from "../Models/CategorySpendingModel";

export const CategoriesApiClient = {
    urlPath : "Category",

    getAllAsync():Promise<CategoryModel[]> {
        return SpendWiseClient.get<CategoryModel[]>(this.urlPath + "/GetCategories").then(
            (response)=>response.data);
    },

    getOneAsync(id : number):Promise<CategoryModel>
    {
        return SpendWiseClient.get<CategoryModel>(this.urlPath + "/GetCategory/" + id).then(
            (response)=>response.data);
    },

    createOneAsync(model : CategoryModel):Promise<CategoryModel>
    {
        return SpendWiseClient.post<CategoryModel>(this.urlPath + "/CreateCategory", model).then(
            (response)=>response.data);
    },

    deleteOneAsync(id : number):Promise<any>
    {
        return SpendWiseClient.delete(this.urlPath + "/DeleteCategory/" + id).then(
            (response) => response.data);
    },

    updateOneAsync(id : number, model : CategoryModel):Promise<any>
    {
        return SpendWiseClient.patch(this.urlPath + "/UpdateCategory/" + id, model).then(
            (response) => response.data);
    },

    getSpendingAsync(dateFrom: Date, dateTo: Date): Promise<CategorySpendingModel[]> {
        const queryParams = new URLSearchParams({
          dateFrom: dateFrom.toISOString(),
          dateTo: dateTo.toISOString()
        });
      
        return SpendWiseClient.get<CategorySpendingModel[]>(
          `${this.urlPath}/GetPriceForCategories?${queryParams}`
        ).then((response) => response.data);
      }

};