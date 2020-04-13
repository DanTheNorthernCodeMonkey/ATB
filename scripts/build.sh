#!/bin/bash

cd "$(dirname $(realpath $0))/";

echo Changing directory to solution root

cd ../
pwd

echo Restoring dependencies on the API
dotnet restore;

echo Compiling the API
dotnet build;

echo Changing directory back office client
cd "ATB/BackOffice/client/atb_bo/";
pwd
echo Installing dependencies for back office client

yarn install &&

echo Changing directory front office client
pwd
cd "../../../../ATB/FrontOffice/client/atb_fo/";

echo Installing dependencies for front office client

yarn install &&

$SHELL