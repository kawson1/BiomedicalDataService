FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY ./bin/Debug/net6.0 ./

RUN apt-get update && apt-get install -y curl

ENV ASPNETCORE_URLS=http://+:5189

ENTRYPOINT [ "dotnet", "DataGenerator.dll" ]
