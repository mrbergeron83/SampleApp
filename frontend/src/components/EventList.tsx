import Paper from "@mui/material/Paper";
import { useContext, useState, useEffect } from "react";
import { SampleApiContext } from "../SampleContextProvider";
import { EventModel } from "../__generated__/SampleApi";
import {
  List,
  ListItem,
  ListItemText,
  Container,
  Pagination,
  Button,
  Modal,
  Box,
} from "@mui/material";
import { CreateEvent } from "./CreateEvent";
import { DateTime } from "luxon";

export const EventList = (): JSX.Element => {
  const eventsPerPage = 3;
  const apiClient = useContext(SampleApiContext);
  const [events, setEvents] = useState<EventModel[]>();
  const [currentPage, setCurrentPage] = useState(1);
  const [createModalOpen, setCreateModalOpen] = useState(false);
  const [refreshTrigger, setRefreshTrigger] = useState(false);

  const pageCount = events ? Math.ceil(events.length / eventsPerPage) : 0;
  const indexOfLastEvent = currentPage * eventsPerPage;
  const indexOfFirstEvent = indexOfLastEvent - eventsPerPage;
  const currentEvents =
    events?.slice(indexOfFirstEvent, indexOfLastEvent) || [];

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        if (apiClient) {
          // could use paging in the getEvents to improve performance. Working with small data set for now...
          const response = await apiClient?.events.getEvents();
          setEvents(response || []);
        }
      } catch (error) {
        console.error("Failed to fetch events:", error);
      }
    };

    fetchEvents();
  }, [apiClient, refreshTrigger]);

  const handleOnCreated = () => {
    setCreateModalOpen(false);
    setRefreshTrigger(!refreshTrigger);
  };

  return (
    <Container
      component={Paper}
      maxWidth={false}
      sx={{
        display: "flex",
        flexDirection: "column",
        width: "90vw",
        height: "80vh",
        justifyContent: "center",
        alignItems: "center",
        marginTop: "5%",
        marginBottom: "5%",
      }}
    >
      <List
        sx={{
          width: "50vw",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        {currentEvents.map((evt, index) => {
          return (
            <ListItem
              key={index}
              sx={{ width: "75%", maxWidth: "100%" }}
              component={Paper}
            >
              <ListItemText primary={evt.name} secondary={evt.description} />
              <ListItemText
                primary={`Starts: ${DateTime.fromSeconds(evt.dateFromUnixSeconds).toLocaleString(DateTime.DATETIME_SHORT)}`}
                secondary={`Ends: ${DateTime.fromSeconds(evt.dateToUnixSeconds).toLocaleString(DateTime.DATETIME_SHORT)}`}
              />
            </ListItem>
          );
        })}
      </List>
      <Pagination
        count={pageCount}
        page={currentPage}
        onChange={(_, v) => handlePageChange(v)}
        sx={{ marginTop: "20px" }}
      />
      <Button variant="contained" onClick={() => setCreateModalOpen(true)}>
        Create
      </Button>
      <Modal open={createModalOpen} onClose={() => setCreateModalOpen(false)}>
        <Box
          sx={{
            position: "absolute" as "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            width: "50vw",
            height: "50vh",
            bgcolor: "background.paper",
            border: "2px solid #000",
            boxShadow: 24,
            p: 4,
            overflow: "scroll",
          }}
        >
          <CreateEvent onCreated={handleOnCreated} />
        </Box>
      </Modal>
    </Container>
  );
};
