import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const DOGS_API_KEY = "cbfb51a2-84b6-4025-a3e2-ed8616edf311";

interface IHome {
  id: string;
  name: string;
  alias: string;
}

interface IAway {
  id: string;
  name: string;
  alias: string;
}

interface Game {
  id: string;
  scheduled: string;
  home: IHome;
  away: IAway;
}

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
      fetchDailyGames: builder.query<Game[], string | void>({
        query(name = "") {
          return `/Games?name=${name}&year=2022&month=11&day=23`;
        },
      }),
    };
  },
});

export const { useFetchDailyGamesQuery } = apiSlice;
