# Extra Song UI
Wanna know if you're still on track for an FC? How far behind are you in getting that 7-star? This tweak is for you!
With this tweak, you can now see your song progress numerical, how many points you need to reach certain star amounts, and how many notes you've currently missed in the song! Hate how things are arranged? Press `Ctrl + Shift + F5` and customise everything!

## Preview
![Extra Song UI Preview (gfycat)](https://giant.gfycat.com/GrouchyEarlyIggypops.gif)

## How to install
- You will need Clone Hero **v0.23.2.2**. You can use any version from the website (i.e. Win64, Win32, Mac, or Linux), but your mileage with the launcher version may vary.
- Install [BepInEx v5.4.17](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.17) into your Clone Hero directory.
  - Download the appropriate version and extract **all** of its files into your Clone Hero directory.
  - Please verify that BepInEx has initialised by running the game after extracting, and then checking that there are five folders and a `LogOutput.log` file inside the `BepInEx` folder. One of those folders will be named `plugins`, and you'll need that to run the mods.
- Go to the [Releases page](https://github.com/Biendeo/My-Clone-Hero-Tweaks/releases) and download the latest version of Extra Song UI you want for your version of Clone Hero.
    - All the downloads are `.zip` files and will need to be extracted to your Clone Hero directory. They should merge with the existing `BepInEx` folder. You can verify it's in the correct place if `BepInEx\plugins\Extra Song UI` exists.
    - You will also need `Biendeo CH Lib` installed before this mod works!
    - To ensure that the mods have been extracted properly, check that `LogOutput.log` (or `LogOutput.log.1`, whichever is newer) has this line: `[Info   :   BepInEx] Loading [Extra Song UI 1.5.2.0]`

## How to uninstall
- Delete the folder `BepInEx\plugins\Extra Song UI`.

## How to use
### Default usage
With the tweak loaded for the first time, you'll have a `com.biendeo.extrasongui.layout.xml` appear in your `BepInEx\config` folder. This file defines the layout used for all the elements in this tweak. If you begin playing any song (not in practice mode), you should notice a bunch of new user interface labels on the left side of the highway. These will live update with details about the song as you play.

### Configuring
You can press `Ctrl + Shift + F5` to open the configuration menu at any time in Clone Hero. This lets you change several general settings of the program, as well as the on-screen layout for the labels.

**The config is saved only when you press the `Save Config` button**, so if you decide to close your game before closing the window, your settings will not be saved. If you wish to reset the config, just simply delete `com.biendeo.extrasongui.layout.xml` (or rename or changing anything such that the file doesn't exist with its original name), and run Clone Hero again. Several defaults of the tweak are based on your screen resolution at the time, so if elements are a bit oddly positioned, try resetting your config.

#### Enable / Disable Keybind
This setting provides and easy way to make all the UI appear/disappear if you want to focus in-game. To set this keybind, click the button, and press a key to bind it. Click the checkboxes below if you want to also require pressing a combination of Ctrl, Shift, or Alt for this.

#### Configuration Keybind
The same thing as above, but shows/hides the config menu.

#### Draggable Labels
This adds a IMGUI window that you can drag around for every label on screen. This allows you to position text elements by dragging them rather than manaully changing their X/Y in the config menu. You probably want to disable this for gameplay though.

#### Hide all extra UI
This is the easy enable/disable setting. This is controlled by the enable/disable keybind above, but you can also just set it here.

#### Layout
The first setting is a radio box corresponding to a number of players (1, 2, 3, or 4). You can set a different layout for differing numbers of players, and this selection changes which layout you're currently editing. Below are all the labels you have configured for your layout. You can click the `Edit` button to edit the details of a that label. For organising these labels, below are `Shift Up` and `Shift Down`, which allow you to move this label up and down in this list (won't have any visual effect in game), `Delete` to delete this label, and `Insert new` to add a new label at this position in the list.

When editing a label, there's a few settings.

##### Name
This is just the name that appears on the main config window; useful for quickly identifying which label is which.

##### Format
This is what will be represented in the label in-game. Typing any characters allows it to appear in game for that label. If you want a value from the game, just make sure the format contains something in the form: `{name}` or `{name:format}` where `name` is a term from the below table, and `format` is any optional formatting options. You can include multiple formatting terms in your string (for example: `Notes: {hitnotes} / {seennotes}`).

The type of the variable determines what formatting options are available. See below the table for some examples and references.

If a variable has a player variant, then it has the option of whether it can count the whole band or just one player. By default, the term counts the band, but if you add a digit at the end of the name, it only counts that one player's statistic (for example, `hitnotes` applies to the band, `hitnotes1` only counts player 1's notes, `hitnotes2` for player 2, etc.).

| Name | Type | Player Variant? | Description |
| ---- | ---- | --------------- | ----------- |
| `songtime` | TimeSpan | No | The current song time |
| `songlength` | TimeSpan | No | The total song length |
| `songtimepercentage` | double | No | The percentage between the start and end of the songs (from 0 to 100) |
| `currentstar` | int | No | How many stars the player score is at |
| `nextstar` | int | No | One plus `currentstar`, caps at 7 |
| `currentscore` | int | No | The current score |
| `currentstarscore` | int | No | The difference between the current score and `previousstarscore` |
| `previousstarscore` | int | No | The amount of score to reach the last achieved star |
| `nextstarscore` | int | No | The amount of score to reach the next star (caps at the 7 star score) |
| `currentstarpercentage` | double | No | The percentage of the `currentstarscore` between the `previousstarscore` and `laststarscore` (from 0 to 100) |
| `sevenstarscore` | int | No | The amount of score to reach 7 stars |
| `sevenstarpercentage` | double | No | The percentage of the `currentstarscore` between 0 and `sevenstarscore` (from 0 to 100) |
| `hitnotes` | int | Yes | The number of notes hit by the band |
| `missednotes` | int | Yes | The number of notes missed by the band |
| `seennotes` | int | Yes | The sum of `hitnotes` and `missednotes` |
| `totalnotes` | int | Yes | The total number of notes on the chart |
| `hitnotespercentage` | double | Yes | The percentage of `hitnotes` against `totalnotes` (counts up from 0 to 100 over the course of a song) |
| `seennotespercentage` | double | Yes | The percentage of `hitnotes` against `seennotes` (starts at 100 and varies between 0 and 100 throughout the song) |
| `fcindicator` | string | Yes | `FC` if the band is on a perfect run, `100%` on an overstrum, and `-X` where `X` is the number of notes missed otherwise |
| starpowersgotten | int | Yes | The number of star powers hit by the band |
| `totalstarpowers` | int | Yes | The number of star powers on the chart |
| `starpowerpercentage` | double | Yes | The percentage of `starpowersgotten` against `totalstarpowers` (counts up from 0 to 100 over the course of a song) |
| `currentstarpower` | double | Yes | The percentage of star power currently held by the band (the player variant goes from 0 to 100 (where 50 is the activation point), the band variant goes from 0 to 400) |
| `currentcombo` | int | Yes | The current streak of notes consistently hit (the band variant resets every time an individual breaks their own streak) |
| `highestcombo` | int | Yes | The highest value of `currentcombo` achieved so far |

| Type | Format | Example |
| ---- | ------ | ------- |
| [`int`](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) | `D6` | `1050` => `001050` |
| | `N0` | `1050` => `1,050` |
| | `X4` | `1050` => `041A` |
| [`double`](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) | `00` | `4.5` => `04` |
| | `00.00` | `4.5` => `04.50` |
| | `F2` | `4.5` => `4.50` |
| [`DateTime`](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings) | `m\:ss\.fff` | 5 minutes 8 seconds => `5:08.000` |
| | `h\:mm\:ss` | 5 minutes 8 seconds => `0:05:08` |
| | `m\:\s\.f` | 5 minutes 8 seconds => `5:8.0` |
| `string` | --- | Strings don't have custom formats |

##### X
The X position of the label from 0 (left) to `Screen.width` (right).

##### Y
The Y position of the label from 0 (top) to `Screen.height` (bottom).

##### Size
The size of the text of the label.

##### Alignment
Determines where the X and Y of the label is anchored on. If your label changes its length, consider using a `Left` alignment so it grows to the right, or a `Right` alignment so it grows to the left.

##### Bold / Italic
Checkboxes if you want you text to stand out from the rest.

##### RGBA
Colour settings for the text.