FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Upope.Game/Upope.Game.csproj", "Upope.Game/"]
RUN dotnet restore "Upope.Game/Upope.Game.csproj"
COPY . .
WORKDIR "/src/Upope.Game"
RUN dotnet build "Upope.Game.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.Game.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.Game.dll"]