# jarhead
Entry for GEMMA Financial Capability Game Design Competition  
Refer to **guidelines.pdf** for assignment guidelines and **slideshow.pdf** for the presentation given by the MITA chairman.  
For documentation we should use overleaf - the google docs of latex:  
https://www.overleaf.com/5997698684sjtftvvzrmgx (editing)  
https://www.overleaf.com/5997698684sjtftvvzrmgx (viewing)  
  
VISION: "Improving the personal financial capability of Maltese citzens during life-events and retirements to enable them to reach better informed financial decisions that fit their individual circumstances"

UI:
For now, the initial money is set to 1000 euros, and the sanity is set to its maximum: 100.

Once the game starts the money decreases quickly, but then this decrease slows down. Sanity decreases depending on how LOW money is: the lower it is the faster it goes down.
As a result, once the game starts, the money's decrease will get slower, and the sanity's increase should get faster.

These accelerating changes are in the script `Money.cs`. The script for the sanity however is in `Sanitybar.cs`, which has a flat decay rate for sanity.

The sanity bar should change emoticons and colors with time. This assumes a range as follows:

| Sanity | Mood |
| :----: | :--: |
| 80 - 100 | Great |
| 60 - 80 | Happy |
| 40 - 60 | Meh |
| 20 - 40 | Sad |
| 0 - 20 | Terrible |

The colors go: *Teal, Green, Yellow, Orange, Red*.

The UI is all in a game object called **Canvas**, which turns elements into UI elements. This is where the sanity bar and the money can be found.
