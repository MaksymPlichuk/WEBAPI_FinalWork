import { Container } from "@mui/material";
import Navbar from "../navbar/Navbar";
import { Outlet } from "react-router";
import Footer from "../footer/Footer";


const DefaultLayout = () => {
    return (
        <>
            <Navbar />
            <Container sx={{ minHeight: "100vh" }}>
                <Outlet></Outlet>
            </Container>
            <Footer />
        </>
    );
}
export default DefaultLayout;