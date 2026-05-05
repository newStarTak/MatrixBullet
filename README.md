# Matrix Bullet

Matrix Bullet은 서로 다른 색상의 권총을 양손에 들고, 색상에 맞는 표적을 조준해 맞추는 VR FPS 게임입니다.

상명대학교 2023년 1학기 지능형혼합현실 전공 수업에서 제작한 프로젝트로, VR 환경에서의 실시간 조명, 컨트롤러 입력 기반 손동작, 무기 그랩 보정, 곡선형 표적 이동 등을 구현했습니다.

## 프로젝트 개요

| 항목 | 내용 |
| --- | --- |
| 프로젝트명 | Matrix Bullet |
| 장르 | VR FPS |
| 개발 환경 | Unity |
| 사용 언어 | C# |
| 렌더 파이프라인 | Universal Render Pipeline |
| 버전 관리 | Plastic SCM |

## 게임 소개

플레이어는 VR 컨트롤러를 사용해 양손에 서로 다른 색상의 권총을 들고, 색상에 맞는 표적을 향해 발사합니다.

단순한 사격 게임이 아니라, VR 환경에서의 손동작 표현, 무기 그랩 보정, 실시간 조명 표현, 곡선 이동 표적 등을 통해 몰입감 있는 플레이 경험을 만드는 것을 목표로 했습니다.

## 주요 구현 내용

## 1. URP 기반 실시간 조명 구현

메타 퀘스트2 장비의 성능에 최적화하기 위해 Universal Render Pipeline을 사용하여 실시간 광원을 적용했습니다.

여러 색의 광원을 배치하고 Realtime Global Illumination 적용 전후를 비교하며 공간의 분위기와 반사 표현을 개선했습니다.

| Realtime Global Illumination 적용 전 | Realtime Global Illumination 적용 후 |
| --- | --- |
| <img width="470" height="300" alt="Realtime Global Illumination 적용 전" src="https://github.com/user-attachments/assets/21a3755e-1aa9-4460-8b5d-0aa6bbbb52e6" /> | <img width="470" height="300" alt="image" src="https://github.com/user-attachments/assets/8adb0e5b-d6af-4c7f-ab50-0660df25d415" />

## 2. 컨트롤러 입력 상태에 따른 손동작 구현

VR 컨트롤러의 입력 상태를 감지하여 손 모델의 동작을 다르게 표현했습니다.

컨트롤러를 단순히 잡고 있는 상태와 버튼 위에 손가락을 올린 상태를 구분하여, 사용자의 조작 상태가 손 모델에 자연스럽게 반영되도록 구현했습니다.

<img width="350" height="180" alt="image" src="https://github.com/user-attachments/assets/06a38c2b-9951-4979-997f-15f202e07fb6" />


## 3. 권총 그랩 시 Transform 및 Rotation 보정

권총을 바닥에서 집을 때 손의 위치와 방향이 조금 어긋나더라도, 의도한 방향으로 자연스럽게 권총이 손에 장착되도록 Transform과 Rotation을 보정했습니다.

<img width="350" height="180" alt="image" src="https://github.com/user-attachments/assets/2293c849-e9d5-41aa-83c3-f727156869b8" />

## 4. 베지어 곡선을 활용한 표적 이동 구현

게임 내 표적이 단순히 직선으로 이동하는 것이 아니라, 곡선 경로를 따라 움직이도록 베지어 곡선을 적용했습니다.

시작점, 제어점, 끝점을 기준으로 곡선 위의 좌표들을 계산하고, 표적이 해당 좌표들을 따라 이동하도록 구현했습니다.

### 핵심 코드

```csharp
private Vector3[] CalculateCurvePoints()
{
    Vector3 pA = point_start.position;
    Vector3 pB = point_control.position;
    Vector3 pC = point_end.position;

    curvePoints_temp = new Vector3[count_temp + 1];

    float unit = 1.0f / count_temp;

    int i = 0;
    float t = 0f;

    for (; i < count_temp + 1; i++, t += unit)
    {
        float u = 1 - t;
        float t2 = t * t;
        float u2 = u * u;

        curvePoints_temp[i] = pA * u2 + pB * (t * u * 2) + pC * t2;
    }

    return curvePoints_temp;
}
```

### 코드 설명

| 변수 | 설명 |
| --- | --- |
| pA | 곡선의 시작점 |
| pB | 곡선의 형태를 결정하는 제어점 |
| pC | 곡선의 끝점 |
| t | 곡선 보간 비율 |
| curvePoints_temp | 계산된 곡선 좌표 배열 |

### 베지어 곡선 계산 방식

2차 베지어 곡선은 시작점, 제어점, 끝점 세 개의 좌표를 사용하여 계산했습니다.

```text
P = A * (1 - t)^2 + B * 2 * (1 - t) * t + C * t^2
```

`t` 값은 0부터 1까지 증가하며, 해당 값에 따라 곡선 위의 좌표가 계산됩니다.

계산된 좌표들은 배열에 저장되고, 표적 오브젝트는 이 좌표들을 순서대로 따라가며 이동합니다.

## 시연 영상

<p align="center">
  <a href="https://www.youtube.com/watch?v=HcgjwvoKt-M">
    <img src="https://img.youtube.com/vi/HcgjwvoKt-M/maxresdefault.jpg" alt="Matrix Bullet 시연 영상" width="640">
  </a>
  <br>
  <sub>위 이미지 클릭하여 시연 영상으로 이동</sub>
</p>
