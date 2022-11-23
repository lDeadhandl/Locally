import React from "react";
import { useAppDispatch, useAppSelector } from "./app/hooks";
import { amountAdded, incremented } from "./features/SomeFeatureSlice";
import reactLogo from "./assets/react.svg";
import { useFetchDailyGamesQuery } from "./features/games/GamesApiSlice";

import "./App.css";
import Dashboard from "./app/components/Dashboard/Dashboard";

function App() {
  return (
    <div className="App">
      <div className="App-header">
        <span className="App-header-logo">Logo.</span>
        <span className="App-header-burger">+</span>
      </div>
      <div></div>
      <Dashboard></Dashboard>
    </div>
  );
}

export default App;
