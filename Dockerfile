FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

RUN apt-get update && apt-get install make

COPY ./src/omdb-search ./omdb-search
WORKDIR /omdb-search
RUN make

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

RUN apt-get update && apt-get install -y

WORKDIR /omdb-search
COPY --from=build-env /omdb-search/dist ./

ENTRYPOINT ["dotnet", "omdb-search.dll"]