# Tiny Forest 3D Game

A small 3D Unity game prototype set in a stylized forest. The player explores the terrain, collects mushrooms, manages health, and tries to reach the goal before the forest overwhelms them.

This project includes a main menu, a playable scene, collectible mushroom mechanics, UI overlays, and a simple progression system.

## Overview

Tiny Forest 3D Game is a lightweight Unity project built around a simple gameplay loop:

- Move through a 3D forest environment
- Collect good mushrooms to gain points and restore health
- Avoid bad mushrooms that reduce health
- Win by collecting 10 good mushrooms
- Lose when health drops to 0

The project demonstrates Unity gameplay systems such as:
- Character movement and camera-follow logic
- Rigidbody-based physics
- Terrain-aware mushroom spawning
- Score and health UI
- Scene transitions and game end states

## Features

- 3D forest-themed gameplay environment
- Character movement with sprinting
- Ground checking and directional movement
- Camera-follow behavior using Cinemachine
- Mushroom spawning system
- Positive and negative collectible types
- Score and health tracking
- Win/lose end screen
- Main menu and restart flow
- Audio feedback for collectible events

## Gameplay Loop

The player starts in the main scene and explores the environment using the keyboard.

- Good mushrooms:
  - Increase health
  - Increase score
  - Trigger a positive sound effect

- Bad mushrooms:
  - Decrease health
  - Trigger a negative sound effect

When the player:
- reaches a score of 10, they win
- loses all health, they lose

## Controls

The following controls are implied by the project scripts and input setup:

- W / A / S / D: movement
- Left Shift: sprint
- Mouse: camera movement / look direction
- Esc or UI action: exit game

This project uses Unity's Input system and Cinemachine-based camera controls.

## Project Structure

```text
tiny-forest-3Dgame/
├── Assets/
│   ├── Animations/
│   ├── Fonts/
│   ├── InputSystem_Actions.inputactions
│   ├── Materials/
│   ├── Music/
│   ├── Prefabs/
│   ├── Scenes/
│   │   ├── MainMenu.unity
│   │   └── MainScene.unity
│   ├── Scripts/
│   │   ├── MushroomGeneration/
│   │   │   ├── MushroomScript.cs
│   │   │   └── MushroomSpawner.cs
│   │   ├── Physics/
│   │   ├── PlayerScripts/
│   │   │   ├── PlayerHealth.cs
│   │   │   ├── PlayerInput.cs
│   │   │   └── PlayerMovement.cs
│   │   └── UIScripts/
│   │       ├── MainMenuControlller.cs
│   │       ├── TextFadeIn.cs
│   │       ├── UIManager.cs
│   │       └── ...
│   ├── Sprites/
│   └── TextMesh Pro/
├── Packages/
│   └── manifest.json
├── ProjectSettings/
├── .gitignore
├── .gitattributes
├── .vsconfig
├── README.md
└── ...
```

## Main Scripts

### PlayerMovement.cs
Handles:
- walking and sprinting
- ground detection
- movement relative to camera direction
- character rotation

### PlayerInput.cs
Reads input from Unity input and forwards movement data to the player controller.

### PlayerHealth.cs
Tracks:
- health
- score
- win/lose conditions
- mushroom collision behavior
- sound playback

### MushroomSpawner.cs
Generates mushrooms across the terrain at valid positions, creating both good and bad variants.

### MushroomScript.cs
Represents a collectible mushroom and defines how it is consumed when touched.

### UIManager.cs
Controls:
- score display
- health display
- end-of-game screen
- win/lose state UI
- restart and exit buttons

### MainMenuControlller.cs
Loads the main game scene and exits the application.

## Requirements

To run this project, you will need:

- Unity 2022 LTS or newer
- A compatible desktop environment
- Unity project dependencies as defined in `Packages/manifest.json`

The project includes dependencies such as:
- Cinemachine
- Input System
- TextMeshPro
- UGUI
- Physics and terrain-related modules

## How to Run

1. Open the project in Unity.
2. Make sure the project is using the correct Unity version compatible with the included packages.
3. Open the `MainMenu` scene or `MainScene` scene from the `Assets/Scenes` folder.
4. Press Play to run the game.

If the project is being launched from the main menu:
- click Start or Start Game to enter the playable scene
- click Quit to exit

## Recommended Development Notes

This repository appears to be a prototype or game jam style project, and the code structure is organized around:
- gameplay scripting
- UI logic
- environmental content
- collectible spawning

If you plan to extend the project, useful improvements could include:
- better enemy or AI behavior
- richer level design
- save/progression system
- polish for sound and animations
- improved player feedback and camera transitions

## License

No explicit license file was found in this repository. If you intend to publish or share this project publicly, it is recommended to add a proper license such as MIT, GPL, or a custom project license.
