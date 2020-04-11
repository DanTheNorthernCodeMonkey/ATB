#!/bin/bash

cd "$(dirname $(realpath $0))/ATB/BackOffice/client/atb_bo/";

yarn start &

cd "$(dirname $(realpath $0))/ATB/FrontOffice/client/atb_fo/";

yarn start &

echo The clients will run while this shell window remains open. They will terminate upon the shell being closed.

$SHELL