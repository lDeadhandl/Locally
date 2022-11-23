import React from "react";
import "./App.css";
import Dashboard from "./app/components/Dashboard/Dashboard";

function App() {
  return (
    <div className="App">
      <div className="App-header">
        <span className="App-header-logo">Logo.</span>
        <span className="App-header-burger">+</span>
      </div>
      <Dashboard></Dashboard>
    </div>
  );
}

export default App;
