# ====== Production ====== #
FROM mcr.microsoft.com/dotnet/runtime:10.0-noble-chiseled-arm64v8 AS final
WORKDIR /app

# ====== Build image ====== #
FROM mcr.microsoft.com/dotnet/sdk:10.0-aot AS publish
ARG RELEASE_VERSION
WORKDIR /sln

COPY ./*.sln ./

# Copy the main source project files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copy the test project files
#COPY test/*/*.csproj ./
#RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done

RUN dotnet restore

#COPY ./test ./test
COPY ./src ./src
RUN dotnet build -c Release --no-restore -o /app/build -p:VersionPrefix=$RELEASE_VERSION

RUN dotnet publish "./src/Omnic/Omnic.csproj" -c Release -p:VersionPrefix=$RELEASE_VERSION -o /app/publish

# ====== Copy to final ====== #
FROM publish AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Omnic.dll"]