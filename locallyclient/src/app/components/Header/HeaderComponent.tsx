import { useState } from "react";
import {
  useAddFavoriteTeamsQuery,
  useFetchTeamsQuery,
} from "../../features/games/GamesApiSlice";
import { ITeam } from "../../types/types";
import "./HeaderComponent.css";
import "./SearchInput.css";

const Header = () => {
  const [teams, setTeams] = useState<ITeam[]>([]);
  const [message, setMessage] = useState("");

  const { data = [], isFetching } = useFetchTeamsQuery();

  // TODO: fix -> when backspacing last letter it displays all the teams
  const handleChange = (event: any) => {
    var teamList = data.filter((x) =>
      x.name.toLowerCase().includes(event.target.value.toLowerCase())
    );
    setTeams(teamList);
  };

  const handleClick = (s: any) => {
    const {} = useAddFavoriteTeamsQuery("stef", s.target.innerHTML);

    console.log(s.target.innerHTML);
  };

  return (
    <div className="App-header">
      <span className="App-header-logo">Logo.</span>
      <span className="App-header-burger"></span>
      <div className="search-wrapper">
        <input
          type="search"
          id="search"
          placeholder="search for teams"
          onChange={handleChange}
        ></input>
        {teams.map((team) => (
          <li className="items" onClick={(x) => handleClick(x)}>
            {team.name}
          </li>
        ))}
      </div>
    </div>
  );
};

export default Header;
