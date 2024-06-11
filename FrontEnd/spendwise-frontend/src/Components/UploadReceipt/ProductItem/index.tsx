import { FC } from "react";

import "./ProductItem.css";
import { ScannedProduct } from "../../Shared/Types/ScannedProduct";
import { Category } from "../../Shared/Types/Category";
import {
  Box,
  MenuItem,
  Select,
  SelectChangeEvent,
  Typography,
} from "@mui/material";

interface ProductItemProps {
  product: ScannedProduct;
  categories: Category[];
  currentCategoryId: number;
  onCategoryChange: (product: ScannedProduct, newCategoryId: number) => void;
}

export const ProductItem: FC<ProductItemProps> = ({
  product,
  categories,
  currentCategoryId,
  onCategoryChange,
}: ProductItemProps) => {
  const handleCategoryChange = (event: SelectChangeEvent<number>) => {
    onCategoryChange(product, event.target.value as number);
  };

  return (
    <Box className={"product-box"}>
      <Typography className={"product-name"}>{product.name}</Typography>
      <Typography className={"product-quantity"}>
        Quantity: {product.quantity}
      </Typography>
      <Typography className={"product-price"}>
        Price: ${product.price.toFixed(2)}
      </Typography>
      <Select
        value={currentCategoryId}
        onChange={handleCategoryChange}
        className="category-select"
      >
        {categories.map((category) => (
          <MenuItem key={category.id} value={category.id}>
            {category.name}
          </MenuItem>
        ))}
      </Select>
    </Box>
  );
};