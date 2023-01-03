import React, { useState, useEffect } from "react";
import {
  Container,
  Button,
  Heading,
  Box,
  Grid,
  GridItem,
  Stack,
} from "@chakra-ui/react";
import SplitCard from "./SplitCard";
import axios from "axios";
import AuthHeader from "../../utils/authorizationHeaders";

const SplitsList = () => {
  const [splits, setSplits] = useState([]);

  useEffect(() => {
    const getExercises = async () => {
      const { data } = await axios({
        method: "get",
        url: "https://localhost:7132/Split/getSplits",
        headers: {
          Authorization: AuthHeader(),
        },
      });
      setSplits(data);
      console.log(data);
    };
    getExercises();
  }, []);

  const addHandler = () => {
    navigate("/exercises/insert-exercise");
  };

  return (
    <Box m={5} display={"flex"} justifyContent="center" flexDir={"column"}>
      <Box
        style={{
          justifyContent: "space-between",
          display: "flex",
        }}
      >
        <Heading>Splits: </Heading>
        <Button
          colorScheme="blue"
          style={{ backgroundColor: "#d4f0a5" }}
          onClick={addHandler}
        >
          Add new split
        </Button>
      </Box>

      <Stack>
        {splits.map(split => {
          return <SplitCard key={split.splitId} split={split}></SplitCard>;
        })}
      </Stack>
    </Box>
  );
};

export default SplitsList;
