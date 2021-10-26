extends StaticBody2D

# Member Variables
export var health = 3

# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
# func _process(delta):
	# pass


func take_damage(damage):
	health -= damage
	
	if health <= 0:
		destroy()


func destroy():
	queue_free()
