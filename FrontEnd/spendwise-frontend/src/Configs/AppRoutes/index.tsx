import {FC} from "react"
import { Route, Routes } from "react-router-dom"
import App from "../../App"
import Home from "../../Components/Home"
import { Categories } from "../../Components/Categories"
import { UploadReceipt } from "../../Components/UploadReceipt"
import { Statistics } from "../../Components/Statistics"
import { Products } from "../../Components/Products"

export const AppRoutes: FC = () => {
    return (
       <Routes>
        <Route path = {"/"} element = {<App/>}>
            <Route path = {"/"} element = {<Home/>}/>
            <Route path = {"/categories"} element = {<Categories/>}/>
            <Route path = {"/statistics"} element = {<Statistics/>}/>
            <Route path = {"/receipt"} element = {<UploadReceipt/>}/>
            <Route path={"/products"} element={<Products />} />
        </Route>
       </Routes>
    );
}