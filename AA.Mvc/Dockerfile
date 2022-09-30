#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["AA.Mvc/AA.Mvc.csproj", "AA.Mvc/"]
#RUN dotnet restore "AA.Mvc/AA.Mvc.csproj"
#COPY . .
#WORKDIR "/src/AA.Mvc"
#RUN dotnet build "AA.Mvc.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "AA.Mvc.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AA.Mvc.dll"]




# 父镜像
#FROM microsoft/aspnetcore:5.0
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS baseWORKDIR /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
## 设置工作目录
#WORKDIR /app
#
## 复制发布文件到/app下
#COPY . /app
#
## 设置端口
#EXPOSE 5005
EXPOSE 80
COPY . .
# 使用AA.Mvc.dll来运行ASP.NET Core项目，注意大小写
#ENTRYPOINT ["dotnet", "AA.Mvc.dll", "--server.urls", "http://*:5005"]
ENTRYPOINT ["dotnet", "AA.Mvc.dll"]
