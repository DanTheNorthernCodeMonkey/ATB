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
* Run ATB.BackOffice 
* Run ATB.FrontOffice
    * Both from Visual Studio or via the command line
* npm or yarn install the clients
    * ATB.BackOffice\client\atb_bo
    * ATB.FrontOffice\client\atb_fo
* npn or yarn start both clients

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
