const apiBaseUrl = import.meta.env.VITE_BACK_URL;

export const env = {
    apiBaseUrl,
    carsUrl: apiBaseUrl + import.meta.env.VITE_CARS_URL,
    manufactUrl: apiBaseUrl + import.meta.env.VITE_MANUFACT_URL,
    carsImgUrl: apiBaseUrl + import.meta.env.VITE_CARS_IMG_URL,
}