import { useState, useEffect } from "react";
import {
  Box,
  Flex,
  Avatar,
  HStack,
  Link,
  IconButton,
  Button,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  MenuDivider,
  useDisclosure,
  useColorModeValue,
  Stack,
} from "@chakra-ui/react";
import { HamburgerIcon, CloseIcon } from "@chakra-ui/icons";
import { useDispatch, useSelector } from "react-redux";
import { accountActions } from "../../store/reducers/account";
import axios from "axios";
const Links = ["Home", "Splits", "Exercises"];

const NavLink = ({ children }) => {
  return (
  <Link
    px={2}
    py={1}
    rounded={"md"}
    _hover={{
      textDecoration: "none",
      bg: useColorModeValue("gray.200", "gray.700"),
    }}
    href={"/" + children.toLowerCase()}
  >
    {children}
  </Link>
)};

export default function Header() {
  const dispatcher = useDispatch();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const accountState = useSelector((state) => state.account);
  const [username, setUsername] = useState(accountState.username);

  const logoutHandler = () => {
    dispatcher(accountActions.signOut());
    location.href = "/login";
  };

  useEffect(() => {
    const token = sessionStorage.getItem("token");
    if (token) {
      setIsLoggedIn(true);
      setUsername(sessionStorage.getItem("username") ?? "User");
    } else {
      setIsLoggedIn(false);
    }
  }, [accountState]);

  return (
    <>
      <Box bg={useColorModeValue("gray.100", "gray.900")} px={4}>
          {isLoggedIn ? (
            <Flex h={16} alignItems={"center"} justifyContent={"space-between"}>
              <IconButton
                size={"md"}
                icon={isOpen ? <CloseIcon /> : <HamburgerIcon />}
                aria-label={"Open Menu"}
                display={{ md: "none" }}
                onClick={isOpen ? onClose : onOpen}
              />
              <HStack spacing={8} alignItems={"center"}>
                <Box>Workout Buddy</Box>
                <HStack
                  as={"nav"}
                  spacing={4}
                  display={{ base: "none", md: "flex" }}
                >
                  {Links.map((link) => (
                    <NavLink key={link}>{link}</NavLink>
                  ))}
                </HStack>
              </HStack>
              <Flex alignItems={"center"}>
              <>Hello, </>
              <Menu>
                <MenuButton
                  as={Button}
                  rounded={"full"}
                  variant={"link"}
                  cursor={"pointer"}
                  minW={0}
                >
                  <p>{username}</p>
                </MenuButton>
                <MenuList>
                  <MenuItem>My profile</MenuItem>
                  <MenuItem>Edit profile</MenuItem>
                  <MenuItem>Change password</MenuItem>
                  <MenuItem>Edit weight</MenuItem>
                  <MenuDivider />
                  <MenuItem onClick={logoutHandler}>Sign Out</MenuItem>
                </MenuList>
              </Menu>
            </Flex>
            </Flex>
          )
          : (
            <Flex alignItems={"flex-end"} justifyContent={"space-between"}>
              <Stack
                flex={{ base: 1, md: 0 }}
                justify={"flex-end"}
                direction={"row"}
                spacing={6}
              >
                <Button
                  as={"a"}
                  fontSize={"sm"}
                  fontWeight={400}
                  variant={"link"}
                  href="/login"
                >
                  <Link href="/login">Sign In</Link>
                </Button>
                <Button
                  display={{ base: "none", md: "inline-flex" }}
                  fontSize={"sm"}
                  fontWeight={600}
                  color={"white"}
                  bg={"pink.400"}
                  _hover={{
                    bg: "pink.300",
                  }}
                >
                  <Link href="/register">Sign Up</Link>
                </Button>
              </Stack>
            </Flex>
          )
          }

        {isOpen ? (
          <Box pb={4} display={{ md: "none" }}>
            <Stack as={"nav"} spacing={4}>
              {Links.map((link) => (
                <NavLink key={link}>{link}</NavLink>
              ))}
            </Stack>
          </Box>
        ) : null}
      </Box>
    </>
  );
}
