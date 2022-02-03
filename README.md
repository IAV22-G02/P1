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

Variable en el GameManager

DistanciaALaQueSeAsustaElPerro

if(pos.modulo <= distAsustado && outside)gameManager.ratas++;
else gameManager.ratas--;

if(asustado) huir();
else seguir();

RatasCerca >= NumeroRatasCerca

Collider en el perro, para detectar cuando entran

void huir(){
    float min;
    GameObject masCercana;

    for(int i=0; i < ratas.size();i++){
        if(ratas[i].distance <= thresolh) 
    }

}

void onEnter(){
    if(ratas.size() == 0) min = newRata();
}

Activar y desactivar los componentes Wander() y Seguir()