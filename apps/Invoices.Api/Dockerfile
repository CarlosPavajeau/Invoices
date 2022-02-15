FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Invoices.Api/Invoices.Api.csproj", "Invoices.Api/"]
RUN dotnet restore "apps/Invoices.Api/Invoices.Api.csproj"
COPY . .
WORKDIR "/src/Invoices.Api"
RUN dotnet build "Invoices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Invoices.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoices.Api.dll"]
