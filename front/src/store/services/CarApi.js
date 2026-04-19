import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const carApi = createApi({
    reducerPath: 'car',
    baseQuery: fetchBaseQuery({baseUrl: import.meta.env.VITE_CARS_URL}),
    tagTypes: ['cars'],
    endpoints: (build) => ({
        getCars: build.query({
            query: () => ({
                url: 'car',
                method: 'GET'
            }),
            providesTags: ['cars']
        }),
        createCar: build.mutation({
            query: (car) => ({
                url: 'car',
                method: 'POST',
                body: car
            }),
            invalidatesTags: ['cars']
        })
    })
})

export const { useGetCarsQuery, useCreateCarMutation } = carApi;