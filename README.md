# 🚀 SimpleIoT.Gateway
> **TCP 소켓 기반의 센서 데이터를 수집하여 REST API로 중계하는 경량형 산업용 게이트웨이 엔진**

![CI/CD Pipeline](https://github.com/gkdl7370/SimpleIoT.Gateway/actions/workflows/main.yml/badge.svg)
![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=flat&logo=docker&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=.net&logoColor=white)

## 💡 프로젝트 동기 (Project Motivation)
본 프로젝트는 기존 WinForms 기반의 레거시 데이터 수신기를 현대적인 마이크로서비스 아키텍처로 전환하기 위해 시작되었습니다.
- **레거시 탈피**: UI 의존성이 높은 WinForms 구조에서 비즈니스 로직을 완벽히 분리.
- **플랫폼 독립성**: Docker 컨테이너화를 통해 Windows 환경을 넘어 Linux 서버 어디서든 동작 가능하게 설계.
- **자동화 도입**: GitHub Actions를 활용하여 코드 수정 시 빌드 및 배포 안정성을 실시간으로 검증.

## 🛠 기술 스택 (Tech Stack)
- **Framework**: .NET 7.0 (Console Application)
- **Communication**: Asynchronous TCP Socket (Async/Await)
- **Data Format**: Binary (Custom Protocol) → JSON (REST API)
- **Container**: Docker (Multi-stage Build)
- **CI/CD**: GitHub Actions

## 🏗 시스템 아키텍처 (System Architecture)


1. **Ingest**: 산업용 센서로부터 TCP/IP를 통해 실시간 바이너리 데이터 수집.
2. **Process**: `DataParser` 유틸리티를 통한 바이너리 분석 및 설정 파일(CSV) 기반의 장비 매핑.
3. **Forward**: 수집된 데이터를 RESTful API 표준에 맞춰 JSON으로 변환 후 클라우드 서버 전송.

## 🧠 핵심 해결 과제 (Key Learning Points)
### 0. JAVA서버의 부하를 줄이기 위한 별도의 미들웨어 셋팅

### 1. 관심사 분리 (Separation of Concerns)
기존 `MainForm.cs` 하나에 집중되어 있던 네트워크 통신, 데이터 분석, 설정 로드 로직을 `Core`, `Utils`, `Models` 폴더 구조로 계층화하여 유지보수성을 극대화했습니다.

### 2. Null 안정성 및 방어적 코딩
.NET의 **Nullable Reference Types** 기능을 활용하여 런타임 중에 발생할 수 있는 `NullReferenceException`을 컴파일 시점에 차단, 경고 0개의 클린 코드를 달성했습니다.

### 3. Docker 멀티 스테이지 빌드
빌드용 이미지(SDK)와 실행용 이미지(Runtime)를 분리하여 컨테이너 용량을 최소화하고 보안을 강화했습니다.

## 🚀 시작하기 (Getting Started)

### Docker 환경에서 실행
```bash
# 이미지 빌드
docker build -t simple-iot-gateway .

# 컨테이너 실행 (호스트 8003 포트를 게이트웨이 8081 포트에 연결)
docker run -d -p 8003:8081 --name my-gateway simple-iot-gateway
