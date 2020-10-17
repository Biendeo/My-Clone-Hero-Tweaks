# Legacy Mod Loader
An alternative mod loader for CHLoader compatible mods.

**Please do not use this and CHLoader**, as both will try and instance tweaks, which can cause undefined behaviour.

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.3](https://github.com/BepInEx/BepInEx/releases/tag/v5.3) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Legacy Mod Loader you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Legacy Mod Loader` exists.
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Legacy Mod Loader 1.5.0.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Legacy Mod Loader`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `Tweaks` folder in the your Clone Hero directory. This is where CHLoader mods expect to be installed. Simply extract any legacy *Tweaks* into this folder (i.e. make sure that the mod's `.dll` file is directly in that folder), and Legacy Mod Loader will load it on the next startup of Clone Hero.

In-game, you can press F1 to bring up a menu showing the names of all the loaded tweaks. If you have the `Enable Unload Feature` enabled in config, you can also click to unload and reload tweaks. This functionality may/may not work depending on the tweak, since CHLoader never properly implemented this functionality.

**Note:** this is only to be used for *tweaks* and other mods that utilise CHLoader. If a mod says it's for BepInEx, you do not require Legacy Mod Loader and should instead install it in the way described for that mod (for example, in `BepInEx/plugins`). Tweaks are compatible with BepInEx mods though, so you can install both BepInEx mods and CHLoader-style tweaks on the same installation of Clone Hero.