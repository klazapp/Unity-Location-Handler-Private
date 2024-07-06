# Global Manager for Unity

## Introduction

The Global Manager, offered under the `com.Klazapp.Utility` namespace, is a Unity package designed to centralize common game management tasks and frequently accessed Unity data. This utility streamlines access to time-related variables, default transform values, and preconfigured WaitForSeconds, enhancing efficiency and reducing repetitive code across your project.

## Features

- **Centralized Time Management:** Provides static access to various time-related values like `deltaTime`, `smoothDeltaTime`, and `fixedDeltaTime`, ensuring consistent and easy updates across all scripts.
- **Default Transform Constants:** Includes predefined `float3` and `quaternion` values for default positions and rotations, facilitating easier transformations and resets.
- **Optimized WaitForSeconds:** Predefined WaitForSeconds instances ranging from 0.6 seconds to 5 seconds, optimizing coroutine performance by reducing the need for repeated instantiation.

## Dependencies

- **Unity Version:** Requires Unity 2019.4 LTS or newer for optimal performance and compatibility.
- **Unity Mathematics Package:** Utilized for vector and quaternion calculations.

## Compatibility

The Global Manager is compatible with various Unity versions and rendering pipelines. It functions primarily within the Unity engine's scripting environment, ensuring broad usability.

| Compatibility | URP | BRP | HDRP |
|---------------|-----|-----|------|
| Compatible    | ✔️   | ✔️   | ✔️    |

## Installation

1. Download the Global Manager script from the [GitHub repository](https://github.com/klazapp/Unity-Global-Manager-Public.git) or via the Unity Package Manager.
2. Integrate the script into your Unity project by adding it to a dedicated manager object in your scene.

## Usage

Attach the `GlobalManager` script to a GameObject in your scene. Access the static variables directly from any script within your project:

```csharp
float currentDeltaTime = GlobalManager.deltaTime;
```

## To-Do List (Future Features)

- [ ] Integration of global event handling.
- [ ] Expansion of utility functions, including more complex time-based operations.

## License

This utility is released under the MIT License, which allows for free use, modification, and distribution within your projects.
