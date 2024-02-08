import {
    Menu,
    MenuButton,
    Button,
    MenuList,
    MenuItem,
    MenuDivider,
    Text,
    Link,
    useDisclosure,
  } from "@chakra-ui/react";
  import React from "react";
  
  function MenuAuthenticatedUser({
    username,
    logoutHandler,
    jwt
  }) {
    const { isOpen, onClose, onOpen } = useDisclosure();
    return (
      <React.Fragment>
        <Text>Hello, </Text>
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
            <MenuItem>
              <Link
                px={2}
                py={1}
                rounded={"md"}
                _hover={{
                  textDecoration: "none",
                }}
                href={`http://localhost:4200/user-profile?token=${jwt}`}
              >
                My profile
              </Link>
            </MenuItem>
  
            <MenuDivider />
            <MenuItem onClick={logoutHandler}>Sign Out</MenuItem>
          </MenuList>
        </Menu>
  
      </React.Fragment>
    );
  }
  
  export default MenuAuthenticatedUser;