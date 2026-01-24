# 🚀 Thrust Issues

**A physics-based rocket game about fighting gravity, managing momentum, and exploding. A lot.**

[![Unity](https://img.shields.io/badge/Made%20with-Unity-black?style=flat&logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-PC%20%2F%20Web-blue)]()
[![Frustration Level](https://img.shields.io/badge/Frustration-High-red)]()

> *"It's not rocket science. Okay, wait, it literally is."*

---

## 🎮 Play The Game
**[👉 CLICK HERE TO LAUNCH (AND CRASH) 👈](https://play.unity.com/en/games/71f937f3-98ad-4738-a735-3c86a0bba254/thrust-issues)**

---

## 🎥 Gameplay

https://github.com/user-attachments/assets/70befcbe-78bb-4b37-a5ce-0f0976833338

---

## 🧐 What is this?
**Thrust Issues** is a vertical flyer built in Unity where you pilot a highly unstable rocket through three distinct, hazardous worlds. 

The goal is simple: Get from the **Launch Pad** to the **Landing Pad**. 
The reality: You will clip a wing, spin out of control, and turn into a fireball.

Designed for desktop, mastered by no one.

---

## 🕹️ Controls

Simple to learn, impossible to master.

| Action | Input |
| :--- | :--- |
| **Thrust** | `SPACE BAR` |
| **Rotate Left** | `LEFT ARROW` |
| **Rotate Right** | `RIGHT ARROW` |

> **Pro Tip:** Gentle taps. If you hold the space bar, you will go to the moon. This is not the moon. This is a cave. You will die.

---

## 🌍 The Worlds (Vibe Check)

### 1. Outpost Zero 🏭
* **The Vibe:** Industrial, electric, and deceptively safe.
* **The Threat:** Narrow corridors and your own inability to fly straight.
* **Tech Flex:** Features **Electric Cyan** dynamic lighting and a "Terminal Typewriter" UI effect that makes you feel like a hacker.
* **Audio:** Chill space station hums to keep your heart rate low before the inevitable crash.

### 2. The Abyss Shaft 🌑
* **The Vibe:** Deep, dark, and claustrophobic. 
* **The Threat:** A vertical drop into the void. If the rocks don't kill you, the fear of the dark will.
* **Tech Flex:** Uses volumetric fog and a custom **"Fly-Through" holographic title** that zooms into the screen like a blockbuster movie trailer.
* **Audio:** Sub-bass horror drones. I ducked the music volume to -20dB just so you can hear the emptiness.

### 3. The Dust Bowl 🏜️
* **The Vibe:** Mars. It’s hot, it’s orange, and it hates you.
* **The Threat:** Open air, high winds, and blinding sun.
* **Tech Flex:** A custom **"Molten Heat" UI effect** where the text rises and cools from black to white like lava.
* **Audio:** Desolate windscapes. It sounds lonely. Because you are.

---

## 🛠️ Under The Hood (The Nerd Stuff)

For the developers watching, here is how I over-engineered this thing:

### ⚛️ Hybrid Physics Movement
* **Thrust:** Uses `Rigidbody.AddRelativeForce` to apply upward momentum relative to the rocket's rotation.
* **Rotation:** Uses `transform.Rotate` but with a catch—we manually freeze the Rigidbody's Z-axis rotation constraints. This prevents the Unity physics engine from flipping the rocket uncontrollably when it hits a wall, allowing for tighter, arcade-style control.

### 💥 Finite State Machine (Collision)
* The game logic prevents "Double Deaths" using a state flag system.
* **Launch Pad:** Tagged "Friendly" (Safe zone).
* **Landing Pad:** Tagged "Finish." Triggers a victory sequence, disables controls, and loads the next scene index.
* **Everything Else:** Tagged as Death. Hitting a wall disables movement input immediately, spawns a particle explosion system, and reloads the current scene after a delay.

### 🔊 Dynamic Audio Mixing
* Built a dedicated Mixer to separate SFX and Music.
* Background music sits comfortably at **-20dB** with a "Duck Volume" setup so the physics-based thruster sounds always punch through the mix.

### 🎨 Procedural UI
* One script (`LevelAnnouncer.cs`) rules them all. It reads the Scene Name and automatically switches the font color, glow intensity, and animation style (Typewriter vs. Zoom vs. Heat Rise) to match the level's mood.

---

## 📦 Credits

* **Engine:** Unity 2022
* **Code & Design:** Kaylin Maharaj
* **Font:** Orbitron (Google Fonts) because everything looks cooler in Sci-Fi font.
* **Audio:** Community assets from Pixabay & Unity Asset Store.

---

*Made with 💖 and too much caffeine.*
