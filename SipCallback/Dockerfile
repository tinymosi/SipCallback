FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#install LDAP dependencies for System.DirectoryServices 
RUN apk update \
    && apk add --upgrade libldap \
    && ln -s libldap.so.2 /usr/lib/libldap-2.4.so.2

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SipCallback/SipCallback.csproj", "SipCallback/"]
RUN dotnet restore "SipCallback/SipCallback.csproj"
COPY . .
WORKDIR "/src/SipCallback"
RUN dotnet build "SipCallback.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SipCallback.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SipCallback.dll"]
