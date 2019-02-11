FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Upope/Upope.csproj", "Upope/"]
RUN dotnet restore "Upope/Upope.csproj"
COPY . .
WORKDIR "/src/Upope"
RUN dotnet build "Upope.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.dll"]