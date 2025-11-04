extends CharacterBody2D

@export var rock: PackedScene
const SPEED = 300.0
const JUMP_VELOCITY = -400.0
var rockVelocity=0


func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("ui_accept") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("ui_left", "ui_right")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
	#move_and_slide()
	if(Input.is_action_pressed("Throw")):
		$Arrow.visible=true
		$Arrow.look_at($ColorRect.position)
		#$Arrow.rotation+=PI
		$ColorRect.position=Input.get_vector("Left", "Right", "Up", "Down")
		rockVelocity+=delta*6
		if(rockVelocity>10):
			rockVelocity=10
		#$Arrow.scale.y=rockVelocity/100
	elif(Input.is_action_just_released("Throw")):
		$Arrow.scale.y=.03
		$Arrow.visible=false
		var newRock=rock.instantiate()
		add_child(newRock)
		newRock.position=Vector2.ZERO
		newRock.linear_velocity=(position-$ColorRect.position)*rockVelocity*200
		rockVelocity=0
		
	shakePlayer()
	
func shakePlayer():
	$Icon.position=Vector2.ZERO
	$Icon.position.x+=randf_range(-rockVelocity, rockVelocity)
	$Icon.position.y+=randf_range(-rockVelocity, rockVelocity)
