import { Box, Button, Divider, IconButton } from "@mui/material";
import { FC, useEffect, useState } from "react";
import "./Categories.css";
import AddIcon from "@mui/icons-material/Add";
import CancelIcon from "@mui/icons-material/Cancel";
import EditIcon from '@mui/icons-material/Edit';
import { Category } from "../Shared/Types/Category";
import { CategoriesApiClient } from "../../Api/Clients/CategoriesApiClients";
import { CategoryModel } from "../../Api/Models/CategoryModel";
import { AddCategoryPopup } from "./AddCategoryPopup";
import { UpdateCategoryPopup } from "./UpdateCategoryPopup";

export const Categories: FC = () => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [openAdd, setOpenAdd] = useState(false);
    const [openUpdate, setOpenUpdate] = useState(false);
    const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);

    const handleOpenAdd = () => setOpenAdd(true);
    const handleCloseAdd = () => setOpenAdd(false);

    const handleOpenUpdate = (category: Category) => {
        setSelectedCategory(category);
        setOpenUpdate(true);
    };
    const handleCloseUpdate = () => {
        setSelectedCategory(null);
        setOpenUpdate(false);
    };

    const fetchCategories = async () => {
        try {
            const res = await CategoriesApiClient.getAllAsync();
            const categories = res.map((e: CategoryModel) => ({ ...e } as Category));
            setCategories(categories);
        } catch (error: any) {
            console.log(error);
        }
    };

    const deleteCategory = async (id?: number) => {
        if (!id) return;

        try {
            await CategoriesApiClient.deleteOneAsync(id);
            const newCategories = categories.filter((el) => el.id !== id);
            setCategories(newCategories);
        } catch (error: any) {
            console.log(error);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    return (
        <Box>
            <Box className={"new-category-section"}>
                <Box className="categories-title-text">Add a new category</Box>
                <Button size="medium" variant="contained" color="primary" onClick={handleOpenAdd} sx={{ color: "#fff" }}>
                    <AddIcon fontSize="large" />
                </Button>
            </Box>

            <Divider />

            <Box className={"categories-list-section"}>
                <Box className={"categories-title-text"}>Current categories</Box>
                <Box className={"categories-list"}>
                    {categories.map((category: Category, index: number) => (
                        <Box key={`${category.id}-${index}`} className={"category"}>
                            <Box className={"category-text-container"}>{category.name}</Box>

                            <IconButton onClick={() => deleteCategory(category.id)}>
                                <CancelIcon color="primary" fontSize="large" />
                            </IconButton>

                            <IconButton onClick={() => handleOpenUpdate(category)}>
                                <EditIcon color="primary" fontSize="large" />
                            </IconButton>
                        </Box>
                    ))}
                </Box>
            </Box>

            <AddCategoryPopup
                open={openAdd}
                onClose={handleCloseAdd}
                onEditing={(category: Category) => {
                    setCategories([...categories, category]);
                }}
            />

            {selectedCategory && (
                <UpdateCategoryPopup
                    open={openUpdate}
                    onClose={handleCloseUpdate}
                    category={selectedCategory}
                    onEditing={(updatedCategory: Category) => {
                        const newCategories = categories.map((cat) =>
                            cat.id === updatedCategory.id ? updatedCategory : cat
                        );
                        setCategories(newCategories);
                    }}
                />
            )}
        </Box>
    );
};
