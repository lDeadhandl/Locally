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

export interface ITeam {
  id: string;
  name: string;
  market: string;
  wins: number;
  losses: number;
}
