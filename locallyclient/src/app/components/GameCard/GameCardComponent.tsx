import { FC } from "react";
import { IGame } from "../../types/types";
import "./GameCardComponent.css";

export interface IGameCardProps {
  game: IGame;
}

export const GameCard: FC<IGameCardProps> = (game) => {
  return (
    <div className="dashboard-card">
      <div className="card-content-left">
        <div className="card-scores">{game.game.home.name}</div>
        <div className="card-scores">{game.game.away.name}</div>
      </div>
      <div className="card-content-right"></div>
    </div>
  );
};

export default GameCard;
