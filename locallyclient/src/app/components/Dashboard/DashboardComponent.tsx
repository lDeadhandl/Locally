import { Fragment, useEffect, useState } from "react";
import { getDailyGames } from "../../api/GamesController";
import { useFetchDailyGamesQuery } from "../../features/games/GamesApiSlice";
import { IGame } from "../../types/types";
import GameCard from "../GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState<IGame[]>([]);
  const [isClicked, setIsClicked] = useState(false);

  var currentDate = new Date();

  useEffect(() => {
    const data = getDailyGames("stef");

    console.log(data);
  }, [isClicked]);

  return (
    <div className="dashboard-section">
      {games.map((game) => (
        <GameCard key={game.id} game={game}></GameCard>
      ))}
      <button onClick={() => setIsClicked(!isClicked)}></button>
    </div>
  );
};

export default Dashboard;
