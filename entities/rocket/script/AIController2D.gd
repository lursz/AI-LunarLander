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
	print("kupa pierd")
	print(action)
	move.x = action['move'][0]
	move.y = action['move'][1]
