extends KinematicBody2D


# Member Variables
export var jump_height = 1.5
export var jump_duration = 1.0
var _jump_force: float
var _gravity: float

var _velocity: Vector2

onready var RayL := $GroundedRays/RayCastGroundL
onready var RayM := $GroundedRays/RayCastGroundM
onready var RayR := $GroundedRays/RayCastGroundR

# Called when the node enters the scene tree for the first time.
func _ready():
	_jump_force = -2*jump_height / jump_duration
	_gravity = 2*jump_height / pow(jump_duration, 2)
	
	_velocity = Vector2(0.0, 0.0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if (Input.is_action_just_pressed("jump")):
		_velocity.y = _jump_force
		print("Jump")

func _physics_process(delta):
	# Move Player Based on Velocity
	var collision = move_and_slide(_velocity)
	
	# TODO: 
	# Fix velocity resetting prematurely 
	# First frame after jump is still considered grounded
	
	print("Before: %f" % _velocity.y)
	print("Ground: %s" % is_grounded())
	
	# Handle Gravity and Falling
	if (!is_grounded()):
		_velocity.y += _gravity*delta
	else:
		_velocity.y = 0.0
		
	print("After: %f" % _velocity.y)


func is_grounded():
	var left = RayL.is_colliding()
	var middle = RayM.is_colliding()
	var right = RayR.is_colliding()
	
	return left || middle || right
