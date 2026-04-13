import { createTheme } from "@mui/material";

export const lightTheme = createTheme({
    palette: {
        mode: "light",
        primary: {
            light: "#B2DFDB",
            main: "#009688",
            dark: "#00796B",
        },
        secondary: {
            main: "#00BCD4"
        }
    }
})