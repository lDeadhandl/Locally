import { Fragment, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { getDailyGames } from "../../api/GamesController";
import { fetchDailyGames } from "../../features/dailyGamesSlice";
import { useFetchDailyGamesQuery } from "../../features/games/GamesApiSlice";
import { IGame } from "../../types/types";
import GameCard from "../GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState<IGame[]>([]);
  const [isClicked, setIsClicked] = useState(false);
  const dispatch = useDispatch();

  var currentDate = new Date();

  var const x = dispatch(fetchDailyGames("stef"));
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
