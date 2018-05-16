# Kick The Jester

A tossing game in Unity.

## Plugins & Frameworks Used
* [UniRx - Reactive Extensions for Unity](https://assetstore.unity.com/packages/tools/unirx-reactive-extensions-for-unity-17276)
* [Arrow Mobile UI for UGUI](https://assetstore.unity.com/packages/2d/gui/arrow-mobile-ui-for-ugui-64369)
* [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween-3595)

## Getting Started
1. Clone the repository and open the project with Unity
2. From the Unity Editor open any C# source file, so that the `.sln` gets generated correctly
3. Open the Asset Store in Unity and import all required Assets (see list above)

**NOTE:** Paid Unity Asset Store plugins are not part of the repository for obvious reasons. You will have to import them manually.

### Starting the Project in Unity
Currently you can only start the project from the `Title` scene, because there are some critical items loaded, which do not get initialized in other scenes.