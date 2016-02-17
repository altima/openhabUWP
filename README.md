[![Build status](https://ci.appveyor.com/api/projects/status/n38mp4b6yjj2t8qt?svg=true)](https://ci.appveyor.com/project/altima/openhabuwp)

# openhabUWP

## About
First of all , this is a hooby project and I will do this in my freetime after work and between family stuff. Features may come. I created it for me, so in the first state the server ip is hardcoded. When you want to test, let me know via an issue on github.

first test: https://youtu.be/M-G_SNrDUqk

## Windows 10 client 
with support for:
* zeroconf 
 * openhab 1.x: works (tested with raspberrypi b as server)
 * openhab 2.0: should work (tested with a snapshot from late 2015)
* http long poolig for receive update events from openhab 1.x and 2.0
* easy to use UI (missing, currently a layout like on Android is implemented)

## How to build

### Prerequists
* WIndows 10 (Pro) with enabled developer mode
* Visual Studio 2015 Community with Windows 10 developer tools (build 10586)
* node.js

### Build with grunt
1. checkout repo
2. goto repo directory
3. call "npm install"
4. call "grunt"

### Build with VS2015
1. checkout repo
2. goto repo directory
3. open solution "openhabUWP.sln"
4. press play

## Inspired by 
* https://github.com/openhab/openhab.winrt
* https://github.com/kobush/OpenHab.Windows (thanks @kobush)
