# My Clone Hero Tweaks
[![Build status](https://ci.appveyor.com/api/projects/status/cslq1e2b6b1rikpl?svg=true)](https://ci.appveyor.com/project/Biendeo/my-clone-hero-tweaks)


A bunch of Clone Hero tweaks I have made!

**More information about each tweak can be found in their respective folders.**

## Preview
![My Clone Hero Tweaks (gfycat)](https://giant.gfycat.com/GrouchyEarlyIggypops.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.3](https://github.com/BepInEx/BepInEx/releases/tag/v5.3) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest versions of the mods you want for your version of Clone Hero.
    - Almost all the mods will require `Biendeo CH Lib` to also be installed so please also download that. If you are missing it, the log will inform you.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder.
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has a line in this format for each mod: `[Info   :   BepInEx] Loading [Biendeo CH Lib 1.5.0.0]`

## How to reset configs
- All mod configs are in `BepInEx\config`, so just delete the appropriate files. All mods have confings in the format `com.biendeo.MODNAME.*`. For example, to reset `Extra Song UI`, delete `com.biendeo.extrasongui.xml` and `com.biendeo.extrasongui.layout.xml`.

## How to uninstall
- If you're just mods, go to `BepInEx\plugins` and delete the folder for the appropriate mod you're removing. All configs are in `BepInEx\config` if you want to also remove those.
- If you're uninstalling BepInEx, delete the whole `BepInEx` folder, and `winhttp.dll` from your Clone Hero directory.

## How to troubleshoot
- **Always perform these steps before giving up, because the logs usually indicate what has gone wrong every time!**
- BepInEx logs to `BepInEx\LogOutput.log` and `BepInEx\LogOutput.log.1`. If both files aren't there, BepInEx didn't initialise and you should try and reinstall it. If they are, open the latest of the two.
  - Ensure that there exists a line for every mod (including `Biendeo CH Lib`) in the log indicating that it's `Loading`. If not, then the mods probably aren't installed. BepInEx will look inside `BepInEx\plugins` for any `dll` files to load, so if you haven't extracted the mods properly, then they probably aren't even loading to begin with.
  - Try and find any lines starting with `ERROR`. If there are, then there's probably an actual issue with that mod and you should [report a bug report through GitHub](https://github.com/Biendeo/My-Clone-Hero-Tweaks/issues/new/choose). Please follow the format and fill out as much information as you can.
  - If a lot of lines start with `WARN` or you don't see anything too obvious, this may be a user error, so double check that you're utilising the mods properly. Otherwise, you can also [report a bug report through GitHub](https://github.com/Biendeo/My-Clone-Hero-Tweaks/issues/new/choose).
- Clone Hero logs to `%USERPROFILE%\AppData\LocalLow\srylain Inc_\Clone Hero\output_log.txt`. Any legacy mods would also log there, but it's only useful for spotting anything Clone Hero related that's gone wrong now.
- Check out the [Clone Hero Modding Discord](https://discord.gg/YsGNFEj) if you want someone (maybe myself) to talk and help you out with anything.

## How to contribute
### Ideas
- [Request a new feature through GitHub](https://github.com/Biendeo/My-Clone-Hero-Tweaks/issues/new/choose), it's the easiest way.
- If it's a new idea as well, you can pitch the idea to the [Clone Hero Modding Discord](https://discord.gg/YsGNFEj) for some general feedback.
### Programming
- If you spot a bug, feel free to make a pull request and I can review it. The functionality of my mods is my say though, so if it changes behaviour I'll have to decide whether I'm keeping it or not.
- If you want to just fork my mods, that's fine, just make sure you've also got the GPL-3.0 license with your code as well.
  - You are free to make new mods that utilise `Biendeo CH Lib` to access the underlying game data if you choose.
  - My development process is described on this repository's wiki if you'd like some additional pointers.