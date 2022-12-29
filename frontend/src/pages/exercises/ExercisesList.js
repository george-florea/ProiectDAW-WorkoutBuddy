import axios from "axios";
import React, { useEffect } from "react";
import AuthHeader from "../../utils/authorizationHeaders";

const ExercisesList = () => {
  useEffect(() => {
    const getExercises = async () => {
      const { data } = await axios({
        method: "get",
        url: "https://localhost:7132/Exercises/get",
        headers: {
          Authorization: AuthHeader(),
        },
      });
    };
    getExercises();
  }, []);
  return <div>ExercisesList</div>;
};

export default ExercisesList;
