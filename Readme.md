
🧠 Project Overview
This project demonstrates a lightweight Augmented Reality experience built in Unity, designed to detect QR codes and spawn objects directly on top of them. Unlike many AR setups, this application uses image-based tracking only—no world anchors or persistent spatial mapping. It is ideal for use cases such as guided installations, educational displays, or simple spatial triggers.

👥 Team Members
Elijah Kelting, Xandyr Brennan

📱 Experience Description
When the user points their device camera at a predefined QR code, the app detects and tracks it in real time. As soon as the code is recognized, a prefabbed 3D object is instantiated directly at the code’s location and will follow the QR’s pose. This creates a responsive, intuitive AR experience that does not require plane detection or persistent anchors.

🧰 Technical Details
✅ AR Framework / SDK
Unity AR Foundation (v6.x)

XR Plugin Management enabled

ARKit/ARCore (via Unity's cross-platform abstraction)

🧭 Tracking Technique
2D Image Tracking using ARTrackedImageManager

Tracking is initiated by matching a QR code against a reference image library

🛠 Development Environment
Unity Version: 6000.1

Scripting Runtime: .NET Standard 2.1

Platform Targets:

iOS (via ARKit)

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

💡 Design Notes
All tracked objects are parented directly to the tracked image (ARTrackedImage) so that the object's position updates in real time with the QR.

No anchors are used in this version for persistence after tracking is lost.

Optional smoothing or easing can be applied to minimize jitter when tracking is reacquired.
<br>
<img src="https://github.com/Elispse/PRO385_ARPROJECT/blob/main/image0.jpg" width="250" height="500">
