# Build stage
FROM node:21-bullseye-slim as build-stage
WORKDIR /app

COPY frontend/package*.json ./
RUN npm install
COPY frontend/ ./
COPY /frontend.local.env ./.env
RUN npm run build
CMD ["npm", "run", "start:prod"]
