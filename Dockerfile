FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "./alten-test.PresentationLayer/alten-test.PresentationLayer.csproj" --disable-parallel
COPY ./ ./

RUN dotnet tool install --global dotnet-ef 

WORKDIR /src/alten-test.DataAccessLayer
RUN rm -r ./Migrations
RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations

WORKDIR /src
RUN dotnet build "./alten-test.PresentationLayer/alten-test.PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./alten-test.PresentationLayer/alten-test.PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet alten-test.PresentationLayer.dll