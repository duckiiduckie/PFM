#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BudgetAPI/BudgetAPI.csproj", "BudgetAPI/"]
COPY ["Message/Message.csproj", "Message/"]
RUN dotnet restore "./BudgetAPI/BudgetAPI.csproj"
COPY . .
WORKDIR "/src/BudgetAPI"
RUN dotnet build "BudgetAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BudgetAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BudgetAPI.dll"]