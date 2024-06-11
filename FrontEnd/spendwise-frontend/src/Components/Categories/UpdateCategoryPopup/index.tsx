import { FC, useState, useEffect } from "react";
import "./UpdateCategoryPopup.css";
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import { Category } from "../../Shared/Types/Category";
import { CategoryModel } from "../../../Api/Models/CategoryModel";
import { CategoriesApiClient } from "../../../Api/Clients/CategoriesApiClients";

interface UpdateCategoryPopupProps {
    open: boolean;
    category: Category;
    onClose: () => void;
    onEditing: (category: Category) => void;
}

export const UpdateCategoryPopup: FC<UpdateCategoryPopupProps> = ({
    open,
    category,
    onClose,
    onEditing,
}: UpdateCategoryPopupProps) => {
    const [categoryName, setCategoryName] = useState(category.name);

    useEffect(() => {
        setCategoryName(category.name);
    }, [category]);

    const handleClose = () => {
        setCategoryName(category.name);
        onClose();
    };

    const updateCategory = async () => {
        const model: CategoryModel = { id: category.id, name: categoryName };
    
        try {
          const res = await CategoriesApiClient.updateOneAsync(category.id || 0 , model);
          return res;
        } catch (error: any) {
          console.log(error);
        }
      };

    const handleSave = async () => {
        const categoryModel = await updateCategory();
        const updatedCategory = { ...category, name: categoryName } as Category;
        onEditing(updatedCategory);
        handleClose();
    };

    return (
        <Dialog fullWidth={true} maxWidth={"md"} open={open} onClose={onClose}>
            <DialogTitle fontSize={24}>Update category</DialogTitle>
            <DialogContent className={"update-category-modal-content"}>
                <TextField
                    fullWidth
                    label="Category Name"
                    value={categoryName}
                    onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                        setCategoryName(event.target.value);
                    }}
                />
            </DialogContent>

            <DialogActions className={"update-category-modal-actions"}>
                <Button onClick={handleClose} variant="outlined">
                    Close
                </Button>
                <Button
                    onClick={handleSave}
                    variant="contained"
                    disabled={!categoryName}
                    className="save-button"
                >
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    );
};
