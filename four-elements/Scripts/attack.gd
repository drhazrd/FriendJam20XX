extends Node2D

enum element { Fire, Water, Air, Earth}

@export var attackPoint: Marker2D
var currentElement = element.Fire
var mana = { element.Fire:100,  element.Water:100,  element.Air:100,  element.Earth:100}
var max_mana = 100
var manaAttackCost = 10

func _input(event):
	if event.is_action_pressed("Attack"):
		_attemptAttack()
	if event.is_action_pressed("SwapLeft"):
		_cycleElementUp()
	if event.is_action_pressed("SwapRight"):
		_cycleElementDown()

func _attemptAttack():
	#helle
	if mana[currentElement] >= manaAttackCost:
		mana[currentElement] -= manaAttackCost
		var projectile = preload("res://Scenes/projectile.tscn").instantiate()
		projectile.global_position = attackPoint.global_position
		var reverseDirection
		if reverseDirection: 
			projectile.direction = projectile.direction * -1
		else:
			projectile.direction = projectile.direction * 1
		get_tree().current_scene.add_child(projectile)
		print("Fire")

func _cycleElementUp():
	# Move up
	var nextElementID = (int(currentElement) + 1) % element.size()
	currentElement = element.values()[nextElementID]
	print("Current Element:", currentElement)

func _cycleElementDown():
	# Move Down
	var previousElementID = (int(currentElement) - 1 + element.size()) % element.size()
	currentElement = element.values()[previousElementID]
	print("Current Element:", currentElement)
