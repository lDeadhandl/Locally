import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface CounterState {
  value: number;
}

const initialState: CounterState = {
  value: 0,
};

const counterSlice = createSlice({
  name: "counter",
  initialState,
  reducers: {
    // increment
    // normally you have to return ...state to make a copy of it for an immutable update
    // redux toolkit uses library called immer to wrap state updates and tracks all mutations
    // that you try to do, saves you from doing all the old copying and stuff
    incremented(state) {
      state.value++;
    },
    amountAdded(state, actions: PayloadAction<number>) {
      state.value += actions.payload;
    },
  },
});

export const { incremented, amountAdded } = counterSlice.actions;
export default counterSlice.reducer;
