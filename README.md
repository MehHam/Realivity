# Realivity VR/ AR project

 * Synopsis  : Ride a lighting and observe the world in a relativistic light doppler shift with UV/IR variations. 

 
 * Pedagogical principle : The major aspect is a heuristic ludo-pedagogical représentation of light shiftings as respect to light speed. This project transposes the MIT Openrelativity http://gamelab.mit.edu/research/openrelativity/ in a "simple" way. Ride the lighting by changing velocity or slowing time througth a pendulum frequency.
 
 The longitudinal doppler shift depends on the observation angle ( i.e. when the observer moves his head) and the normalized velocity. The velocity can be controlled using a remote (using a **Movuino** or **Smartphone** UDP streamer (see https://github.com/MehHam/Moves-Remote) ). A set of data is given by  "Acc\t" + Input.acceleration.ToString() +"\t"+ "Gyro\t" + gyro.gravity.ToString()". If one controls the pendulum frequency, time will be controlled and consequently the light shift.   
  
  
 * VR Options  : 
- Resolution : By clicking on the Resolution Option (blue) on can change the accuracy of vision.
- Point Of View : By clicking on the Position Option (pink ) on can change the observation position in the Orion Nebulae.



- Static Velocity :  By Clicking the Static option (Green), on can observe the light shifting manually.
- Remote Component : By clicking on the Remote Component Option (yellow) for switching to remote axis that controls the light shifting.
-Infos : Infos : The White Option Offers a couple of informations about the mode, UDP port, etc...

# Technical issues
In order to get a lighter webcamtexture behaviour, it would be better to work on shaders.

# VR Building subtility 
As see in many cases, unity webcamtexture() crashes in VR mode. On has to enable VR/ Cardboard + Split Stereo Display 


