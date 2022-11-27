import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getDailyGames } from "../api/GamesController";
import { IGame } from "../types/types";

interface dailyGamesState {
  games: IGame[];
  status: string;
}

const initialState: dailyGamesState = {
  games: [],
  status: "",
};

export const fetchDailyGames = createAsyncThunk(
  "games/fetchDailyGames",
  async (name: string) => {
    const response = await getDailyGames(name);
    console.log(response);
    return response.games;
  }
);

const counterSlice = createSlice({
  name: "counter",
  initialState,
  reducers: {
    // increment
    // normally you have to return ...state to make a copy of it for an immutable update
    // redux toolkit uses library called immer to wrap state updates and tracks all mutations
    // that you try to do, saves you from doing all the old copying and stuff
    // incremented(state) {
    //   state.value++;
    // },
    // amountAdded(state, actions: PayloadAction<number>) {
    //   state.value += actions.payload;
    // },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchDailyGames.pending, (state, action) => {
        state.status = "loading";
      })
      .addCase(fetchDailyGames.fulfilled, (state, action) => {
        console.log("hi");
      });
  },
});

// export const { incremented, amountAdded } = counterSlice.actions;
export const {} = counterSlice.actions;

export default counterSlice.reducer;
