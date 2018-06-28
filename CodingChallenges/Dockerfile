#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 5115
#EXPOSE 44342
#
#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /src
#COPY CodingChallenges/CodingChallenges.csproj CodingChallenges/
#COPY CodingChallenges.DataAcess/CodingChallenges.DataAcess.csproj CodingChallenges.DataAcess/
#COPY CodingChallenges.Domains/CodingChallenges.Domains.csproj CodingChallenges.Domains/
#COPY CodingChallenges.Services/CodingChallenges.Services.csproj CodingChallenges.Services/
#COPY CoddingChallenges.Common/CoddingChallenges.Common.csproj CoddingChallenges.Common/
#RUN dotnet restore CodingChallenges/CodingChallenges.csproj
#COPY . .
#WORKDIR /src/CodingChallenges
#RUN dotnet build CodingChallenges.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish CodingChallenges.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "CodingChallenges.dll"]



# Define environment variable
#ENV NAME World

# build runtime image
#FROM tells Docker that we want to base our image on the existing image (microsoft/aspnetcore:1.1), that already contains all the dependencies for running
FROM microsoft/aspnetcore:2.1
#ARG is getting from this "docker build --build-arg var=xxx" --> ARG is "xxx"
ARG source
#WORKDIR set working directory inside the container to /app, you can access to this foder using this command "docker exec -t -i 802af9400d82 /bin/bash"
WORKDIR /app
# Make port 80 available to the world outside this container
EXPOSE 80
#when you build this, COPY the path you find the in $source variable in to the current directory in the image/container (.). If $source is empty or absent, just use the default path obj/Docker/publish"
#or COPY --from=build-env /app/out .
COPY ${source:-obj/Docker/publish} .
#ENTRYPOIN to specifiy where to start building
ENTRYPOINT ["dotnet", "CodingChallenges.dll"]