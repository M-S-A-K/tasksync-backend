FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Project file copy karke dependencies restore karna
COPY ["TaskManagerAPI.csproj", "./"]
RUN dotnet restore "TaskManagerAPI.csproj"

# Baaki saara code copy karna aur build karna
COPY . .
RUN dotnet build "TaskManagerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManagerAPI.csproj" -c Release -o /app/publish

# Final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render ke port mapping ke liye url configuration
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskManagerAPI.dll"]