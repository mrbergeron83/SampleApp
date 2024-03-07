import "./App.css";
import { Box, AppBar, Toolbar, Typography } from "@mui/material";
import { EventList } from "./components/EventList";

function App() {
  return (
    <div className="App">
      <Box sx={{ flexGrow: 1 }}>
        <AppBar position="static">
          <Toolbar>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              Sample App
            </Typography>
          </Toolbar>
        </AppBar>
      </Box>
      <EventList></EventList>
    </div>
  );
}

export default App;
