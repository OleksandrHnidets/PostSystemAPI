FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PostSystemAPI/PostSystemAPI.WebApi.csproj", "PostSystemAPI/"]
COPY ["PostSystemAPI.DAL/PostSystemAPI.DAL.csproj", "PostSystemAPI.DAL/"]
COPY ["PostSystemAPI.Common/PostSystemAPI.Common.csproj", "PostSystemAPI.Common/"]
COPY ["PostSystemAPI.Domain/PostSystemAPI.Domain.csproj", "PostSystemAPI.Domain/"]
RUN dotnet restore "PostSystemAPI/PostSystemAPI.WebApi.csproj"
COPY . .
WORKDIR "/src/PostSystemAPI"
RUN dotnet build "PostSystemAPI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostSystemAPI.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostSystemAPI.WebApi.dll"]
