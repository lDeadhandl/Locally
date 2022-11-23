import { FC } from "react";
import "./GameCardComponent.css";

export interface IHome {
  id: string;
  name: string;
  alias: string;
}

export interface IAway {
  id: string;
  name: string;
  alias: string;
}

export interface IGame {
  id: string;
  scheduled: string;
  home: IHome;
  away: IAway;
}

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
