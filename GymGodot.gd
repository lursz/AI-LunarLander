extends Node

# Enable / disable this node
@export var enabled: bool = true

# Amount of frames simulated per step. 
# During each of these frames, the current action will be applied. 
# Once these frames are elapsed, the reward is computed and returned.
@export var stepLength: int = 2

# Reference to the Environment node which must implement the methods :
# get_observation(), get_reward(), reset(), is_done()
@export var environmentNode: NodePath

# Default url of the server (if not provided through cmdline arguments)
var serverIP : String = '127.0.0.1'
# Default port of the server (if not provided through cmdline arguments)
var serverPort : int = 8000

# Default path to store render frames (if not provided through cmdline arguments)
var renderPath : String = './render_frames/'
# Counter for the rendered frames
var renderFrameCounter : int = 0

# Print debug logs
@export var debugPrint: bool = false

@onready var currentAction : Array = []
@onready var environment : Node = get_node(environmentNode)
@onready var frameCounter : int = 0
@onready var webSocket = get_node('WebSocketClient')

func _parse_arguments() -> Dictionary :
	var arguments = {}
	for argument in OS.get_cmdline_args():
		# Parse valid command-line arguments into a dictionary
		if argument.find('=') > -1:
			var key_value = argument.split('=')
			arguments[key_value[0].lstrip('--')] = key_value[1]
	return arguments

func _ready() -> void :
	if not enabled :
		webSocket.free()
		return
	# This node will never be paused

	# WAŻNE
	# TU DODAŁEM VAR ALE NIE WIEM CZY TAK MA BYĆ
	var pause_mode = 2 
	# Initialy, the environment is paused
	get_tree().paused = true
	# Get the server IP/Port from argument
	var arguments = _parse_arguments()
	if 'serverIP' in arguments :
		serverIP = arguments['serverIP']
	if 'serverPort' in arguments :
		serverPort = int(arguments['serverPort'])
	# Get frame render parameters
	if 'renderPath' in arguments :
		renderPath = arguments['renderPath']
	# Connect to the ws server using those IP/port
	$WebSocketClient.connect_to_server(serverIP, serverPort)
	
func _physics_process(_delta : float) -> void :
	if not enabled :
		return
	# Simulate stepLength frames with the current action. 
	# Then pause the game and return the observation/reward/isDone to the server
	if frameCounter >= stepLength :
		get_tree().paused = true
		frameCounter = 0
		_returnData()
	else :
		if !get_tree().paused :
			frameCounter += 1
			environment.apply_action(currentAction)

# Called by WebSocketClient node when it recieve a step msg
func step(action : Array) -> void :
	# Set the action for this new step and run this step
	
	currentAction = action
	get_tree().paused = false
	
# Called by WebSocketClient node when it recieve a close msg
func close() -> void :
	webSocket.close()
	get_tree().quit()
	
# Return current observation/reward/isDone to the server
func _returnData() -> void :
	var obs : Array = environment.get_observation()
	var reward : float = environment.get_reward()
	var done : bool = environment.is_done()
	var answer : Dictionary = {'observation': obs, 'reward': reward, 'done': done}
	# $WebSocketClient.send_to_server(JSON.print(answer))
	webSocket.send_to_server(JSON.stringify(answer))
	
# Called by WebSocketClient when it recieve a reset msg
func reset() -> void :
	environment.reset()
	var obs : Array = environment.get_observation()
	var answer : Dictionary = {'init_observation': obs}
	webSocket.send_to_server(JSON.stringify(answer))
	renderFrameCounter = 0

# Called by WebSocketClient when it recieve a render msg. 
# Renders to .png in the renderPath folder
func render() -> void :
	RenderingServer.force_draw()
	var screenshot = get_viewport().get_texture().get_data()
	screenshot.flip_y()
	var error = screenshot.save_png(renderPath + str(renderFrameCounter) + '.png')
	renderFrameCounter += 1
	var answer : Dictionary = {'render_error': str(error)}
	webSocket.send_to_server(JSON.stringify(answer))
