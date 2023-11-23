FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AdventOfCode2023/AdventOfCode2023.csproj", "AdventOfCode2023/"]
RUN dotnet restore "AdventOfCode2023/AdventOfCode2023.csproj"
COPY . .
WORKDIR "/src/AdventOfCode2023"
RUN dotnet build "AdventOfCode2023.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventOfCode2023.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventOfCode2023.dll"]
