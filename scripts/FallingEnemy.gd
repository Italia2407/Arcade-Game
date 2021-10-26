extends Area2D


# Member Variables
export var damage = 1
export var health = 5

var velocity = Vector2(0.0, 50.0)


# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	position += velocity*delta

