FROM centos:7 AS base

# Add Microsoft package repository and install ASP.NET Core
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm \
    && yum install -y aspnetcore-runtime-6.0

ENV DOTNET_URLS=http://+:5000
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as build

WORKDIR /source

# Copy everything
COPY ["practices.csproj", "."]

# Restore as distinct layers
WORKDIR /src
RUN dotnet restore "./practices.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "practices.csproj" -c Release -o /app/build
# Build and publish a release
FROM build AS publish
RUN dotnet publish "./practices.csproj" -c release -o /app --no-restore

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "practices.dll"]
