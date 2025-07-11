extends CharacterBody2D
@export var projectPoint: Node2D
@export var speed : float = 200
var jumpVelocity : float = -400
var gravity : float = 1200

var faceRight = true

var checkCost = 0


func _physics_process(delta):
	var direction = Input.get_action_strength("Right") - Input.get_action_strength("Left")
	
	velocity.x = direction * speed
	
	if not is_on_floor():
		velocity.y += gravity * delta
	else:
		if Input.is_action_just_pressed("Action"):
			velocity.y = jumpVelocity
	move_and_slide()
	_handle_sprite_flip(direction)

func _handle_sprite_flip(direction):
	if direction > 0 and not faceRight:
		$Sprite2D.flip_h = false
		faceRight = true
		projectPoint.position.x = abs(projectPoint.position.x)  # Move to right side

	elif direction < 0 and faceRight:
		$Sprite2D.flip_h = true
		faceRight = false
		projectPoint.position.x = abs(projectPoint.position.x)  # Move to right side


func add_check_cost(amount: int):
	checkCost += amount
	print("Check Cost is now:", checkCost)

func get_check_cost():
	return checkCost
