import React, { useState, useEffect } from "react";
import {
  Box,
  Flex,
  HStack,
  Link,
  IconButton,
  Button,
  useDisclosure,
  useColorModeValue,
  Stack,
  Heading,
  Grid,
  GridItem,
} from "@chakra-ui/react";
import { HamburgerIcon, CloseIcon } from "@chakra-ui/icons";
import { useDispatch, useSelector } from "react-redux";
import { accountActions } from "../../store/reducers/account";
import { RootState } from "../../store";
import MenuAuthenticatedUser from "./Menu";

const Links = [
  { text: "Home", path: "/" },
  { text: "Splits", path: "/splits" },
  { text: "Exercises", path: "/exercises" },
];

const NavLink = ({ children, path }) => {
  return (
    <Link
      px={2}
      py={1}
      rounded={"md"}
      _hover={{
        textDecoration: "none",
        bg: useColorModeValue("gray.200", "gray.700"),
      }}
      href={path}
    >
      {children}
    </Link>
  );
};

export default function Header() {
  const dispatcher = useDispatch();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const accountState = useSelector((state) => state.account);
  const [username, setUsername] = useState(accountState.username);
  const [isAdmin, setisAdmin] = useState(false);
  const [jwtToken, setJwtToken] = useState("");

  const logoutHandler = () => {
    dispatcher(accountActions.signOut());
    window.location.href = "/login";
  };

  useEffect(() => {
    const token = sessionStorage.getItem("token");
    if (token) {
      setJwtToken(token);
      setIsLoggedIn(true);
      setUsername(sessionStorage.getItem("username") ?? "User");
      setisAdmin(sessionStorage.getItem("roles")?.includes("Admin") || false);
    } else {
      setIsLoggedIn(false);
    }
  }, [accountState]);

  return (
    <>
      <Box >
        <Box textAlign={"center"} bg={"primary"} py={2}>
          Welcome to the best fitness app!
        </Box>
        <Flex justify={"center"} py={6}>
          <Heading _hover={{ textDecoration: "none" }}>Workout Buddy</Heading>
        </Flex>
        <Grid
          gridTemplateColumns={"repeat(12, 1fr);"}
          px={{ md: 8, base: 2 }}
          py={4}
          bg={"rgba(0, 0, 0, 0.1)"}
        >
          <GridItem colSpan={8}>
            <IconButton
              size={"md"}
              icon={isOpen ? <CloseIcon /> : <HamburgerIcon />}
              aria-label={"Open Menu"}
              display={{ md: "none" }}
              onClick={isOpen ? onClose : onOpen}
            />
            <HStack
              spacing={8}
              alignItems={"center"}
              marginBottom={{ base: "10px", xl: 0 }}
              flexBasis={{ base: "100%", xl: "0" }}
              justifyContent={{ base: "center", xl: "start" }}
            >
              <HStack
                as={"nav"}
                spacing={4}
                display={{ base: "none", md: "flex" }}
                justifyItems="center"
              >
                {Links.map((link) => (
                  <NavLink key={link.text} path={link.path}>
                    {link.text}
                  </NavLink>
                ))}
                {isAdmin && (
                  <Link
                  px={2}
                  py={1}
                  rounded={"md"}
                  _hover={{
                    textDecoration: "none",
                    bg: useColorModeValue("gray.200", "gray.700"),
                  }}
                  href={`http://localhost:4200/pending-exercises?token=${jwtToken}`}
                >
                  Pending Exercises
                </Link>
                )}
              </HStack>
            </HStack>
          </GridItem>

          <GridItem colSpan={4}>
            <Flex alignItems={"center"} justifyContent={"end"} flexBasis="30%">
              {isLoggedIn ? (
                <MenuAuthenticatedUser
                  username={username}
                  logoutHandler={logoutHandler}
                  jwt={jwtToken}
                />
              ) : (
                <HStack spacing={3}>
                  <Button fontSize={"sm"} fontWeight={400} variant={"link"}>
                    <Link href="/login">Sign In</Link>
                  </Button>
                  <Button
                    display={{ base: "none", md: "inline-flex" }}
                    fontSize={"sm"}
                    fontWeight={600}
                    // colorScheme={colors.primaryScheme}
                  >
                    <Link
                      href="/register"
                      _hover={{
                        textDecoration: "none",
                      }}
                    >
                      Sign Up
                    </Link>
                  </Button>
                </HStack>
              )}
            </Flex>
          </GridItem>
        </Grid>

        {isOpen ? (
          <Box pb={4} display={{ md: "none" }}>
            <Stack as={"nav"} spacing={4}>
              {Links.map((link) => (
                <NavLink key={link.text} path={link.path}>
                  {link.text}
                </NavLink>
              ))}
              <Link
                px={2}
                py={1}
                rounded={"md"}
                _hover={{
                  textDecoration: "none",
                  // bg: colors.bgHover,
                }}
                href={`https://localhost:4200/pending-exercises`}
              >
                Pending Exercises
              </Link>
            </Stack>
          </Box>
        ) : null}
      </Box>
    </>
  );
}
