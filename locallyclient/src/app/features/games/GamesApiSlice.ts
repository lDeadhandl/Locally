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
    var currentDate = new Date();
    return {
      // query<returntype, arguements we're passing in for parameter generator>
      fetchDailyGames: builder.query<IGame[], string | void>({
        query(name = "") {
          return `/Games?name=${name}&year=${currentDate.getFullYear()}&month=${
            currentDate.getMonth() + 1
          }&day=${currentDate.getDate()}`;
        },
      }),
    };
  },
});

export const { useFetchDailyGamesQuery } = apiSlice;
