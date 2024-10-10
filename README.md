# Jenga 3D Educational Project

I've created this project to showcase my coding skills in Unity.

I'm using Dependency Injection (Zenject), Asynchronous Operations (UniTask), SOLID practices & various design patterns.

[Code is located here](https://github.com/wolkp/code-sample/tree/main/Assets/Jenga3DModule/Scripts/Runtime)

![image](https://github.com/user-attachments/assets/c991a075-24af-426e-a40a-7030678f7397)

## Description
In contrast to the traditional Jenga game, the blocks are created based on the data fetched from the sample API with static JSON data about their knowledge. 
Each Jenga stack represents a grade associated with it (e.g. 7th Grade).
There are three distinct types of blocks:
* Glass, which symbolizes a concept the student does not know and needs to learn;
* Wood, which symbolizes a concept that the student has learned but was not tested on;
* Stone, which symbolizes a concept that the student was tested on and mastery was confirmed;

Upon clicking "Test My Stack" button, the Glass blocks get destroyed and the physics are enabled in the corresponding stack. 
If the stack falls down, it means that the student has gaps in their knowledge and cannot yet pass the grade.
