# Accuracy Indicator
Ever overstrummed in a rapid string of notes because you were slightly ahead or behind of the hit window? This indicator will tell you exactly how early/late you are when hitting every note! It even works in practice mode!
By pressing `Ctrl + Shift + F7`, you can bring up a window that allows you to customise where the indicator is, and what colours it'll display!

## Preview
![Accuracy Indicator Preview 1 (gfycat)](https://giant.gfycat.com/CarefreeHonorableGourami.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.3](https://github.com/BepInEx/BepInEx/releases/tag/v5.3) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Accuracy Indicator you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Accuracy Indicator` exists.
    - You will also need `Biendeo CH Lib` installed before this mod works!
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Accuracy Indicator 1.5.0.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Accuracy Indicator`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `com.biendeo.accuracyindicator.config.xml` appear in your `BepInEx\config` folder. This file defines the layout used for the UI elements of this tweak, as well as enabling or disabling the accuracy indicator.

The accuracy indicator should appear to the bottom right of the highway. You can customise where it is by pressing `Ctrl + Shift + F7` to open the configuration menu. Up the top is `Enabled`, which determines whether the mode is on or off. It should be enabled by default. As you play notes, you'll see the indicator pop up, telling you how early/late you were to the note. Negative times mean you were early, positive times mean you were late.

Also, if the tweak is enabled in the configuration menu, you will get a count of how many notes you hit with each accuracy level (very late, perfect, etc.). For each accuracy level, you'll get listed the number of notes, as well as the overall percentage of notes. There is also a number in milliseconds showing what your most forgiving cutoff was for each level throughout the song. This prevents people from tweaking the cutoffs just as a song ends to make their results look good; you have to stick with your thresholds for the whole song. Along with the totals, these reset if you reset the song (either from the results screen or the pause menu), so if you want to test yourself, just remember to not change the cutoffs while in the middle of a song.

### Configuring
You can press `Ctrl + Shift + F7` to open the configuration menu at any time in Clone Hero. This lets you access the details of every element used by the tweak, from the position, colour, size, alignment, and formatting. It also lets you change some of the settings of the tweak's functionality. Simply navigate the menus and change the details of settings as you please. The elements on-screen will change in real time to help you create the layout you desire.

**The config is saved only when you press the `Save Config` button**,, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `com.biendeo.accuracyindicator.config.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again. Several defaults of the tweak are based on your screen resolution at the time, so if elements are a bit oddly positioned, try resetting your config.

Enabling `Test Layout` at the top of the configuration window will make the indicator visible at all times. This is useful for helping you arrange the indicator where you want without it disappearing. This mode is not really intended for regular usage, so it is recommended to disable it when you are finished (or not, it's your indicator 🤷‍♂️).

Each color's `Cutoff Time` refers to the amount of time you can be off for this color. By default, being slightly early/late is 0.01s from the note, early/late is 0.02s, and very early/late is 0.03s. For the maximum functionality, ensure that slightly times are less than the early/late times, which are less than the very early/late times. The maximum times you can use are 0.07s, where the note is outside the hit window.