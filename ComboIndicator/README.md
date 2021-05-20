# Combo Indicator
Get that Guitar Hero 3 nostalgia by having an animated note streak indicator pop up at 50, 100, 200, and so on combo!

## Preview
![Combo Indicator Preview 1 (gfycat)](https://giant.gfycat.com/GreenThickIndianrockpython.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.4.11](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.11) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Combo Indicator you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Combo Indicator` exists.
    - You will also need `Biendeo CH Lib` installed before this mod works!
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Combo Indicator 1.5.0.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Combo Indicator`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `com.biendeo.comboindicator.layout.xml` appear in your `BepInEx\config` folder. This file defines the layout used for all the elements in this tweak. If you begin playing any song, you should notice a "Hot Start" indicator if you hit the first 25 notes of the song without breaking a combo, a note streak indicator at 50, 100, 200, etc. notes, and a "Star Power Active" indicator when you can activate it.

### Configuring
You can press `Ctrl + Shift + F8` to open the configuration menu at any time in Clone Hero. This lets you change several general settings of the program, such as where the indicator appears on screen, which events would trigger the indicator, and the keybind for opening this prompt.

**The config is saved only when you press the `Save Config` button**, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `com.biendeo.comboindicator.config.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again. Several defaults of the tweak are based on your screen resolution at the time, so if elements are a bit oddly positioned, try resetting your config.

Clicking `Test Indicator` will flash up a test messsage. This'll let you test how the indicator will look when it activates in-game.