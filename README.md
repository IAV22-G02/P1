# P1
Repository for the Artificial Intelligence signature of the UCM. Practice 1

Ucm

Requisitos:
Lisa de tareas
Algoritmo

Video: las cosas que funcionan
Jueves 10: documentacion
Jueves 17 : todo funcionando con el video

Dog: Follows flautist constantly except when there are a lot of rats in which case dog flees

Rats: Wander

When u play flute: rats follow flautist in an ordered way

Componente ComportamientoRata

Componente Wander

Componente ComportamientoRata

en ComportamientoRata


void update(){
    tocando la flauta = space.down;

    if(tocando la flauta){
        wander.active = false;
        seguir.active = true;
    }
    else if(!tocandolaflauta){
        wander.active = true;
        seguir.active = false;
    }

    //Podemos hacer esto para ahorrar codigo pero es probablemente terrible

    wander.active = !tocando la flauta;
    seguir.active = tocando la flauta;
}

Add campo de vision al perro

Componente detectRats al campo de vision

en detectRats

void ontriggerEnter(GameObject rat){
    if(rat.getComponent<comporamientoRata>() != nullptr){
        numRatas++:

        if(numRatas >= ratasParaAsustarse){
            seguir.active = false;
            huir.active = true;
        }
    }
}

Componente Wander para las ratas