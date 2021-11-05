import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Header from "./components/Header/Header";
import Login from "./components/Login/Login";
import About from "./components/About/About";
import CreateQR from "./components/CreateQR/CreateQR";
import React, {useContext} from 'react';

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
  return (
    <ThemeContext.Provider value={themes.light}>
      <div>
        <Header />
        <Router>
          <div>
            <Switch>
              <Route path="/login">
                <Login />
              </Route>
              <Route path="/about">
                <About />
              </Route>
              <Route path="/create-qr">
                < CreateQR/>
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

function Home() {
  const theme = useContext(ThemeContext);
  return <h2 style={{color: theme.accent_yellow}}>Home</h2>;
}