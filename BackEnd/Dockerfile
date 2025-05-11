FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

FROM base AS build
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c docker -o out

# Build runtime image
FROM base
WORKDIR /App
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "BeansAPI.dll"]