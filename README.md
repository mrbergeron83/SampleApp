# Sample App
A Sample app to show basic workings

## Setup
To run completely in docker, simple go to the root folder of the folder and run `docker-compose up -d`
You need docker to be installed

To run and debug the backend, open SampeApp.sln in visual studio, build the solution and press F5 to start the backend.
Navigate to 'https://localhost:7240/swagger/index.html' for the api information

To run and debug the front end, mount to the frontend folder and `npm install` and then 'npm run start'
Navigate to 'http://localhost:3000/' to browse the frontend
