# GDIM33 Vertical Slice
## Milestone 1 Devlog
### 1
I used Visual Scripting to implement the basic logic that allows the player to control their movement direction by moving the mouse. First, the graph uses the On Input System Event (Look) function to get the mouse movement Vector2. Then, it splits into X and Y values. The X value controls looking left or right, and the Y value controls looking up or down. Next, I multiply these two values by mouseSensitivity and Time.DeltaTime to make movement smoother and adjust sensitivity more easily. Then the process is divided into two parts. For looking left and right, the entire character, including the camera under "PlayerRoot", rotates along the Y-axis. For looking up and down, only the camera rotates along the X-axis. Moreover, for Y-axis movement, I multiplied it by -1 to align with our everyday perception since Unity's mouse Y input direction is opposite to our intuitive understanding. Before performing the rotation, I get the DialogueAdvancer component from the Canvas and use the isInDialogue state, which is passed from C#, to check if the player is in a dialogue. If the player is in a dialogue, I prevent camera rotation so that the view doesn't shake while the player reads the dialogue or makes selections with the mouse. 

### 2
<img width="1681" height="1027" alt="Breakdown with State Machines" src="https://github.com/user-attachments/assets/b32d6cca-b1d9-40a1-b555-0a4d30108c31" />

In the breakdown for this update, I added an NPC state machine to better organize the interaction and dialogue flow between the player and NPCs in the game. The state machine assigns three states to the NPC: Calm, Alert, and Dialogue. The NPC starts in the Alert state. When the distance between the NPC and the player is less than three, the NPC enters the Calm state. Clicking on the NPC at this point triggers the Dialogue state. After the dialogue ends, the NPC returns to the Calm state. I also assigned different animations to each state to make the NPC's behavior more complete.

In this system, the Dialogue state is responsible for calling the StartDialogue method in C# to initiate the conversation. It is also bound to the dialogue UI, ensuring that the UI only appears when the NPC is in the dialogue state. This prevents the UI from interfering with the player's exploration outside of dialogue. Additionally, the dialogue UI that appears during the dialogue state works with the friendship level UI. Based on the friendship level value, it determines if the NPC will help the player during future exploration.

## Milestone 2 Devlog
### 1
1. Create a basic opening cutscene using Timeline.


	a. Open the Timeline window in Unity, then create a new empty GameObject and name it OpeningTimeline. Create a Timeline asset for it. Open the Inspector for OpeningTimeline and verify that PlayableDirector is present;
	
	
	b. In the OpeningTimeline, create an OpeningCanvas and an OpeningText to display the opening text. Check the Game view to make sure the text appears on the screen;
	
	
	c. Adjust the display duration of the Activation Track so that OpeningText appears at 0 seconds and disappears a few seconds later. Set the Post-playback State of the Activation Track to Inactive so that the text remains hidden after the cutscene ends.Run a test of the game to confirm that the text does not reappear after the Timeline has finished playing.


2. Create a cutscene that transitions from an opening shot to the standard game perspective. 
	
	
	a. Create a new camera, name it OpeningSceneCamera, and place it where you want it. Check the Camera Preview or Game view to make sure the camera view shows the area of the story;
	
	
	b. In the Camera settings of OpeningSceneCamera, open Rendering and set its Priority higher than that of the Main Camera. This ensures that when OpeningSceneCamera is active, it will appear on top of the Main Camera. Run the game to test and confirm that when OpeningSceneCamera is active, the Game View displays the opening scene;

	
	c. Set the Post-playback State of the OpeningSceneCamera to Inactive to ensure that it remains off after the cutscene ends. Run the game to test and confirm that the view switches to the Main Camera's gameplay perspective after the cutscene ends;
	
 
	d. Under OpeningCanvas, create a semi-transparent background image to serve as the text background. Also adjust the color and transparency of the text background. Run the game to test it and ensure that the text is clear and legible while still allowing the opening scene behind it to be visible.




## Milestone 3 Devlog
Milestone 3 Devlog goes here.


## Milestone 4 Devlog
Milestone 4 Devlog goes here.


## Final Devlog
Final Devlog goes here.


## Open-source assets
- [Stylized House Interior](https://assetstore.unity.com/packages/3d/environments/stylized-house-interior-224331)
- [Toon Fox](https://assetstore.unity.com/packages/3d/characters/animals/toon-fox-183005)
- [Heart Icon](https://icons8.com/icon/87/heart)
