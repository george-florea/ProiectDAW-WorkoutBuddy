import { createSlice } from "@reduxjs/toolkit";

const initialState = {
        token: "",
        expiration: Date.now(),
        username: ""
}

const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        login(state, action) {
            console.log(action.payload)
            debugger;
            return action.payload; 
        },
        register(state, action) {
            console.log(action.payload)
            debugger;
            return action.payload;
        },
        signOut() {},
        getUser() {}
    }
})

export const accountActions = accountSlice.actions;
export const accountReducer = accountSlice.reducer