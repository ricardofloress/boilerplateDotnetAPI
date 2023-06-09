FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app

COPY .baseProject/*.csproj ./
RUN dotnet restore

COPY .baseProject/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0 
WORKDIR /app
EXPOSE 7228
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "baseProject.dll" ]
