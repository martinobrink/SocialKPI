#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk

FROM sdk AS debug
RUN wget -O getvsdbg.sh "https://aka.ms/getvsdbgsh" \
    && sh ./getvsdbg.sh -v vs2019 -l /remote_debugger \
    && rm getvsdbg.sh
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM sdk AS build
WORKDIR /src
COPY ["SocialKpiApi/SocialKpiApi.csproj", "SocialKpiApi/"]
RUN dotnet restore "SocialKpiApi/SocialKpiApi.csproj"
COPY . .
WORKDIR "/src/SocialKpiApi"
RUN dotnet build "SocialKpiApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialKpiApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialKpiApi.dll"]
