# Documentación Práctica 1
___________________________________________________________________________

## Autores

##### Grupo 02
López Benítez, Ángel   -   angelo06@ucm.es
Rave Robayo, Jose Daniel   -   jrave@ucm.es
Prado Echegaray, Iván   -   ivprado@ucm.es
Mendoza Reyes, Juan Diego   -   juandiem@ucm.es


## Resumen

La práctica consiste en implementar un prototipo de una simulación de la leyenda de El Flautista de Hamelín.
Este prototipo tratará de que un jugador controle el movimiento del flautista por un escenario mientras que el perro y las ratas son controlados por agentes inteligentes previamente creados por código.
El perro siempre seguirá al jugador a donde quiera que él vaya. Además, las ratas del escenario seguirán al jugador siempre y cuando este toque la flauta, en caso contrario, dejarán de seguirle y seguirán un movimiento propio, aparte, cuando haya suficientes ratas cerca del perro provocará que huya de ellas.



## Descripción Punto de Partida

La escena de Unity consta con una jaula en la que se encuentran tres objetos, todos siendo esferas. El agente flautista (jugador), el agente que huye (perro), y el agente que persigue (ratas). 

Cada uno de los agentes contiene un script que les otorga el comportamiento descrito anteriormente. Estos heredan de una clase padre llama ComportamientoAgente la cual contiene el método que actualiza su posición en función de una dirección. Asimismo difiere entre tres posibilidades para determinar la dirección:
Combinar por Peso: asigna una dirección dada, multiplicada por un factor que modifica el módulo de dicha dirección (siendo esta un Vector3). 
Combinar por Prioridad: asigna una dirección dentro de un lista que se identifica por un factor de prioridad. Existen varias listas dentro de un mapa, cada una con distintas prioridades. Se selecciona una dirección en función de un umbral de prioridad.
Dirección Absoluta: la dirección es simplemente determinada por el input del jugador.
La selección de esta dirección se realiza en el componente Agente , el cual lo tienen todos los agentes. Este también se encarga de aplicar la dirección al objeto, manteniendo una rotación máxima (que no debe superar el rango de 0º < <= α <= 360º), una aceleración y velocidad máxima.



## Descripción de la solución

La solución constará de implementar tres nuevos componentes, el componente Wander, encargado de que las ratas se muevan de forma errática mientras no sigan al flautista, el componente Separation encargado de que las ratas se mantengan a una distancia coherente y el componente FollowNDodge, encargado de que los agentes no se choquen contras los obstáculos del escenario.

El pseudocódigo de dichos componentes:

### Ratas
```sh
class KinematicWander :
  character: Static
  maxSpeed: float
 
  // The maximum rotation speed we’d like, probably should be smaller
  // than the maximum possible, for a leisurely change in direction.
  maxRotation: float
 
  function getSteering() -> KinematicSteeringOutput:
   result = new KinematicSteeringOutput()
 
   // Get velocity from the vector form of the orientation.
   result.velocity = maxSpeed * character.orientation.asVector()
 
   // Change our orientation randomly.
   result.rotation = randomBinomial() * maxRotation
 
   return result;
```

### Perro  (Llegada)
```sh
class KinematicArrive:

	character: Static
	target: Static
	
	maxSpeed: float
    
	# The satisfaction radius.
	radius: float
    
 	# The time to target constant.
 	timeToTarget: float = 0.25
    
 	function getSteering() -> KinematicSteeringOutput:
 	result = new KinematicSteeringOutput()
    
 	# Get the direction to the target.
 	result.velocity = target.position - character.position
    
 	# Check if we’re within radius.
 	if result.velocity.length() < radius:
 	# Request no steering.
 	return null
    
 	# We need to move to our target, we’d like to
 	# get there in timeToTarget seconds.
 	result.velocity /= timeToTarget
    
 	# If this is too fast, clip it to the max speed.
 	if result.velocity.length() > maxSpeed:
 	result.velocity.normalize()
 	result.velocity *= maxSpeed
    
 	# Face in the direction we want to move.
 	character.orientation = newOrientation(
 	character.orientation,
 	result.velocity)
    
 	result.rotation = 0
 	return result;
```

### Huida
```sh
class KinematicSeek:
    character: Static
    target: Static

    maxSpeed: float

    function getSteering() -> KinematicSteeringOutput:
        result = new KinematicSteeringOutput()

        # Get the direction away from the target.
        steering.velocity = character.position - target.position

        # The velocity is along this direction, at full speed.
        result.velocity.normalize()
        result.velocity *= maxSpeed

        # Face in the direction we want to move.
        character.orientation = newOrientation(character.orientation, result.velocity)

        result.rotation = 0
        return result;
```

### CollisionAvoidance
```sh
class CollisionAvoidance:
 character: Kinematic
 maxAcceleration: float

# A list of potential targets.
 targets: Kinematic[]
# The collision radius of a character (assuming all characters
# have the same radius here).
 radius: float

 function getSteering() -> SteeringOutput:
	# 1. Find the target that’s closest to collision
	# Store the first collision time.
	shortestTime: float = infinity

	# Store the target that collides then, and other data that we
	# will need and can avoid recalculating.
	firstTarget: Kinematic = null
	firstMinSeparation: float
	firstDistance: float
	firstRelativePos: Vector
	firstRelativeVel: Vector

	# Loop through each target.
	for target in targets:
		# Calculate the time to collision.
		relativePos = target.position - character.position
		relativeVel = target.velocity - character.velocity
		relativeSpeed = relativeVel.length()
		timeToCollision = dotProduct(relativePos, relativeVel) /
		(relativeSpeed * relativeSpeed)

		# Check if it is going to be a collision at all.
		distance = relativePos.length()
		minSeparation = distance - relativeSpeed * timeToCollision
		if minSeparation > 2 * radius:
			continue

		# Check if it is the shortest.
		if timeToCollision > 0 and timeToCollision < shortestTime:
			# Store the time, target and other data.
			shortestTime = timeToCollision
			firstTarget = target
			firstMinSeparation = minSeparation
			firstDistance = distance
			firstRelativePos = relativePos
			firstRelativeVel = relativeVel

 # 2. Calculate the steering
 # If we have no target, then exit.
 if not firstTarget:
 	return null

 # If we’re going to hit exactly, or if we’re already
 # colliding, then do the steering based on current position.
 if firstMinSeparation <= 0 or firstDistance < 2 * radius:
 		relativePos = firstTarget.position - character.position

 # Otherwise calculate the future relative position.
 else:
 	relativePos = firstRelativePos + firstRelativeVel * shortestTime

 # Avoid the target.
 relativePos.normalize()

 result = new SteeringOutput()
 result.linear = relativePos * maxAcceleration
 result.anguar = 0
 
 return result
```

### Separate
```sh
class Separation:
 character: Kinematic
 maxAcceleration: float

 # A list of potential targets.
 targets: Kinematic[]

 # The threshold to take action.
 threshold: float

 # The constant coefficient of decay for the inverse square law.
 decayCoefficient: float

 function getSteering() -> SteeringOutput:
 result = new SteeringOutput()

 # Loop through each target.
 for target in targets:
	# Check if the target is close.
	direction = target.position - character.position
	distance = direction.length()

	if distance < threshold:
	# Calculate the strength of repulsion
	# (here using the inverse square law).
		strength = min(
			decayCoefficient / (distance * distance),
			maxAcceleration)

	# Add the acceleration.
	direction.normalize()
	result.linear += strength * direction

 return result
```

### Wall Avoidance
```sh
class ObstacleAvoidance extends Seek:
 detector: CollisionDetector

 # The minimum distance to a wall (i.e., how far to avoid
 # collision) should be greater than the radius of the character.
 avoidDistance: float

 # The distance to look ahead for a collision
 # (i.e., the length of the collision ray).
 lookahead: float

 # ... Other data is derived from the superclass ...

 function getSteering():
	# 1. Calculate the target to delegate to seek
	# Calculate the collision ray vector.
	ray = character.velocity
	ray.normalize()
	ray *= lookahead

 # Find the collision.
 collision = detector.getCollision(character.position, ray)

 # If have no collision, do nothing.
 if not collision:
 	return null

 # 2. Otherwise create a target and delegate to seek.
 target = collision.position + collision.normal * avoidDistance
 return Seek.getSteering()
```