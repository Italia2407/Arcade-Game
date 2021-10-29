extends Area2D


# Member Variables
export var terrain_damage: int = 1
export var player_damage: int = 1

export var health = 1

var _velocity = Vector2(0.0, 50.0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	position += _velocity*delta


func _on_FallingEnemy_body_entered(body):
	# Collision with Terrain
	if body.is_in_group("Terrain"):
		body.take_damage(terrain_damage)


func take_damage(damage):
	health -= damage
	if (health <= 0):
		die()


func die():
	queue_free()
