# syntax = docker/dockerfile:1

ARG NODE_VERSION=20.18.0

FROM node:${NODE_VERSION}-slim AS base

# Build stage
FROM base AS build

WORKDIR /FrontEnd

# copy both 'package.json' and 'package-lock.json' (if available)
COPY package*.json ./

# install project dependencies
RUN npm install

COPY . .

RUN npm run build

# Run stage
FROM base

# Set Environment Variables
# Default port for personal webhosting, can be overridden with --build-arg PORT=<port>
ARG PORT=8484
ENV PORT=$PORT
ENV NODE_ENV=production

# Install web server
RUN npm install -g serve

# Copy the build artifacts from the build stage
COPY --from=build /FrontEnd/dist /dist

CMD [ "serve", "-s", "dist" ]