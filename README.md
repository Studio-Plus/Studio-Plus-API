# Studio-Plus-API
## Current Version: v4.0.1
This is an API for mods created by Studio Plus, open source because stuff like this should be open source.

There are 2 types of snippets in here:
- [MOD] is an actual mod that you can throw into your mods folder
- [SNIPPET] is either a file or group of files that would not work as a mod on their own without you doing something to make it work.<br/>
  Those are mostly now redundant however thanks to the documentation

### How do I use it?
1. Download the 'StudioPlusAPI' folder and put it into your mod root folder (You can put the files wherever, but I personally would do this and the further instructions will also assume you did this. If you put the files somewhere else, you have to adjust to it)
2. Add all the file paths into your mod.json file (There is an example of that in the repository)
3. Add 'using StudioPlusAPI;' to the beginning of each file, the same area where 'using UnityEngine;' is

Alternatively you can just use the template included in the repository and skip to step 3 for any files you add (files in the template have step 3 completed by default)

This API is modular, meaning that you are meant to be able to remove modules at will. However, sometimes modules will have dependencies on other modules. Here are all the dependencies:
- TexturePlus: Requires PlusAPI
- CreationPlus: Requires PlusAPI
- ArmorBehaviour: Requires PlusAPI and CreationPlus
- PowerPlus: Requires PlusAPI

As you can see, the dependencies are mostly simply PlusAPI so in general I'd recommend keeping it in at all times and removing other modules instead.

For everything else, check the documentation to get started!

In case of any issues or questions, please join the [Studio Plus Server](https://discord.gg/MxY3n6wfjw) and ask them there.