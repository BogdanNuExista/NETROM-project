import { FC, useEffect, useState } from "react";
import { CategorySpending } from "../Shared/Types/CategorySpending";
import { CategoriesApiClient } from "../../Api/Clients/CategoriesApiClients";
import { CategorySpendingModel } from "../../Api/Models/CategorySpendingModel";
import { Box, Divider } from "@mui/material";
import { Pie } from "react-chartjs-2";
import "chart.js/auto";

import "./Statistics.css";

export const Statistics: FC = () => {
  const [categorySpendings, setCategorySpendings] = useState<
    CategorySpending[]
  >([]);

  const processData = (data: CategorySpending[]) => {
    return {
      labels: data.map((item) => item.name),
      datasets: [
        {
          label: "Spending",
          data: data.map((item) => item.totalPrice),
          backgroundColor: [
            "rgba(255, 99, 132, 0.6)",
            "rgba(54, 162, 235, 0.6)",
            "rgba(255, 206, 86, 0.6)",
            "rgba(75, 192, 192, 0.6)",
            "rgba(153, 102, 255, 0.6)",
            "rgba(255, 159, 64, 0.6)",
            "rgba(155, 159, 64, 0.6)",
          ],
        },
      ],
    };
  };

  const spendingData = processData(categorySpendings);

  const fetchSpendings = async () => {
    try {

      const startDate = new Date('2003-01-01');
      const endDate = new Date('2025-01-31');

      const res = await CategoriesApiClient.getSpendingAsync(startDate, endDate);

      const spendings = res.map(
        (e: CategorySpendingModel) => ({ ...e } as CategorySpending)
      ); // res.categories.map(..)

      setCategorySpendings(spendings);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchSpendings();
  }, []);

  return (
    <Box className={"statistics-page-container"}>
      <Box className={"statistics-title-text"}>Statistics</Box>

      <Divider />

      <Box className={"statistics-wrapper"}>
        <Box className={"statistics-graph-container"}>
          <Box className={"statistics-title-text"}>
            Total spending:{" "}
            {categorySpendings.reduce((acc, item) => acc + item.totalPrice, 0)}
          </Box>
          <Pie data={spendingData} />
        </Box>
      </Box>
    </Box>
  );
};
