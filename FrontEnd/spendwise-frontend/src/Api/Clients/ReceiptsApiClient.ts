import { Category } from "../../Components/Shared/Types/Category";
import { SpendWiseClient } from "../Base/BaseApiClient";
import { SaveCartModel } from "../Models/SaveCartModel";

export const ReceiptApiClient = {
    urlPath: "Receipts",

    scanReceipt(image: File, categories:Category[]) {
        const formData = new FormData();
        formData.append("image",image);
        formData.append("categories", JSON.stringify(categories));

        return SpendWiseClient.post(this.urlPath + "/ScanReceipt" , formData, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        }).then((response) => response.data);
    },

    saveCart(model: SaveCartModel) {
        return SpendWiseClient.post(this.urlPath + "/SaveCart", model).then(
          (response) => response.data
        );
      },

};