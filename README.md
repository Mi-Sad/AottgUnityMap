# Aottg PBMod UnityMap

Set of tools (editor scripts) and few basic prefab you can use to make your own unity levels.

To create unity maps you are required to use <b>Unity 4</b> installed in your machine.<br/>
You can download Unity from their <a href = "https://unity3d.com/get-unity/download/archive"> download archive </a>

1) Download the <a href = "https://github.com/Mi-Sad/AottgUnityMap/raw/main/Aottg_PBMod_UnityMap_Tools.unitypackage">.unitypackage</a> file
2) Open <b>Unity 4</b> and import the package. (https://i.imgur.com/xiOruBt.png)

After the importing is done and successful, let the editor scripts compile (will do automatically upon loading them).<br/>
Once done compiling 2 new voice will be shown in the "Window" tab (https://i.imgur.com/KREwVzv.png)<br/>

3) Click "Aottg Proj Formatter", this will open the tool to set up the proj layers and tags ready for aottg (click both buttons).

Your project is now ready to go, remember to set everything that is ground and need to be collided by the object in game with the layer "Ground".<br/>
In order to collide any object requires a collider, the prefabs included in the package are already setted up properly to work with the game.<br/>
You can freely add new objects, animations, or any element you want to the game and instance in the scene.

Make sure you also setted up at least few spawns for both humans and titans! (prefabs for spawns are already in the package)<br/>
Having any camera in the scene you'll export named maincamera will break the game! So make sure there'sno MainCamera already spawned!

When you're ready to export the scene, just save it in a Unity Scene, after that, right click on the Scene in the Assets folder and click on "Export Streaming Asset" (https://i.imgur.com/WevAURU.png)

To play the map you have exported you can either play it locally (using the local path on your computer) or play it multiplayer with your friends 
by uploading it in any "cloud" or online storage service that allow direct download (like dropbox).<br/>
To do so you need the link (or local path but in this case you have to add "file:///" before) and the name of the scene, you have to insert those inside the Unity Map
Tab, once done click on USE (https://imgur.com/ScBWgU4.png), now you are ready to host a unity map, so just click on play, (multi or single) and select as map UnityMap (https://imgur.com/rAl4e55.png).
The unity map loader will show the link you're attempting to download and will ask you to trust it or not (https://imgur.com/A1bbbeB.png), after trusting the link the download will begin and the 
loading of the map will start shortly after (the time of the loading strictly depends on 2 factors, your internet speed and the size of the map itself, big maps will
require more time to laod).<br/><br/>
<img src="https://imgur.com/1WI9fzH.png">
