extends AIController2D
var move = Vector2.ZERO
var steps = 0

@onready var rocket = get_parent()
var best_distance = 0

func get_obs() -> Dictionary:
	var result := [rocket.position.x-rocket.goal_pos.x, rocket.position.y-rocket.goal_pos.y, rocket.velocity.x, rocket.velocity.y, rocket.rotation]

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
	# zero_reward()
	steps = 0
	var rocket = get_parent()
	rocket.propagate_call("Reset")
	best_distance = rocket.position.distance_to(rocket.goal_pos)


func _on_rocket_crash_signal():
	reward -= 100.0
	reset()

func _on_rocket_landing_signal():
	reward +=  500.0
	reset()

# func _on_rocket_staying_alive_signal(degrees, speed, distance):
# 	# print("Degrees: ", degrees, "Speed: ", speed, "Distance: ", distance)
# 	steps += 1

# 	if abs(degrees) > 30.0:
# 		reward -= 1
# 	if speed > 1.5:
# 		reward -= 1
# 	elif speed > 1.2:
# 		reward -= 0.7

# 	var dist_reward = 0.0
# 	if distance < best_distance:
# 		dist_reward += best_distance - distance
# 		best_distance = distance
# 		dist_reward /= 4.0
# 		dist_reward /= (steps / 100.0)
# 	# print("Distance reward: ", dist_reward)
# 	reward += dist_reward

# 	if distance < 100.0:
# 		reward += 5.0
# 		if speed < 1.2:
# 			reward += (1.2 - min(speed, 0.4)) * 10.0 /=  
# 		if abs(degrees) < 20.0:
# 			reward += (20.0 - min(abs(degrees), 6.0)) * 10.0

# 	if steps > 1000:
# 		reward -= 100.0
# 		reset()

func _on_rocket_staying_alive_signal(degrees, speed, distance):
	# print("Degrees: ", degrees, "Speed: ", speed, "Distance: ", distance)
	steps += 1

	if abs(degrees) > 50.0:
		reward -= 10
	elif abs(degrees) > 30.0:
		reward -= 2
	if speed > 1.5:
		reward -= 2
	elif speed > 1.2:
		reward -= 1.5

	var dist_reward = 0.0
	if distance < best_distance:
		dist_reward += best_distance - distance
		best_distance = distance
		# dist_reward /= 4.0
		# dist_reward /= steps / 100.0
	# print("Distance reward: ", dist_reward)
	reward += dist_reward

	if distance < 100.0:
		reward += 5.0
		if speed < 1.2:
			reward += (1.2 - min(speed, 0.4)) * 20.0 / distance
		if abs(degrees) < 20.0:
			reward += (20.0 - min(abs(degrees), 6.0)) * 20.0 / distance

	if steps > 1000:
		reward -= 100.0
		reset()