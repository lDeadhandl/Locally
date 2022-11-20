import { Fragment, useEffect, useState } from "react";
import GameCard from "../../Components/GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7242/api/Favorites/stefan")
      .then((response) => response.json())
      .then((user) => {
        setGames(user.games);
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
