import {
  Avatar,
  Box,
  chakra,
  Container,
  Flex,
  Icon,
  SimpleGrid,
  Stack,
  useColorModeValue,
} from "@chakra-ui/react";
import Comment from "./CommentCard";

export default function Comments(props) {
  //const [comments, setComments] = useState(props.comments);

  return (
    <Flex
      textAlign={"center"}
      pt={10}
      justifyContent={"center"}
      direction={"column"}
      width={"full"}
    >
      <Stack spacing="3rem">
        {props.comments.map((comment, index) => (
          <Comment {...comment} key={comment.commentId} index={index} addHandler={props.addHandler} isReply="true"/>
        ))}
      </Stack>
    </Flex>
  );
}
