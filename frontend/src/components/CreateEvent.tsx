import { useContext, useState } from "react";
import { Box, Button, TextField } from "@mui/material";
import { DateTimePicker } from "@mui/x-date-pickers";
import { DateTime } from "luxon";
import { SampleApiContext } from "../SampleContextProvider";
import { EventModel } from "../__generated__/SampleApi";

interface CreateEventProps {
  onCreated: () => void;
}

export const CreateEvent = ({ onCreated }: CreateEventProps): JSX.Element => {
  const now = DateTime.local();
  const apiClient = useContext(SampleApiContext);

  const [name, setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [dateFrom, setDateFrom] = useState<DateTime>(now);
  const [dateTo, setDateTo] = useState<DateTime>(now);

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    const model: EventModel = {
      name: name,
      description: description,
      dateFromUnixSeconds: dateFrom.toUnixInteger(),
      dateToUnixSeconds: dateTo.toUnixInteger(),
    };
    var res = await apiClient?.events.createEvent(model);
    console.log(res);
    if (res) {
      onCreated();
    }
  };

  return (
    <Box component="form" autoComplete="off" onSubmit={handleSubmit}>
      <div>
        <TextField
          required
          id="name"
          label="name"
          placeholder="Name..."
          value={name ?? ""}
          onChange={(e) => setName(e.target.value)}
          sx={{
            marginBottom: "20px",
          }}
        />
        <TextField
          required
          id="description"
          label="description"
          placeholder="Description..."
          multiline={true}
          rows={6}
          value={description ?? ""}
          onChange={(e) => setDescription(e.target.value)}
          sx={{
            width: "50vw",
            marginBottom: "20px",
          }}
        />
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            marginBottom: "20px",
          }}
        >
          <DateTimePicker
            label="From"
            value={dateFrom}
            timezone={now.zoneName}
            onChange={(e) => setDateFrom(e as DateTime)}
          />
          <DateTimePicker
            label="To"
            value={dateTo}
            timezone={now.zoneName}
            onChange={(e) => setDateTo(e as DateTime)}
          />
        </Box>
      </div>
      <Button
        type="submit"
        variant="contained"
        sx={{
          marginBottom: "20px",
        }}
      >
        Submit
      </Button>
    </Box>
  );
};
