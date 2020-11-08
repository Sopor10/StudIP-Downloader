FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish StudIPDownloader.WebApi/StudIPDownloader.WebApi.csproj -c Release -o releasebuild
RUN dotnet dev-certs https -ep ./certs
##
## Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS baseruntime
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS baseruntime

WORKDIR /app
RUN apk update
RUN apk add firefox-esr

FROM baseruntime
COPY --from=build-env /app/StudIPDownloader.WebApi/bin/Release/netcoreapp3.1/ .

RUN mkdir data
EXPOSE 5000
EXPOSE 5001
CMD ASPNETCORE_URLS="http://*:$PORT;https://*:$HTTPS_PORT" dotnet StudIPDownloader.WebApi.dll
#ENTRYPOINT ["dotnet", "StudIPDownloader.WebApi.dll"]
    