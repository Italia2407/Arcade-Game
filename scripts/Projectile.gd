extends Area2D


# Member Variables
export var terrain_damage: int = 1
export var enemy_damage: int = 1

export var speed = 1.0

export var max_lifetime = 10.0

var _direction
var _lifetime


func _init(direction):
	_direction = direction
	_lifetime = 0.0


func _on_Projectile_body_entered(body):
	if (body.is_in_group("Terrain")):
		body.take_damage(terrain_damage)
		explode()
	elif (body.is_in_group("Enemies")):
		body.take_damage(enemy_damage)
		explode()


func _physics_process(delta):
	position += _direction * speed * delta
	
	_lifetime += delta
	if (_lifetime >= max_lifetime):
		explode()


func explode():
	queue_free()
