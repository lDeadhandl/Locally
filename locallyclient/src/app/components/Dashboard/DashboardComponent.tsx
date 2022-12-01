import { is } from "immer/dist/internal";
import { Fragment, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { addGames } from "../../features/dailyGamesSlice";
import { useFetchDailyGamesQuery } from "../../features/games/GamesApiSlice";
import { useAppDispatch } from "../../hooks";
import { IGame } from "../../types/types";
import GameCard from "../GameCard/GameCardComponent";

const Dashboard = () => {
  const [games, setGames] = useState<IGame[]>([]);
  const [isClicked, setIsClicked] = useState(false);
  const {
    data = [],
    isFetching,
    refetch,
  } = useFetchDailyGamesQuery("stef", {});

  const dispatch = useAppDispatch();

  console.log(data);
  // console.log(data);
  const handleClick = () => {
    refetch();
    dispatch(addGames(data));
    setGames(data);
  };
  useEffect(() => {
    setGames(data);
  }, [isClicked]);

  return (
    <div className="dashboard-section">
      {games.map((game) => (
        <GameCard key={game.id} game={game}></GameCard>
      ))}
      <button onClick={handleClick}></button>
    </div>
  );
};

export default Dashboard;
