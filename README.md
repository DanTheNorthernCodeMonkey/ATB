# ATB

## Required:
* .NET Core 3.1
* node, npm/yarn
* Docker or postgreSQL server

## Setup

### PosgreSQL setup

* Run the postgreSQL_setup.sh bash script.
* This will pull the posgreSQL docker image and set it up.
* If you encounter issues please see <placeholder>


* Compile the .Net Core Web projects
* npm or yarn install the clients
    * ATB.BackOffice\client\atb_bo
    * ATB.FrontOffice\client\atb_fo

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
