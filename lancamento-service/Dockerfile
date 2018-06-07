FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Stone.FluxoCaixa.WebApi/ Stone.FluxoCaixa.WebApi/
COPY src/Stone.FluxoCaixa.Application/ Stone.FluxoCaixa.Application/
COPY src/Stone.FluxoCaixa.Domain/ Stone.FluxoCaixa.Domain/
COPY src/Stone.FluxoCaixa.Persistence/ Stone.FluxoCaixa.Persistence/
RUN dotnet restore
COPY . .
WORKDIR /src/src/Stone.FluxoCaixa.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stone.FluxoCaixa.WebApi.dll"]