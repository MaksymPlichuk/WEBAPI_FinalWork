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
import { useNavigate, useParams } from "react-router";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import UploadImage from "../../components/uploadImage/UploadImage";

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
    id: 0,
    name: "",
    year: 0,
    volume: 0,
    price: 0,
    description: "",
    manufacturerId: 0,
};


const CarUpdateForm = () => {

    const [image, setImage] = useState(null);
    let curYear = new Date().getFullYear();
    const validationScheme = object({
        name: string().required("Обов'язкове поле").max(100, "Максимальна довжина 100 символів"),
        year: number().required("Обов'язкове поле").min(0, "Рік не може бути < 0").max(curYear, `Рік має бути не більше за ${curYear}`),
        volume: number().required("Обов'язкове поле").min(0, "Об'єм не може бути < 0"),
        price: number().required("Обов'язкове поле").min(0, "Ціна не може бути < 0"),
        description: string().required("Обов'язкове поле")
    });
    const getError = (prop) => {
        return formik.touched[prop] && formik.errors[prop] ? (
            <Typography sx={{ mx: 1, color: "red" }} variant="h7">
                {formik.errors[prop]}
            </Typography>
        ) : null;
    };

    function setFormValues(data) {
        formik.setValues({
            id: data.id,
            name: data.name || "",
            year: data.year || 0,
            volume: data.volume || 0,
            price: data.price || 0,
            description: data.description || "",
            manufacturerId: data.manufacturerId || 1
        });
    }

    const baseURL = import.meta.env.VITE_CARS_URL;
    const navigate = useNavigate();
    const { id } = useParams();

    const dispatch = useDispatch();
    const { cars } = useSelector(state => state.car)


    const handleSubmit = async () => {
        try {
            console.log(formik.values);

            const formData = new FormData();
            formData.append("id", formik.values.id);
            formData.append("name", formik.values.name);
            formData.append("year", parseInt(formik.values.year));
            formData.append("volume", parseFloat(String(formik.values.volume).replace(',', '.')));
            formData.append("price", parseInt(formik.values.price));
            formData.append("description", formik.values.description);
            formData.append("manufacturerId", parseInt(formik.values.manufacturerId));

            if (image) {
                formData.append("image", image);
            }

            const resp = await axios.put(baseURL, formData);
            const { status } = resp
            console.log(resp);

            const newCars = cars.filter((a) => a.id != id);
            newCars[id] = formik.values;

            dispatch({ type: "updateCar", payload: newCars })

            if (status == 200) {
                navigate("/cars")
            }
        } catch (error) {
            console.log(error);

        }

    };

    const formik = useFormik({
        initialValues: initValues,
        onSubmit: handleSubmit,
        validationSchema: validationScheme
    });

    useEffect(() => {
        async function fetchCars() {
            const resp = await axios.get(`${baseURL}/by-id?id=${id}`)
            const { data, status } = resp;

            if (status == 200) {
                //console.warn(data);
                setFormValues(data.payload)
            } else {
                navigate("/cars", { replace: true })
            }
        }
        fetchCars();
    }, [])

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
                        Редагування Автомобіля
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
                            <FormLabel htmlFor="description">Опис</FormLabel>
                            <TextField
                                name="description"
                                placeholder="Опис..."
                                autoComplete="description"
                                fullWidth
                                type="text"
                                variant="outlined"
                                value={formik.values.description}
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                            />
                            {getError("description")}
                        </FormControl>


                        <UploadImage label="Фото автора" onChange={(i) => setImage(i)}
                            buttonText="Обрати файл зображення" />

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                        >
                            Редагувати
                        </Button>
                    </Box>
                </Card>
            </SignInContainer>
        </Box>
    );
}
export default CarUpdateForm;