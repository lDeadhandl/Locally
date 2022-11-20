import React from "react";
import logo from "./logo.svg";
import "./App.css";
import GameCard from "./Components/GameCard/GameCardComponent";
import Dashboard from "./Pages/Dashboard/Dashboard";

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
