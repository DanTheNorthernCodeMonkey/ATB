#!/bin/bash
#PosgreSQL setup

# Get the container
docker pull postgres;

echo Pulled latest posgreSQL docker image

# Run the container
docker run --name pg-docker-new -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v /var/lib/postgresql/data_1 postgres;

echo Booted the docker postgreSQL container

docker ps 

echo Waiting 3 seconds for the container to spin up fully.

sleep 3

# Create the DB
docker exec -u postgres pg-docker-new psql postgres -c 'CREATE DATABASE atb;';

echo Created the DB

# Add table and add password - has to be seperate, can't change DB within sql file in postgreSQL - e.g. use atb;
docker cp "$(dirname $(realpath $0))/atb.sql" pg-docker-new:/docker-entrypoint-initdb.d/atb.sql;
docker exec -u postgres pg-docker-new psql atb postgres -f docker-entrypoint-initdb.d/atb.sql;

echo Successfully added table and changed user password.

echo Success, you can now close the shell.
$SHELL