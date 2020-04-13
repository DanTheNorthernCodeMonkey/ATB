trap "exit" INT TERM ERR
trap "kill 0" EXIT

cd ../

cd "$(dirname $(realpath $0))/";

dotnet run --project ./ATB/ATB.csproj &

wait