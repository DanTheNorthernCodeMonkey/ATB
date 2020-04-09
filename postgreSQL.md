This worked on a mac but haven't tried it on a windows machine yet.

Windows machine would need to do it in a bash shell (git bash) or have the ubuntu subsystem installed.

### Install

#### Get the container
docker pull postgres

#### Make a directory for the store
mkdir -p $HOME/docker/volumes/docker_postgres

#### Run the container
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5433:5433 -v $HOME/docker/volumes/docker_postgres:/var/lib/postgresql/data  postgres

#### Connect the containers bash shell
docker exec -it pg-docker bash

#### Connect to the postreSQL Server
psql -h localhost -U postgres -d postgres

#### Create the DB
CREATE DATABASE atb;

#### Connect to the DB
\c atb

#### Create the table
CREATE TABLE Beans (
    id UUID PRIMARY KEY,
    image_id UUID NOT NULL,
    cost NUMERIC (5,2) NOT NULL,
    bean_name TEXT NOT NULL,
    aroma TEXT NOT NULL,
    colour TEXT NOT NULL,
    date TIMESTAMP NOT NULL
);

#### Check the table create
\d

#### Quit the postrSQL server
\q
