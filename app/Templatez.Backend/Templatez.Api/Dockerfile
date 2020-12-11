##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#ENV ASPNETCORE_URLS = https://+:443;http://+:80
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["Templatez.Api/Templatez.Api.csproj", "Templatez.Api/"]
#RUN dotnet restore "Templatez.Api/Templatez.Api.csproj"
#COPY . .
#WORKDIR "/src/Templatez.Api"
#RUN dotnet build "Templatez.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Templatez.Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Templatez.Api.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

EXPOSE 80
EXPOSE 443 

COPY . .

ENTRYPOINT ["dotnet", "Templatez.Api.dll"]