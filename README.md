# ICEBREAKER

**Small mobile game prototype born from the JOBS tech demo.**

Project was created in a short time under the time pressure only as a prototype project and still may contain bugs.

Currently runs in Editor and Android platform. Feel free to download Android APK from releases.

![Record_2024-12-01-16-14-00-ezgif com-optimize](https://github.com/user-attachments/assets/1ef42ecc-5f89-4209-9c37-b6474b703787)

### EDITOR RUN INSTRUCTIONS

**Unity Editor version: 2022.3.47f1**

1. Open EntryPoint scene.
2. Run it.
3. Ahoj!


## Main Features

## Ice JOB system
The main idea of this project is to show the potential of JOBS system in UNITY.
Each particle has 2 behaviours.
1. Simulating the ice flee behaviour.
2. Simulating the wave.



## UI Toolkit
Instead of Unity UI, the Unity Toolkit was used.
![image](https://github.com/user-attachments/assets/4a3f97d2-7bce-42f4-b3c8-12867f07d114)
Each screen was separated to unique document. This approach helps avoid the conflicts in UI changes for designers and increases readibility.



## Design patterns
1. ServiceLocator
2. StateMachine
3. MVP
4. Object Pool
5. Factory



## Optimisation
Big variation of optimisation techniques where used to match the target 30 FPS and ~30 ms. I have to admit, that it still can be improved, but it's already quite optimised game.

1. JOBS SYSTEM
   
2. Dynamic batching and general draw calls optimisation. Total draw calls is 17 (where ship model takes 10 XD)
![image](https://github.com/user-attachments/assets/2fb34ecc-1c1b-4046-8397-e28591ab7b6e)
![image](https://github.com/user-attachments/assets/2673b039-178b-40a1-82f8-f8294074dc27)

3. Asynchronous world generating
   
![ScreenRecording2024-11-30131131-ezgif com-optimize](https://github.com/user-attachments/assets/06bf7135-682e-402b-902c-96a02ae7d53f)

4. World chunks loading
   
![ScreenRecording2024-11-30131846-ezgif com-optimize](https://github.com/user-attachments/assets/38435dab-feaf-42ca-a621-680a18b00841)

## Used Plugins

1. DOTween
2. UniTask
3. OdinInspector
