import { Box, Grid, IconButton, Typography, CircularProgress, Button } from "@mui/material";
import CarCard from "./CarCard";
import AddCircleIcon from "@mui/icons-material/AddCircle";
import { useState, useEffect } from "react";
import { Link } from "react-router";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";

import InputLabel from '@mui/material/InputLabel';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import TextField from '@mui/material/TextField';
import MenuItem from "@mui/material/MenuItem";
import AccordionSummary from '@mui/material/AccordionSummary';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';


const CarListPage = () => {

    const { cars, isLoaded } = useSelector(state => state.car);
    const dispatch = useDispatch();
    const baseURL = import.meta.env.VITE_CARS_URL;

    const [filters, setFilters] = useState({
        year: "",
        color: "",
        manufacture: "",
        volume: "",
        minValue: "",
        maxValue: ""
    })

    useEffect(() => {
        if (!isLoaded) {
            fetchCars();
        }
    }, [])

    async function fetchCars() {
        const response = await axios.get(baseURL, {
            params: {
                page_size: 20,
                page: 5
            }
        });  //?page_size=20&page=5

        const { data, status } = response;
        if (status == 200) {
            console.log(data);

            const newCars = []
            for (const car of data.data.items) {
                newCars.push(car)
            }
            dispatch({ type: "loadCars", payload: newCars });
            console.log(newCars);
        }
    }
    if (!isLoaded) {
        return (
            <Box sx={{ display: "flex", justifyContent: "center", mt: 5, flexDirection: "column", alignItems: "center" }}>
                <Typography variant="h5">Loading Data...</Typography>
                <CircularProgress enableTrackSlot size="3rem" sx={{ mt: 5 }} />
            </Box>
        );
    }

    async function onSubmitFiltersHandle(event) {
        event.preventDefault();
        console.log(filters);

        let urlFilter = {}

        const responseHandle = async (response) => {
            //console.log(urlFilter);
            
            const { data, status } = response;
            if (status == 200) {
                const newCars = []
                for (const car of data.data.items) {
                    newCars.push(car)
                }
                dispatch({ type: "loadCars", payload: newCars });
                console.log(newCars);
            }
        }


        if (filters.year) { urlFilter.property = "year"; urlFilter.value = filters.year; }
        if (filters.color) { urlFilter.property = "color"; urlFilter.value = filters.color; }
        if (filters.price) { urlFilter.property = "price"; urlFilter.value = filters.price; }
        if (filters.manufacture) { urlFilter.property = "manufacture"; urlFilter.value = filters.manufacture; }
        if (filters.volume) { urlFilter.property = "volume"; urlFilter.value = filters.volume; }

        if (filters.minValue || filters.maxValue) {
            const response = await axios.get(`${baseURL}/by-price`, {
                params: {
                    minValue: filters.minValue || 0,
                    maxValue: filters.maxValue || 0
                }
            });

            //console.log(response);
            await responseHandle(response);
        }
        else {
            const response = await axios.get(baseURL, { params: urlFilter });
            await responseHandle(response);
        }

    }

    function onChangeHandle(event) {
        const { name, value } = event.target;
        setFilters({ ...filters, [name]: value });
    }

    return (
        <Box
            sx={{ display: "flex", alignItems: "center", flexDirection: "column", }}>

            <Box sx={{ flexGrow: .5, mx: 5, mt: 5 }}>
                <Accordion>
                    <AccordionSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1-content"
                        id="panel1-header"
                    >
                        <Typography component="span">Фільтри</Typography>
                    </AccordionSummary>
                    <AccordionDetails >
                        <Box component="form" onSubmit={onSubmitFiltersHandle}>
                            <TextField sx={{ mt: 1 }} label="Year" name="year" value={filters.year} onChange={onChangeHandle} fullWidth type="number" />
                            <TextField sx={{ mt: 1 }} label="Color" name="color" value={filters.color} onChange={onChangeHandle} fullWidth />
                            <TextField sx={{ mt: 1 }} label="manufacture" name="manufacture" value={filters.manufacture} onChange={onChangeHandle} fullWidth />
                            <TextField sx={{ mt: 1 }} label="volume" name="volume" value={filters.volume} onChange={onChangeHandle} fullWidth type="number" />
                            <Box sx={{ w: 100 }}>
                                <TextField label="minValue" name="minValue" value={filters.minValue} onChange={onChangeHandle} sx={{ w: 25, mt: 1 }} type="number" />
                                <TextField label="maxValue" name="maxValue" value={filters.maxValue} onChange={onChangeHandle} sx={{ w: 25, mt: 1, mx: 2 }} type="number" />
                                <Button sx={{ mt: 1 }} variant="contained" type="submit" fullWidth>Use</Button>
                            </Box>
                        </Box>
                    </AccordionDetails>
                </Accordion>
            </Box>


            <Grid container spacing={2} mx="100px" my="50px">
                {cars.map((c, index) => (
                    <Grid size={3} key={index}>
                        <CarCard car={c} />
                    </Grid>
                ))}
                <Grid size={cars.length % 4 == 0 ? 12 : 3} >
                    <Box sx={{ width: "100%", justifyContent: "center", height: "100%", display: "flex", flexDirection: "column", alignItems: "center" }}>
                        <Link to="create">
                            <IconButton color="secondary">
                                <AddCircleIcon sx={{ fontSize: "3em" }} />
                            </IconButton>
                        </Link>
                    </Box>
                </Grid>
            </Grid>
        </Box>
    );
}
export default CarListPage;