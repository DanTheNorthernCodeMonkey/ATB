# ATB

## Required:
* .NET Core 3.1
* node, npm/yarn
* Docker or postgreSQL server

## Setup

### PosgreSQL setup

* Double click or run in a bash terimal postgreSQL_setup.sh bash script in /scripts
* This will pull the posgreSQL docker image and set it up.
* If you encounter issues with the docker container starting then please change the volume to another area

### Build
* Double click or run in a bash terimal build.sh bash script in /scripts

### Run
* Double click or run in a bash terimal run_api.sh bash script in /scripts
    * Alternatively run from your IDE
* Double click or run in a bash terimal run_clients.sh bash script in /scripts

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
