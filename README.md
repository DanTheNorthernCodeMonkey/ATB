# ATB

## Required:
* .NET Core 3.1
* node, npm/yarn
* Docker or postgreSQL server
* Bash (if you have git you will have bash)

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
    * Alternatively run yarn/npm start in each clients directory within a shell.

## TODO / further iterations:

* Mass upload - CSV upload
* Integration test suite
* More unit tests
* Authentication here is implied, I haven't implemented it as it would be noise for the actual test.
