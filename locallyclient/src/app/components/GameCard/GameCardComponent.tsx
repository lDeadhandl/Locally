import { FC } from "react";
import dailyGamesSlice from "../../features/dailyGamesSlice";
import { useAppSelector } from "../../hooks";
import { IGame } from "../../types/types";
import "./GameCardComponent.css";
import CavsLogo from "../../assets/CavsLogo.png";
import HeatLogo from "../../assets/HeatLogo.png";

export interface IGameCardProps {
  game: IGame;
}

export const GameCard: FC<IGameCardProps> = (game) => {
  const g = useAppSelector((state) => state.counter.games);
  const homeTeam = game.game.home.name.split(" ").pop();
  const awayTeam = game.game.away.name.split(" ").pop();

  // based off of game status create "upcoming", "live", or "empty" card
  // TODO: clean up card when score is triple digits
  console.log();

  return (
    <div className="dashboard-card">
      <div className="card-content-top">
        <div className="card-score">{game.game.home.points}</div>
        <img
          className="card-logo"
          src={`/src/app/assets/NBALogos/${homeTeam}.png`}
        />
        <div className="card-record">
          {homeTeam}
          <br></br> ({game.game.home.wins}-{game.game.home.losses})
        </div>
      </div>
      <div className="card-content-bottom">
        <div className="card-score">{game.game.away.points}</div>
        <img
          className="card-logo"
          src={`/src/app/assets/NBALogos/${awayTeam}.png`}
        />
        <div className="card-record">
          {awayTeam}
          <br></br> ({game.game.away.wins}-{game.game.away.losses})
        </div>
      </div>
      <span className="card-time">
        Q{game.game.quarter} {game.game.clock}
      </span>
    </div>
  );
};

export default GameCard;

{
  /* <div className="dashboard-card">
<div className="card-content-left">
  <div className="card-scores">{game.game.home.name}</div>
  <div className="card-scores">{game.game.away.name}</div>
</div>
<div className="card-content-right"></div>
</div> */
}
