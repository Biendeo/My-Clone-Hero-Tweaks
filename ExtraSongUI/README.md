# Extra Song UI
Wanna know if you're still on track for an FC? How far behind are you in getting that 7-star? This tweak is for you!
With this tweak, you can now see your song progress numerical, how many points you need to reach certain star amounts, and how many notes you've currently missed in the song! Hate how things are arranged? Press `Ctrl + Shift + F5` and customise everything!

## Preview
![Extra Song UI Preview (gfycat)](https://giant.gfycat.com/GrouchyEarlyIggypops.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.3](https://github.com/BepInEx/BepInEx/releases/tag/v5.3) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Extra Song UI you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Extra Song UI` exists.
    - You will also need `Biendeo CH Lib` installed before this mod works!
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Extra Song UI 1.5.0.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Extra Song UI`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `com.biendeo.extrasongui.layout.xml` appear in your `BepInEx\config` folder. This file defines the layout used for all the elements in this tweak. If you begin playing any song (not in practice mode), you should notice a bunch of new user interface labels on the left side of the highway. These will live update with details about the song as you play.

### Configuring
You can press `Ctrl + Shift + F5` to open the configuration menu at any time in Clone Hero. This lets you access the details of every element used by the tweak, from the position, colour, size, alignment, and formatting. Simply navigate the menus and change the details of settings as you please. The elements on-screen will change in real time to help you create the layout you desire.

**The config is saved only when you press the `Save Config` button**, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `com.biendeo.extrasongui.layout.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again. Several defaults of the tweak are based on your screen resolution at the time, so if elements are a bit oddly positioned, try resetting your config.