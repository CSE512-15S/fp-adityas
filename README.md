# Immersive Data Visualization with Virtual Reality
![Summary Image](summary.png "User selects data point in VR" )
## Abstract
There has been a resurgence of interest in Virtual Reality (VR) with the advent of several consumer-ready head mounted displays. And while VR has a long standing tradition in graphics literature traceable to the early 90's, little research has been conducted on the implications of modern VR and 3D displays on Data Visualization. In this work we explore how to create immersive 3D data visualizations and subsequently interact with them effectively using novel modalities such as head pose. We hypothesize that an immersive data visualization experience provides benefits beyond traditional desktop counterparts and that a user is better connected to the data, both perceptually and emotionally.

## Team Members
- Aditya Sankar (adityas@uw) - Design and development of Unity and WebGL applications

## Running Instructions
Correctly running these visualizations requires Oculus Rift DK2 hardware and npvr plugin installed from npvr-master/bin/ ([further instructions](https://github.com/benvanik/vr.js/)). They may run without the hardware attached, but you will not be able to fully experience the virtual reality demos and there may be some unexpected behavior. Happy to organize another demo for grading purposes, if required.

* Unity Demo: Download and run UnityVR/brushing_demo.exe
* Link: [AsterankVR](http://homes.cs.washington.edu/~aditya/files/misc/fp-adityas/asterankVR)
* Link: [NeuralNetworkVR](http://homes.cs.washington.edu/~aditya/files/misc/fp-adityas/NeuralNetworkVR)

## Repository Structure
* asterank-master: Clone of the Asterank project, modified.
* asterankVR: Build of the Asterank project, modified to run with VR.
* Neural-Network-master: Clone of the Neural-Network project, modified.
* NeuralNetworkVR: Build of the Neural-Network project, modified to run with VR.
* npvr-master: Clone of the vr.js project, modified and updated to run with latest Oculus SDK
* oculusjs: Supporting javascript glue files for the WebGL VR demos
* UnityVR: Unity VR app for brushing and linking
* progress: Progress report and slides
* final: Final report and poster
* index.html: project page
* summary.png: summary image
* README.md: This file



## Development Process
Wrangling the experimental drivers, pre-release SDK and finicky hardware were by far the most challenging part of this project. Since VR is a newly emerging platform, a lot of the tools are not yet mature. But I felt like that's a fair price to pay in order to be able to experiment with such ground breaking technology. Fortunately, due to it's popularity, there are also a lot of open source projects related to VR and several forum posts and troubleshooting guides available online.

This project builds upon the following amazing open-source projects:

* [vr.js](https://github.com/benvanik/vr.js/)
* [Asterank](https://github.com/typpo/asterank)
* [Neural Network](https://github.com/nxxcxx/Neural-Network)

