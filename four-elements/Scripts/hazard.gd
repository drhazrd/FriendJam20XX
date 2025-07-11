extends Node

var damage = 1

func GiveDamage(player):
	player.health.TakeDamage(damage)
