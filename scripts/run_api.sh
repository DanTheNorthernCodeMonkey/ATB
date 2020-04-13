trap "exit" INT TERM ERR
trap "kill 0" EXIT

cd "$(dirname $(realpath $0))/";

cd ../

dotnet run --project ./ATB/ATB.csproj &

$SHELL