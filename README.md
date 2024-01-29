# AI-LunarLander
This repo contains a simple Lunar Lander game with AI agents. The aim of the project was to create an arcade experience resembling the original and then connect it with machine learning algorithms. However, we had placed a certain constraint upon ourselves - to only use open source tools. Thus we have decided to use [Godot](https://godotengine.org/) as a game engine and [Godot RL Agents](https://github.com/edbeeching/godot_rl_agents) as a reinforcement learning framework. The core of the game is written in C#, whilst the RL Agents part is written in GDScript.

## Run
Install:  
- Godot Engine 4.2 or higher  
- Python 3.11 or higher and create `venv` from `requirements.txt`

In order to update used machine learning libraries, you may also:
- download [this repo](https://github.com/edbeeching/godot_rl_agents_plugin) and copy `addons` and `script_templates` into root folder of your godot project. Then enable the plugin in `plugins` section of Godot Editor.
- install missing C# libraries
```bash
dotnet add package Microsoft.ML
dotnet add package Microsoft.ML.OnnxRuntime
```

## Reward function
A solid reward function is the key to success in reinforcement learning. We  learnt this the hard way, but eventually we managed to create a decent reward function. The function is based on the following factors:
- successful / unsuccessful landing
- landing pad contact
- distance from the landing pad
- velocity
- angle

## Screenshots
#### Outturn of RL Agents
![mars7208](https://github.com/lursz/AI-LunarLander/assets/93160829/0c3d7557-191e-4201-8b92-300897674b0d)

#### Learning process
![multi85](https://github.com/lursz/AI-LunarLander/assets/93160829/d8c1aa97-16a8-4293-9608-2be5af9921ac)

#### Main Menu
![main_menu5](https://github.com/lursz/AI-LunarLander/assets/93160829/bf72bff7-cb85-4efa-a865-4ac591dc1007)

#### Maps creator
![mars136](https://github.com/lursz/AI-LunarLander/assets/93160829/e5fb5ef5-e509-44c5-95ee-404d1cf71d30)
