import { SpendWiseClient } from "../Base/BaseApiClient";
import { ProductModel } from "../Models/ProductModel";

export const ProductsApiClient = {
  urlPath: "Product", //products

  getAllAsync(): Promise<ProductModel[]> {
    return SpendWiseClient.get<ProductModel[]>(
      this.urlPath + "/GetProducts" //this.urlPath
    ).then((response) => response.data);
  },
};