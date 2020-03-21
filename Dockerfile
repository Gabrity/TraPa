FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /source

RUN ls -lat /source
COPY **/* ./
RUN ls -lat
RUN dotnet restore

#COPY . ./
#RUN dotnet publish -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#COPY --from=build-env /app/out .
#ENTRYPOINT [ "dotnet", "WebUI.Console.dll" ]

#  docker build -t gabrity/trapa .