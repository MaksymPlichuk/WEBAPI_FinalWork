import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import FavoriteIcon from '@mui/icons-material/Favorite';
import { Link } from 'react-router';
import EditIcon from "@mui/icons-material/Edit";
import { useState } from 'react';
import DeleteIcon from '@mui/icons-material/Delete';
import axios from 'axios';
import { Box } from '@mui/material'
import { useDispatch } from 'react-redux';

const CarCard = ({ car }) => {
    const baseURL = import.meta.env.VITE_CARS_URL;
    const dispatch = useDispatch();
    const [isFavorite, setIsFavorite] = useState(car.isFavorite);

    const removeCarHandle = async () => {
        try {
            await axios.delete(`${baseURL}/${car.id}`);
            dispatch({ type: "deleteCar", payload: car.id })
        } catch (error) {
            console.warn(error);
        }
    }
    const setFavoriteHandle = () => {
        const favoriteState = !isFavorite;
        setIsFavorite(favoriteState);
    }

    return (
        <Card sx={{ maxWidth: 345, height: "100%" }}>
            <CardHeader

                action={
                    <IconButton onClick={removeCarHandle} color='error' aria-label="settings">
                        <DeleteIcon />
                    </IconButton>
                }
                title={car.name}
            />
            <Link to={`description/${car.id}`}>
                <CardMedia
                    sx={{ objectFit: "contain" }}
                    component="img"
                    height="350"
                    image={
                        car.image ? car.image : "https://img.freepik.com/premium-vector/no-photo-available-vector-icon-default-image-symbol-picture-coming-soon-web-site-mobile-app_87543-18055.jpg"
                    }
                    alt={car.name}
                />
            </Link>
            <CardContent>
                <Typography variant="body2" sx={{ color: "text.secondary" }}>
                    {car.price}$
                    <span style={{ margin: 10 }}>{car.year}</span>
                </Typography>
                <Typography variant="body2" sx={{ color: "text.secondary" }}>
                    <span >Volume: {car.volume}L</span>
                    <span style={{ margin: 10 }}>{car.manufacture?.name}</span>
                </Typography>
            </CardContent>
            <CardActions disableSpacing>
                <IconButton
                    onClick={setFavoriteHandle} aria-label="add to favorites" color={isFavorite ? "error" : ""}
                >
                    <FavoriteIcon />
                </IconButton>
                <Link to={`update/${car.id}`}>
                    <IconButton aria-label="share" color='success'>
                        <EditIcon />
                    </IconButton>
                </Link>
            </CardActions>
        </Card >
    );
}
export default CarCard;