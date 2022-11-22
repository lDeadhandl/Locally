import { Fragment, useEffect, useState } from "react";
import GameCard from "../../Components/GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState([]);

  var currentDate = new Date();

  useEffect(() => {
    fetch(
      `https://localhost:7242/api/Games?name=strah&year=2022&month=11&day=21`
    )
      .then((response) => response.json())
      .then((data) => {
        setGames(data);
      });
  }, []);

  return (
    <div className="dashboard-section">
      {games.map((game) => (
        <GameCard game={game}></GameCard>
      ))}
    </div>
  );
};

export default Dashboard;
