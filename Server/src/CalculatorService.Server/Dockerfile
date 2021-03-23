#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/src/CalculatorService.Server/CalculatorService.Server.csproj", "Server/src/CalculatorService.Server/"]
COPY ["Server/src/CalculatorService.Server.Application/CalculatorService.Server.Application.csproj", "Server/src/CalculatorService.Server.Application/"]
RUN dotnet restore "Server/src/CalculatorService.Server/CalculatorService.Server.csproj"
COPY . .
WORKDIR "/src/Server/src/CalculatorService.Server"
RUN dotnet build "CalculatorService.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculatorService.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculatorService.Server.dll"]