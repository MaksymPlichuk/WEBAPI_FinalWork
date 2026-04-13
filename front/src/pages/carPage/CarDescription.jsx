import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { Box, Stack, Typography, Rating } from "@mui/material";
import axios from "axios";

const CarDescription = () => {

    const navigate = useNavigate();
    const { id } = useParams();
    const [car, setCar] = useState(null);

    const baseURL = import.meta.env.VITE_CARS_URL;
    async function fetchCar() {

        const resp = await axios.get(`${baseURL}/${id}`)
        const { data, status } = resp;
        if (status == 200) {
            const formated = {
                name: data.data.name || "",
                year: data.data.year || 0,
                volume: data.data.volume || 0,
                price: data.data.price || 0,
                color: data.data.color || "",
                desciption: data.data.desciption || "",
                image: data.data.image || "",
                manufactureName: data.data.manufacture?.name || 1
            }
            setCar(formated);
        }
        else { alert(`${status} Error`) }
    }


    useEffect(() => { fetchCar() })

    if (!car) {
        return (
            navigate("/cars")
        );
    }

    return (
        <Box sx={{ display: "flex", mt: "10%", alignItems: "center" }}>
            <Box sx={{ flexGrow: .5 }}>
                <img style={{ maxHeight: 650, maxWidth: 650 }} src={car.image} alt={car.name} />
            </Box>

            <Stack sx={{ flexDirection: 'column', alignSelf: 'center', gap: 4, maxWidth: 450 }} >
                <Stack direction="row" sx={{ gap: 2 }}>
                    <div>
                        <Typography variant="h2" gutterBottom sx={{ fontWeight: 'medium' }}>
                            {car.name}
                        </Typography>
                        <Typography variant="h4" sx={{ color: 'text.secondary' }}>
                            {car.manufactureName}
                        </Typography>
                        <Typography variant="h5" sx={{ color: 'text.secondary', mt: "20%" }}> Year: {car.year} </Typography>
                        <Typography variant="h5" sx={{ color: 'text.secondary' }}> Volume: {car.volume} </Typography>
                        <Typography variant="h5" sx={{ color: 'text.secondary' }}> Price: $ {car.price} </Typography>
                        <Typography variant="h5" sx={{ color: 'text.secondary' }}> Color: {car.color} </Typography>
                        <Typography variant="h7" sx={{ color: 'text.secondary', mt: "10%" }}> {car.desciption} </Typography>

                    </div>
                </Stack>
            </Stack>
        </Box>
    );
}
export default CarDescription;