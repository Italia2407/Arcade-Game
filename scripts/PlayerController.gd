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
#func _process(delta):
#	pass

func _physics_process(delta):
	print(is_grounded())


func is_grounded():
	var left = RayL.is_colliding()
	var middle = RayM.is_colliding()
	var right = RayR.is_colliding()
	
	return left || middle || right
