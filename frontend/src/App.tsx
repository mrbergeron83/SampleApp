import { useContext, useEffect, useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import { EventModel } from "./__generated__/SampleApi";
import { SampleApiContext } from "./SampleContextProvider";

function App() {
  const apiClient = useContext(SampleApiContext);
  const [events, setEvents] = useState<EventModel[]>();
  useEffect(() => {
    const fetchEvents = async () => {
      try {
        if (apiClient) {
          const response = await apiClient?.events.getEvents();
          setEvents(response || []);
        }
      } catch (error) {
        console.error("Failed to fetch events:", error);
      }
    };

    fetchEvents();
  }, [apiClient]);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        {events &&
          events.map((event, index) => {
            return <div key={index}>{JSON.stringify(event)}</div>;
          })}
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
