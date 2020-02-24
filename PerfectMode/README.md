# Perfect Mode
Hate having to notice that you missed a note, pausing, and scrolling to restart, and pressing green twice? Have the game do it for you!
With one easy tweak, you can now toggle whether the game automatically restarts when you botch up a note. Too harsh? Just tune it down to any note amount for you!
By pressing `Ctrl + Shift + F6`, you can bring up a window that allows you to customise how many notes you can miss, how long before the song restarts, and how the on-screen indicators look!

## Preview
![Perfect Mode Preview 1 (gfycat)](https://giant.gfycat.com/FearlessGlumElectriceel.gif)
![Perfect Mode Preview 2 (gfycat)](https://giant.gfycat.com/ThriftySereneJumpingbean.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**.
- Make sure you install CHLauncher first!
  - Navigate to the `#ch-launcher` channel, and download the latest version of CHLauncher.exe.
  - Run it, and patch your version of Clone Hero with it. You should now have a `Tweaks` folder in your Clone Hero folder.
- Download the latest version of `PerfectMode.dll` from [the releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and place it in your tweaks folder.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `PerfectModeConfig.xml` appear in your `Tweaks` folder. This file defines the layout used for the UI elements of this tweak, as well as enabling or disabling the perfect mode.

**By default perfect mode is off!** You need to enable it by pressing `Ctrl + Shift + F6` to open the configuration menu. Up the top is `Enabled`, which determines whether the mode is on or off. To assist with knowing whether this mode is on or off, you can check `On-screen Indicator`, which will show a message on-screen when your have perfect mode on.

You can also control whether the perfect mode restart kicks in when you break an FC, 100%, or any other number of notes missed. When `FC Mode` is checked, you must maintain a full combo in order to successfully pass the song. If you uncheck it, then the game will only restart the song when you miss more notes than the `Note Miss Limit`. You can also tweak the `Fail Delay Before Restart` to change how long it takes before the game restarts the song.

### Configuring
You can press `Ctrl + Shift + F6` to open the configuration menu at any time in Clone Hero. This lets you access the details of every element used by the tweak, from the position, colour, size, alignment, and formatting. It also lets you change some of the settings of the tweak's functioanlity. Simply navigate the menus and change the details of settings as you please. The elements on-screen will change in real time to help you create the layout you desire.

**The config is saved only when you press the `Save Config` button**, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `PerfectModeConfig.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again. Several defaults of the tweak are based on your screen resolution at the time, so if elements are a bit oddly positioned, try resetting your config.