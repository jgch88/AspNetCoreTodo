FROM microsoft/dotnet:2.1-sdk AS build
COPY AspNetCoreTodo/*.csproj ./app/AspNetCoreTodo/
WORKDIR /app/AspNetCoreTodo
RUN dotnet restore

# because we are in "workdir"
COPY AspNetCoreTodo/. ./
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/AspNetCoreTodo/out ./
ENTRYPOINT ["dotnet", "AspNetCoreTodo.dll"]
