# Sample App
A Sample app to show basic workings

## Setup
To run completely in docker, simple go to the root folder of the folder and run `docker-compose up -d`
You need docker to be installed

To run and debug the backend, open SampeApp.sln in visual studio, build the solution and press F5 to start the backend.
Navigate to 'https://localhost:7240/swagger/index.html' for the api information

To run and debug the front end, mount to the frontend folder and `npm install` and then 'npm run start'
Navigate to 'http://localhost:3000/' to browse the frontend

## Information
### Backend
The backend is in .net8 and uses EFCore8
dotenv.net is used to manage environment variables
Tests are integration tests(e2e) with the app in memory and the data base is a new file created every time it runs. After running it is deleted.

### Frontend
The frontend is using React with typescript
Luxon is used to manage dates
swagger-typescript-api is used to strongy type the dto between the frontend and backend. The type generation is automated, you just need to run the backend and run `npm run codegen`
Tests are todo


### Pipeline
A pipeline is setup to run the backend tests on each `merge-request`
The rules are setup as to block a merge to `main` from a `merge-request` if not all the tests pass
The backend rule is only applied if there are changes to the /backend folder
Todo do the same thing for frontend tests
Todo add a `build` with a code quality check (lint for frontend, set the rules in .editorconfig in backend and build with `WarningLevel=0` to break the build if code quality is not applied correctly) step to build the frontend and backend and pass the builds as artifacts to the subsequent appropriate pipelines
