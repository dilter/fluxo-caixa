FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/Stone.Lancamento.WebApi/ Stone.Lancamento.WebApi/
COPY src/Stone.Lancamento.Application/ Stone.Lancamento.Application/
COPY src/Stone.Lancamento.Domain/ Stone.Lancamento.Domain/
COPY src/Stone.Lancamento.Persistence/ Stone.Lancamento.Persistence/
RUN dotnet restore
COPY . .
WORKDIR /src/src/Stone.Lancamento.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stone.Lancamento.WebApi.dll"]