import {
  Button,
  Flex,
  FormControl,
  FormLabel,
  Heading,
  Input,
  Stack,
  useColorModeValue,
  HStack,
  Avatar,
  AvatarBadge,
  IconButton,
  Center,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import axios from "axios";
import AuthHeader from "../../utils/authorizationHeaders";
import Select from "react-select";

const exerciseInitialState = {
  exerciseId: "00000000-0000-0000-0000-000000000000",
  name: " ",
  description: "",
  exerciseTypes: [],
  selectedType: 0,
  muscleGroups: [],
  selectedMuscleGroups: [],
  image: "",
};

export default function InsertExercise() {
  const [exercise, setExercise] = useState(exerciseInitialState);

  useEffect(() => {
    const getExercise = async () => {
      const { data } = await axios({
        method: "get",
        url: `https://localhost:7132/Exercises/getExerciseForInsert`,
        headers: {
          Authorization: AuthHeader(),
        },
      });
      setExercise(data);
      console.log(data);
    };

    getExercise();
  }, []);

  const submitHandler = (e) => {
    console.log(exercise);
  }

  return (
    <Flex
      minH={"100vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
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
        <Heading lineHeight={1.1} fontSize={{ base: "2xl", sm: "3xl" }}>
          Insert Exercise
        </Heading>
        <FormControl isRequired>
          <FormLabel>Name</FormLabel>
          <Input
            value={exercise.name}
            onChange={(e) => setExercise({ ...exercise, name: e.target.value })}
            placeholder="exercise name"
            _placeholder={{ color: "gray.500" }}
            type="text"
          />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Description</FormLabel>
          <Input
            value={exercise.description}
            onChange={(e) =>
              setExercise({ ...exercise, description: e.target.value })
            }
            placeholder="description"
            _placeholder={{ color: "gray.500" }}
            type="text"
          />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Exercise Types</FormLabel>
          <Select
            value={exercise.selectedType}
            onChange={(e) =>
              setExercise({ ...exercise, selectedType: e.target.value })
            }
            options={exercise.exerciseTypes}
          />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Muscle groups</FormLabel>
          <Select
            value={exercise.selectedMuscleGroups}
            onChange={(e) => {
              setExercise({ ...exercise, selectedMuscleGroups: e });
            }}
            isMulti
            options={exercise.muscleGroups}
          />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Image</FormLabel>
          <Input
            //value={exercise.image}
            onChange={(e) => {
              setExercise({ ...exercise, image: e.target.files[0] });
            }}
            placeholder="description"
            _placeholder={{ color: "gray.500" }}
            type="file"
            accept="image/*"
          />
        </FormControl>
        <Stack spacing={6} direction={["column", "row"]}>
          <Button
            bg={"blue.400"}
            color={"white"}
            w="full"
            _hover={{
              bg: "blue.500",
            }}
            onClick={submitHandler}
          >
            Submit
          </Button>
        </Stack>
      </Stack>
    </Flex>
  );
}
