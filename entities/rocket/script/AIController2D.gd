extends AIController2D
var move = Vector2.ZERO


func get_obs() -> Dictionary:
	return {"obs":[]}

func get_reward() -> float:	
	return reward
	
func get_action_space() -> Dictionary:
	return {
		"move" : {
			"size": 2,
			"action_type": "continuous"
		}
	}
	
func set_action(action) -> void:	
	# print("GdScript action:")
	move.x = action['move'][0]
	move.y = action['move'][1]
	# print(move)

func reset() -> void:
	# reward = 0.0
	# get this node
	var rocket = get_parent()
	rocket.propagate_call("_Reset")


func _on_rocket_crash_signal():
	reward -= 1.0
	reset()

func _on_rocket_success_signal():
	reward += 100.0
	reset()
