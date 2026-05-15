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


### 2
The task breakdown activity in Week 5 was somewhat helpful to me, but not as effective as I had expected. For the Week 5 activity, I worked on NavMesh, which I had done before. Since I already had some foundational knowledge, what I wrote down was pretty much the same as what I ended up doing. 


However, for this milestone, I primarily worked on the opening Timeline cutscene. At first, writing down the steps did help me understand the general production sequence¡ªfor example, creating the Timeline, adding the opening text, adjusting the camera, and then transitioning back to the normal gameplay flow. But during actual development, I realized that because I lacked a foundational understanding of Timeline, simply writing down the steps didn¡¯t guarantee that they would flow together seamlessly, nor did it ensure they would ultimately fit my game. Sometimes, while writing, I thought a step would be useful, but after actually implementing it in Unity, I found the result was off or no longer aligned with the game's atmosphere or my current needs. So later on, I realized that rather than writing out every step in full before starting production, I'm better suited to a "build and refine as I go approach", which is completing one step, testing it, and then deciding how to adjust the next step based on the results.


If I were to redo the breakdown, I'd make it more flexible rather than writing it as a fixed, complete process from the start. This is because, during actual development, I¡¯ve found that many ideas change based on the game's atmosphere, the results of feature implementation, or technical limitations. Some steps that were originally planned may no longer align with the gaming experience I'm aiming for. So if I were to do it again, I would structure the breakdown in a way that allows for modifications and reordering at any time, rather than planning everything out in one go. This approach better reflects my actual development process and makes it easier for me to adjust the design direction based on the game's current state.

### 3
<img width="1785" height="988" alt="屏幕截图 2026-05-14 224249" src="https://github.com/user-attachments/assets/823316c5-f590-494a-866b-597fcbda4f6f" />

In my game, I use the Visual Scripting Graph to call C# scripts, which bridges the gap between visual scripting and code. Initially, I tried using custom events to connect the Graph and C#, but this approach was unstable, so I switched to a more direct method. 


In this graph, when the state enters the dialogue state, 'On Enter State' first activates the dialogue UI, then retrieves the canvas object with the DialogueAdvancer script attached from the scene variables and calls the 'StartDialogue()' method in the DialogueAdvancer C# script to officially start the dialogue.


This architecture is very helpful for my game because Visual Scripting primarily handles high-level game state transitions, such as entering dialogue mode, while the C# script handles more specific dialogue behaviours, such as displaying dialogue text, advancing the conversation and processing reply options. This means that I don't have to write the entire dialogue system in Visual Scripting, but I can still use the Graph to control the entire game flow.


### 4
For Feature (3), I would like to have my ScriptableObject dialogue system evaluated. My dialogue nodes are built using DialogueNode ScriptableObjects, and a DialogueAdvancer C# script is responsible for reading and displaying dialogue lines and player reply options. This system is prominently featured in the game and can be found in the 'dialogueState' within the 'foxStateGraph' and in my dialogue script.

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
