import { useState } from "react";
import "./HeaderComponent.css";
import "./SearchInput.css";

const Header = () => {
  const [message, setMessage] = useState("");

  const handleChange = (event: any) => {
    setMessage(event.target.value);

    console.log("value is:", event.target.value);
  };
  const handleClick = () => {
    console.log(message);
  };

  return (
    <div className="App-header">
      <span className="App-header-logo">Logo.</span>
      <span className="App-header-burger"></span>
    </div>
  );
};

export default Header;
