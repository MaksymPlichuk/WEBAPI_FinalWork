import { combineReducers } from "@reduxjs/toolkit";
import { carReducer } from "./reducers/carReducer";
import { manufactureReducer } from "./reducers/manufactureReducer";

export const rootReducer = combineReducers({
    car: carReducer,
    manufacture: manufactureReducer
})