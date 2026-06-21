FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskManagerAPI.csproj", "./"]
RUN dotnet restore "TaskManagerAPI.csproj"

COPY . .
RUN dotnet build "TaskManagerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManagerAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskManagerAPI.dll"]