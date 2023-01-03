import {
  Box,
  chakra,
  Container,
  Stack,
  Text,
  Image,
  Flex,
  VStack,
  Button,
  Heading,
  SimpleGrid,
  StackDivider,
  useColorModeValue,
  VisuallyHidden,
  List,
  ListItem,
  ListIcon,
  Textarea,
  Card,
} from "@chakra-ui/react";
import axios from "axios";
import React, { useEffect, useState } from "react";
import AuthHeader from "../../utils/authorizationHeaders";
import { getURLID } from "../../utils/URLUtils";
import { ArrowRightIcon } from "@chakra-ui/icons";
import Comments from "../comments/Comments";

const ViewSplit = () => {
  const [split, setSplit] = useState({ workouts: [], comments: [] });
  const [commentText, setCommentText] = useState("");
  const [isNewComment, setIsNewComment] = useState(false);

  useEffect(() => {
    const id = getURLID(location.href);
    const getSplit = async (id) => {
      const { data } = await axios({
        method: "get",
        url: `https://localhost:7132/Split/viewSplit?id=${id}`,
        headers: {
          Authorization: AuthHeader(),
        },
      });
      setSplit(data);
      console.log(data);
    };

    getSplit(id);
  }, [isNewComment]);

  const addHandler = async (text, parentCommentId = null) => {
    debugger;
    let newComment = {
      commentText: text,
      parentCommentId,
      parentSplitId: split.splitId,
    };

    await axios({
      method: "post",
      url: `https://localhost:7132/Comment/add`,
      data: newComment,
      headers: {
        Authorization: AuthHeader(),
      },
    });
    setIsNewComment(state => !state);
    setCommentText("")
  };

  return (
    <Container>
      <Stack spacing={{ base: 6, md: 10 }} alignContent="center" py="3rem">
        <Box as={"header"}>
          <Heading
            lineHeight={1.1}
            fontWeight={600}
            fontSize={{ base: "2xl", sm: "4xl", lg: "5xl" }}
            textAlign="center"
          >
            {split.name}
          </Heading>
          <Text
            fontWeight={600}
            color={"gray.500"}
            size="sm"
            mb={4}
            textAlign="center"
          >
            @{split.creatorName}
          </Text>
        </Box>

        <Stack
          spacing={{ base: 4, sm: 6 }}
          direction={"column"}
          divider={
            <StackDivider
              borderColor={useColorModeValue("gray.200", "gray.600")}
            />
          }
        >
          <VStack spacing={{ base: 4, sm: 6 }}>
            <Text
              color={useColorModeValue("gray.500", "gray.400")}
              fontSize={"2xl"}
              fontWeight={"300"}
            >
              {split.description}
            </Text>
          </VStack>
          {split.workouts.map((workout, index) => {
            return (
              <div key={index}>
                <Box>
                  <Text
                    fontSize={{ base: "16px", lg: "18px" }}
                    color={useColorModeValue("yellow.500", "yellow.300")}
                    fontWeight={"500"}
                    textTransform={"uppercase"}
                    mb={"4"}
                  >
                    {workout.workoutName}
                  </Text>

                  <SimpleGrid columns={{ base: 1, md: 2 }} spacing={10}>
                    {workout.exercisesList.map((ex) => {
                      return (
                        <List>
                          <ListItem>
                            <ListIcon as={ArrowRightIcon} color="green.500" />
                            {ex}
                          </ListItem>
                        </List>
                      );
                    })}
                  </SimpleGrid>
                </Box>
              </div>
            );
          })}
        </Stack>
        <Stack>
          <Heading>Comments:</Heading>
          <Stack
            spacing="10"
            borderWidth="1px"
            rounded="lg"
            shadow="1px 1px 3px rgba(0,0,0,0.3)"
            maxWidth={800}
            p={6}
            m="10px auto"
          >
            <Heading size="lg">Do you have any question?</Heading>
            <Stack direction="row">
              <Textarea
                placeholder="say something nice"
                value={commentText}
                onChange={(e) => setCommentText(e.target.value)}
              />
              <Flex alignItems="center" justifyItems="center">
                <Button
                  w="full"
                  colorScheme="blue"
                  variant="outline"
                  onClick={() => addHandler(commentText)}
                >
                  Add comment
                </Button>
              </Flex>
            </Stack>
          </Stack>

          <Comments comments={split.comments} addHandler={addHandler}/>
        </Stack>
      </Stack>
    </Container>
  );
};

export default ViewSplit;
