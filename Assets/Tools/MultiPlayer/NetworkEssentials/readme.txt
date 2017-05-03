Getting started with the example scene
--------------------------------------

Load the example scene under /Scenes and select the "Network Manager" in the hierarchy.

You will find a slightly customized version of the default Unity Network Manager here. Notice the added Properties: "VR Player Prefab" and the bool "Should Be Server".

Make sure you have a valid IP address entered into the "Network Address" field which can be found under "Network Info" in the Network Manager.

Check the "Should Be Server" option and press play. Now put on your HTC Vive HMD. You should see the example scene before you.

For a client to connect, you will need to build a standalone version. Make sure to configure one running instance
as client and one as server. Either via "Should Be Server" toggle mentioned above or via the external configuration file "settings.cfg" (more details below).

Remember: You cannot have two VR supported applications running at once on one machine. So make sure that you only have one instance of a VR app running per computer.

Hint: You can disable "Virtual Reality Supported" in "Player settings.." -> "Other Settings" -> "Rendering" and then start the project with FPS controls and without the need for VR HMD.
This is useful for testing, debugging and asynchronous gameplay mechanics.

Start the server instance first and make sure you have TCP/IP connection between your client and server instance.
The client will try to connect to the server automatically upon start.


Principles of VR Networking
---------------------------

I will not explain the underlying philosophy of UNet and the Unity Networking framework since there is plenty information about that available already. Instead I will focus on my integration of the SteamVR plugin concepts into the UNet paradigm.

First of all player spawning is handled manually since we want to be able to spawn a seperate prefab depending upon wether or not the current play has VR support enabled or not. In order to make this possible each client sends an extra message upon connecting, indicating the VR support status. The Network Manager then instantiates the right prefab when a client connects.

Next we need to have the hands of the VR players represented in our game world a synched across the network. We are using UNets NetworkTransforms here. We additionally enable player authority for their hands.

In order to make it possible to pickup items we need to be able to change authority at runtime. The VRPlayerController takes care of this.

The last step is to make sure the items are usable. We use our IUsable interface for the GunItem and implement it.


How to use the included prfabs
------------------------------

1. Create a new Unity 3D Project
2. Import "SteamVR Network Essentials"
3. Navigate to the /Prefabs folder of "SteamVR Network Essentials" and drag the "Network Manager" prefab into your scene
4. You also need to add at least one "PlayerSpawnPoint" prefab. This is where the players will start once the connect to the server.
5. Optional: Add a "Gun" prefab to try object interaction and shooting.
6. Optional: Add "Enemy" prefabs or a "EnemySpawner" prefab for some target practice.
7. Configure the "Network Manager":
  - Make sure the "Network Address" and "Should Be Server" settings are correct
  - Open the "Spawn Info" configuration panel and adjust the settings here
     - "Player Prefab" needs to be set to the NonVRPlayer prefab
     - "Auto Create Player" needs to be disabled
     - Enter every GameObject that will get spawned during network play into the list of "Registered Spawnable Prefabs", e.g. Player, Hand, Bullet, Enemy, Gun, etc.
     - Make sure you have set the "VR Player Prefab" at the bottom to the right prefab for the VR players.


How to use the included scripts
-------------------------------

Bullet.cs:
This is a simple lifetime script which destroy the projectile after some time or on collision.

CustomNetworkManager.cs:
Add this script to an empty GameObject to create a network manager. Allows configuration of basic network settings.

EnemySpawner.cs:
Randomly spawns enemies around the origin.

GunItem.cs:
Implements IUsable. Simple demonstration of an usable item. Shoots a projectile upon use.

Health.cs:
Simple health script. Includes "Destroy on Death" toggle.

InteractableObject.cs:
Expects a rigidbody and colliders on the gameobject. Add this to items you want the users to interact with, e.g. touch, grab, drop, throw, use.

NetworkVRHands.cs:
This manages basic item manipulation and corresponding network communication. Add it to your representation of the VR player's hands.

VRPlayerController.cs:
Creates the VR Camera Rig and handles instaniation of the networked VR hands.

PlayerController.cs:
A simple first-person controls based control-scheme for the non-VR players.

SpawnMessage.cs:
Contains the extra network message indicating VR support status on login.

interfaces ITouchable and IUsable:
Implement these for custom object behaviour.


How to expand on the example
----------------------------

Adding enemy behaviour:
Just add some scripts to the enemies.

Adding a new item:
Create a GameObject with a rigidbody component. Add the "InteractableObject" script.
If the item should be usable, tick the "IsUsable" checkbox on the "InteractableObject" component. For usable items you also need another script to the GameObject which implements the interface "IUsable".

Adding Spawnpoints:
Just add more spawnpoints to the scene. The scripts will automatically detect them.


Configuration via settings.cfg
------------------------------

There is a simple configuration mechanism available for the resulting executable project: Add a file called "settings.cfg" to the data folder of your Unity build. The first line has to read "Server" for the host. The code expects to find the IP address of the server on the second line of the settings file.

Example configuration file contents for server in settings.cfg:
Server
0.0.0.0

Example configuration file contents for client in settings.cfg:
Client
192.168.0.101


List of Prefabs
---------------
 * VRPlayer - The VR Player Prefab
 * Hand - A visual representation of the Hands of a VR player
 * NonVRPlayer - Prefab for non VR players
 * Gun - A simple interactable object with a GunItem script as IUsable
 * Bullet - Simple projectile for the Gun
 * Enemy - A simple enemy prefab. Can be spawned and shot at. Will vanish if health drops below zero.
 * EnemySpawner - Randomly spawns enemies
 * Network Manager - Ready for Drag and Drop
 * PlayerSpawnPoint - Network start locations for the players 