import React from "react";
import { Navigate } from "react-router-dom";

const PrivateRoute = ({children}) => {
  const authenticated = sessionStorage.getItem("token");
  return authenticated ? children : <Navigate to="/login" />;
};

export default PrivateRoute;
