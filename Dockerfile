# 1. 빌드 스테이지
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# 프로젝트 파일 복사 및 복원
COPY ["SimpleIoT.Gateway.csproj", "./"]
RUN dotnet restore

# 전체 소스 복사 및 빌드
COPY . .
RUN dotnet publish "SimpleIoT.Gateway.csproj" -c Release -o /app/publish

# 2. 실행 스테이지 (가볍고 보안이 강화된 런타임 사용)
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS final
WORKDIR /app

# 빌드된 파일만 가져오기
COPY --from=build /app/publish .

# CSV 설정 파일들이 함께 복사되었는지 확인 (빌드 시 자동 포함 설정됨)
# 포트 개방 (소켓 서버용 8003)
EXPOSE 8003

# 실행 명령
ENTRYPOINT ["dotnet", "SimpleIoT.Gateway.dll"]