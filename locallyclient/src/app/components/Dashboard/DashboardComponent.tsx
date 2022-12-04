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

  // TODO: filter scheduled and inprogress games then create inprogress jsx then scheduled conditionally
  // Pass each game in as props, the gamecard can check the status and decide to make an upcoming or live game card or empty
  // Find a way to have 4 blank game cards that get replaced when there is data
  // REMOVE BUTTON, data should load when dashboard is opened, ADD SPINNER while isFetching == true
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
