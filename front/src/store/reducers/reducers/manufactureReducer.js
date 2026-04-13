const initState = {
    manufacture: [],
    isLoaded: false
}

export const manufactureReducer = (state = initState, action) => {
    switch (action.type) {
        case "loadMan": {
            return { ...state, isLoaded: true, manufacture: action.payload }
        }
        default:
            return state;
    }
}