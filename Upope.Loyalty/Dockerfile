FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Upope.Loyalty/Upope.Loyalty.csproj", "Upope.Loyalty/"]
RUN dotnet restore "Upope.Loyalty/Upope.Loyalty.csproj"
COPY . .
WORKDIR "/src/Upope.Loyalty"
RUN dotnet build "Upope.Loyalty.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.Loyalty.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.Loyalty.dll"]