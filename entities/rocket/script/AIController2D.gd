extends AIController2D
var move = Vector2.ZERO
var best_distance = 100000.0

@onready var rocket = get_parent()

func get_obs() -> Dictionary:
	var result := [rocket.position.x, rocket.position.y, rocket.velocity.x, rocket.velocity.y, rocket.rotation, rocket.distance]

	# print("OBS: ", result)
	return {"obs": result}

func get_reward() -> float:	
	# print("Reward: ", reward)
	return reward
	
func get_action_space() -> Dictionary:
	return {
		"move" : {
			"size": 2,
			"action_type": "continuous"
		}
	}
	
func set_action(action) -> void:	
	move.x = action['move'][0]
	move.y = action['move'][1]


func reset() -> void:
	# reward = 0.0
	var rocket = get_parent()
	rocket.propagate_call("Reset")
	best_distance = 100000.0


func _on_rocket_crash_signal():
	reward -= 10.0
	reset()

func _on_rocket_landing_signal():
	reward += 15.0
	reset()

func _on_rocket_staying_alive_signal(degrees, speed, distance):
	# print("Degrees: ", degrees, "Speed: ", speed, "Distance: ", distance)
	if abs(degrees) > 30.0:
		reward -= 0.5
	if speed > 1.5:
		reward -= 1.2
	elif speed > 1.0:
		reward -= 0.2


	var dist_reward = 0.0
	if distance < best_distance:
		dist_reward += best_distance - distance
		best_distance = distance
		dist_reward /= 10.0
	# print("Distance reward: ", dist_reward)
	reward += dist_reward

		
