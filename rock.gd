extends RigidBody2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	angular_velocity=randf_range(100, 5000)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
