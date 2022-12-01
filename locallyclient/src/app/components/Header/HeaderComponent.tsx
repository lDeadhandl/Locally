import { useState } from "react";
import {
  useAddFavoriteTeamsMutation,
  useFetchTeamsQuery,
} from "../../features/games/GamesApiSlice";
import { ITeam } from "../../types/types";
import "./HeaderComponent.css";
import "./SearchInput.css";

const Header = () => {
  const [teams, setTeams] = useState<ITeam[]>([]);
  const [message, setMessage] = useState("");
  const [showSearch, setShowSearch] = useState(false);

  const { data = [], isFetching } = useFetchTeamsQuery();
  const [addTeams, status] = useAddFavoriteTeamsMutation();

  // TODO: fix -> when backspacing last letter it displays all the teams
  const handleChange = (event: any) => {
    var teamList = data.filter((x) =>
      x.name.toLowerCase().includes(event.target.value.toLowerCase())
    );
    event.target.value.length > 0 ? setShowSearch(true) : setShowSearch(false);
    setTeams(teamList);
  };

  const handleClick = (s: any) => {
    var vals = { name: "stef", team: s.target.innerHTML };
    addTeams(vals);
    console.log(s.target.innerHTML);
  };

  return (
    <div className="App-header">
      <div className="App-header-logo">Logo.</div>
      <div className="App-header-search-burger">
        <div className="search-wrapper">
          <div style={{ display: "flex" }}>
            <input
              type="search"
              id="search"
              placeholder="search for teams"
              onChange={handleChange}
            ></input>
          </div>
          <div
            style={{ display: showSearch ? "block" : "none" }}
            className="items-list"
          >
            <ul>
              {teams.map((team) => (
                <li className="items" onClick={(x) => handleClick(x)}>
                  {team.name}
                </li>
              ))}
            </ul>
          </div>
        </div>
        <div className="App-header-burger">+</div>
      </div>
    </div>
  );
};

export default Header;
