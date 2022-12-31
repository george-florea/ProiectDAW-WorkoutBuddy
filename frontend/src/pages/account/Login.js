import {
  Flex,
  Box,
  FormControl,
  FormLabel,
  Input,
  Checkbox,
  Stack,
  Link,
  Button,
  Heading,
  Text,
  useColorModeValue,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { accountActions } from "../../store/reducers/account";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";

const loginModelInitialState = {
  email: '',
  password: '',
  areCredentialsInvalid: false,
  isDisabled: false
}

export default function Login() {
  const dispatcher = useDispatch()
  
  const [loginModel, setLoginModel] = useState(loginModelInitialState);

  const submitHandler = async (e) => {
    e.preventDefault();
    const res = await axios({
      method: 'post',
      url: 'https://localhost:7132/UserAccount/login',
      data: loginModel
    })
    dispatcher(accountActions.login(res.data));

    location.href = "/"
}


  return (
    <Flex
      minH={"100vh"}
      align={"center"}
      justify={"center"}
      bg={useColorModeValue("gray.50", "gray.800")}
    >
      <Stack spacing={8} mx={"auto"} maxW={"lg"} py={12} px={6}>
        <Stack align={"center"}>
          <Heading fontSize={"4xl"}>Sign in to your account</Heading>
        </Stack>
        <Box
          rounded={"lg"}
          bg={useColorModeValue("white", "gray.700")}
          boxShadow={"lg"}
          p={8}
        >
          <Stack spacing={4}>
            <FormControl id="email">
              <FormLabel>Email address</FormLabel>
              <Input type="email" 
                value={loginModel.email}
                onChange={(e) => setLoginModel({...loginModel, email: e.target.value})}
              />
            </FormControl>
            <FormControl id="password">
              <FormLabel>Password</FormLabel>
              <Input type="password" 
                value={loginModel.password}
                onChange={(e) => setLoginModel({...loginModel, password: e.target.value})}
              />
            </FormControl>
            <Stack spacing={10}>
              <Stack
                direction={{ base: "column", sm: "row" }}
                align={"start"}
                justify={"space-between"}
              >
                <Checkbox>Remember me</Checkbox>
                <Link color={"blue.400"}>Forgot password?</Link>
              </Stack>
              <Button
                bg={"blue.400"}
                color={"white"}
                _hover={{
                  bg: "blue.500",
                }}
                onClick={submitHandler}
              >
                Sign in
              </Button>
            </Stack>
          </Stack>
        </Box>
      </Stack>
    </Flex>
  );
}
