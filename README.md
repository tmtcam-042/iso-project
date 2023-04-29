# WFC Hexmap project
My project to develop a 3d hexmap fantasy city procedural generator using the Wave Function Collapse algorithm 

## Setup
Unity version: *2021.3.19f1*

Platforms: Linux, WebGL, macOS

1. Clone repo to location of your choice.
2. Setup repo location as a Unity Project.
3. Hit play and watch the magic happen!

## Unity editor instructions
Modify **Size** parameter of Worldmap object to alter hexmap size

Modify **Time Step** parameter of EngineSphere object to alter generation rate (0.005 is good speed)

## Workflow
Functional version on main branch. Development version on testing branch. Merges to main need version number

## Resources
Wave function collapse: https://robertheaton.com/2018/12/17/wavefunction-collapse-algorithm/

Hexmap co-ordinate system: https://www.redblobgames.com/grids/hexagons/

Shannon Entropy Equation: $$H=\log\left(\sum weight\right)-(\frac{\sum (weight*\log(weight))}{ \sum weight} )$$
