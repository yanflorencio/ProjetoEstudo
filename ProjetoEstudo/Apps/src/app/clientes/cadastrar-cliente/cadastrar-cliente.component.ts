import { Component } from "@angular/core";

import { ClienteService } from "../cliente.service";

@Component({
    selector: 'app-cliente',
    templateUrl: 'cadastrar-cliente.component.html'
})
export class CadastrarClienteComponent{

    cliente : Object = new Object();

    constructor(private clienteService: ClienteService){ }
}