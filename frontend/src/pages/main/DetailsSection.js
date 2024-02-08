import {Image, Text, Grid, GridItem} from "@chakra-ui/react";

const DetailsSection = () => {
    return (
        <Grid templateColumns="repeat(3, 1fr);" gap={8} fontSize={"md"} justifyContent="center" textAlign="center"
              p={10} bg={"white"} color={"black"}>
            <GridItem>
                <Image
                    mx={"auto"}
                    h="300px"
                    src="https://img.freepik.com/free-vector/home-gymnastics-abstract-concept-vector-illustration-stay-active-amid-quarantine-power-training-online-exercise-program-home-workout-social-distance-fitness-livestream-abstract-metaphor_335657-1713.jpg?size=626&ext=jpg&ga=GA1.1.782206126.1697618837&semt=sph"
                />
                <Text fontFamily={"Montserrat"} fontStyle={"italic"}>“I hated every minute of training, but I said,
                    ‘Don’t quit. Suffer now and live the rest of your life as a champion.” – Muhammad Ali</Text>
            </GridItem>

            <GridItem>
                <Image
                    mx={"auto"}
                    h="300px"
                    src="https://img.freepik.com/free-vector/happy-smiling-couple-running-summer-park_74855-6517.jpg?size=626&ext=jpg&ga=GA1.1.782206126.1697618837&semt=sph"
                />
                <Text fontFamily={"Montserrat"} fontStyle={"italic"}>“I’ve failed over and over again in my life and
                    that is why I succeed.” – Michael Jordan</Text>
            </GridItem>

            <GridItem>
                <Image
                    h="300px"
                    mx={"auto"}
                    src="https://img.freepik.com/free-vector/outdoor-workout-training-healthy-lifestyle-open-air-jogging-fitness-activity-male-athlete-running-park-muscular-sportsman-exercising-outdoors-vector-isolated-concept-metaphor-illustration_335657-1338.jpg?size=626&ext=jpg&ga=GA1.1.782206126.1697618837&semt=sph"
                />
                <Text fontFamily={"Montserrat"} fontStyle={"italic"}>“Most people fail, not because of lack of desire,
                    but, because of lack of commitment.” –Vince Lombardi</Text>
            </GridItem>
        </Grid>
    );
};

export default DetailsSection;