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


However, for this milestone, I primarily worked on the opening Timeline cutscene. At first, writing down the steps did help me understand the general production sequence. For example, creating the Timeline, adding the opening text, adjusting the camera, and then transitioning back to the normal gameplay flow. But during actual development, I realized that because I lacked a foundational understanding of Timeline, simply writing down the steps didn't guarantee that they would flow together seamlessly, nor did it ensure they would ultimately fit my game. Sometimes, while writing, I thought a step would be useful, but after actually implementing it in Unity, I found the result was off or no longer aligned with the game's atmosphere or my current needs. So later on, I realized that rather than writing out every step in full before starting production, I'm better suited to a "build and refine as I go approach", which is completing one step, testing it, and then deciding how to adjust the next step based on the results.


If I were to redo the breakdown, I'd make it more flexible rather than writing it as a fixed, complete process from the start. This is because, during actual development, I've found that many ideas change based on the game's atmosphere, the results of feature implementation, or technical limitations. Some steps that were originally planned may no longer align with the gaming experience I'm aiming for. So if I were to do it again, I would structure the breakdown in a way that allows for modifications and reordering at any time, rather than planning everything out in one go. This approach better reflects my actual development process and makes it easier for me to adjust the design direction based on the game's current state.

### 3
<img width="1785" height="988" alt="屏幕截图 2026-05-14 224249" src="https://github.com/user-attachments/assets/823316c5-f590-494a-866b-597fcbda4f6f" />

In my game, I use the Visual Scripting Graph to call C# scripts, which bridges the gap between visual scripting and code. Initially, I tried using custom events to connect the Graph and C#, but this approach was unstable, so I switched to a more direct method. 


In this graph, when the state enters the dialogue state, 'On Enter State' first activates the dialogue UI, then retrieves the canvas object with the DialogueAdvancer script attached from the scene variables and calls the 'StartDialogue()' method in the DialogueAdvancer C# script to officially start the dialogue.


This architecture is very helpful for my game because Visual Scripting primarily handles high-level game state transitions, such as entering dialogue mode, while the C# script handles more specific dialogue behaviours, such as displaying dialogue text, advancing the conversation and processing reply options. This means that I don't have to write the entire dialogue system in Visual Scripting, but I can still use the Graph to control the entire game flow.


### 4
For Feature (3), I would like to have my ScriptableObject dialogue system evaluated. My dialogue nodes are built using DialogueNode ScriptableObjects, and a DialogueAdvancer C# script is responsible for reading and displaying dialogue lines and player reply options. This system is prominently featured in the game and can be found in the 'dialogueState' within the 'foxStateGraph' and in my dialogue script.

## Milestone 3 Devlog
### 1
<img width="1559" height="654" alt="屏幕截图 2026-05-28 225541" src="https://github.com/user-attachments/assets/a264cf9b-30d1-4831-8a0a-24e8d0c6c7db" />

I used a shader to create the glowing flame effect on the hint object. You can find this effect in the pile of gifts by the window. It appears among a pile of gifts in the corner of the room as a "glowing" item to signal to the player that there's a clue there. Additionally, when you reach the end of the story and unlock the notebook on the desk near the entrance, the effect will appear on the notebook as well. First, I took the object's original UVs and took the horizontal coordinates while linking the vertical coordinates to time. This caused the texture's sampling position to shift continuously over time. The flame texture itself isn’t animated, but because the UVs are changing, it appears as if the flame is flowing upward. Next, I multiply the dynamically sampled flame color by a "GlowStrength" to control its brightness. Then, I combine it with the object’s original texture color and output the result to "BaseColor." This shader primarily utilizes UV manipulation and texture sampling in the fragment stage, as discussed in class. In other words, it creates animation effects by altering the texture read position at the pixel stage rather than creating an additional animation clip.


### 2
Based on playtest feedback, I have made improvements to improve the player experience. First, I fixed an issue that prevented players from exiting the default dialogue properly, which could cause them to get stuck in conversations. Second, I revised the introductory information screen. I updated the visual style to better match the story's setting, and I added buttons that allow players to control the reading pace. Now, players can control the pace themselves rather than having the timeline automatically advance the pages as before. Some testers felt that the original autoplay speed was too fast, so this change allows players to absorb the background information at a more comfortable pace. I have also added two new clues to expand the story further, making the overall investigation and deduction process more complete.


### 3
Since the last milestone, I have added two new clues to enrich the story's background and help players make deductions more easily. To improve information accessibility, I have split each clue into two pages. The first page displays the clue’s title and a brief description. The second page shows the detailed content. This allows players to quickly grasp a clue's key points before deciding whether to read further. Within the gameplay loop, this helps players organize and analyze evidence more effectively. I also designed corresponding NPC dialogues for each clue. When players find and read a relevant clue, they unlock new dialogue content and receive more background information from the NPC. This helps them establish connections between clues. I also added a second set of default dialogue as a hint system. When players are unsure of their next steps, they can speak with the NPC again to receive hints, which are presented visually through Shader Graph effects.


## Final Devlog
### 1
My core gameplay loop is: Players explore the house, talk to the fox, and look for interactive items, such as the gift, the notebook, the assistant notebook, and the final book. The fox will only provide guidance regarding clues if the player's friendship level with the fox is high enough. After reading the clues, players unlock new dialogue options. The fox gradually explains the reason for the mother’s departure based on the information players have discovered. Game content includes opening dialogue, interaction with the gift, multiple clues, a friendship level UI, the fox following the player, and the final key, book, and truth-revealing dialogue. This vertical slice showcases the core experience of the full game. Rather than relying on combat or puzzles, the game focuses on exploring spaces, reading fragmented text, and piecing clues together through dialogue to form a complete story. Though currently just a small-scale scenario, it demonstrates how the full game will allow players to uncover the truth by interacting with the environment, objects, and NPCs.

### 2
<img width="1293" height="637" alt="屏幕截图 2026-06-05 005020" src="https://github.com/user-attachments/assets/aa19cfca-b43e-4aab-89a1-4a68c072b625" />

My rendering effect is an outline/highlight effect that is triggered by the game's logic. Specifically, players cannot see this effect at the beginning. They must first progress through the dialogue with the fox. Once the story reaches a certain point, the fox unlocks new clues. At this point, the relevant scripts call either UnlockOutline() or ShowBookPrompt(). This switches the corresponding object's outlineTarget from the standard Default layer to the Outline layer. For example, [NotebookInteract.cs](https://github.com/Laura27Apr/VerticalSlice/blob/main/Vertical%20Slice/Assets/Scripts/NotebookInteract.cs) and [AssistantNotebookInteract.cs](https://github.com/Laura27Apr/VerticalSlice/blob/main/Vertical%20Slice/Assets/Scripts/AssistantNotebookInteract.cs) both contain UnlockOutline(), which only switches the object to the Outline layer if the corresponding clue has not yet been read. Meanwhile, ShowBookPrompt() and UnlockBookInteract() in [BookInteract.cs](https://github.com/Laura27Apr/VerticalSlice/blob/main/Vertical%20Slice/Assets/Scripts/BookInteract.cs) activate the outline when a key book can be prompted or read. After approaching these objects, players can press F to open the reading UI. Once reading is complete, the script calls SetLayer(outlineTarget, LayerMask.NameToLayer(normalLayerName)), which switches the object back to the Default layer and makes the outline disappear. This way, the outline is not merely decorative but tied to narrative progression, indicating where interactive clue items are located and disappearing after the player has read them, avoiding repetitive hint prompts. Technically, this system combines Unity's layer system with the gameplay states, and rendering effects based on the outline layer.

### 3
When I start a large project, I usually begin by focusing on the game's core objective, and then break it down into several independent systems. For this Vertical Slice project, my initial goal was to create a visual novel combining exploration and storytelling. Players would gradually uncover the truth behind their mother's sudden leaving by searching for clues and talking to foxes. However, as I began development, I realized that the project was composed of many smaller systems. I broke the project down into several components: the Dialogue System, Reading System, NPC System, UI System, Scene Change System, and Clue System. I then broke down each system further. For example, the Dialogue System could be broken down into functions such as text, reply options, plot progression, and state management. This approach gave me a clearer understanding of what each system needed to accomplish and how they related to one another.


I plan to continue using the bubble diagram. The most useful feature of the Bubble Diagram is that it enables me to consider the relationships between systems before beginning a project instead of jumping straight into implementation. For example, in this Vertical Slice project, I quickly realized that the dialogue system does not exist in isolation. It influences the progression of the story, which determines which clues are unlocked. Once clues are unlocked, they trigger new visual cues and player interactions. This makes it easier to determine which features belong to the core experience and which can be implemented later. Therefore, rather than starting to write code right away, I believe it is important first to use the bubble diagram to understand which systems should be prioritized for development to implement the most basic game mechanics.


Meanwhile, I don't think I would use a Task Step Breakdown as my primary planning method. I'm more accustomed to learning as I build than planning every step in advance before starting development. Often, I'll start implementing a feature during the planning phase and adjust my approach continuously based on the problems I encounter. This "learn-as-you-go" approach feels more natural to me than strictly following a predefined set of steps.


Breaking down a large project into multiple systems gives me a better understanding of its scope. For example, I initially thought my game was just a simple exploratory visual novel, but as I broke it down, I realized that many seemingly simple features actually consisted of multiple interdependent parts. A feature for reading clues, for example, requires a reading UI and involves mouse state switching, player input control, story progress tracking, and unlocking subsequent dialogue. Therefore, I believe the decomposition process helps me estimate the workload more accurately and think more deeply about how different systems work together.


Looking back on this Vertical Slice project, I believe the biggest takeaway was realizing the importance of system planning, which made me think about how important it is to plan out a system before implementing it. At first, I focused on ensuring that the various systems were present in the game without considering their relationships or the workload required for each system. Consequently, I spent a significant amount of time in the later stages of development resolving conflicts between dialogue, the reading UI, visual scripting states, and scene transitions. If I were to plan this project again, I would complete the core gameplay loop and the whole story line earlier on. I would also confirm how the clues relate to these storylines. This would prevent the piecemeal approach of writing one line at a time, which ultimately required major revisions to the dialogue. However, I also found that breaking the project down into independent systems and completing them one by one was highly effective. It's a workflow I hope to retain and improve upon in my future projects.

## Open-source assets
- [Stylized House Interior](https://assetstore.unity.com/packages/3d/environments/stylized-house-interior-224331)
- [Toon Fox](https://assetstore.unity.com/packages/3d/characters/animals/toon-fox-183005)
- [Heart Icon](https://icons8.com/icon/87/heart)
- [Gifts Assets](https://assetstore.unity.com/packages/3d/props/pbr-christmas-gifts-237877)
- [Fire Model](https://sketchfab.com/3d-models/animated-stylized-fire-c872b1d5a14f456c93d2b275b6c2642e)
- [Notebook Assets](https://assetstore.unity.com/packages/3d/props/grimoire-style-book-3996)
