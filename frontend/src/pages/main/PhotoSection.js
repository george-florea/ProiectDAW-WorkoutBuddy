
import {Box,Image, Heading, Text, Card, Button, useColorModeValue, Link } from "@chakra-ui/react";

const PhotoSection = () => {
    return (
        <Card position="relative">
            <Image
                width="100%"
                src="https://img.freepik.com/free-photo/incognito-man-building-biceps-with-barbell_7502-5120.jpg?size=626&ext=jpg&ga=GA1.1.782206126.1697618837&semt=sph"
            />
             <Card color={"lightPallette.background.main"} position="absolute" w="100%" height="100%" bg="rgba(0,0,0,0.5)">
            <Box width="75%" mx="auto" mt="100px" height="75%">
                <Text fontSize="22px" color="white">
                    #1 app for gym enthusiasts
                </Text>
                <Heading size="xl" lineHeight="50px" mt="20px" color="white">
                    Let's start your journey with <Text><Text display="inline" fontWeight="800" color={useColorModeValue("lightPallette.primary.600", "lightPallette.primary.300")}>Workout</Text>Buddy</Text>
                </Heading>
                <Text color={"white"}>
                    The world’s largest hardcore training site. It is specifically designed for more advanced fitness enthusiasts.
                </Text>
            </Box>
        </Card>
        </Card>
    );
};

export default PhotoSection;