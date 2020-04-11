trap "exit" INT TERM ERR
trap "kill 0" EXIT

cd "$(dirname $(realpath $0))/";

dotnet build;
dotnet restore;
dotnet run --project ./ATB/ATB.csproj &

wait