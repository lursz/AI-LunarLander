import gym
from gym import spaces
from gym.wrappers import TimeLimit
from stable_baselines3.common.monitor import Monitor
from stable_baselines3 import PPO
from stable_baselines3.ppo import MlpPolicy
from stable_baselines3 import DDPG
from stable_baselines3.common.vec_env import SubprocVecEnv
import numpy as np
import torch as th
import os

# import gym_server


class Server:
    def __init__(self):
        print("Starting server")
        self.serverIP = "127.0.0.1"
        self.serverPort = "8000"

        # Action Space : Jet activation : Main, Left, Right, None
        action_space = spaces.Discrete(4)
        # Observation Space : Jet position : X, Y, Velocity, Alpha
        observation_space = spaces.Box(
            low=np.array([-1, -1, -1, -1]),
            high=np.array([1, 1, 1, 1]),
            dtype=np.float32,
        )

        # Excutable
        project_path = os.getcwd()[:-20]  # project.godot folder
        # TODO: Change godotPath to your godot editor executable path
        godot_path = "flatpak run org.godotengine.Godot"  # godot editor executable
        scene_path = "./scenes/main/main.tscn"  # env Godot scene
        self.exe_cmd = f"cd {project_path} && {godot_path} {scene_path}"
        env = self.setUp()

        for _ in range(5):
            print(env.step(0))

    def setUp(self, window_rendering=False):
        env = gym.make(
            "server-v0",
            serverIP=self.serverIP,
            serverPort=self.serverPort,
            exeCmd=self.exe_cmd,
            action_space=self.action_space,
            observation_space=self.observation_space,
            window_render=window_rendering,
            renderPath=self.renderPath,
        )

        env = Monitor(TimeLimit(env, max_episode_steps=250))
        return env


class Training:
    def __init__(self, server: Server):
        self.server: Server = server
        # Create folder to store renders
        self.renderPath: str = f"{os.getcwd()}/render_frames/"
        if not os.path.exists(self.renderPath):
            os.makedirs(self.renderPath)

        self.model = DDPG(
            "MlpPolicy",
            server.env,
            verbose=0,
            tensorboard_log="./tensorboard_logs/",
            device="cpu",
            seed=0,
        )
        self.model.learn(total_timesteps=100000)


server = Server()
