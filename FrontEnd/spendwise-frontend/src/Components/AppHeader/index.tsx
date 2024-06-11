import { AppBar, Button, Container, Toolbar, Typography } from "@mui/material"
import {FC} from "react"
import "./AppHeader.css"
import { Link } from "react-router-dom"

export const AppHeader : FC = () =>
{
    return (
        <AppBar position="static">
            <Container maxWidth={false} >
            <Toolbar  disableGutters className="menu-container">

                <Button variant = "contained" component={Link} to ="/">
                    <Typography className="menu-button-text">Home</Typography>
                </Button>

                <Button variant = "contained" component={Link} to="/categories">
                    <Typography className="menu-button-text">Categories</Typography>
                </Button>

                <Button variant = "contained" component={Link} to="/statistics">
                    <Typography className="menu-button-text">Statistics</Typography>
                </Button>

                <Button variant = "contained" component={Link} to="/receipt">
                    <Typography className="menu-button-text">Upload Receipt</Typography>
                </Button>

                <Button variant = "contained" component={Link} to="/products">
                    <Typography className="menu-button-text">Products</Typography>
                </Button>
                
            </Toolbar>
            </Container>
        </AppBar>
    )
}