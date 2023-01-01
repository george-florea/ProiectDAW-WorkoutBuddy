import {
  Box,
  Center,
  useColorModeValue,
  Heading,
  Text,
  Stack,
  Image,
  GridItem,
  Button,
  Link,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import AuthHeader from "../../utils/authorizationHeaders";

export default function Exercise({ exercise, deleteHandler: deleteExercises }) {
  const navigate = useNavigate();
  const [isAdmin, setIsAdmin] = useState(false);
  useEffect(() => {
    var roles = sessionStorage.getItem("roles");
    if (roles) {
      setIsAdmin(roles.includes("Admin"));
    }
  }, []);

  const viewHandler = (exerciseId) => {
    navigate(`/exercises/${exerciseId}`)
  }

  const editHandler = (exerciseId) => {
    navigate(`/exercises/insert-exercise?id=${exerciseId}`)
  }

  const deleteHandler = async (exerciseId) => {
    let res = confirm("Are you sure you want to delete this exercise?");
    if(res){
      try{
        await axios({
          method: "post",
          url: `https://localhost:7132/Exercises/delete`,
          data: exerciseId,
          headers: {
            "Content-Type": 'application/json',
            "Authorization": AuthHeader(),
          },
        });
        deleteExercises(exerciseId)
      }
      catch (err){

      }
     

    }
  }

  return (
    <GridItem w="100%">
      <Center py={12}>
        <Box
          role={"group"}
          p={6}
          maxW={"330px"}
          w={"full"}
          bg={useColorModeValue("white", "gray.800")}
          boxShadow={"2xl"}
          rounded={"lg"}
          pos={"relative"}
          zIndex={1}
        >
          <Box
            rounded={"lg"}
            mt={-12}
            pos={"relative"}
            height={"230px"}
            _after={{
              transition: "all .3s ease",
              content: '""',
              w: "full",
              h: "full",
              pos: "absolute",
              top: 5,
              left: 0,
              backgroundImage: `url(${`https://localhost:7132/Image/getImageById?id=${exercise.idImage}`})`,
              filter: "blur(15px)",
              zIndex: -1,
            }}
            _groupHover={{
              _after: {
                filter: "blur(20px)",
              },
            }}
          >
            <Image
              loading="eager"
              rounded={"lg"}
              height={230}
              width={282}
              objectFit={"cover"}
              src={`https://localhost:7132/Image/getImageById?id=${exercise.idImage}`}
            />
          </Box>
          <Stack pt={10} align={"center"}>
            <Text
              color={"gray.500"}
              fontSize={"sm"}
              textTransform={"uppercase"}
            >
              {exercise.exerciseType}
            </Text>
            <Heading fontSize={"2xl"} fontFamily={"body"} fontWeight={500}>
              {exercise.name}
            </Heading>
          </Stack>
          <Stack mt={8} direction={"row"} spacing={4}>
            <Button
              flex={1}
              fontSize={"sm"}
              rounded={"full"}
              _focus={{
                bg: "gray.200",
              }}
              onClick={(e) => viewHandler(exercise.exerciseId)}
            >
              View
            </Button>
            {isAdmin && (
              <>
                <Button
                  flex={1}
                  fontSize={"sm"}
                  rounded={"full"}
                  bg={"blue.400"}
                  color={"white"}
                  boxShadow={
                    "0px 1px 25px -5px rgb(66 153 225 / 48%), 0 10px 10px -5px rgb(66 153 225 / 43%)"
                  }
                  _hover={{
                    bg: "blue.500",
                  }}
                  _focus={{
                    bg: "blue.500",
                  }}
                  onClick={(e) => editHandler(exercise.exerciseId)}
                >
                  Edit
                </Button>
                <Button
                  flex={1}
                  fontSize={"sm"}
                  rounded={"full"}
                  bg={"red.400"}
                  color={"white"}
                  boxShadow={
                    "0px 1px 25px -5px rgb(220 20 60 / 48%), 0 10px 10px -5px rgb(220 20 60 / 43%)"
                  }
                  _hover={{
                    bg: "red.500",
                  }}
                  _focus={{
                    bg: "red.500",
                  }}
                  onClick={(e) => deleteHandler(exercise.exerciseId)}
                >
                  Delete
                </Button>
              </>
            )}
          </Stack>
        </Box>
      </Center>
    </GridItem>
  );
}
