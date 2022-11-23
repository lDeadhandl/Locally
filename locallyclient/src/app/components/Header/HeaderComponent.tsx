import "./HeaderComponent.css";
import "./SearchInput.css";

const Header = () => {
  const handleClick = () => {
    console.log("button clicked");
  };

  return (
    <div className="App-header">
      <span className="App-header-logo">Logo.</span>
      <span className="App-header-burger">
        <div className="search-box">
          <button className="btn-search" onClick={handleClick}>
            <i className="fas fa-search"></i>
          </button>
          <input
            type="text"
            className="input-search"
            placeholder="Type to Search..."
          ></input>
        </div>
      </span>
    </div>
  );
};

export default Header;
