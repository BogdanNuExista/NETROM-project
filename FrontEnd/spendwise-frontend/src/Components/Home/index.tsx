import { Box, Button, Card, CardActions, CardContent, Grid, Typography } from "@mui/material";
import React, { FC } from "react";
import "./Home.css"
import { Link } from "react-router-dom";

export const Home: FC = () => {
    return <Box className={"content-container"}>
        <Box className={"background-image"}></Box>
        <Box className={"title-logo-container"}>
            <img src="../../asset/logo.png" alt="Spendwise logo" className="title-logo"></img>
        </Box>

        <Grid container spacing={{ xs: 2, md: 3 }}>


            <Grid item xs={4} sm={4} md={4}>
            <Card className={"card"}>
            <img
              src="/asset/categories-logo.png"
              alt="Categories"
              className={"card-image"}
            />
            <CardContent>
              <Typography
                gutterBottom
                variant="h5"
                component="div"
                textAlign={"center"}
                className={"section-title"}
              >
                Categories
              </Typography>
            </CardContent>
            <CardActions>
              <Button component={Link} to="/categories" variant="contained" className="card-button">
                <Typography>Go to</Typography>
              </Button>
            </CardActions>
          </Card>
            </Grid>


            <Grid item xs={4} sm={4} md={4}>
            <Card className={"card"}>
            <img
              src="/asset/receipt-logo.png"
              alt="Receipts"
              className={"card-image"}
            />
            <CardContent>
              <Typography
                gutterBottom
                variant="h5"
                component="div"
                textAlign={"center"}
                className={"section-title"}
              >
                Receipts
              </Typography>
            </CardContent>
            <CardActions>
              <Button component={Link} to="/receipt" variant="contained" className="card-button">
                <Typography>Go to</Typography>
              </Button>
            </CardActions>
          </Card>
            </Grid>


            <Grid item xs={4} sm={4} md={4}>
            <Card className={"card"}>
            <img
              src="/asset/statistics.png"
              alt="Statistics"
              className={"card-image"}
            />
            <CardContent>
              <Typography
                gutterBottom
                variant="h5"
                component="div"
                textAlign={"center"}
                className={"section-title"}
              >
                Statistics
              </Typography>
            </CardContent>
            <CardActions>
              <Button component={Link} to="/statistics" variant="contained" className="card-button">
                <Typography>Go to</Typography>
              </Button>
            </CardActions>
          </Card>
            </Grid>



        </Grid>

    </Box>;
}

export default Home;