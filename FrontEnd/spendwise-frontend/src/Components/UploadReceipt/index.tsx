import { Box, Button, CircularProgress, Divider, Typography } from "@mui/material";
import React, { FC, useEffect, useState } from "react";
import './UploadRceipt.css'
import UploadIcon from "@mui/icons-material/Upload";
import { Category } from "../Shared/Types/Category";
import { CategoryModel } from "../../Api/Models/CategoryModel";
import { CategoriesApiClient } from "../../Api/Clients/CategoriesApiClients";
import { ScannedRceiptPopup } from "./ScannedReceiptPopup";
import { CategorizedProduct } from "../Shared/Types/CategorizedProduct";
import { CategorizedProductModel } from "../../Api/Models/CategorizedProductModel";
import { SaveCartModel } from "../../Api/Models/SaveCartModel";
import { ReceiptApiClient } from "../../Api/Clients/ReceiptsApiClient";
import { useNavigate } from "react-router-dom";

import { DragDropContext, Draggable, Droppable, DropResult } from "react-beautiful-dnd";


export const UploadReceipt: FC = () => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [isSetupComplete, setIsSetupComplete] = useState(false);
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const [scannedImage, setScannedImage] = useState<File | null>(null);
    const [categorizedProducts, setCategorizedProducts] = useState<CategorizedProduct[]>([]);
    const navigate = useNavigate();
  
    const fetchCategories = async () => {
      try {
        const res = await CategoriesApiClient.getAllAsync();
  
        const categories = res.map((e: CategoryModel) => ({ ...e } as Category));
        setCategories(categories);
        setIsSetupComplete(true);
      } catch (error: any) {
        console.log(error);
      }
    };

    const saveProductsInReceipt = async () => {
        try {
          const model: SaveCartModel = {
            date: new Date(),
            categoryProducts: categorizedProducts.map(
              (el) => ({ ...el } as CategorizedProductModel)
            ),
          };
          const res = await ReceiptApiClient.saveCart(model);
    
          navigate("/products");
        } catch (error: any) {
          console.log(error);
        }
      };


    /// for drag and drop 

    const onDragEnd = (result: DropResult) => {
      if (!result.destination) {
          return;
      }
  
      const sourceCategoryIndex = categorizedProducts.findIndex(
          (category) => category.id === Number(result.source.droppableId)
      );
      const destinationCategoryIndex = categorizedProducts.findIndex(
          (category) => category.id === Number(result.destination?.droppableId)
      );
  
      const sourceCategory = categorizedProducts[sourceCategoryIndex];
      const destinationCategory = categorizedProducts[destinationCategoryIndex];
  
      const sourceProducts = Array.from(sourceCategory.products);
      const [movedProduct] = sourceProducts.splice(result.source.index, 1);
  
      if (sourceCategoryIndex === destinationCategoryIndex) {
          // Reordering within the same category
          sourceProducts.splice(result.destination.index, 0, movedProduct);
          const newCategories = Array.from(categorizedProducts);
          newCategories[sourceCategoryIndex] = {
              ...sourceCategory,
              products: sourceProducts,
          };
          setCategorizedProducts(newCategories);
      } else {
          // Moving to a different category
          const destinationProducts = Array.from(destinationCategory.products);
          destinationProducts.splice(result.destination.index, 0, movedProduct);
          const newCategories = Array.from(categorizedProducts);
          newCategories[sourceCategoryIndex] = {
              ...sourceCategory,
              products: sourceProducts,
          };
          newCategories[destinationCategoryIndex] = {
              ...destinationCategory,
              products: destinationProducts,
          };
          setCategorizedProducts(newCategories);
      }
  };
  
    




  
    useEffect(() => {
      fetchCategories();
    }, []);
  
    return !isSetupComplete ? (
      <Box className={"spinner-layout"}>
        <CircularProgress />
      </Box>
    ) : (
      <Box>
        <Box className={"upload-receipt-section"}>
          <Box className={"upload-receipt-title-text"}>Upload a receipt</Box>
          <Button
            size="medium"
            color="primary"
            variant="contained"
            onClick={handleOpen}
          >
            <UploadIcon fontSize="large" color="secondary" sx={{color: "white"}}/>
          </Button>
        </Box>
  
        <Divider />

        
        {scannedImage && (
        <>
          <Box className={"uploaded-image-section"}>
            <Box>
              <Box className={"uploaded-image-container"}>
                <Box
                  className={"upload-receipt-title-text"}
                  textAlign={"center"}
                >
                  Uploaded image
                </Box>
                <img
                  src={URL.createObjectURL(scannedImage)}
                  className={"uploaded-image"}
                />
              </Box>
            </Box>
            <Box className={"categorized-products-section"}>
              <Box className={"upload-receipt-title-text"}>
                Categorized Products
              </Box>
              <Button
                onClick={() => saveProductsInReceipt()}
                variant="contained"
                className={"save-products-button"}
              >
                Save products
              </Button>
              
              <DragDropContext onDragEnd={onDragEnd}>
                                {categorizedProducts.map((category) => (
                                    <Droppable key={category.id} droppableId={category.id.toString()}>
                                        {(provided) => (
                                            <Box
                                                {...provided.droppableProps}
                                                ref={provided.innerRef}
                                                className={"category-box"}
                                            >
                                                <Typography variant="h6" className={"category-name"}>
                                                    {category.name}
                                                </Typography>
                                                <Box className={"products-list"}>
                                                    {category.products.map((product, index) => (
                                                        <Draggable
                                                            key={`${category.id}-${index}`}
                                                            draggableId={`${category.id}-${index}`}
                                                            index={index}
                                                        >
                                                            {(provided) => (
                                                                <Box
                                                                    ref={provided.innerRef}
                                                                    {...provided.draggableProps}
                                                                    {...provided.dragHandleProps}
                                                                    className={"product-box"}
                                                                >
                                                                    <Typography className={"product-name"}>
                                                                        {product.name}
                                                                    </Typography>
                                                                    <Typography className={"product-quantity"}>
                                                                        Quantity: {product.quantity}
                                                                    </Typography>
                                                                    <Typography className={"product-price"}>
                                                                        Price: ${product.price.toFixed(2)}
                                                                    </Typography>
                                                                </Box>
                                                            )}
                                                        </Draggable>
                                                    ))}
                                                    {provided.placeholder}
                                                </Box>
                                            </Box>
                                        )}
                                    </Droppable>
                                ))}
                            </DragDropContext>
                        </Box>
                    </Box>
                </>
            )}
        
        <ScannedRceiptPopup
          open={open}
          onClose={handleClose}
          categories={categories}
          onScanning={(
            file: File,
            categorizedProducts: CategorizedProduct[]
          ) => {
            setScannedImage(file);
            setCategorizedProducts(categorizedProducts);
          }}
        />
      </Box>
    );
  };