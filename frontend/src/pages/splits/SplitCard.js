import { StarIcon } from "@chakra-ui/icons";
import {
  Badge,
  Box,
  Button,
  Center,
  Flex,
  Heading,
  HStack,
  Image,
  Link,
  Stack,
  Text,
  useColorModeValue,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function SplitCard({ split }) {
  const navigate = useNavigate();
  const [isAdmin, setIsAdmin] = useState(false);
  useEffect(() => {
    var roles = sessionStorage.getItem("roles");
    if (roles) {
      setIsAdmin(roles.includes("Admin"));
    }
  }, []);

  const viewHandler = (id) => {
    navigate(`/splits/${id}`);
  }

  const editHandler = (id) => {
    navigate(`/splits/insert-split?id=${id}`);
  }

  const deleteHandler = async (id) => {
    let res = confirm("Are you sure you want to delete this split?");
    if(res){
      try{
        await axios({
          method: "post",
          url: `https://localhost:7132/Splits/delete`,
          data: id,
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
    <Center py={6}>
      <Stack
        borderWidth="1px"
        borderRadius="3xl"
        w={{ sm: "100%", md: "65rem" }}
        height={{ sm: "476px", md: "20rem" }}
        direction={{ base: "column", md: "row" }}
        bg={useColorModeValue("white", "gray.900")}
        boxShadow={"2xl"}
        spacing={8}
      >
        <Flex
          flex={1}
          borderLeftRadius="3xl"
          bgColor={"#393E46"}
          alignItems="center"
          justifyContent="center"
          color="#efefef"
        >
          <Heading>{split.name}</Heading>
        </Flex>
        <Stack
          flex={1}
          flexDirection="column"
          justifyContent="center"
          alignItems="center"
          p={4}
          pt={2}
        >
          <HStack>
            <Heading fontSize={"2xl"} fontFamily={"body"}>
              Rating: {split.rating} <StarIcon color="yellow" />
            </Heading>
          </HStack>

          <Text fontWeight={600} color={"gray.500"} size="sm" mb={4}>
            @{split.creatorName}
          </Text>
          <Text
            textAlign={"center"}
            color={useColorModeValue("gray.700", "gray.400")}
            px={3}
          >
            {split.description}
          </Text>
          <Stack align={"center"} justify={"center"} direction={"row"} mt={6}>
            <Text>Workouts: </Text>
            {split.workouts.map((w, index) => {
              return (
                <Badge
                  px={2}
                  py={1}
                  bg={useColorModeValue("gray.50", "gray.800")}
                  fontWeight={"400"}
                  key={index}
                >
                  {w}
                </Badge>
              );
            })}
          </Stack>

          <Stack
            width={"100%"}
            mt={"2rem"}
            direction={"row"}
            padding={2}
            justifyContent={"space-between"}
            alignItems={"center"}
          >
            <Button
              flex={1}
              fontSize={"sm"}
              rounded={"full"}
              _focus={{
                bg: "gray.200",
              }}
              onClick={() => viewHandler(split.splitId)}
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
                  onClick={(e) => editHandler(split.splitId)}
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
        </Stack>
      </Stack>
    </Center>
  );
}
