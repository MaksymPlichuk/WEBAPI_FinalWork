const initState = {
    cars: [],
    isLoaded: false
}

export const carReducer = (state = initState, action) => {
    switch (action.type) {
        case "loadCars": {
            return { ...state, isLoaded: true, cars: action.payload }
        }
        case "createCar": {
            return { ...state, cars: [...state.cars, action.payload] }
        }
        case "deleteCar": {
            return { ...state, cars: state.cars.filter(c=>c.id != action.payload) }
        }
        case "updateCar": {
            return { ...state, cars: action.payload }
        }
        default:
            return state;
    }
}