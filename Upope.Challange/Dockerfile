FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Upope.Challange/Upope.Challange.csproj", "Upope.Challange/"]
RUN dotnet restore "Upope.Challange/Upope.Challange.csproj"
COPY . .
WORKDIR "/src/Upope.Challange"
RUN dotnet build "Upope.Challange.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.Challange.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.Challange.dll"]