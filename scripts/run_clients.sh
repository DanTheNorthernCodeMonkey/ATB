#!/bin/bash

cd "$(dirname $(realpath $0))/";
echo Changing directory to solution root

cd ../
pwd

cd "ATB/BackOffice/client/atb_bo/";
pwd

yarn start &

cd "../../../../ATB/FrontOffice/client/atb_fo/";
pwd

yarn start &

echo The clients will run while this shell window remains open. They will terminate upon the shell being closed.

$SHELL