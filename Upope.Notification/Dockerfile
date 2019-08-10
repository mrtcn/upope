FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Upope.Notification/Upope.Notification.csproj", "Upope.Notification/"]
RUN dotnet restore "Upope.Notification/Upope.Notification.csproj"
COPY . .
WORKDIR "/src/Upope.Notification"
RUN dotnet build "Upope.Notification.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Upope.Notification.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Upope.Notification.dll"]