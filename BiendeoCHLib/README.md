# Biendeo CH Lib
The common code for all of my mods. If you're a developer, you may also use this to help make your own mods!

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.4.17](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.17) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Biendeo CH Lib you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Biendeo CH Lib` exists.
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Biendeo CH Lib 1.5.2.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Biendeo CH Lib`.

## How to use
### Default usage
There's nothing a regular user would probably get out of this mod by itself; it's mostly boilerplate for my other mods, so you probably want those as well.