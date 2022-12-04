import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IGame } from "../types/types";

interface dailyGamesState {
  games: IGame[];
  status: string;
}

const initialState: dailyGamesState = {
  games: [],
  status: "",
};

const dailyGamesSlice = createSlice({
  name: "dailyGames",
  initialState,
  reducers: {
    // normally you have to return ...state to make a copy of it for an immutable update
    // redux toolkit uses library called immer to wrap state updates and tracks all mutations
    // that you try to do, saves you from doing all the old copying and stuff
    addGames(state, actions: PayloadAction<IGame[]>) {
      state.games = actions.payload;
    },
  },
});

export const { addGames } = dailyGamesSlice.actions;
export default dailyGamesSlice.reducer;
