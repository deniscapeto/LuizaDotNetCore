FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 4150
#EXPOSE 44341
#EXPOSE 8009

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Luizalabs.EmployeeManager.API.DotNetCore.csproj", ""]
RUN dotnet restore "./Luizalabs.EmployeeManager.API.DotNetCore.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Luizalabs.EmployeeManager.API.DotNetCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Luizalabs.EmployeeManager.API.DotNetCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Luizalabs.EmployeeManager.API.DotNetCore.dll"]