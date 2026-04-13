import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import FormLabel from "@mui/material/FormLabel";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Stack from "@mui/material/Stack";
import MuiCard from "@mui/material/Card";
import { styled } from "@mui/material/styles";
import { useFormik } from "formik";
import { object, string, number, date } from "yup";
import { useNavigate } from "react-router";
import axios from "axios";
import { useDispatch } from "react-redux";



const Card = styled(MuiCard)(({ theme }) => ({
    display: "flex",
    flexDirection: "column",
    alignSelf: "center",
    width: "100%",
    padding: theme.spacing(4),
    gap: theme.spacing(2),
    margin: "0px auto",
    [theme.breakpoints.up("sm")]: {
        maxWidth: "450px",
    },
    boxShadow:
        "hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px",
    ...theme.applyStyles("dark", {
        boxShadow:
            "hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px",
    }),
}));

const SignInContainer = styled(Stack)(({ theme }) => ({
    minHeight: "calc((1 - var(--template-frame-height, 0)) * 100dvh)",
    padding: theme.spacing(2),
    [theme.breakpoints.up("sm")]: {
        padding: theme.spacing(4),
    },
    "&::before": {
        content: '""',
        display: "block",
        position: "absolute",
        zIndex: -1,
        inset: 0,
        backgroundImage:
            "radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))",
        backgroundRepeat: "no-repeat",
        ...theme.applyStyles("dark", {
            backgroundImage:
                "radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))",
        }),
    },
}));

const initValues = {
    name: "",
    year: 0,
    volume: 0,
    price: 0,
    color: "",
    desciption: "",
    image: "",
    manufactureId: 0
};

const baseURL = import.meta.env.VITE_CARS_URL;

const CarCreateForm = () => {

    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleSubmit = async (newCar) => {
        console.log(newCar);

        try {
            const resp = await axios.post(baseURL, newCar);
            if (resp.status == 200) {
                console.log("car Added");
                dispatch({ type: "createCar", payload: newCar })
                navigate("/cars")
            }
        } catch (error) {
            console.warn(error);
        }
        console.log(curYear);
    }

    const getError = (prop) => {
        return formik.touched[prop] && formik.errors[prop] ? (
            <Typography sx={{ mx: 1, color: "red" }} variant="h7">
                {formik.errors[prop]}
            </Typography>
        ) : null;
    };

    let curYear = new Date().getFullYear();
    const validationScheme = object({
        name: string().required("Обов'язкове поле").max(100, "Максимальна довжина 100 символів"),
        year: number().required("Обов'язкове поле").min(0, "Рік не може бути < 0").max(curYear, `Рік має бути не більше за ${curYear}`),
        volume: number().required("Обов'язкове поле").min(0, "Об'єм не може бути < 0"),
        price: number().required("Обов'язкове поле").min(0, "Ціна не може бути < 0"),
        color: string().required("Обов'язкове поле").max(100, "Максимальна довжина 100 символів"),
        desciption: string().required("Обов'язкове поле")
    });

    const formik = useFormik({
        initialValues: initValues,
        onSubmit: handleSubmit,
        validationSchema: validationScheme
    });

    return (
        <Box>
            <SignInContainer direction="column" justifyContent="space-between">
                <Card variant="outlined">
                    <Typography
                        component="h1"
                        variant="h4"
                        sx={{
                            width: "100%",
                            fontSize: "clamp(2rem, 10vw, 2.15rem)",
                        }}
                    >
                        Додавання Автомобіля
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={formik.handleSubmit}
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            width: "100%",
                            gap: 2,
                        }}
                    >
                        <FormControl>
                            <FormLabel htmlFor="name">Ім'я</FormLabel>
                            <TextField
                                name="name"
                                placeholder="Ім'я авто"
                                autoComplete="name"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.name}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("name")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="year">Рік випуску</FormLabel>
                            <TextField
                                name="year"
                                placeholder="1991"
                                autoComplete="year"
                                fullWidth
                                type="number"
                                variant="outlined"
                                value={formik.values.year}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("year")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="volume">Об'єм</FormLabel>
                            <TextField
                                name="volume"
                                placeholder="2,5"
                                autoComplete="volume"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.volume}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("volume")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="price">Ціна</FormLabel>
                            <TextField
                                name="price"
                                placeholder="15000"
                                autoComplete="price"
                                fullWidth
                                type="number"
                                variant="outlined"
                                value={formik.values.price}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("price")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="color">Колір</FormLabel>
                            <TextField
                                name="color"
                                placeholder="Black"
                                autoComplete="color"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.color}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("color")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="desciption">Опис</FormLabel>
                            <TextField
                                name="desciption"
                                placeholder="Опис..."
                                autoComplete="desciption"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.desciption}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("desciption")}
                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="manufactureId">ID Виробника</FormLabel>
                            <TextField
                                name="manufactureId"
                                placeholder="1"
                                autoComplete="manufactureId"
                                fullWidth
                                type="number"
                                inputProps={{
                                    min: 1,
                                    max: 20
                                }}

                                variant="outlined"
                                value={formik.values.manufactureId}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />

                        </FormControl>
                        <FormControl>
                            <FormLabel htmlFor="image">Фото</FormLabel>
                            <TextField
                                name="image"
                                placeholder="Посилання на фото"
                                autoComplete="image"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.image}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                        </FormControl>

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                        >
                            Додати
                        </Button>
                    </Box>
                </Card>
            </SignInContainer>
        </Box>
    );
}
export default CarCreateForm;