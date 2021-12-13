# Splash Text Editor
Are you tired of seeing how hungry the cache is? Don't want to go to AGDQ 2020? Don't like messages about bugs?
With one easy tweak, you can now write your own splash messages for the title screen! If you want, you can also add your messages to the regular rotation of splash messages as well!
By pressing `Ctrl + Shift + F9`, you can bring up a window that allows you to customise your splash messages!

## Preview
![Splash Text Editor Preview 1 (imgur)](https://i.imgur.com/eYHVvym.png)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.4.17](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.17) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Splash Text Editor you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Splash Text Editor` exists.
    - You will also need `Biendeo CH Lib` installed before this mod works!
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Splash Text Editor 1.5.2.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Splash Text Editor`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `com.biendeo.splashtexteditor.config.xml` appear in your `BepInEx\config` folder. This file describes what splash texts you've defined, as well as some other settings to tweak.

**By default there are no custom splash messages!** You can press `Ctrl + Shift + F9` to bring up the editor menu, where you can edit some settings about the splash messages, as well as edit any custom splash messages.

### Configuring
You can press `Ctrl + Shift + F9` to open the configuration menu at any time in Clone Hero. At the top of the configuration menu is an "*Enabled*" setting, which enables/disables the custom splash message functionality. If enabled, your custom splash messages will be displayed. You can also tick "*Vanilla Splash Messages*" if you'd like to see the messages the game itself defines. You can also tick "*DragonForce Override*" and "*April Fools Splashes*" if you'd like the see those special messages at the appropriate times (if you don't know what they are, it'll be a mystery). You can also change the cycle time (the time each splash message will be displayed on screen).

Underneath is where you can customise the splash messages. Click "*Insert*" to add a new textbox where you can type your message. You can also delete messages, as well as shift them up or down for organisation. Do note that since splash messages are random, this is just for your neatness. You can also click "*Preview message*" to see it on the actual title screen. The splash messages will return to randomly cycling if you click "*Reset*" above, or when you change scenes (such as going into a song or the credits).

**NOTE: Typing into the text areas will also interact with the game underneath, so please make sure all your user profiles are dropped out before typing anything to prevent accidentally going into a bunch of menus! Also, beware hitting Enter because that is often the default for assigning a profile.**

**The config is saved only when you press the `Save Config` button**, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `com.biendeo.splashtexteditor.config.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again.