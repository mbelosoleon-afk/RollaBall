Este proyecto, es un juego llamado RollaBall en el que el nivel,
es un laberinto del que tienes que escapar de un enemigo para ganar.

Para realizarlo lo primero es crear desde Unity una esfera, que será el jugador,
un plano que será el tablero del juego con los muros del laberinto, que 
serán cubos, y un cubo especial que persiga al jugador para eliminarlo.

<---SCRIPT MOVIMIENTO--->

Después de darle las características necesarias a la esfera del jugador,
como su material, proporciones posiciones etc... 
Debemos crear un script para su movimiento, en el que especificaremos las 
acciones que este tendrá.

Para ello necesitamos método como:

El Start, donde empezamos la cuenta de puntuación del jugador y la mostramos
por pantalla y obtenemos el rigidbody.

El OnMove, que se encarga de tomar los valores del InputSyste, es decir
las teclas del jugador, y las actualiza para así crear el movimiento
de la esfera.

SetCountText, muestra la puntuación del jugador y si llega a cierto
límite, salta el texto de ganador y desaparece el enemigo.

El OnFire es un método para permitir el salto del jugador, pero como
este era inpreciso, ya que podías saltar infinitamente, tuve que crear
dos nuevos métodos para controlar esto, OnCollisionStay y OnCollisionExit,
que se encargan de ver si el jugador está en contacto con el suelo o con
las paredes, en caso de no estarlo, se desactiva la capacidad de saltar.

El FixedUpdate se actualiza cada frame y se encarga de darle el movimiento
al jugador y aplica la fuerza sobre el.

OnTriggerEnter, se encarga de contar la puntuación del jugador, cada vez
que recoge un "pickUp".

Por último el OnCollisionEnter, se encarga de comprobar si
el enemigo, atrapó al jugador y en ese caso, se pierde la partida.

<---SCRIPT CAMERACONTROLLER--->

Es importante que la cámara siga al jugador mientras este se mueve, por lo que,
después de situarla en la posición requerida, el script se encarga de 
obtener la distancia entre la cámara y el jugador y mantenerla constantemente
para no perderlo de vista.

<---SCRIPT ROTACIÓN--->

Este script se encarga de un efecto visual para que los "pickUp", la 
moneda del juego estén en constante movimiento, rotando sobre si mismos.
Estos pickUp, son un prefab, que se ocupa de darle las proporciones y
características a cada uno.

<---SCRIPT ENEMIGOMOVEMENT--->

Por último, es necesario crear otro script, para que el enemigo persiga
al jugador en todo momento.
Para esto, en Unity debemos añadirle al enemigo el NavMeshAgent, y a cada muro
debemos añadirle el NavMeshObstacle, para que estos colisionen entre sí.
Aún me faltó crearlo en los muros, lo hice en un cubo distinto para probar su fincionamiento.