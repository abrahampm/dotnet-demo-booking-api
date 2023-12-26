FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "./demo-booking-api.PresentationLayer/demo-booking-api.PresentationLayer.csproj" --disable-parallel
COPY ./ ./

RUN dotnet tool install --global dotnet-ef --version 6.0.8

WORKDIR /src/demo-booking-api.DataAccessLayer
RUN rm -r ./Migrations
RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations

WORKDIR /src
RUN dotnet build "./demo-booking-api.PresentationLayer/demo-booking-api.PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./demo-booking-api.PresentationLayer/demo-booking-api.PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:80
ENTRYPOINT ["dotnet", "demo-booking-api.PresentationLayer.dll"]