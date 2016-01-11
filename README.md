##Mechanomyogram Device for Gesture Detection##

**A low cost hardware assisted hand gesture detection app**

[Mechanmyogram](https://en.wikipedia.org/wiki/Mechanomyogram), or [MMG](https://en.wikipedia.org/wiki/Mechanomyogram) for short, is a biomedical signal that is emitted by the muscles.This signal is detected from the forearm muscles to know what hand gesture is made and fed into the computer. This was my Masters project at Mehran University.

**How it works**
MMG works on the principal of sound waves. The muscle fibers when move, make noise and this noise is picked up by the microphone. Then this microphone signal is collected by computer sound card. [DTW algorithm](https://en.wikipedia.org/wiki/Dynamic_time_warping) does the magic of detecting the gestures with already saved ones in the memory.

![MMG Device Screenshot](MainScreen.PNG?raw=true)

Basically you plug in the MMG Device (the hardware), switch it on. Open the application and a nice GUI will pop up. Click Start Listening. Now to detect gestures, all you have to do is to make a gesture and then click Save Sample Left/Middle/Right buttons. That's it. Now try making that gesture again. The app will be able to detect it. 

**Software libraries used**

 - [NAudio](https://github.com/naudio/NAudio)
 - [NDtw](https://github.com/doblak/ndtw)
 
**Hardware**
 
The hardware is an armband with circuit and microphones in it. Which is based on [Xth Sense](http://res.marcodonnarumma.com/category/project-xth-sense/hardware-design/), but with addition of two microphones. However the project code only uses Mono for now (Both mics will be used for future). 

> Schematic design

![Schematic](Circuit.png?raw=true)


> Circuit on Veroboard

![Circuit](Circuit_Real.PNG?raw=true)


> The whole armband with circuit and microphones look like this

![Armband](Armband.PNG?raw=true)


> And when it is worn on the forearm

![Armband worn](Worn.PNG?raw=true)
