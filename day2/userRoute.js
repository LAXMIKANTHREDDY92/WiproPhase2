import express from "express";
import { create, deleteData, getAll, getOne, update } from "../controller/userController.js";

const route=express.Router();

route.post("/create",create);

route.get("/getall",getAll);

route.get("/getone/:id",getOne);

route.post("/update/:id",update);

route.delete("/deleteUser/:id",deleteData);

export default route;