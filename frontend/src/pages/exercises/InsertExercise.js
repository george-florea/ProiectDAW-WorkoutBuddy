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
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import axios from "axios";
import AuthHeader from "../../utils/authorizationHeaders";
import Select from "react-select";
import { useNavigate } from "react-router-dom";

const exerciseInitialState = {
  exerciseId: "00000000-0000-0000-0000-000000000000",
  name: " ",
  description: "",
  exerciseTypes: [],
  selectedType: {},
  muscleGroups: [],
  selectedMuscleGroups: [],
  image: "",
};

export default function InsertExercise() {
  const navigate = useNavigate();
  const [exercise, setExercise] = useState(exerciseInitialState);

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const id = params?.get("id");

    const getExercise = async () => {
      const { data } = await axios({
        method: "get",
        url: `https://localhost:7132/Exercises/getExerciseForInsert?id=${
          id ?? "00000000-0000-0000-0000-000000000000"
        }`,
        headers: {
          Authorization: AuthHeader(),
        },
      });
      setExercise(data);
    };

    getExercise();
  }, []);

  
  const submitHandler = async (e) => {
    e.preventDefault();

    let formData = new FormData();

    let querryString = `?selectedType.value=${exercise.selectedType.value}&selectedType.label=${exercise.selectedType.label}`
    
    formData.append("exerciseId", exercise.exerciseId);
    formData.append("name", exercise.name);
    formData.append("description", exercise.description);
    formData.append("selectedType", exercise.selectedType);
    let index = 0;
    debugger;
    for(let mg of exercise.selectedMuscleGroups){
      formData.append("selectedMuscleGroups", mg);
      querryString += `&selectedMuscleGroups[${index}].value=${mg.value}`
      querryString += `&selectedMuscleGroups[${index}].label=${mg.label}`
      index++;
    }
    formData.append("image", exercise.image);
    
    

    try {
      await axios({
        method: "post",
        url: `https://localhost:7132/Exercises/insertExercise${querryString}`,
        data: formData,
        headers: {
          Authorization: AuthHeader(),
          "Content-Type": "multipart/form-data"
        },
      });
      navigate("/exercises");
    } catch (err){
      console.log("treat errs")
    }
  };

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
        <FormControl
          isRequired
        >
          <FormLabel>Name</FormLabel>
          <Input
            value={exercise.name}
            onChange={(e) => setExercise({ ...exercise, name: e.target.value })}
            placeholder="name"
            _placeholder={{ color: "gray.500" }}
            type="text"
          />
        </FormControl>
        <FormControl
          isRequired
        >
          <FormLabel>Description</FormLabel>
          <Textarea
            value={exercise.description}
            onChange={(e) =>
              setExercise({ ...exercise, description: e.target.value })
            }
            placeholder="description"
            _placeholder={{ color: "gray.500" }}
            type="text"
          />
        </FormControl>
        <FormControl
        >
          <FormLabel>Exercise Types</FormLabel>
          <Select
            value={exercise.selectedType}
            onChange={(e) => setExercise({ ...exercise, selectedType: e })}
            options={exercise.exerciseTypes}
          />
        </FormControl>
        <FormControl
          isRequired
        >
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
        <FormControl
          isRequired={
            exercise.exerciseId == "00000000-0000-0000-0000-000000000000"
          }
        >
          <FormLabel>Image</FormLabel>
          <Input
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
