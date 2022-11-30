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
  const teamName = "CavsLogo";
  console.log(g);
  return (
    <div className="dashboard-card">
      <div className="card-content-top">
        <div className="card-score">54</div>
        <img className="card-logo" src={`/src/app/assets/${teamName}.png`} />
        <div className="card-record">
          Cavaliers<br></br> (11-13)
        </div>
      </div>
      <div className="card-content-bottom">
        <div className="card-score">66</div>
        <img className="card-logo" src={HeatLogo} />
        <div className="card-record">
          Atlanta Hawks<br></br> (11-13)
        </div>
      </div>
      <span className="card-time">Q4 3:11</span>
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
