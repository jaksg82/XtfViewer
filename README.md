Xtf Viewer
====================

A Windows application that can read the files created by some geophisics acquisition softwares.

This software can visualize the information stored inside the eXtensible Triton Format files.

The software contain a project for each target platform.

Actually the situation is:

jakxtflib --> the base class that convert the bytes in to objects.

XtfViewer_CommonAssets --> this is the core library that handle the reading of the files and then format the results for the UI.

XtfViewerAppCommons --> this contain some code that are shared between the different UIs.

XtfViewer_Phone8 --> this is the UI application designed for Windows Phone 8.1.

XtfViewer_Win8 --> this is the UI application designed for Windows 8.1.

XtfViewer_UWP10 --> this is the UI application designed for Windows 10 and compatible with all the families of devices.

gh-pages --> this folder contain the pages and other related resources for the GitHub Pages.
