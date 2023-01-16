import React, { useEffect, useState } from "react";
import {
  Button,
  Flex,
  FormControl,
  FormLabel,
  Heading,
  Input,
  Stack,
  useColorModeValue,
  Textarea,
  FormErrorMessage,
  Checkbox,
  HStack,
  Box,
} from "@chakra-ui/react";
import Select from "react-select";
import axios from "axios";
import AuthHeader from "../../utils/authorizationHeaders";
import { CloseIcon } from "@chakra-ui/icons";

const Workout = (props) => {
  const [exercises, setExercises] = useState([]);

  const muscleGroupsChangeHandler = async (e) => {
    let queryString = "?";
    let indexOfMuscles = 0;

    for (let elem of e) {
      queryString = `${queryString}[${indexOfMuscles}]=${elem.value}&`;
      indexOfMuscles++;
    }
    const { data } = await axios({
      method: "get",
      url: `https://localhost:7132/Exercises/getExercisesByMuscleGroups${queryString}`,
      headers: {
        Authorization: AuthHeader(),
      },
    });
    setExercises(data);

    const workouts = props.split.workouts;
    workouts[props.index] = {
      ...props.split.workouts[props.index],
      selectedMuscleGroups: e,
    };
    props.setSplit({ ...props.split, workouts });
  };

  const changeNameHandler = (e) => {
    const workouts = props.split.workouts;
    workouts[props.index] = {
      ...props.split.workouts[props.index],
      [e.target.id]: e.target.value,
    };
    props.setSplit({ ...props.split, workouts });
  };

  const changeExercisesHandler = (e) => {
    const workouts = props.split.workouts;
    workouts[props.index] = {
      ...props.split.workouts[props.index],
      exercises: e,
    };
    props.setSplit({ ...props.split, workouts });
  };

  const deleteWorkout = (e) => {
    const workouts = props.split.workouts;
    workouts[props.index] = {
      ...props.split.workouts[props.index],
      isDeleted: true,
    };
    props.setSplit({ ...props.split, workouts });
  };

  return (
    <Stack
      spacing={4}
      w={"full"}
      maxW={"md"}
      bg={useColorModeValue("white", "gray.700")}
      rounded={"xl"}
      boxShadow={"lg"}
      p={6}
      my={12}
    >
      <Box display="flex" justifyContent="space-between">
        <Heading lineHeight={1.1} fontSize={{ base: "lg", sm: "md" }}>
          New workout
        </Heading>
        <a>
          <CloseIcon color="red" onClick={deleteWorkout}/>
        </a>
      </Box>

      <FormControl isRequired>
        <FormLabel> Workout Name</FormLabel>
        <Input
          value={props.split.workouts[props.index].workoutName}
          id="workoutName"
          onChange={changeNameHandler}
          placeholder="name"
          _placeholder={{ color: "gray.500" }}
          type="text"
        />
      </FormControl>

      <FormControl isRequired>
        <FormLabel>Muscle groups</FormLabel>
        <Select
          value={props.split.workouts[props.index].selectedMuscleGroups}
          id="selectedMuscleGroups"
          onChange={muscleGroupsChangeHandler}
          isMulti
          options={props.split.musclesGroups}
        />
      </FormControl>
      <FormControl isRequired>
        <FormLabel>Exercises</FormLabel>
        <Select
          value={props.split.workouts[props.index].exercises}
          id="exercises"
          onChange={changeExercisesHandler}
          isMulti
          options={exercises}
        />
      </FormControl>
    </Stack>
  );
};

export default Workout;
