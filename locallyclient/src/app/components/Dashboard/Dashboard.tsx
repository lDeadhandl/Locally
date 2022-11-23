import { Fragment, useEffect, useState } from "react";
import { useFetchDailyGamesQuery } from "../../../features/games/GamesApiSlice";
import GameCard from "../GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState([]);

  var currentDate = new Date();

  const { data = [], isFetching } = useFetchDailyGamesQuery("stef");
  console.log(data);

  return (
    <div className="dashboard-section">
      {data.map((game) => (
        <GameCard game={game}></GameCard>
      ))}
    </div>
  );
};

export default Dashboard;
