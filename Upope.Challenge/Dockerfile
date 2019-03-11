FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Upope.Challenge/Upope.Challenge.csproj", "Upope.Challenge/"]
RUN dotnet restore "Upope.Challenge/Upope.Challenge.csproj"
COPY . .
WORKDIR "/src/Upope.Challenge"
RUN dotnet build "Upope.Challenge.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.Challenge.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.Challenge.dll"]
