FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
EXPOSE 5000
EXPOSE 5001
RUN dotnet tool install --global dotnet-ef --version 6.0.8
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet dev-certs https
CMD dotnet watch run --urls "http://0.0.0.0:5000;https://0.0.0.0:5001" --project "/app/demo-booking-api.PresentationLayer/demo-booking-api.PresentationLayer.csproj"