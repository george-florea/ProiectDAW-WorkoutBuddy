import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  token: "",
  expiration: Date.now(),
  username: "",
};

const accountSlice = createSlice({
  name: "account",
  initialState,
  reducers: {
    login(state, action) {
      sessionStorage.setItem("token", action.payload.token);
      sessionStorage.setItem("expiration", action.payload.expiration);
      sessionStorage.setItem("username", action.payload.username);
      return action.payload;
    },
    register(state, action) {
      sessionStorage.setItem("token", action.payload.token);
      sessionStorage.setItem("expiration", action.payload.expiration);
      sessionStorage.setItem("username", action.payload.username);
      return action.payload;
    },
    signOut() {
      sessionStorage.removeItem("token");
      sessionStorage.removeItem("expiration");
      sessionStorage.removeItem("username");
      return {
        token: "",
        expiration: "",
        username: ""
      }
    },
    getUser() {},
  },
});

export const accountActions = accountSlice.actions;
export const accountReducer = accountSlice.reducer;
