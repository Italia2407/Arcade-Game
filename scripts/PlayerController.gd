extends KinematicBody2D


# Member Variables
export var jump_height = 1.0
export var jump_distance = 1.0

export var movement_speed = 1.0

var _jump_force: float
var _gravity: float

var _velocity: Vector2

onready var RayL := $GroundedRays/RayCastGroundL
onready var RayM := $GroundedRays/RayCastGroundM
onready var RayR := $GroundedRays/RayCastGroundR

# Called when the node enters the scene tree for the first time.
func _ready():
	_jump_force = -2*jump_height*movement_speed / jump_distance
	_gravity = 2*jump_height*pow(movement_speed, 2) / pow(jump_distance, 2)
	
	_velocity = Vector2(0.0, 0.0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if (Input.is_action_pressed("move_left") && !Input.is_action_pressed("move_right")):
		_velocity.x = -movement_speed
	elif (Input.is_action_pressed("move_right") && !Input.is_action_pressed("move_left")):
		_velocity.x = movement_speed
	else:
		_velocity.x = 0.0
	
	if (Input.is_action_just_pressed("jump")):
		_velocity.y = _jump_force

func _physics_process(delta):
	# Move Player Based on Velocity
	var collision = move_and_slide(_velocity)
	
	# Handle Gravity and Falling
	if (!is_grounded()):
		_velocity.y += _gravity*delta
	else:
		_velocity.y = 0.0


func is_grounded():
	RayL.force_raycast_update()
	RayM.force_raycast_update()
	RayR.force_raycast_update()
	
	var left = RayL.is_colliding()
	var middle = RayM.is_colliding()
	var right = RayR.is_colliding()
	
	return left || middle || right
