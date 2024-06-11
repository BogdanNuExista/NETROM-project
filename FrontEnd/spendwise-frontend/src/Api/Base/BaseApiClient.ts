import axios from "axios";

const defaultHeathers = {
    'Content-Type': 'application/json',
};

export const SpendWiseClient = axios.create({
    baseURL: "https://localhost:7272/api/",
    headers: defaultHeathers,
});