import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Header from "./components/Header/Header";
import Login from "./components/Login/Login";
import Logout from "./components/Logout/Logout";
import About from "./components/About/About";
import CreateQR from "./components/CreateQR/CreateQR";
import Home from './components/Home/Home';
import Statistics from "./components/Statistics/Statistics";
import React, {useState, useContext, useEffect } from 'react';

const themes = {
  light: {
    dark_blue: "#006BA6",
    light_blue: "#0496FF",
    accent_yellow: "#FFBC42",
    dark_pink: "#D81159",
    rose_budget: "#8F2D56"
  }
};

const ThemeContext = React.createContext(themes.light);

export default function App() {

  const [loggedIn, setLoggedIn] = useState(false);

  useEffect(() => {
    localStorage.getItem('user') ? setLoggedIn(true) : setLoggedIn(false);
  }, []);

  const onLoginSuccesful = (token) => { 
    localStorage.setItem('user', token);
    window.location.href = '/'; 
    setLoggedIn(true); 
  }

  const onLogoutSuccesful = () => { 
    localStorage.removeItem('user');
    window.location.href = '/login'; 
    setLoggedIn(false);
  }

  return (
    <ThemeContext.Provider value={themes.light}>
      <div>
        <Header loggedIn = {loggedIn}/>
        <Router>
          <div>
            <Switch>
              <Route exact path="/login"
                  render = {(props) => (<Login {...props} onLoginSuccesful = {onLoginSuccesful}/>)}/>
              <Route exact path="/logout"
                  render = {(props) => (<Logout {...props} onLogoutSuccesful = {onLogoutSuccesful}/>)}/>
              <Route exact path="/about">
                <About />
              </Route>
              <Route exact path="/create-qr">
                < CreateQR/>
              </Route>
              <Route exact path="/statistics">
                <Statistics />
              </Route>
              <Route path="/">
                <Home />
              </Route>
            </Switch>
          </div>
        </Router>
      </div>
    </ThemeContext.Provider>
  );
}

export { ThemeContext };