extends AIController2D
var move = Vector2.ZERO
var steps = 0

@onready var rocket = get_parent()
var best_distance = 0

func get_obs() -> Dictionary:
	var normalized_distance_x = (rocket.position.x-rocket.goal_pos.x)/600.0
	var normalized_distance_y = (rocket.position.y-rocket.goal_pos.y)/-600.0
	var normalized_rotation = rocket.rotation / 6.0 * 5.0
	var result: Array = [normalized_distance_x, normalized_distance_y, rocket.velocity.x, rocket.velocity.y, normalized_rotation]
	# print(result)
	# if typeof(rocket.ray_results) == TYPE_ARRAY:
		# result += rocket.ray_results
	# else:
		# result += [0, 0, 0, 0, 0, 0, 0, 0]
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
	steps = 0
	var rocket = get_parent()
	rocket.propagate_call("Reset")
	await get_tree().process_frame
	best_distance = rocket.position.distance_to(rocket.goal_pos)


func _on_rocket_crash_signal():
	reward -= 200.0
	reset()

func _on_rocket_landing_signal():
	reward +=  1000.0
	reset()

func _on_rocket_landing_pad_crash_signal():
	var rocket = get_parent()
	var speed = rocket.velocity.length()
	reward -= (100.0 + speed * 50.0) 
	reset()

func _on_rocket_staying_alive_signal(degrees, speed, distance):
	steps += 1
	if steps > 2500:
		reward -= 100.0
		reset()

	if abs(degrees) > 90.0:
		reward -= 100
		reset()

	# elif abs(degrees) > 50.0:
	# 	reward -= 5
	# elif abs(degrees) > 30.0:
	# 	reward -= 2

	if speed > 1.5:
		reward -= 10

	var dist_reward = 0.0
	if distance < best_distance:
		dist_reward += best_distance - max(distance, 105)
		best_distance = max(distance, 105)
	# print("Distance ", distance, " Distance reward: ", dist_reward*8, " Best distance: ", best_distance)
	elif distance+100 > best_distance:
		dist_reward -= 2.5
	if (dist_reward * 8.0) < 20.0:
		reward += dist_reward * 8.0
	
	var distance_x = rocket.position.x-rocket.goal_pos.x
	var distance_y = rocket.position.y-rocket.goal_pos.y
	
	var speed_reward = 0
	var rotation_reward = 0
	if abs(distance_x) < 120.0 and abs(distance_y) < 200:
		if speed < 1.2:
			speed_reward = (1.5 - max(speed-0.1, 0)) * 50*(2.2-speed)**3
		if abs(degrees) < 30.0:
			rotation_reward = (30.0 - max(abs(degrees)-5.0, 6.0)) * 1.5
	# print("Speed: ", speed, "Speed reward: ", speed_reward)
	# print("Rotation: ", abs(degrees),  "Rotation reward: ", rotation_reward)

	reward += speed_reward + rotation_reward

	# print("Reward: ", reward)




	# var distance_x = rocket.position.x-rocket.goal_pos.x
	# var distance_y = rocket.position.y-rocket.goal_pos.y
	# # print("Degrees: ", degrees, "Speed: ", speed, "Distance x: ", distance_x, "Distance y: ", distance_y, "Distance: ", distance)
	# steps += 1

	# if abs(degrees) > 50.0:
	# 	reward -= 10
	# elif abs(degrees) > 30.0:
	# 	reward -= 4
	# # if speed > 1.5:
	# # 	reward -= 2
	# # elif speed > 1.2:
	# # 	reward -= 1.5
	# if speed > 2.0:
	# 	reward -= 4

	# var dist_reward = 0.0
	# if distance < best_distance:
	# 	dist_reward += best_distance - max(distance, 105)
	# 	best_distance = max(distance, 105)
	# reward += dist_reward * 8.0

	# if abs(distance_x) < 100.0 and abs(distance_y) < 200:
	# 	reward += 20 - (distance-120)/10
	# 	if speed < 1.2:
	# 		reward += (1.2 - max(speed-0.2, 0.4)) * 20.0
	# 	if abs(degrees) < 20.0:
	# 		reward += (20.0 - max(abs(degrees)-5.0, 6.0)) * 10.0


	# if steps > 2500:
	# 	reward -= 100.0
	# 	reset()


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
