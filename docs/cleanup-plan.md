# 정리 노트

`SimpleIoT.Gateway`는 기존 C# 기반 게이트웨이를 Linux/Docker 환경에서도
돌릴 수 있는 구조로 바꿔보는 현대화 실험입니다.

코드를 다시 보니, 소스 코드와 함께 NuGet 패키지 산출물까지 저장소에 들어가
있었습니다. 실제로 계속 관리할 저장소라면 소스, 테스트, 빌드 설정만 남기는 편이
좋다고 판단했습니다. 이번에는 테스트와 CI를 먼저 붙이고, 산출물 정리는 별도
커밋으로 분리하는 방향으로 정리했습니다.

## 이번에 정리한 부분

- 대상 프레임워크를 `.NET 8` LTS로 변경했습니다.
- 파서 동작을 검증하는 단위 테스트를 추가했습니다.
- CI에서 빌드뿐 아니라 테스트도 실행하도록 바꿨습니다.
- 앞으로 `packages/` 폴더가 다시 올라가지 않도록 `.gitignore`에 추가했습니다.

## 다음에 따로 정리할 부분

현재 저장소에는 이미 Git이 추적 중인 NuGet 패키지 산출물이 `packages/`
폴더 아래에 남아 있습니다. 이 파일들은 삭제되는 양이 많아서 테스트 추가와
섞기보다 별도 커밋으로 제거하는 편이 낫다고 판단했습니다.

```bash
git rm -r packages
dotnet restore
dotnet test tests/SimpleIoT.Gateway.Tests/SimpleIoT.Gateway.Tests.csproj
```