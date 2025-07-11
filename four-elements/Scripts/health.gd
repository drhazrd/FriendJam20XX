extends Node
var currentHealth
var maxHealth
var currentArmor
var maxArmor

func takeDamage(damaage):
	currentHealth -= damaage
	if currentHealth < 0:
		die()

func die():
	queue_free()
