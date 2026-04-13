import { Box } from "@mui/material";
import notFoundImg from "../../assets/images/404notFoundUpscale.png"

const ErrorPage = () => {
    return (
        <Box sx={{ textAlign: "center" }}>
            <h1 style={{ fontFamily: "sans-serif" }}>WRONG PAGE 404</h1>
            <Box component="img" maxWidth={400} maxHeight={400} src={notFoundImg} />
        </Box>
    );
}
export default ErrorPage;