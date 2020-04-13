trap "exit" INT TERM ERR
trap "kill 0" EXIT

cd "$(dirname $(realpath $0))/";

cd ../

dotnet run --project ./ATB/ATB.csproj &

echo Running API on localhost:5000, this will take a few seconds;

$SHELL