#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LoyaltyPrime.Web.Api/LoyaltyPrime.Web.Api.csproj", "LoyaltyPrime.Web.Api/"]
RUN dotnet restore "LoyaltyPrime.Web.Api/LoyaltyPrime.Web.Api.csproj"
COPY . .
WORKDIR "/src/LoyaltyPrime.Web.Api"
RUN dotnet build "LoyaltyPrime.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoyaltyPrime.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoyaltyPrime.Web.Api.dll"]