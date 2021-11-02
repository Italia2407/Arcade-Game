extends KinematicBody2D


# Member Variables
export var jump_height = 1.0
export var jump_distance = 1.0

var _jump_force: float
var _gravity: float

export var movement_speed = 1.0
export var accel_time = 1.0
export var ground_decel_time = 1.0
export var air_decel_time = 1.0

var _velocity: Vector2

export var health = 4

var _can_move: bool

onready var RayL := $GroundedRays/RayCastGroundL
onready var RayM := $GroundedRays/RayCastGroundM
onready var RayR := $GroundedRays/RayCastGroundR

# Called when the node enters the scene tree for the first time.
func _ready():
	_jump_force = -2*jump_height*movement_speed / jump_distance
	_gravity = 2*jump_height*pow(movement_speed, 2) / pow(jump_distance, 2)
	
	_velocity = Vector2(0.0, 0.0)
	
	_can_move = true


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if (Input.is_action_pressed("move_left") && !Input.is_action_pressed("move_right") && _can_move):
		_velocity.x -= (movement_speed / accel_time) * delta
		_velocity.x = clamp(_velocity.x, -movement_speed, movement_speed)
	elif (Input.is_action_pressed("move_right") && !Input.is_action_pressed("move_left") && _can_move):
		_velocity.x += (movement_speed / accel_time) * delta
		_velocity.x = clamp(_velocity.x, -movement_speed, movement_speed)
	else:
		var decel_time = (ground_decel_time if is_grounded() else air_decel_time)
		var movement_direction: int = sign(_velocity.x)
		_velocity.x += ((-movement_direction * movement_speed) / decel_time) * delta
		
		if (movement_direction == 1 && _velocity.x < 0.0):
			_velocity.x = 0.0
		elif (movement_direction == -1 && _velocity.x > 0.0):
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


func take_damage(damage):
	health -= damage
	
	if (health <= 0):
		health = 0
		die()


func die():
	$CollisionBox.disabled = true
	_can_move = false
	
	queue_free()
