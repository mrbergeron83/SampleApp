import "./App.css";
import { Box, AppBar, Toolbar, Typography } from "@mui/material";
import { EventList } from "./components/EventList";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterLuxon } from "@mui/x-date-pickers/AdapterLuxon";

function App() {
  return (
    <div className="App">
      <LocalizationProvider dateAdapter={AdapterLuxon} adapterLocale="en">
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
      </LocalizationProvider>
    </div>
  );
}

export default App;
