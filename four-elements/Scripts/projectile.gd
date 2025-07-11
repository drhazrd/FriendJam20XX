extends Node2D

var speed = 40
var direction = 1
var elementType = null

@export var lifetime := 2.0 # seconds before self-destruction

func _ready():
	# Schedule auto-destruction after lifetime
	await get_tree().create_timer(lifetime).timeout
	queue_free()

func _physics_process(delta):
	position.x += speed * delta * direction

func set_element(elem):
	elementType = elem

func _on_body_entered(body):
	# Destroy when hitting something
	if body: 
		queue_free()
