# demo

An initial working demo for the XR/Unity code challenge. 

A few things that are not ideal at this point but could receive polished near term :
* The placement marker should be refactored as more of a reticle component rather than composed in the PlacementManager.
* The marker follows the forward of the surface geometry. Since I am placing a plane for this purpose, this was the most direct way to handle verticle surfaces as well as horizontal surfaces. Ideally, this would move to a "look at" projection in front of (or over) the marker.


Longer term items :
* As ARFoundation is an abstraction of core AR functionality, some details (better shadow casting, environment probes, etc.) are not yet available. These additions should be added over time as they roll-up into ARFoundation.


Other notes :
* Tested on an iPad Pro 10.5 running iOS 12.1; not tested on any other configuration.
* Not tested on Android.
