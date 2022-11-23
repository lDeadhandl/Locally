// config store is a wrapper around basic store,
// automatically takes your store and sets it up with the right
// defaults ex. auto turns on redux dev tools extensions, thunk middleware,
// dev checks like accidental mutations
import { configureStore } from "@reduxjs/toolkit";
import { apiSlice } from "./features/games/GamesApiSlice";
import counterReducer from "./features/SomeFeatureSlice";

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(apiSlice.middleware);
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
