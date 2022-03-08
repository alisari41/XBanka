FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app

COPY ./Core/*.csproj ./Core/
COPY ./Entities/*.csproj ./Entities/
COPY ./DataAccess/*.csproj ./DataAccess/
COPY ./Business/*.csproj ./Business/
COPY ./WebAPI/*.csproj ./WebAPI/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./WebAPI/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","WebAPI.dll" ]