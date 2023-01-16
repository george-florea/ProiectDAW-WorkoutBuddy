import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  token: "",
  expiration: Date.now(),
  username: "",
  roles: []
};

const accountSlice = createSlice({
  name: "account",
  initialState,
  reducers: {
    login(state, action) {
      sessionStorage.setItem("token", action.payload.token);
      sessionStorage.setItem("expiration", action.payload.expiration);
      sessionStorage.setItem("username", action.payload.username);
      sessionStorage.setItem("roles", action.payload.roles);
      localStorage.setItem("token", action.payload.token);
      localStorage.setItem("expiration", action.payload.expiration);
      localStorage.setItem("username", action.payload.username);
      localStorage.setItem("roles", action.payload.roles);
      return action.payload;
    },
    register(state, action) {
      sessionStorage.setItem("token", action.payload.token);
      sessionStorage.setItem("expiration", action.payload.expiration);
      sessionStorage.setItem("username", action.payload.username);
      sessionStorage.setItem("roles", action.payload.roles);
      return action.payload;
    },
    signOut() {
      sessionStorage.removeItem("token");
      sessionStorage.removeItem("expiration");
      sessionStorage.removeItem("username");
      sessionStorage.removeItem("roles");
      return {
        token: "",
        expiration: "",
        username: "",
        roles: []
      }
    },
    getUser() {},
  },
});

export const accountActions = accountSlice.actions;
export const accountReducer = accountSlice.reducer;
