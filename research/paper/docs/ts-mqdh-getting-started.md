# Ts Mqdh Getting Started

**Documentation Index:** Set up Meta Quest Developer Hub to develop and test Meta Quest applications with developer tools.

---

---
title: "Get Started with Meta Quest Developer Hub"
last_updated: "2025-12-16"
---

## Prerequisites

Before setting up Meta Quest Developer Hub, make sure that you have reviewed the following documents:

- [Before You Begin](/documentation/unity/unity-before-you-begin)
- [Set Up Your Device](/documentation/unity/unity-env-device-setup/)

To follow the instructions on this page, you need:

- A [Meta developer account](/sign-up/)
- A Meta Quest headset
- A USB-C cable
- An Android or iOS mobile device

## Install Meta Quest Developer Hub

1. Download and install the Meta Quest Developer Hub application for [macOS](/downloads/package/oculus-developer-hub-mac) or [Windows](/downloads/package/oculus-developer-hub-win).
2. Open the application and log in using your Meta developer credentials. Use the same Meta developer credentials you used to log in on the headset.

## Connect headset to Meta Quest Developer Hub

To use MQDH features, you need to first connect your Meta Quest headset to your development machine:

1. On your mobile device, open the Meta Horizon app.

2. In the app, tap the hamburger menu (the icon with three horizontal lines) next to the search bar.
   Then, tap **Devices** and select your headset from the results.

   <box display="flex" flex-direction="column" align-items="center" padding-vertical="16">
     <section>
       <embed-video width="100%">
      <video-source handle="GKSPUB3GIsntyskEAFLIZLVm0MhwbosWAAAF" />
       </embed-video>
     </section>
     <text display="block" color="secondary">
       <b>Video</b>: Shows selection of the Devices item in the hamburger menu.
     </text>
   </box>

3. Tap **Headset Settings** beneath the image of your headset.

   {:width="50%"}

4. Tap **Developer Mode**.

   {:width="50%"}

5. Turn on the **Developer Mode** toggle switch.

   <img src="/images/horizon-mobile-developer-toggle.png" alt="Toggle Developer Mode to the on position" width="400px">

6. Use a USB-C cable to connect the headset to your computer.

7. Put on the headset.

8. In the headset, go to **Settings** > **Advanced** > **Developer**, and then enable **Enable custom settings** and **MTP Notification**.

9. When asked to allow USB debugging, select **Always allow from this computer**.

   <img src="/images/allow-usb-debugging-2020.png" alt="Allow USB Debugging prompt">

To verify the connection:

1. Open MQDH.
2. In the left navigation pane, choose **Device Manager**. All the devices you have set up are displayed in the main pane. Each device is shown with its status, which includes the device ID and connection status. The active device shows a green **Active** designator.
3. If multiple headsets are connected to your computer, select the currently connected headset from the drop-down list in the upper-right corner.

### Updating Meta Quest Developer Hub

MQDH maintains a regular update cadence to ship new features and important bug fixes. It supports auto-update and you will be prompted to install the new release when it becomes available.