import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { IGame } from "../../types/types";

export const apiSlice = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://localhost:7242/api",
    // prepareHeaders(headers) {
    //   headers.set("x-api-key", DOGS_API_KEY);

    //   return headers;
    // },
  }),
  endpoints(builder) {
    return {
      // query<returntype, arguements we're passing in for parameter generator>
      fetchDailyGames: builder.query<IGame[], string | void>({
        query(name = "") {
          return `/Games?name=${name}&year=2022&month=11&day=27`;
        },
      }),
    };
  },
});

export const { useFetchDailyGamesQuery } = apiSlice;
