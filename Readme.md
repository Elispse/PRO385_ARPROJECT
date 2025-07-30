🧠 Project Overview
<br>
AR Pets is an AR experience where you are able to summon a pet from a QR code and take care of them in the real world, along the lines of Tamagotchi.

👥 Team Members
Elijah Kelting, Xandyr Brennan

📱 Experience Description
When the user points their device camera at a predefined QR code, the app detects and tracks it in real time. As soon as the code is recognized, a prefabbed 3D object is instantiated directly at the code’s location and will follow the QR’s pose. This creates a responsive, intuitive AR experience that does not require plane detection or persistent anchors.

🧰 Technical Details
✅ AR Framework / SDK
Unity AR Foundation (v6.x)
XR Plugin Management enabled
ARCore

🧭 Tracking Technique
2D Image Tracking using ARTrackedImageManager

Tracking is initiated by matching a QR code against a reference image library

🛠 Development Environment
Unity Version: 6000.1

Scripting Runtime: .NET Standard 2.1

Platform Targets:
Android (via ARCore)

▶️ How to Run the Project
1. Project Setup
Clone or download the project repository into Unity Hub

Open it with Unity 6000.1

2. Setup for Your Device
Ensure ARKit XR Plugin (iOS) or ARCore XR Plugin (Android) is installed via the Unity Package Manager

In Project Settings > XR Plugin Management, enable your platform's XR support

Add your QR image(s) to an XR Reference Image Library

Create the library in Assets → Right-click → XR → Reference Image Library

Add a high-quality QR code image with known physical dimensions

3. Build Settings
Switch to Android or iOS platform

Enable the following:

AR Support

Camera Usage Description in Player Settings

Plug in your AR-capable device and build to it directly

<br>
<img src="https://github.com/Elispse/PRO385_ARPROJECT/blob/main/image0.jpg" width="250" height="500">
