import { Box, lighten, Typography } from "@mui/material";
import { lightBlue, yellow } from "@mui/material/colors";

const MainPage = () => {
    return (
        <>
            <h3 style={{ mx: 5, textAlign: "center" }}>Welcome</h3>

            <Box sx={{display: "flex", mt: "10%", alignItems: "center"}}>
                <Typography variant="h6" sx={{ textAlign: "center" }}>
                    Carpedia - Бібліотека машин, які ви можете редагувати та переглядати без реєстрації
                </Typography>
                <Box component="img" src="https://i.pinimg.com/736x/42/67/ff/4267ff287dfbef103d07494ce932ec23.jpg" alt="cars"></Box>
            </Box>
        </>
    );
}
export default MainPage;