import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { getDailyGames } from "../api/GamesController";
import { IGame } from "../types/types";

interface dailyGamesState {
  games: IGame[];
  status: string;
}

// export const todoAdded = () => {
//   return {
//     type: "dailygames/todoAdded",
//     payload: todo,
//   };
// };

const initialState: dailyGamesState = {
  games: [],
  status: "",
};

const dailyGamesSlice = createSlice({
  name: "dailyGames",
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
    addGames(state, actions: PayloadAction<IGame[]>) {
      state.games = actions.payload;
      // console.log("here are the games" + state.games);
    },
  },
});

// export const { incremented, amountAdded } = counterSlice.actions;
export const { addGames } = dailyGamesSlice.actions;

export default dailyGamesSlice.reducer;
