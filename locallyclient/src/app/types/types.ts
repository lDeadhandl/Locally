export interface IHome {
  id: string;
  name: string;
  alias: string;
  market?: string;
  points?: number;
  wins?: number;
  losses?: number;
}

export interface IAway {
  id: string;
  name: string;
  alias: string;
  market?: string;
  points?: number;
  wins?: number;
  losses?: number;
}

export interface IGame {
  id: string;
  scheduled: string;
  status: string;
  clock?: string;
  quarter?: number;
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
