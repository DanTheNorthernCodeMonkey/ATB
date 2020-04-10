# ATB

## Required:
* .NET Core 3.1
* node, npm/yarn
* PostgreSQL server

## Setup

* Download and install postgres or run a docker image:
   * https://hub.docker.com/_/postgres
* See postgreSQL.mds
* Compile the .Net Core Web projects
* npm or yarn install the clients
    * ATB.BackOffice\client\atb_bo
    * ATB.FrontOffice\client\atb_fo

### PosgreSQL setup

#### Get the container
docker pull postgres

#### Run the container
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -p 5432:5432 postgres -d

#### Connect the containers bash shell
docker exec -it pg-docker bash

#### Connect to the postreSQL Server
psql -h localhost -U postgres -d postgres

#### Create the DB
CREATE DATABASE atb;

#### Connect to the DB
\c atb;

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

#### Give password so user can connect from outside container
ALTER USER postgres WITH PASSWORD 'postgres';

#### Quit the postrSQL server
\q

#### Exit docker shell
exit

## Known issues
* Calendar does not handle timezones
    * Hack to add hours onto the date - brittle.
* Base64Encoding of the image in JS. 
    * None-standard & browser differences
    * Time constraints, coupled with issues with cors and media types contributed to this.


## TODO / further iterations:

* Front/Back End Validation
* Mass upload - CSV upload
* Show dates that are aleady taken
    * Call onComponentMount
    * Return collection of dates taken
    * Update calendar showing dates taken
    * Validate against chosen dates
* Integration test suite
* More unit tests
