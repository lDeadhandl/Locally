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
