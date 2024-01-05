extends Node2D


# Becomes true if lander crashes
var isDone : bool = false






# Called when the node enters the scene tree for the first time.
func _ready():
	reset()

# receives action in array and executes them
func apply_action(action : Array) -> void:
	pass

# return current observable states
func get_observation() -> Array:
	return []

# return current reward as float
func get_reward() -> float:
	return 0.

# reset the environment
func reset() -> float:
	return 0.

func is_done() -> bool:
	return isDone
